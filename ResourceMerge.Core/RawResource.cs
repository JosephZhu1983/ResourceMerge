using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web;
using System.Web.UI.HtmlControls;

namespace ResourceMerge.Core
{
    public class RawResource : HtmlContainerControl
    {
        public string Url { get; set; }

        public bool? IsMinify { get; set; }

        public bool? IsMerge { get; set; }

        public ResourceType ResourceType { get; set; }

        public string Charset { get; set; }

        public string Content { get; set; }

        public int RenderPriority { get; set; }

        public RenderLocation RenderLocation { get; set; }

        public RawResource()
        {
            this.EnableViewState = false;
        }

        protected override void OnInit(EventArgs e)
        {
            MergeService.AddResource(new ResourceItem
            {
                IsMerge = this.IsMerge,
                IsMinify = this.IsMinify,
                ResourceType = this.ResourceType,
                Url = this.Url ?? string.Empty,
                Content = InnerHtml ?? string.Empty,
                Charset = this.Charset,
                RenderPriority = this.RenderPriority,
                RenderLocation = this.RenderLocation,
            });
        }

        protected override void Render(HtmlTextWriter writer)
        {
            
        }
    }
}
