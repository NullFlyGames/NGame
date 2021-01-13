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
    internal class DeleteObjectVersionsResultDeserializer : ResponseDeserializer<DeleteObjectVersionsResult, DeleteObjectVersionsResultModel>
    {
        public DeleteObjectVersionsResultDeserializer(IDeserializer<Stream, DeleteObjectVersionsResultModel> contentDeserializer)
                 : base(contentDeserializer)
        { }

        public override DeleteObjectVersionsResult Deserialize(ServiceResponse xmlStream)
        {
            var result = new DeleteObjectVersionsResult();

            if (int.Parse(xmlStream.Headers[HttpHeaders.ContentLength]) != 0)
            {
                var model = ContentDeserializer.Deserialize(xmlStream.Content);
                string encodeType = model.EncodingType == null ?
                                    string.Empty : model.EncodingType.ToLowerInvariant();

                result.EncodingType = model.EncodingType;
                if (model.DeletedObjects != null)
                {
                    foreach (var o in model.DeletedObjects)
                    {
                        var summary = new DeletedObjectSummary
                        {
                            Key = Decode(o.Key, encodeType),
                            VersionId = o.VersionId,
                            DeleteMarker = o.DeleteMarker,
                            DeleteMarkerVersionId = o.DeleteMarkerVersionId

                        };
                        result.AddDeletedObjectSummary(summary);
                    }
                }
            }

            DeserializeGeneric(xmlStream, result);

            return result;
        }
    }
}
