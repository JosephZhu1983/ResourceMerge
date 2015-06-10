using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceMerge.Core
{
    [Serializable]
    internal class MergedResource : IComparable<MergedResource>
    {
        public string Token { get; set; }

        public string Type { get; set; }

        public string CacheKey { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public bool InCache { get; set; }

        public int ContentLength { get; set; }

        public string ConfigSetName { get; set; }

        public int CompareTo(MergedResource other)
        {
            return string.Compare(this.Token, other.Token);
        }
    }

    internal enum CacheLocation
    {
        InProcessServerCache = 1,
        DistributedServerCache = 2
    }
}
