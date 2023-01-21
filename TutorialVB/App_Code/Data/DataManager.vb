Imports System
Imports System.Data
Imports System.Data.Common
Imports DayPilot.Web.Ui

Namespace Data
	Public Class DataManager

		Private Function CreateDataAdapter(ByVal [select] As String) As DbDataAdapter
			Dim da As DbDataAdapter = Factory.CreateDataAdapter()
			da.SelectCommand = CreateCommand([select])
			Return da
		End Function

		Public Function GetBlocks() As DataTable
			Dim da = CreateDataAdapter("select * from [Block] order by [BlockId]")
			Dim dt As New DataTable()
			da.Fill(dt)
			Return dt
		End Function

		Public Sub CreateAssignment(ByVal start As Date, ByVal [end] As Date, ByVal note As String, ByVal color As String)
			Using con As DbConnection = CreateConnection()
				con.Open()

				Dim cmd = CreateCommand("insert into [Assignment] ([AssignmentStart], [AssignmentEnd], [AssignmentNote], [AssignmentColor]) values (@start, @end, @note, @color)", con)
				AddParameterWithValue(cmd, "start", start)
				AddParameterWithValue(cmd, "end", [end])
				AddParameterWithValue(cmd, "note", note)
				AddParameterWithValue(cmd, "color", color)
				cmd.ExecuteNonQuery()

			End Using
		End Sub

		Public Sub DeleteAssignment(ByVal id As Integer)
			Using con = CreateConnection()
				con.Open()

				Dim cmd = CreateCommand("delete from [Assignment] where [AssignmentId] = @id", con)
				AddParameterWithValue(cmd, "id", id)
				cmd.ExecuteNonQuery()
			End Using
		End Sub

		Public Function GetAssignments(ByVal calendar As DayPilotCalendar) As Object
			Dim dt As New DataTable()
			Dim da = CreateDataAdapter("select * from [Assignment] where NOT (([AssignmentEnd] <= @start) OR ([AssignmentStart] >= @end))")
			AddParameterWithValue(da.SelectCommand, "start", calendar.StartDate)
			AddParameterWithValue(da.SelectCommand, "end", calendar.EndDate.AddDays(1))
			da.Fill(dt)
			Return dt
		End Function

		Public Sub MoveAssignment(ByVal id As Integer, ByVal start As Date, ByVal [end] As Date)
			Using con = CreateConnection()
				con.Open()

				Dim cmd = CreateCommand("update [Assignment] set [AssignmentStart] = @start, [AssignmentEnd] = @end where [AssignmentId] = @id", con)
				AddParameterWithValue(cmd, "id", id)
				AddParameterWithValue(cmd, "start", start)
				AddParameterWithValue(cmd, "end", [end])

				cmd.ExecuteNonQuery()
			End Using
		End Sub

		Public Sub UpdateAssignment(ByVal id As Integer, ByVal start As Date, ByVal [end] As Date, ByVal note As String)
			Using con = CreateConnection()
				con.Open()

				Dim cmd = CreateCommand("update [Assignment] set [AssignmentStart] = @start, [AssignmentEnd] = @end, [AssignmentNote] = @note where [AssignmentId] = @id", con)
				AddParameterWithValue(cmd, "id", id)
				AddParameterWithValue(cmd, "start", start)
				AddParameterWithValue(cmd, "end", [end])
				AddParameterWithValue(cmd, "note", note)
				cmd.ExecuteNonQuery()
			End Using

		End Sub

		Public Function GetAssignment(ByVal id As Integer) As DataRow
			Dim da = CreateDataAdapter("select * from [Assignment] where [Assignment].[AssignmentId] = @id")
			AddParameterWithValue(da.SelectCommand, "id", id)
			Dim dt As New DataTable()
			da.Fill(dt)
			If dt.Rows.Count = 1 Then
				Return dt.Rows(0)
			End If
			Return Nothing
		End Function


		#Region "Helper methods"
		Private ReadOnly Property ConnectionString() As String
			Get
				Return Db.ConnectionString()
			End Get
		End Property

		Private ReadOnly Property Factory() As DbProviderFactory
			Get
				Return Db.Factory()
			End Get
		End Property

		Private Function CreateConnection() As DbConnection
			Dim connection As DbConnection = Factory.CreateConnection()
            connection.ConnectionString = ConnectionString
			Return connection
		End Function

		Private Function CreateCommand(ByVal text As String) As DbCommand
			Dim command As DbCommand = Factory.CreateCommand()
			command.CommandText = text
			command.Connection = CreateConnection()

			Return command
		End Function

		Private Function CreateCommand(ByVal text As String, ByVal connection As DbConnection) As DbCommand
			Dim command As DbCommand = Factory.CreateCommand()
			command.CommandText = text
			command.Connection = connection

			Return command
		End Function

		Private Sub AddParameterWithValue(ByVal cmd As DbCommand, ByVal name As String, ByVal value As Object)
			Dim parameter = Factory.CreateParameter()
			parameter.Direction = ParameterDirection.Input
			parameter.ParameterName = name
			parameter.Value = value
			cmd.Parameters.Add(parameter)
		End Sub

		Private Function GetIdentity(ByVal c As DbConnection) As Integer
			Dim cmd = CreateCommand(Db.IdentityCommand(), c)
			Return Convert.ToInt32(cmd.ExecuteScalar())
		End Function

		#End Region

		Public Function GetBlock(ByVal id As Integer) As DataRow
			Dim da = CreateDataAdapter("select * from [Block] where [BlockId] = @id")
			AddParameterWithValue(da.SelectCommand, "id", id)
			Dim dt As New DataTable()
			da.Fill(dt)
			If dt.Rows.Count = 1 Then
				Return dt.Rows(0)
			End If
			Return Nothing

		End Function



		Public Sub UpdateBlock(ByVal id As Integer, ByVal start As TimeSpan, ByVal [end] As TimeSpan)
			Using con = CreateConnection()
				con.Open()

				Dim cmd = CreateCommand("update [Block] set [BlockStart] = @start, [BlockEnd] = @end where [BlockId] = @id", con)
				AddParameterWithValue(cmd, "id", id)
				AddParameterWithValue(cmd, "start", Date.Today.Add(start))
				AddParameterWithValue(cmd, "end", Date.Today.Add([end]))
				cmd.ExecuteNonQuery()
			End Using

		End Sub

		Public Sub InsertBlock(ByVal id As Integer, ByVal start As TimeSpan, ByVal [end] As TimeSpan)
			Using con = CreateConnection()
				con.Open()

				Dim cmd = CreateCommand("insert into [Block] ([BlockId], [BlockStart], [BlockEnd]) values (@id, @start, @end)", con)
				AddParameterWithValue(cmd, "id", id)
				AddParameterWithValue(cmd, "start", Date.Today.Add(start))
				AddParameterWithValue(cmd, "end", Date.Today.Add([end]))
				cmd.ExecuteNonQuery()
			End Using
		End Sub

		Public Sub UpdateAssignmentNote(ByVal id As Integer, ByVal note As String, ByVal color As String)
			Using con = CreateConnection()
				con.Open()

				Dim cmd = CreateCommand("update [Assignment] set [AssignmentNote] = @note, [AssignmentColor] = @color where [AssignmentId] = @id", con)
				AddParameterWithValue(cmd, "id", id)
				AddParameterWithValue(cmd, "note", note)
				AddParameterWithValue(cmd, "color", color)
				cmd.ExecuteNonQuery()
			End Using
		End Sub
	End Class
End Namespace