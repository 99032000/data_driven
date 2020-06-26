<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="week2.AddProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Name:"></asp:Label>
        <br />
        <asp:TextBox ID="NameTextBox" runat="server" Width="171px"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Description:"></asp:Label>
        <br />
        <asp:TextBox ID="DescriptionTextBox" runat="server" Width="512px"></asp:TextBox>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Price:"></asp:Label>
        <br />
        <asp:TextBox ID="PriceTextBox" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="AddButton" runat="server" OnClick="AddButton_Click" Text="Add to List" />
        <br />
        <asp:Label ID="Label4" runat="server" Text="List Of Products In Session"></asp:Label>
        <asp:Button ID="SaveButton" runat="server" OnClick="SaveButton_Click" Text="Save Products To Database" />
        <br />
        <asp:BulletedList ID="ProductBulletedList" runat="server">
        </asp:BulletedList>
    
    </div>
    </form>
</body>
</html>
