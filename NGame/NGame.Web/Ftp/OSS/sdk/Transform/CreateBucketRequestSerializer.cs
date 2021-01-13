/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System.IO;
using NClient.OSS.Common.Communication;
using NClient.OSS.Model;
using NClient.OSS.Util;

namespace NClient.OSS.Transform
{
    internal class CreateBucketRequestSerializer : RequestSerializer<CreateBucketRequest, CreateBucketRequestModel>
    {
        public CreateBucketRequestSerializer(ISerializer<CreateBucketRequestModel, Stream> contentSerializer)
            : base(contentSerializer)
        {
        }

        public override Stream Serialize(CreateBucketRequest request)
        {
            var model = new CreateBucketRequestModel
            {
                StorageClass = request.StorageClass,
                DataRedundancyType = request.DataRedundancyType
            };
            return ContentSerializer.Serialize(model);
        }
    }
}
