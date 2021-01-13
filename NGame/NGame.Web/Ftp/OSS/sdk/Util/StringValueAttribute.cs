/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.OSS.Utilities
 * 
 */

using System;

namespace NClient.OSS.Util
{
    internal sealed class StringValueAttribute : Attribute
    {
        public string Value { get; private set; }

        public StringValueAttribute(string value)
        {
            Value = value;
        }
    }
}
