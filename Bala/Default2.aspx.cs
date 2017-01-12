using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["urlbala"] != null && Request.Params["objectguid"] != null)
        {
            if (Request.Params["urlbala"].ToString() != "" && Request.Params["objectguid"].ToString() != "")
            {
                BusinessLogic.Bala1 b = new BusinessLogic.Bala1();

                b.createOrUpdateURL( Request.Params["objectguid"].ToString(), Request.Params["urlbala"].ToString());
            }
        }
    }
}