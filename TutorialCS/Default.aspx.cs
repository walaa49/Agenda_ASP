using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data;
using DayPilot.Json;
using DayPilot.Utils;
using DayPilot.Web.Ui;
using DayPilot.Web.Ui.Data;
using DayPilot.Web.Ui.Enums;
using DayPilot.Web.Ui.Events;
using DayPilot.Web.Ui.Events.Scheduler;
using BeforeTimeHeaderRenderEventArgs = DayPilot.Web.Ui.Events.Calendar.BeforeTimeHeaderRenderEventArgs;
using CommandEventArgs = DayPilot.Web.Ui.Events.CommandEventArgs;

public partial class _Default : System.Web.UI.Page
{
    private DataTable blocks;

    protected void Page_Load(object sender, EventArgs e)
    {
        blocks = new DataManager().GetBlocks();

        if (!IsPostBack)
        {
            DayPilotCalendar1.StartDate = Week.FirstWorkingDayOfWeek(DayPilotNavigator1.SelectionStart);
            DayPilotCalendar1.Days = 5;

            DayPilotCalendar1.DataSource = new DataManager().GetAssignments(DayPilotCalendar1);
            DayPilotCalendar1.DataBind();
        }
    }

    protected void DayPilotCalendar1_OnBeforeTimeHeaderRender(BeforeTimeHeaderRenderEventArgs ea)
    {
        int id = ea.Start.Hours;
        DataRow r = FindBlock(id);

        if (r != null)
        {
            ea.InnerHTML = String.Format("Block {0}<br/>{1}<br/>{2}", id, TimeFormatter.GetHourMinutes((DateTime)r["BlockStart"], DayPilotCalendar1.TimeFormat), TimeFormatter.GetHourMinutes((DateTime)r["BlockEnd"], DayPilotCalendar1.TimeFormat));
        }
        else
        {
            ea.InnerHTML = "Unknown Block.";
        }

        ea.Areas.Add(new Area().Width(15).Top(0).Bottom(0).Right(0).CssClass("resource_action_menu").Html("<div><div></div></div>").JavaScript("editBlock(e);"));
    }


    private DataRow FindBlock(int i)
    {
        return blocks.Rows.Cast<DataRow>().FirstOrDefault(dr => Convert.ToInt32(dr["BlockId"]) == i);
    }

    protected void DayPilotCalendar1_OnCommand(object sender, CommandEventArgs e)
    {
        switch (e.Command)
        {
            case "navigate":
                DayPilotCalendar1.StartDate = Week.FirstWorkingDayOfWeek((DateTime) e.Data["start"]);
                DayPilotCalendar1.Update();
                LoadEvents();
                break;

            case "refresh":
                LoadEvents();

                if (e.Data != null && e.Data["message"] != null)
                {
                    DayPilotCalendar1.UpdateWithMessage((string)e.Data["message"]);
                }
                else
                {
                    DayPilotCalendar1.UpdateWithMessage("Updated.");
                }
                break;
        }
    }

    private void LoadEvents()
    {
        DayPilotCalendar1.DataSource = new DataManager().GetAssignments(DayPilotCalendar1);
        DayPilotCalendar1.DataBind();
    }

    protected void DayPilotCalendar1_EventMove(object sender, EventMoveEventArgs e)
    {
        new DataManager().MoveAssignment(Convert.ToInt32(e.Value), e.NewStart, e.NewEnd);
        DayPilotCalendar1.DataSource = new DataManager().GetAssignments(DayPilotCalendar1);
        DayPilotCalendar1.DataBind();
        DayPilotCalendar1.Update();
    }

    protected void DayPilotCalendar1_EventResize(object sender, EventResizeEventArgs e)
    {
        new DataManager().MoveAssignment(Convert.ToInt32(e.Value), e.NewStart, e.NewEnd);
        DayPilotCalendar1.DataSource = new DataManager().GetAssignments(DayPilotCalendar1);
        DayPilotCalendar1.DataBind();
        DayPilotCalendar1.Update();
    }

    protected void DayPilotCalendar1_BeforeEventRender(object sender, DayPilot.Web.Ui.Events.Calendar.BeforeEventRenderEventArgs e)
    {
        string color = (string) e.DataItem["AssignmentColor"];
        if (!String.IsNullOrEmpty(color))
        {
            e.BackgroundColor = color;
            e.BorderColor = color;
            e.FontColor = "#ffffff";
        }
    }
}
