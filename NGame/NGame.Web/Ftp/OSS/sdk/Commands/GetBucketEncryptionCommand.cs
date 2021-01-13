/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System;
using System.Collections.Generic;
using NClient.OSS.Common.Communication;
using NClient.OSS.Util;
using NClient.OSS.Transform;

namespace NClient.OSS.Commands
{
    internal class GetBucketEncryptionCommand : OssCommand<BucketEncryptionResult>
    {
        private readonly string _bucketName;

        protected override string Bucket
        {
            get { return _bucketName; }
        }

        private GetBucketEncryptionCommand(IServiceClient client, Uri endpoint, ExecutionContext context,
                                    string bucketName, IDeserializer<ServiceResponse, BucketEncryptionResult> deserializer)
            : base(client, endpoint, context, deserializer)
        {
            OssUtils.CheckBucketName(bucketName);
            _bucketName = bucketName;
        }

        public static GetBucketEncryptionCommand Create(IServiceClient client, Uri endpoint,
                                                     ExecutionContext context,
                                                    string bucketName)
        {
            return new GetBucketEncryptionCommand(client, endpoint, context, bucketName,
                                              DeserializerFactory.GetFactory().CreateGetBucketEncryptionResultDeserializer());
        }

        protected override IDictionary<string, string> Parameters
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { RequestParameters.SUBRESOURCE_ENCRYPTION, null }
                };
            }
        }
    }
}
