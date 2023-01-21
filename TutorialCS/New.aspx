<%@ Page Language="C#" AutoEventWireup="true" CodeFile="New.aspx.cs" Inherits="NewDialog" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New</title>
    <link href="~/media/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellspacing="4" cellpadding="0">
            <tr>
                <td align="right" valign="top"></td>
                <td><h1>New Event</h1></td>
            </tr>
            <tr>
                <td align="right">Day:</td>
                <td><asp:TextBox ID="TextBoxDay" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">Start:</td>
                <td><asp:DropDownList ID="DropDownListStart" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td align="right">Duration:</td>
                <td>
                <asp:DropDownList ID="DropDownListDuration" runat="server">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
                <asp:ListItem>7</asp:ListItem>
                </asp:DropDownList>
                </td>
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
                <td align="right">Description:</td>
                <td><asp:TextBox ID="TextBoxNote" runat="server"></asp:TextBox></td>
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

        <script type="text/javascript">
            document.getElementById("TextBoxNote").focus();
        </script>

</body>
</html>
