/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using NClient.OSS.Common.Communication;
using NClient.OSS.Model;
using System.IO;
using System.Xml;

namespace NClient.OSS.Transform
{
    internal class GetBucketLocationResultDeserializer : ResponseDeserializer<BucketLocationResult, BucketLocationResult>
    {
        public GetBucketLocationResultDeserializer(IDeserializer<Stream, BucketLocationResult> contentDeserializer)
            : base(contentDeserializer)
        { }

        public override BucketLocationResult Deserialize(ServiceResponse xmlStream)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlStream.Content);

            var result = new BucketLocationResult {
                Location = xmlDoc.SelectSingleNode("/LocationConstraint").InnerText
            };

            DeserializeGeneric(xmlStream, result);
            
            return result;
        }
    }
}
