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
    internal class GetBucketRequestPaymentCommand : OssCommand<GetBucketRequestPaymentResult>
    {
        private readonly string _bucketName;

        protected override string Bucket
        {
            get { return _bucketName; }
        }

        private GetBucketRequestPaymentCommand(IServiceClient client, Uri endpoint, ExecutionContext context,
                                      string bucketName, IDeserializer<ServiceResponse, GetBucketRequestPaymentResult> deserializer)
           : base(client, endpoint, context, deserializer)
        {
            OssUtils.CheckBucketName(bucketName);
            _bucketName = bucketName;
        }

        public static GetBucketRequestPaymentCommand Create(IServiceClient client, Uri endpoint,
                                                   ExecutionContext context,
                                                   string bucketName)
        {
            return new GetBucketRequestPaymentCommand(client, endpoint, context, bucketName,
                                           DeserializerFactory.GetFactory().CreateGetBucketRequestPaymentResultDeserializer());
        }

        protected override IDictionary<string, string> Parameters
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { RequestParameters.SUBRESOURCE_REQUESTPAYER, null }
                };
            }
        }
    }
}
