Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Data
Imports DayPilot.Json
Imports DayPilot.Utils
Imports DayPilot.Web.Ui
Imports DayPilot.Web.Ui.Data
Imports DayPilot.Web.Ui.Enums
Imports DayPilot.Web.Ui.Events
Imports DayPilot.Web.Ui.Events.Scheduler
Imports BeforeTimeHeaderRenderEventArgs = DayPilot.Web.Ui.Events.Calendar.BeforeTimeHeaderRenderEventArgs
Imports CommandEventArgs = DayPilot.Web.Ui.Events.CommandEventArgs

Partial Public Class _Default
	Inherits System.Web.UI.Page

	Private blocks As DataTable

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		blocks = (New DataManager()).GetBlocks()

		If Not IsPostBack Then
			DayPilotCalendar1.StartDate = Week.FirstWorkingDayOfWeek(DayPilotNavigator1.SelectionStart)
			DayPilotCalendar1.Days = 5

			DayPilotCalendar1.DataSource = (New DataManager()).GetAssignments(DayPilotCalendar1)
			DayPilotCalendar1.DataBind()
		End If
	End Sub

	Protected Sub DayPilotCalendar1_OnBeforeTimeHeaderRender(ByVal ea As BeforeTimeHeaderRenderEventArgs)
		Dim id As Integer = ea.Start.Hours
		Dim r As DataRow = FindBlock(id)

		If r IsNot Nothing Then
			ea.InnerHTML = String.Format("Block {0}<br/>{1}<br/>{2}", id, TimeFormatter.GetHourMinutes(CDate(r("BlockStart")), DayPilotCalendar1.TimeFormat), TimeFormatter.GetHourMinutes(CDate(r("BlockEnd")), DayPilotCalendar1.TimeFormat))
		Else
			ea.InnerHTML = "Unknown Block."
		End If

		ea.Areas.Add((New Area()).Width(15).Top(0).Bottom(0).Right(0).CssClass("resource_action_menu").Html("<div><div></div></div>").JavaScript("editBlock(e);"))
	End Sub


	Private Function FindBlock(ByVal i As Integer) As DataRow
		Return blocks.Rows.Cast(Of DataRow)().FirstOrDefault(Function(dr) Convert.ToInt32(dr("BlockId")) = i)
	End Function

	Protected Sub DayPilotCalendar1_OnCommand(ByVal sender As Object, ByVal e As CommandEventArgs)
		Select Case e.Command
			Case "navigate"
				DayPilotCalendar1.StartDate = Week.FirstWorkingDayOfWeek(CDate(e.Data("start")))
				DayPilotCalendar1.Update()
				LoadEvents()

			Case "refresh"
				LoadEvents()

				If e.Data IsNot Nothing AndAlso e.Data("message") IsNot Nothing Then
					DayPilotCalendar1.UpdateWithMessage(CStr(e.Data("message")))
				Else
					DayPilotCalendar1.UpdateWithMessage("Updated.")
				End If
		End Select
	End Sub

	Private Sub LoadEvents()
		DayPilotCalendar1.DataSource = (New DataManager()).GetAssignments(DayPilotCalendar1)
		DayPilotCalendar1.DataBind()
	End Sub

	Protected Sub DayPilotCalendar1_EventMove(ByVal sender As Object, ByVal e As EventMoveEventArgs)
		CType(New DataManager(), DataManager).MoveAssignment(Convert.ToInt32(e.Value), e.NewStart, e.NewEnd)
		DayPilotCalendar1.DataSource = (New DataManager()).GetAssignments(DayPilotCalendar1)
		DayPilotCalendar1.DataBind()
		DayPilotCalendar1.Update()
	End Sub

	Protected Sub DayPilotCalendar1_EventResize(ByVal sender As Object, ByVal e As EventResizeEventArgs)
		CType(New DataManager(), DataManager).MoveAssignment(Convert.ToInt32(e.Value), e.NewStart, e.NewEnd)
		DayPilotCalendar1.DataSource = (New DataManager()).GetAssignments(DayPilotCalendar1)
		DayPilotCalendar1.DataBind()
		DayPilotCalendar1.Update()
	End Sub

	Protected Sub DayPilotCalendar1_BeforeEventRender(ByVal sender As Object, ByVal e As DayPilot.Web.Ui.Events.Calendar.BeforeEventRenderEventArgs)
		Dim color As String = CStr(e.DataItem("AssignmentColor"))
		If Not String.IsNullOrEmpty(color) Then
			e.BackgroundColor = color
			e.BorderColor = color
			e.FontColor = "#ffffff"
		End If
	End Sub
End Class
