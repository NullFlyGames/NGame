/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using NClient.OSS.Model;

namespace NClient.OSS
{
    /// <summary>
    /// The request class of the operatoin to upload an object
    /// </summary>
    public class PutObjectResult : StreamResult
    {
        /// <summary>
        /// Gets or sets the Etag.
        /// </summary>
        public string ETag { get; internal set; }

        /// <summary>
        /// Gets or sets the version id.
        /// </summary>
        public string VersionId { get; internal set; }

        internal PutObjectResult()
        { }
    }
}
