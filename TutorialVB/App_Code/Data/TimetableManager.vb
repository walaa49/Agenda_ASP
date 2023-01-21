Imports System
Imports System.Data
Imports Data

''' <summary>
''' Summary description for TimetableManager
''' </summary>
Public Class TimetableManager

	Public Shared Function UnmapBlock(ByVal start As TimeSpan) As Integer
		Return start.Hours
	End Function

	Public Shared Function MapBlock(ByVal id As Integer) As TimeSpan
		Return TimeSpan.FromHours(id)
	End Function

	Public Shared Function UnmapEventStart(ByVal start As Date) As Date
		Return UnmapEventStart(start.Date, start.TimeOfDay)
	End Function

	Public Shared Function UnmapEventStart(ByVal day As Date, ByVal time As TimeSpan) As Date
		Dim block As Integer = UnmapBlock(time)
		Return UnmapEventStart(day, block)
	End Function

	Public Shared Function UnmapEventStart(ByVal day As Date, ByVal block As Integer) As Date
		Dim dr As DataRow = (New DataManager()).GetBlock(block)
		If dr Is Nothing Then
			Throw New Exception("Can't map event start (block not found).")
		End If
		Dim blockStart As Date = CDate(dr("BlockStart"))
		Return day.Date.Add(blockStart.TimeOfDay)
	End Function

	Public Shared Function UnmapEventEnd(ByVal [end] As Date) As Date
		Return UnmapEventEnd([end].Date, [end].TimeOfDay)
	End Function

	Public Shared Function UnmapEventEnd(ByVal day As Date, ByVal time As TimeSpan) As Date
		Dim block As Integer = UnmapBlock(time) - 1
		Return UnmapEventEnd(day, block)
	End Function

	Public Shared Function UnmapEventEnd(ByVal day As Date, ByVal block As Integer) As Date
		Dim dr As DataRow = (New DataManager()).GetBlock(block)
		If dr Is Nothing Then
			Throw New Exception("Can't map event start (block not found).")
		End If
		Dim blockEnd As Date = CDate(dr("BlockEnd"))
		Return day.Date.Add(blockEnd.TimeOfDay)
	End Function

End Class