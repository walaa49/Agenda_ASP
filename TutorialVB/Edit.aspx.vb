Imports System
Imports System.Collections
Imports System.Data
Imports System.Web
Imports Data
Imports Util

Partial Public Class Edit
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		Response.Cache.SetCacheability(HttpCacheability.NoCache)
		Response.Cache.SetNoStore()

		If Not IsPostBack Then

			Dim dr As DataRow = (New DataManager()).GetAssignment(Convert.ToInt32(Request.QueryString("id")))

			Dim start As Date = TimetableManager.UnmapEventStart(CDate(dr("AssignmentStart")))
			Dim [end] As Date = TimetableManager.UnmapEventEnd(CDate(dr("AssignmentEnd")))

			LabelStart.Text = start.ToString()
			LabelEnd.Text = [end].ToString()
			TextBoxNote.Text = Convert.ToString(dr("AssignmentNote"))
			DropDownListColor.SelectedValue = CStr(dr("AssignmentColor"))

			'DataBind();
		End If
	End Sub

	Protected Sub ButtonOK_Click(ByVal sender As Object, ByVal e As EventArgs)
		Dim note As String = TextBoxNote.Text
		Dim color As String = DropDownListColor.SelectedValue

		CType(New DataManager(), DataManager).UpdateAssignmentNote(Convert.ToInt32(Request.QueryString("id")), note, color)

		Dim ht As New Hashtable()
		ht("refresh") = "yes"
		ht("message") = "Event updated."

		Modal.Close(Me, ht)
	End Sub

	Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
		Modal.Close(Me)
	End Sub

	Protected Sub ButtonDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
		CType(New DataManager(), DataManager).DeleteAssignment(Convert.ToInt32(Request.QueryString("id")))
		Dim ht As New Hashtable()
		ht("refresh") = "yes"
		ht("message") = "Event deleted."
		Modal.Close(Me, ht)
	End Sub
End Class