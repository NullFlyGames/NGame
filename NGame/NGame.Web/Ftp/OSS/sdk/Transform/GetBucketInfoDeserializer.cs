/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System;
using System.IO;
using NClient.OSS.Common.Communication;
using NClient.OSS.Util;
using NClient.OSS.Model;

namespace NClient.OSS.Transform
{
    internal class GetBucketInfoDeserializer : ResponseDeserializer<BucketInfo, BucketInfo>
    {
        public GetBucketInfoDeserializer(IDeserializer<Stream, BucketInfo> contentDeserializer)
            : base(contentDeserializer)
        { }

        public override BucketInfo Deserialize(ServiceResponse xmlStream)
        {
            return ContentDeserializer.Deserialize(xmlStream.Content);
        }
    }
}
