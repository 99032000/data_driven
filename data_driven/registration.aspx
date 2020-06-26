<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registration.aspx.cs" Inherits="week2.registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title></title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
</head>
<header>
  
  <div class="navbar navbar-dark bg-dark shadow-sm">
    <div class="container d-flex justify-content-between">
        <strong  class="navbar-brand d-flex align-items-center text-center">AIT Survey</strong>
    </div>
  </div>
</header>
</br>
<body>
   <form id="form1" runat="server" class="container-fluid h-100">
   <div class="row justify-content-center align-items-center h-100">
    <div class="col col-sm-6 col-md-6 col-lg-4 col-xl-3">
         <div class="form-group">
            <label>First Name</label>
            
            <asp:TextBox ID="FirstNameTextBox" runat="server"  CssClass="form-control" placeholder="Enter first name" ></asp:TextBox>
             <asp:RequiredFieldValidator Display="Dynamic" ID="FirstNameValidator" ControlToValidate="FirstNameTextBox" runat="server" ErrorMessage="Please enter first name" ValidationGroup="Group1" CssClass="text-warning"></asp:RequiredFieldValidator>
        </div>
         <div class="form-group">
            <label>Last Name</label>
            
            <asp:TextBox ID="LastNameTextBox" runat="server"  CssClass="form-control" placeholder="Enter last name"></asp:TextBox>
               <asp:RequiredFieldValidator Display="Dynamic" ID="LastNameValidator" ControlToValidate="LastNameTextBox" runat="server" ErrorMessage="Please enter last name" ValidationGroup="Group1" CssClass="text-warning"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
    <label>Date of birth</label>
    
     <asp:TextBox ID="DOBTextBox" runat="server" type="date" CssClass="form-control" placeholder="Enter DOB" ></asp:TextBox>
      <asp:RequiredFieldValidator Display="Dynamic" ID="DOBValidator" ControlToValidate="DOBTextBox" runat="server" ErrorMessage="Please enter DOB" ValidationGroup="Group1" CssClass="text-warning"></asp:RequiredFieldValidator>
  </div>
  <div class="form-group">
    <label>Contact Number</label>
    
     <asp:TextBox ID="PhoneNumberTextBox" runat="server" type="tel" CssClass="form-control" placeholder="Enter contact number" /> 
      <asp:RegularExpressionValidator ID="PhoneNumberValidator" runat="server" CssClass="text-warning"
            Display="Dynamic" ErrorMessage="That is not an phone number" ControlToValidate="PhoneNumberTextBox" 
            ValidationExpression="[0-9]{10}" ValidationGroup="Group1"
            ></asp:RegularExpressionValidator>
       <asp:RequiredFieldValidator Display="Dynamic" ID="PhoneNumberRequiredValidator" ControlToValidate="PhoneNumberTextBox" runat="server" ErrorMessage="Please enter phone number" ValidationGroup="Group1" CssClass="text-warning"></asp:RequiredFieldValidator>
      <small class="form-text text-muted">Format: 0400123123</small>

    <small id="phonehelp" class="form-text text-muted">We'll never share your phone number with anyone else.</small>
  </div>
  
  <br/>
    <div class="row justify-content-around ">
        <asp:Button ID="RegisterButton" runat="server" Text="Registration" Cssclass="btn btn-primary btn-lg col-4" OnClick="RegisterButton_Click"/>
        
        
      <asp:Button ID="NoRegisterButton" runat="server" Cssclass="btn btn-primary col-4" Text="Do not Register" OnClick="NoRegisterButton_Click" />
     </div>   
   </div>
      
   </div>
   
   
          
    
    
   </br>
     
       <asp:Label ID="MessageLabel" runat="server" Text=""></asp:Label>
   
   
          
    
    
   
     
  </form>
            
    
    
   
</body>
</html>
