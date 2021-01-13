/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using NClient.OSS.Model;
using System.Collections.Generic;
namespace NClient.OSS
{
    /// <summary>
    /// The result class of the operation to get live channel history.
    /// </summary>
    public class GetLiveChannelHistoryResult : GenericResult
    {
        /// <summary>
        /// The iterator of <see cref="LiveRecord" />.
        /// </summary>
        public IEnumerable<LiveRecord> LiveRecords { get; internal set; }
    }
}
