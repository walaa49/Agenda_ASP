Imports System
Imports System.Collections
Imports System.Data
Imports System.Web.UI.WebControls
Imports Data
Imports DayPilot.Utils
Imports DayPilot.Web.Ui.Enums
Imports DayPilot.Web.Ui.Recurrence
Imports Util

Partial Public Class Edit
	Inherits System.Web.UI.Page

	Private _rule As RecurrenceRule = RecurrenceRule.NoRepeat
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		If Not IsPostBack Then

			Dim dr As DataRow = (New DataManager()).GetBlock(Convert.ToInt32(Request.QueryString("id")))

			Dim t As Date = Date.Today
			Do While t <= Date.Today.AddDays(1)
				Dim text As String = TimeFormatter.GetHourMinutes(t.TimeOfDay, TimeFormat.Auto)
				Dim item As New ListItem(text, t.TimeOfDay.TotalMinutes.ToString())
				DropDownListStart.Items.Add(item)
				t = t.AddMinutes(15)
			Loop

			t = Date.Today
			Do While t <= Date.Today.AddDays(1)
				Dim text As String = TimeFormatter.GetHourMinutes(t.TimeOfDay, TimeFormat.Auto)
				Dim item As New ListItem(text, t.TimeOfDay.TotalMinutes.ToString())
				DropDownListEnd.Items.Add(item)
				t = t.AddMinutes(15)
			Loop

			If dr IsNot Nothing Then
				Dim selectedStart As String = CDate(dr("BlockStart")).TimeOfDay.TotalMinutes.ToString()
				DropDownListStart.SelectedValue = selectedStart

				Dim selectedEnd As String = CDate(dr("BlockEnd")).TimeOfDay.TotalMinutes.ToString()
				DropDownListEnd.SelectedValue = selectedEnd

			End If

			DataBind()
		End If
	End Sub

	Protected Sub ButtonOK_Click(ByVal sender As Object, ByVal e As EventArgs)
		Dim start As TimeSpan = TimeSpan.FromMinutes(Convert.ToInt32(DropDownListStart.SelectedValue))
		Dim [end] As TimeSpan = TimeSpan.FromMinutes(Convert.ToInt32(DropDownListEnd.SelectedValue))

		Dim id As Integer = Convert.ToInt32(Request.QueryString("id"))
		Dim block As DataRow = (New DataManager()).GetBlock(id)
		If block Is Nothing Then
			CType(New DataManager(), DataManager).InsertBlock(id, start, [end])
		Else
			CType(New DataManager(), DataManager).UpdateBlock(id, start, [end])
		End If

		Dim ht As New Hashtable()
		ht("refresh") = "yes"
		ht("message") = "Block updated."

		Modal.Close(Me, ht)
	End Sub

	Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
		Modal.Close(Me)
	End Sub

	Protected Sub ButtonDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
		CType(New DataManager(), DataManager).DeleteAssignment(Convert.ToInt32(Request.QueryString("id")))
		Dim ht As New Hashtable()
		ht("refresh") = "yes"
		ht("message") = "Reservation deleted."
		Modal.Close(Me, ht)
	End Sub
End Class