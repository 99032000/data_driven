<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionPage.aspx.cs" Inherits="week2.QuestionPage" %>

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
<body>
    <div class="row justify-content-center align-items-center h-100">
        <div class="col col-sm-6 col-md-6 col-lg-4 col-xl-3">
            <form id="form1" runat="server" class="container-fluid h-100">
                <div>

                    <asp:Label ID="CurrentQuestionLabel" runat="server" Text="Label"></asp:Label>

                    <br />
                    <asp:PlaceHolder ID="questionPlaceHolder" runat="server"></asp:PlaceHolder>
                    <br />

                    <br />
                    <asp:Button ID="NextButton" runat="server" Cssclass="btn btn-primary" OnClick="NextButton_Click" Text="Next" />

                    <br />



                </div>
            </form>
        </div>
    </div>
</body>
</html>
