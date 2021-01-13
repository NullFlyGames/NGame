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
    internal class SetBucketEncryptionRequestSerializer : RequestSerializer<SetBucketEncryptionRequest, ServerSideEncryptionRule>
    {
        public SetBucketEncryptionRequestSerializer(ISerializer<ServerSideEncryptionRule, Stream> contentSerializer)
            : base(contentSerializer)
        {
        }

        public override Stream Serialize(SetBucketEncryptionRequest request)
        {
            var model = new ServerSideEncryptionRule()
            {
                ApplyServerSideEncryptionByDefault = new ServerSideEncryptionRule.ApplyServerSideEncryptionByDefaultModel()
            };

            model.ApplyServerSideEncryptionByDefault.SSEAlgorithm = request.SSEAlgorithm;
            model.ApplyServerSideEncryptionByDefault.KMSMasterKeyID = request.KMSMasterKeyID;
            return ContentSerializer.Serialize(model);
        }
    }
}

