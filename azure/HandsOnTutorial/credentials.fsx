﻿#I "../../packages/MBrace.Azure.Standalone/tools" 
#I "../../packages/Streams/lib/net45" 
#r "../../packages/Streams/lib/net45/Streams.Core.dll"
#I "../../packages/MBrace.Flow/lib/net45" 
#r "../../packages/MBrace.Flow/lib/net45/MBrace.Flow.dll"
#load "../../packages/MBrace.Azure.Standalone/MBrace.Azure.fsx"

namespace global

[<AutoOpen>]
module ConnectionStrings = 

    open MBrace.Core
    open MBrace.Azure
    open MBrace.Azure.Client
    open MBrace.Azure.Runtime

    // Both of the connection strings can be found under "Cloud Service" --> "Configure" --> scroll down to "MBraceWorkerRole"
    //
    // The storage connection string is of the form  
    //    DefaultEndpointsProtocol=https;AccountName=myAccount;AccountKey=myKey 
    //
    // The serice bus connection string is of the form
    //    Endpoint=sb://%s.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=%s

    let myStorageConnectionString = "your storage connection string here"
    let myServiceBusConnectionString = "your service bus connection string here"

    // Alternatively you can specify the connection strings by calling the functions below
    //
    // storageName: the one you specified when you created cluster.
    // storageAccessKey: found under "Manage Access Keys" for that storage account in the Azure portal.
    // serviceBusName: the one you specified when you created cluster.
    // serviceBusKey: found under "Configure" for the service bus in the Azure portal
    
    // let createStorageConnectionString(storageName, storageAccessKey) = sprintf "DefaultEndpointsProtocol=https;AccountName=%s;AccountKey=%s" storageName storageAccessKey
    // let createServiceBusConnectionString(serviceBusName, serviceBusKey) = sprintf "Endpoint=sb://%s.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=%s" serviceBusName serviceBusKey

    let config =
        { Configuration.Default with
            StorageConnectionString = myStorageConnectionString
            ServiceBusConnectionString = myServiceBusConnectionString }