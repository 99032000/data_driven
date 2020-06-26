<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminPage.aspx.cs" Inherits="week2.adminPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
</head>
    <header>

    <div class="navbar navbar-dark bg-dark shadow-sm">
        <div class="container d-flex justify-content-between">
            <strong class="navbar-brand d-flex align-items-center text-center">AIT Survey</strong>
        </div>
    </div>
</header>


<body>
    <form id="form1" runat="server">
        <div  class="container">
            <div class="row">
        <div class="col-8">
            <asp:GridView ID="userGrid" runat="server">
            </asp:GridView>
            </br>
            
        </div>
            <div class="col">
                <asp:Label ID="ageRangeLabel" runat="server" Text="Age range"></asp:Label>

            <asp:DropDownList ID="ageRangeDropdown" CssClass="btn btn-info dropdown-toggle" runat="server">
            </asp:DropDownList>
            <br />
        <asp:Label ID="StateLabel" runat="server" Text="State"></asp:Label>
        <asp:DropDownList ID="StateDropdown" CssClass="btn btn-info dropdown-toggle" runat="server">
        </asp:DropDownList>
        </br>
                <asp:Label ID="BankusedLabel" runat="server" Text="Bank used"></asp:Label>
                <asp:CheckBoxList ID="BankusedCheckBoxList" runat="server"></asp:CheckBoxList>
                </br>
                <asp:Button ID="Search" runat="server" Cssclass="btn btn-primary" Text="Search" OnClick="Search_Click" />
                </br>
                </br>
                <asp:Button ID="ShowAllUser" runat="server" Cssclass="btn btn-primary" Text="show Users" OnClick="ShowAllUser_Click" />
            </div>
                </div>
            </div>
    </form>
</body>
</html>
