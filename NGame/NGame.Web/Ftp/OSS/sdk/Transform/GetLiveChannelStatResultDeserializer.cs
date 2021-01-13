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
    internal class GetLiveChannelStatResultDeserializer : ResponseDeserializer<GetLiveChannelStatResult, LiveChannelStat>
    {
        public GetLiveChannelStatResultDeserializer(IDeserializer<Stream, LiveChannelStat> contentDeserializer)
            : base(contentDeserializer)
        { }

        public override GetLiveChannelStatResult Deserialize(ServiceResponse xmlStream)
        {
            var model = ContentDeserializer.Deserialize(xmlStream.Content);

            GetLiveChannelStatResult result = new GetLiveChannelStatResult()
            {
                Status = model.Status,
                ConnectedTime = model.ConnectedTime,
                RemoteAddr = model.RemoteAddr
            };

            if (model.Video != null)
            {
                result.Width = model.Video.Width;
                result.Height = model.Video.Height;
                result.FrameRate = model.Video.FrameRate;
                result.VideoBandwidth = model.Video.Bandwidth;
                result.VideoCodec = model.Video.Codec;
            }

            if (model.Audio != null)
            {
                result.SampleRate = model.Audio.SampleRate;
                result.AudioBandwidth = model.Audio.Bandwidth;
                result.AudioCodec = model.Audio.Codec;
            }

            DeserializeGeneric(xmlStream, result);

            return result;
        }
    }
}
