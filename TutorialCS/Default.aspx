<%@ Page Title="Timetable Tutorial Demo" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<div class="left">
<DayPilot:DayPilotNavigator 
    ID="DayPilotNavigator1"
    runat="server"
    BoundDayPilotID="DayPilotCalendar1"
    Theme="navigator_green"
    ShowMonths="3"
    SelectMode="Week"
/>
</div>
<div class="right">
    <DayPilot:DayPilotCalendar
        ID="DayPilotCalendar1" 
        runat="server" 
        ClientObjectName="dp"
        DataEndField="AssignmentEnd"
        DataStartField="AssignmentStart" 
        DataTextField="AssignmentNote" 
        DataValueField="AssignmentId" 
        Theme="calendar_green"

        HeaderHeight="30"
        HeaderDateFormat="D"
        CellDuration="60"
        CellHeight="70"
        DayBeginsHour="1"
        DayEndsHour="8"
        HeightSpec="Full"
        HourWidth="100"
        OnBeforeTimeHeaderRender="DayPilotCalendar1_OnBeforeTimeHeaderRender"
        TimeRangeSelectedHandling="JavaScript"
        TimeRangeSelectedJavaScript="create(start, end, resource);"
        EventMoveHandling="CallBack"
        EventResizeHandling="CallBack"
        OnCommand="DayPilotCalendar1_OnCommand"
        EventClickHandling="JavaScript"
        EventClickJavaScript="edit(e)"
        ColumnMarginRight="0" 
        oneventmove="DayPilotCalendar1_EventMove" 
        oneventresize="DayPilotCalendar1_EventResize"
        MoveBy="Top" 
        onbeforeeventrender="DayPilotCalendar1_BeforeEventRender"
    />
</div>

</asp:Content>

