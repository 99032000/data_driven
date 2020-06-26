<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminLogin.aspx.cs" Inherits="week2.adminLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

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
</br>
<body>
    <form id="form1" runat="server" class="container-fluid h-100">
        <div class="row justify-content-center align-items-center h-100">
            <div class="col col-sm-6 col-md-6 col-lg-4 col-xl-3">
                <div class="form-group">
                    <label>Username</label>

                    <asp:TextBox ID="UsernameTextBox" runat="server" CssClass="form-control" placeholder="Enter username" required></asp:TextBox>

                </div>
                <div class="form-group">
                    <label>password</label>

                    <asp:TextBox ID="PasswordTextBox" runat="server" CssClass="form-control" placeholder="Enter password" required></asp:TextBox>

                </div>
                 
 <asp:Button ID="LoginButton" runat="server" CssClass="btn btn-primary" Text="Log in" OnClick="LoginButton_Click"></asp:Button>
                </br>
                <asp:Label ID="messageLabel" CssClass="text-warning" runat="server" Text=""></asp:Label>
            </div>

            <br />



            
            
            
            
        </div>
       
   
  </form>




</body>
</html>

