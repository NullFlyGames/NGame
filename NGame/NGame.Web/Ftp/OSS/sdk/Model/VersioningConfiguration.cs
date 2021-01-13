/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System.Xml.Serialization;

namespace NClient.OSS.Model
{
    [XmlRoot("VersioningConfiguration")]
    public class VersioningConfiguration
    {
        [XmlElement("Status")]
        public VersioningStatus Status { get; set; }
    }
}
