/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System;

namespace NClient.OSS.Common.Authentication
{
    internal abstract class ServiceSignature
    {
        public abstract string SignatureMethod { get; }

        public abstract string SignatureVersion { get; }

        public string ComputeSignature(String key, String data)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("", "key");
            if (string.IsNullOrEmpty(data))
                throw new ArgumentException("", "data");

            return ComputeSignatureCore(key, data);
        }

        protected abstract string ComputeSignatureCore(string key, string data);

        public static ServiceSignature Create()
        {
            return new HmacSha1Signature();
        }
    }
}
