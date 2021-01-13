/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System.IO;
using NClient.OSS.Common.Communication;
using NClient.OSS.Util;

namespace NClient.OSS.Transform
{
    internal class DeleteObjectResultDeserializer : ResponseDeserializer<DeleteObjectResult, Stream>
    {
        public DeleteObjectResultDeserializer(IDeserializer<Stream, Stream> contentDeserializer)
                 : base(contentDeserializer)
        {
        }
        
        public override DeleteObjectResult Deserialize(ServiceResponse xmlStream)
        {
            var result = new DeleteObjectResult()
            {
                DeleteMarker = false
            };

            if (xmlStream.Headers.ContainsKey("x-oss-delete-marker"))
            {
                result.DeleteMarker = bool.Parse(xmlStream.Headers["x-oss-delete-marker"]);
            }
            if (xmlStream.Headers.ContainsKey(HttpHeaders.VersionId))
            {
                result.VersionId = xmlStream.Headers[HttpHeaders.VersionId];
            }

            DeserializeGeneric(xmlStream, result);

            return result;
        }
    }
}
