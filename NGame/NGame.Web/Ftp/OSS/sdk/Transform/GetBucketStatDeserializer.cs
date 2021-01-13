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
    internal class GetBucketStatDeserializer : ResponseDeserializer<BucketStat, BucketStat>
    {
        public GetBucketStatDeserializer(IDeserializer<Stream, BucketStat> contentDeserializer)
            : base(contentDeserializer)
        { }

        public override BucketStat Deserialize(ServiceResponse xmlStream)
        {
            return ContentDeserializer.Deserialize(xmlStream.Content);
        }
    }
}
