/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System.IO;
using NClient.OSS.Common.Communication;

namespace NClient.OSS.Transform
{
    internal class GetVodPlaylistResultDeserializer : ResponseDeserializer<GetVodPlaylistResult, GetVodPlaylistResult>
    {
        public GetVodPlaylistResultDeserializer(IDeserializer<Stream, GetVodPlaylistResult> contentDeserializer)
            : base(contentDeserializer)
        { }

        public override GetVodPlaylistResult Deserialize(ServiceResponse xmlStream)
        {
            var result = new GetVodPlaylistResult();

            StreamReader reader = new StreamReader(xmlStream.Content);
            result.Playlist = reader.ReadToEnd();

            DeserializeGeneric(xmlStream, result);
            return result;
        }
    }
}
