/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using NClient.OSS.Model;

namespace NClient.OSS.Transform
{
    internal interface IDeserializer<in TInput, out TOutput>
    {
        TOutput Deserialize(TInput xmlStream);
    }
}
