<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="narciarze.Test1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="T1" runat="server"></asp:TextBox><br />
            <asp:Button ID="B1" runat="server" Text="Szukaj" OnClick="B1_Click" /><br /><br />
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            <br />
            Zarejestruj siem c:</div>
        <asp:TextBox ID="Name" runat="server" placeholder="Imię"></asp:TextBox>
        <asp:TextBox ID="Last_name" runat="server" placeholder="Nazwisko"></asp:TextBox>
        <asp:TextBox ID="Email" runat="server" placeholder="Email"></asp:TextBox>
        <asp:TextBox ID="Password" runat="server" TextMode="Password" placeholder="Hasło"></asp:TextBox>
        <p>
            <asp:Button ID="Register" runat="server" OnClick="Button1_Click" Text="Rejestruj" />
        </p>
    </form>
</body>
</html>
