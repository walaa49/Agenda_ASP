Imports System
Imports System.Collections
Imports System.Data
Imports System.Web.UI.WebControls
Imports Data
Imports DayPilot.Utils
Imports DayPilot.Web.Ui.Enums
Imports Util

Partial Public Class NewDialog
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		If Not IsPostBack Then

			FillDropDownListStart()

			Dim start As Integer = TimetableManager.UnmapBlock(Convert.ToDateTime(Request.QueryString("start")).TimeOfDay)
			Dim [end] As Integer = TimetableManager.UnmapBlock(Convert.ToDateTime(Request.QueryString("end")).TimeOfDay)

			Dim duration As Integer = [end] - start

			DropDownListDuration.SelectedValue = duration.ToString()
			DropDownListStart.SelectedValue = start.ToString()
			TextBoxDay.Text = Convert.ToDateTime(Request.QueryString("start")).ToShortDateString()

		End If
	End Sub

	Private Sub FillDropDownListStart()
		Dim blocks As DataTable = (New DataManager()).GetBlocks()

		For Each r As DataRow In blocks.Rows
			Dim id As Integer = Convert.ToInt32(r("BlockId"))
			Dim start As Date = CDate(r("BlockStart"))
			Dim [end] As Date = CDate(r("BlockEnd"))
			Dim name As String = String.Format("Block {0} ({1} - {2})", id, TimeFormatter.GetHourMinutes(start, TimeFormat.Auto), TimeFormatter.GetHourMinutes([end], TimeFormat.Auto))
			Dim item As New ListItem(name, id.ToString())
			DropDownListStart.Items.Add(item)
		Next r
	End Sub

	Protected Sub ButtonOK_Click(ByVal sender As Object, ByVal e As EventArgs)

		'DateTime end = Convert.ToDateTime(TextBoxEnd.Text);
		Dim day As Date = Convert.ToDateTime(TextBoxDay.Text)
		Dim start As Integer = Convert.ToInt32(DropDownListStart.SelectedValue)
		Dim duration As Integer = Convert.ToInt32(DropDownListDuration.SelectedValue)
		Dim note As String = TextBoxNote.Text
		Dim color As String = DropDownListColor.SelectedValue

		Dim totalBlocks As Integer = 8
		duration = If(start + duration > totalBlocks, totalBlocks - start, duration) ' make sure it's within the visible range


		Dim startDate As Date = day.Add(TimetableManager.MapBlock(start))
		Dim endDate As Date = startDate.Add(TimetableManager.MapBlock(duration))

		CType(New DataManager(), DataManager).CreateAssignment(startDate, endDate, note, color)

		' passed to the modal dialog close handler, see Scripts/DayPilot/event_handling.js
		Dim ht As New Hashtable()
		ht("refresh") = "yes"
		ht("message") = "Event created."

		Modal.Close(Me, ht)
	End Sub

	Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
		Modal.Close(Me)
	End Sub
End Class
