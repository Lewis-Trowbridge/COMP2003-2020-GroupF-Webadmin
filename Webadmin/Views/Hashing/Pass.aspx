<%@ Page Language="C#" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="test" runat="server">
        <div>
            Test Hashing</div>
        <p>TESTIN</p>
        Username :
        <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
        <p>
        Password :
        <asp:TextBox ID="txtPass" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" Text="Button" />
        </p>
        <p>
        <asp:Label ID="lblUser" runat="server"></asp:Label>
        </p>
        <p>
            <asp:Label ID="lblPass" runat="server"></asp:Label>
        </p>
        <p>
            <asp:Label ID="lblSalt" runat="server"></asp:Label>
        </p>
    </form>
</body>
</html>
