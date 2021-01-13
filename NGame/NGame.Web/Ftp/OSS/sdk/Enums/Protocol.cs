/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using NClient.OSS.Util;

namespace NClient.OSS
{
    /// <summary>
    /// supported protocol definition. HTTP is the default one.
    /// </summary>
    public enum Protocol
    {
        /// <summary>
        /// HTTP
        /// </summary>
        [StringValue("http")]
        Http = 0,

        /// <summary>
        /// HTTPs
        /// </summary>
        [StringValue("https")]
        Https
    }
}
