<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Edit.aspx.vb" Inherits="Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<link href="~/media/layout.css" rel="stylesheet" type="text/css" />
	<title>Edit</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table border="0" cellspacing="4" cellpadding="0">
			<tr>
				<td align="right" valign="top"></td>
				<td><h1>Edit Event</h1></td>
			</tr>
			<tr>
				<td align="right" valign="top">Start:</td>
				<td><asp:Label ID="LabelStart" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td align="right" valign="top">End:</td>
				<td><asp:Label ID="LabelEnd" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td align="right" valign="top">Color:</td>
				<td><asp:DropDownList ID="DropDownListColor" runat="server">
				<asp:ListItem Value="">(default)</asp:ListItem>
				<asp:ListItem Value="#666666">Gray</asp:ListItem>
				<asp:ListItem Value="#008e00">Green</asp:ListItem>
				<asp:ListItem Value="#d74f29">Red</asp:ListItem>
				<asp:ListItem Value="#004dc3">Blue</asp:ListItem>
				<asp:ListItem Value="#eab71e">Yellow</asp:ListItem>
				</asp:DropDownList></td>
			</tr>
			<tr>
				<td align="right" valign="top">Note:</td>
				<td><asp:TextBox ID="TextBoxNote" runat="server"></asp:TextBox></td>
			</tr>
			<tr>
				<td></td>
				<td><asp:LinkButton ID="ButtonDelete" runat="server" OnClick="ButtonDelete_Click" Text="Delete Event" /></td>
			</tr>
			<tr>
				<td align="right"></td>
				<td>
					<asp:HiddenField ID="Recurrence" runat="server" />
					<asp:Button ID="ButtonOK" runat="server" OnClick="ButtonOK_Click" Text="OK" />
					<asp:Button ID="ButtonCancel" runat="server" Text="Cancel" OnClick="ButtonCancel_Click" />
				</td>
			</tr>
		</table>

		</div>

		<script type="text/javascript">
			document.getElementById("TextBoxNote").focus();
		</script>
	</form>
</body>
</html>