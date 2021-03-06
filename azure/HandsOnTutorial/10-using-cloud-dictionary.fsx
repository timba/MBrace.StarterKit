﻿#load "credentials.fsx"

open System
open System.IO
open MBrace.Core
open MBrace.Store
open MBrace.Azure
open MBrace.Azure.Client
open MBrace.Flow

(**
 This example distributed key/value storage using CloudDictionary
 
 Before running, edit credentials.fsx to enter your connection strings.
**)

// First connect to the cluster
let cluster = Runtime.GetHandle(config)

// create a cloud dictionary
let dict =
    cloud {
        let! dict = CloudDictionary.New<int> ()
        return dict
    } |> cluster.Run

// add an entry to the dictionary
dict.Add("key0", 42) |> cluster.Run
dict.ContainsKey "key0" |> cluster.Run
dict.TryFind "key0" |> cluster.Run
dict.TryFind "key-not-there" |> cluster.Run

// contested, distributed updates
let key = "contestedKey"
let contestJob = 
    [|1 .. 100|]
    |> CloudFlow.OfArray
    |> CloudFlow.iterLocal(fun i -> dict.AddOrUpdate(key, function None -> i | Some v -> i + v) |> Local.Ignore)
    |> cluster.CreateProcess

contestJob.ShowInfo()

// verify result is correct
dict.TryFind key |> cluster.Run

