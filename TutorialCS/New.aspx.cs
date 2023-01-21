using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using Data;
using DayPilot.Utils;
using DayPilot.Web.Ui.Enums;
using Util;

public partial class NewDialog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            FillDropDownListStart();

            int start = TimetableManager.UnmapBlock(Convert.ToDateTime(Request.QueryString["start"]).TimeOfDay);
            int end = TimetableManager.UnmapBlock(Convert.ToDateTime(Request.QueryString["end"]).TimeOfDay);

            int duration = end - start;
            
            DropDownListDuration.SelectedValue = duration.ToString();
            DropDownListStart.SelectedValue = start.ToString();
            TextBoxDay.Text = Convert.ToDateTime(Request.QueryString["start"]).ToShortDateString();

        }
    }

    private void FillDropDownListStart()
    {
        DataTable blocks = new DataManager().GetBlocks();

        foreach(DataRow r in blocks.Rows)
        {
            int id = Convert.ToInt32(r["BlockId"]);
            DateTime start = (DateTime) r["BlockStart"];
            DateTime end = (DateTime)r["BlockEnd"];
            string name = String.Format("Block {0} ({1} - {2})", id, TimeFormatter.GetHourMinutes(start, TimeFormat.Auto), TimeFormatter.GetHourMinutes(end, TimeFormat.Auto));
            ListItem item = new ListItem(name, id.ToString());
            DropDownListStart.Items.Add(item);
        }
    }

    protected void ButtonOK_Click(object sender, EventArgs e)
    {
        
        //DateTime end = Convert.ToDateTime(TextBoxEnd.Text);
        DateTime day = Convert.ToDateTime(TextBoxDay.Text);
        int start = Convert.ToInt32(DropDownListStart.SelectedValue);
        int duration = Convert.ToInt32(DropDownListDuration.SelectedValue);
        string note = TextBoxNote.Text;
        string color = DropDownListColor.SelectedValue;

        int totalBlocks = 8;
        duration = start + duration > totalBlocks ? totalBlocks - start : duration; // make sure it's within the visible range


        DateTime startDate = day.Add(TimetableManager.MapBlock(start));
        DateTime endDate = startDate.Add(TimetableManager.MapBlock(duration));

        new DataManager().CreateAssignment(startDate, endDate, note, color);

        // passed to the modal dialog close handler, see Scripts/DayPilot/event_handling.js
        Hashtable ht = new Hashtable();
        ht["refresh"] = "yes";
        ht["message"] = "Event created.";

        Modal.Close(this, ht);
    }

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Modal.Close(this);
    }
}
