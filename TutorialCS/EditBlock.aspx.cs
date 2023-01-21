using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using Data;
using DayPilot.Utils;
using DayPilot.Web.Ui.Enums;
using DayPilot.Web.Ui.Recurrence;
using Util;

public partial class Edit : System.Web.UI.Page
{
    private RecurrenceRule _rule = RecurrenceRule.NoRepeat;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            DataRow dr = new DataManager().GetBlock(Convert.ToInt32(Request.QueryString["id"]));

            for (DateTime t = DateTime.Today; t <= DateTime.Today.AddDays(1); t = t.AddMinutes(15))
            {
                string text = TimeFormatter.GetHourMinutes(t.TimeOfDay, TimeFormat.Auto);
                ListItem item = new ListItem(text, t.TimeOfDay.TotalMinutes.ToString());
                DropDownListStart.Items.Add(item);
            }

            for (DateTime t = DateTime.Today; t <= DateTime.Today.AddDays(1); t = t.AddMinutes(15))
            {
                string text = TimeFormatter.GetHourMinutes(t.TimeOfDay, TimeFormat.Auto);
                ListItem item = new ListItem(text, t.TimeOfDay.TotalMinutes.ToString());
                DropDownListEnd.Items.Add(item);
            }

            if (dr != null)
            {
                string selectedStart = ((DateTime) dr["BlockStart"]).TimeOfDay.TotalMinutes.ToString();
                DropDownListStart.SelectedValue = selectedStart;

                string selectedEnd = ((DateTime)dr["BlockEnd"]).TimeOfDay.TotalMinutes.ToString();
                DropDownListEnd.SelectedValue = selectedEnd;

            }

            DataBind();
        }
    }

    protected void ButtonOK_Click(object sender, EventArgs e)
    {
        TimeSpan start = TimeSpan.FromMinutes(Convert.ToInt32(DropDownListStart.SelectedValue));
        TimeSpan end = TimeSpan.FromMinutes(Convert.ToInt32(DropDownListEnd.SelectedValue));

        int id = Convert.ToInt32(Request.QueryString["id"]);
        DataRow block = new DataManager().GetBlock(id);
        if (block == null)
        {
            new DataManager().InsertBlock(id, start, end);
        }
        else
        {
            new DataManager().UpdateBlock(id, start, end);
        }

        Hashtable ht = new Hashtable();
        ht["refresh"] = "yes";
        ht["message"] = "Block updated.";

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
        ht["message"] = "Reservation deleted.";
        Modal.Close(this, ht);
    }
}