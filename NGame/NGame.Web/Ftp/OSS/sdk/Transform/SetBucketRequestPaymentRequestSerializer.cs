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
    internal class SetBucketRequestPaymentRequestSerializer : RequestSerializer<SetBucketRequestPaymentRequest, RequestPaymentConfiguration>
    {
        public SetBucketRequestPaymentRequestSerializer(ISerializer<RequestPaymentConfiguration, Stream> contentSerializer)
            : base(contentSerializer)
        {
        }

        public override Stream Serialize(SetBucketRequestPaymentRequest request)
        {
            var model = new RequestPaymentConfiguration();
            model.Payer = request.Payer;
            return ContentSerializer.Serialize(model);
        }
    }
}

