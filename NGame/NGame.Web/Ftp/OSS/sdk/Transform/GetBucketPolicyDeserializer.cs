/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System.IO;
using NClient.OSS.Common.Communication;

namespace NClient.OSS.Transform
{
    internal class GetBucketPolicyDeserializer : ResponseDeserializer<GetBucketPolicyResult, GetBucketPolicyResult>
    {
        public GetBucketPolicyDeserializer(IDeserializer<Stream, GetBucketPolicyResult> contentDeserializer)
            : base(contentDeserializer)
        { }

        public override GetBucketPolicyResult Deserialize(ServiceResponse xmlStream)
        {
            var result = new GetBucketPolicyResult();

            StreamReader reader = new StreamReader(xmlStream.Content);
            result.Policy = reader.ReadToEnd();

            DeserializeGeneric(xmlStream, result);
            return result;
        }
    }
}
