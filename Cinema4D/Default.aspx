<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Cinema4D._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div class="d-flex flex-column">
            <p class="m-0">Nome:</p>
             <asp:TextBox ID="nome" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="nome" ErrorMessage="Il campo Nome è obbligatorio" />

             <p class="m-0">cognome:</p>
            <asp:TextBox ID="cognome" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cognome" ErrorMessage="Il campo Cognome è obbligatorio" />


            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem Text="SalaNord" Value="SalaNord"></asp:ListItem>
                <asp:ListItem Text="SalaSud" Value="SalaSud"></asp:ListItem>
                <asp:ListItem Text="SalaEst" Value="SalaEst"></asp:ListItem>
            </asp:DropDownList>
            <div class="d-flex align-items-center my-3">
              <p class="m-0 me-3">Biglietto Ridotto?</p>  <asp:CheckBox ID="CheckBox1" runat="server" /> 
            </div>
           
        </div>
        
        <asp:Button class="btn btn-success" ID="Button1" runat="server" Text="Compra Biglietto"  OnClick="ConnettiAldb"/>


        <div id="contenitore" runat="server" class="mt-5 d-flex flex-column gap-3 fs-4">
        </div>

        
    </main>

</asp:Content>
