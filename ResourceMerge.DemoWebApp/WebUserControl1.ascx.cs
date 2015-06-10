using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ResourceMerge.DemoWebApp
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        protected string aa = "asdaedas11111111111111111";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PlaceHolder1.Controls.Add(new LiteralControl("asdas"));
            DropDownList1.Items.Add("22");
            this.DropDownList1.SelectedValue = "22";

            DropDownList d = new DropDownList();
            d.Items.Add("1");
            d.Items.Add("2");
            this.PlaceHolder1.Controls.Add(d);
            d.SelectedValue = Request.QueryString["d"];
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = DateTime.Now.ToString();
        }
    }
}