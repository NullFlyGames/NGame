/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System;
using System.Globalization;

using NClient.OSS.Common;
using NClient.OSS.Common.Communication;
using NClient.OSS.Util;
namespace NClient.OSS.Transform
{
    internal class GetSymlinkResultDeserializer : ResponseDeserializer<OssSymlink, OssSymlink>
    {
        public GetSymlinkResultDeserializer()
            : base(null)
        { }

        public override OssSymlink Deserialize(ServiceResponse xmlStream)
        {
            OssSymlink symlink = new OssSymlink();

            GetObjectMetadataResponseDeserializer metaDeserializer = new GetObjectMetadataResponseDeserializer();
            symlink.ObjectMetadata = metaDeserializer.Deserialize(xmlStream);

            if (!xmlStream.Headers.ContainsKey(OssHeaders.SymlinkTarget))
            {
                throw new OssException("The required header is not found:" + OssHeaders.SymlinkTarget);
            }

            symlink.Target = xmlStream.Headers[OssHeaders.SymlinkTarget];

            return symlink;
        }
    }
}
