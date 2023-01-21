using System;
using System.Collections;
using System.Data;
using System.Web;
using Data;
using Util;

public partial class Edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        if (!IsPostBack)
        {

            DataRow dr = new DataManager().GetAssignment(Convert.ToInt32(Request.QueryString["id"]));

            DateTime start = TimetableManager.UnmapEventStart((DateTime) dr["AssignmentStart"]);
            DateTime end = TimetableManager.UnmapEventEnd((DateTime) dr["AssignmentEnd"]);

            LabelStart.Text = start.ToString();
            LabelEnd.Text = end.ToString();
            TextBoxNote.Text = Convert.ToString(dr["AssignmentNote"]);
            DropDownListColor.SelectedValue = (string) dr["AssignmentColor"];

            //DataBind();
        }
    }

    protected void ButtonOK_Click(object sender, EventArgs e)
    {
        string note = TextBoxNote.Text;
        string color = DropDownListColor.SelectedValue;

        new DataManager().UpdateAssignmentNote(Convert.ToInt32(Request.QueryString["id"]), note, color);

        Hashtable ht = new Hashtable();
        ht["refresh"] = "yes";
        ht["message"] = "Event updated.";

        Modal.Close(this, ht);
    }

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Modal.Close(this);
    }

    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        new DataManager().DeleteAssignment(Convert.ToInt32(Request.QueryString["id"]));
        Hashtable ht = new Hashtable();
        ht["refresh"] = "yes";
        ht["message"] = "Event deleted.";
        Modal.Close(this, ht);
    }
}