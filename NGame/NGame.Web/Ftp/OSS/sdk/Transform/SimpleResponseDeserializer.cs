/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System.IO;
using NClient.OSS.Common.Communication;

namespace NClient.OSS.Transform
{
    internal class SimpleResponseDeserializer<T> : ResponseDeserializer<T, T>
    {
        public SimpleResponseDeserializer(IDeserializer<Stream, T> contentDeserializer)
            : base(contentDeserializer)
        { }

        public override T Deserialize(ServiceResponse xmlStream)
        {
            using (xmlStream.Content)
            {
                return ContentDeserializer.Deserialize(xmlStream.Content);
            }
        }
    }
}