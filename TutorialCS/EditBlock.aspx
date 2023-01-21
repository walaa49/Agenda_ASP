<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditBlock.aspx.cs" Inherits="Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/media/layout.css" rel="stylesheet" type="text/css" />
    <title>Edit</title>
</head>
<body class="padded">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellspacing="4" cellpadding="0">
            <tr>
                <td align="right" valign="top"></td>
                <td><h1>Edit Block</h1></td>
            </tr>
            <tr>
                <td align="right" valign="top">Start:</td>
                <td>
                <asp:DropDownList ID="DropDownListStart" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top">End:</td>
                <td><asp:DropDownList ID="DropDownListEnd" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td align="right"></td>
                <td>
                    <asp:Button ID="ButtonOK" runat="server" OnClick="ButtonOK_Click" Text="OK" />
                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" OnClick="ButtonCancel_Click" />
                </td>
            </tr>
        </table>
        
        </div>
    </form>
</body>
</html>
