/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NClient.OSS
{
    /// <summary>
    /// Bucket state.
    /// </summary>
    [XmlRoot("BucketStat")]
    public class BucketStat
    {
        [XmlElement("Storage")]
        public ulong Storage { get; set; }

        [XmlElement("ObjectCount")]
        public ulong ObjectCount { get; set; }

        [XmlElement("MultipartUploadCount")]
        public ulong MultipartUploadCount { get; set; }
    }
}
