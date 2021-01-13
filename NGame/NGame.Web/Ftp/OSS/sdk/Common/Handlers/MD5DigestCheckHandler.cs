/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System.IO;
using NClient.OSS.Common.Communication;
using NClient.OSS.Common.Internal;
using NClient.OSS.Util;
using System;

namespace NClient.OSS.Common.Handlers
{
    internal class MD5DigestCheckHandler : ResponseHandler
    {
        private Stream _inputStream;

        public MD5DigestCheckHandler(Stream inputStream) 
        {
            _inputStream = inputStream;
        }

        public override void Handle(ServiceResponse response)
        {
            if (_inputStream is MD5Stream)
            {
                MD5Stream stream = (MD5Stream)_inputStream;

                if (stream.CalculatedHash == null)
                {
                    stream.CalculateHash();
                }
                if (response.Headers.ContainsKey(HttpHeaders.ContentMd5) && stream.CalculatedHash != null) 
                {
                    var sdkCalculatedHash = Convert.ToBase64String(stream.CalculatedHash);
                    var ossCalculatedHash = response.Headers[HttpHeaders.ContentMd5];
                    if (!sdkCalculatedHash.Equals(ossCalculatedHash))
                    {
                        response.Dispose();
                        throw new ClientException("Expected hash not equal to calculated hash");
                    }
                }
            }
        }
    }
}

