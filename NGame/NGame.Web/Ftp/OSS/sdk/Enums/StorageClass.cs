/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */
using System;
namespace NClient.OSS
{
    /// <summary>
    /// Storage class of OSS Bucket
    /// </summary>
    public enum StorageClass
    {
        Standard, // Standard bucket
        IA,       // Infrequent Access bucket
        Archive,   // Archive bucket
        ColdArchive     // Cold Archive bucket
    }
}
