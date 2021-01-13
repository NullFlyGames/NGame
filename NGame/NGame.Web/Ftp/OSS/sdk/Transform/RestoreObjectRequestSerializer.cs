/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System.IO;
using NClient.OSS.Model;

namespace NClient.OSS.Transform
{
    internal class RestoreObjectRequestSerializer : RequestSerializer<RestoreObjectRequest, RestoreRequestModel>
    {
        public RestoreObjectRequestSerializer(ISerializer<RestoreRequestModel, Stream> contentSerializer)
            : base(contentSerializer)
        {
        }

        public override Stream Serialize(RestoreObjectRequest request)
        {
            var model = new RestoreRequestModel();
            model.Days = request.Days;
            model.JobParameter = new RestoreRequestModel.JobParameters();
            model.JobParameter.Tier = request.Tier;
            return ContentSerializer.Serialize(model);
        }
    }
}

