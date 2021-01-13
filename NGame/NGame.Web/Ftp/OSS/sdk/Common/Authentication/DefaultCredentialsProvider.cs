/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System;
using NClient.OSS.Common.Authentication;
using NClient.OSS.Util;

namespace NClient.OSS.Common.Authentication
{
    /// <summary>
    /// Default ICredentialProvider implementation
    /// </summary>
    public class DefaultCredentialsProvider : ICredentialsProvider
    {
        private volatile ICredentials _creds;

        /// <summary>
        /// Creates a instance of <see cref="DefaultCredentialsProvider"/>
        /// </summary>
        /// <param name="creds"><see cref="ICredentials"/>ICredentials instance</param>
        public DefaultCredentialsProvider(ICredentials creds)
        {
            SetCredentials(creds);
        }

        /// <inheritdoc/>
        public void SetCredentials(ICredentials creds)
        {
            if (creds == null)
                throw new ArgumentNullException("creds");

            OssUtils.CheckCredentials(creds.AccessKeyId, creds.AccessKeySecret);
            _creds = creds;
        }

        /// <inheritdoc/>
        public ICredentials GetCredentials()
        {
            return _creds;
        }
    }
}
