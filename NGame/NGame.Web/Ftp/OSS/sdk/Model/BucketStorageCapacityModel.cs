/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System.Xml.Serialization;

namespace NClient.OSS.Model
{
    [XmlRoot("BucketUserQos")]
    public class BucketStorageCapacityModel
    {
        [XmlElement("StorageCapacity")]
        public long StorageCapacity { get; set; }
    }
}
