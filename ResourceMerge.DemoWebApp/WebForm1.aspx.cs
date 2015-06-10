using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResourceMerge.DemoWebApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected string path;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(DateTime.Now);

        }

        protected override void OnPreInit(EventArgs e)
        {
            path = "RESOURCE";
            //ResourceMerge.Core.MergeService.AddResouece("~/RESOURCE/script.js");
            for (int i = 1; i <= 3; i++)
            {
                ResourceMerge.Core.MergeService.AddResouece("~/RESOURCE/Script0" + i + ".js");
            }
            for (int i = 1; i <= 3; i++)
            {
                ResourceMerge.Core.MergeService.AddResouece("~/RESOURCE/Style0" + i + ".css");
            }


            ResourceMerge.Core.MergeService.AddResource(new ResourceMerge.Core.ResourceItem
            {
                ResourceType = ResourceMerge.Core.ResourceType.Style,
                Url = "~/RESOURCE/style.css",
            });
            base.OnPreInit(e);
        }
    }
}
