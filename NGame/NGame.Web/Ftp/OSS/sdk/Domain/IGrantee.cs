/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

namespace NClient.OSS
{
    /// <summary>
    /// The interface for the grantee entity
    /// </summary>
    public interface IGrantee
    {
        /// <summary>
        /// Gets or sets the grantee entity's identifier.
        /// </summary>
        string Identifier { get; set; }
    }

}
