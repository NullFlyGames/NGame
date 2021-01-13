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
using System.IO;

namespace NClient.OSS.Commands
{
    internal class SetBucketRequestPaymentCommand : OssCommand
    {
        private readonly string _bucketName;
        private readonly SetBucketRequestPaymentRequest _request;

        protected override HttpMethod Method
        {
            get { return HttpMethod.Put; }
        }

        protected override string Bucket
        {
            get { return _bucketName; }
        }

        private SetBucketRequestPaymentCommand(IServiceClient client, Uri endpoint, ExecutionContext context,
                                    string bucketName, SetBucketRequestPaymentRequest request)
            : base(client, endpoint, context)
        {
            OssUtils.CheckBucketName(bucketName);

            _bucketName = bucketName;
            _request = request;
        }

        public static SetBucketRequestPaymentCommand Create(IServiceClient client, Uri endpoint,
                                                 ExecutionContext context,
                                                 string bucketName, SetBucketRequestPaymentRequest request)
        {
            return new SetBucketRequestPaymentCommand(client, endpoint, context, bucketName, request);
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

        protected override Stream Content
        {
            get
            {
                return SerializerFactory.GetFactory().CreateSetBucketRequestPaymentRequestSerializer()
                    .Serialize(_request);
            }
        }
    }
}

