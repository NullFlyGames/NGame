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
    internal class Crc64CheckHandler : ResponseHandler
    {
        private Stream _inputStream;

        public Crc64CheckHandler(Stream inputStream)
        {
            _inputStream = inputStream;
        }

        public override void Handle(ServiceResponse response)
        {
            if (_inputStream is Crc64Stream)
            {
                Crc64Stream stream = (Crc64Stream)_inputStream;

                if (stream.CalculatedHash == null)
                {
                    stream.CalculateHash();
                }
                if (response.Headers.ContainsKey(HttpHeaders.HashCrc64Ecma) && stream.CalculatedHash != null)
                {
                    var sdkCalculatedHash = BitConverter.ToUInt64(stream.CalculatedHash, 0);
                    var ossCalculatedHashStr = response.Headers[HttpHeaders.HashCrc64Ecma];
                    if (!sdkCalculatedHash.ToString().Equals(ossCalculatedHashStr))
                    {
                        response.Dispose();
                        throw new ClientException("Crc64 validation failed. Expected hash not equal to calculated hash");
                    }
                }
            }
        }
    }
}

