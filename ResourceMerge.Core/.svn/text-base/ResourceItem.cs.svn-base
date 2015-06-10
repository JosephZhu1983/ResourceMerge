using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceMerge.Core
{
    [Serializable]
    public class ResourceItem :  IComparable<ResourceItem>
    {
        public string Url { get; set; }

        public string Content { get; set; }

        public bool? IsMinify { get; set; }

        public bool? IsMerge { get; set; }

        public ResourceType ResourceType { get; set; }

        public string Charset { get; set; }

        public int RenderPriority { get; set; }

        public RenderLocation RenderLocation { get; set; }

        public ResourceItem()
        {
            Url = string.Empty;
            Content = string.Empty;
            ResourceType = ResourceType.Auto;
            RenderLocation = RenderLocation.Auto;
        }

        public int CompareTo(ResourceItem other)
        {
            return RenderPriority - other.RenderPriority;
        }

    }
}
