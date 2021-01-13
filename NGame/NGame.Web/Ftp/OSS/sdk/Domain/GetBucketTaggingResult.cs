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
    /// The result class of the operation to get bucket's tagging.
    /// </summary>
    public class GetBucketTaggingResult : GenericResult
    {
        /// <summary>
        /// The bucket tagging.
        /// </summary>
        private readonly IList<Tag> _tags = new List<Tag>();

        internal void AddTag(Tag tag)
        {
            _tags.Add(tag);
        }

        public IList<Tag> Tags
        {
            get { return _tags; }
        }
    }
}
