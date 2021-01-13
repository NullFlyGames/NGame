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
    internal class GetLiveChannelInfoResultDeserializer : ResponseDeserializer<GetLiveChannelInfoResult, LiveChannelConfiguration>
    {
        public GetLiveChannelInfoResultDeserializer(IDeserializer<Stream, LiveChannelConfiguration> contentDeserializer)
            : base(contentDeserializer)
        { }

        public override GetLiveChannelInfoResult Deserialize(ServiceResponse xmlStream)
        {
            var model = ContentDeserializer.Deserialize(xmlStream.Content);

            GetLiveChannelInfoResult result = new GetLiveChannelInfoResult()
            {
                Description = model.Description,
                Status = model.Status,
            };

            if (model.Target != null)
            {
                result.Type = model.Target.Type;
                result.FragDuration = model.Target.FragDuration;
                result.FragCount = model.Target.FragCount;
                result.PlaylistName = model.Target.PlaylistName;
            }

            DeserializeGeneric(xmlStream, result);

            return result;
        }
    }
}
