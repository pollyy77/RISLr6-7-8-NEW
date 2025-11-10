<%@ Page Title="Добавить студента" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentsAdd.aspx.cs" Inherits="WebApp.StudentsAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-header">Добавить нового студента</h2>
    <div class="card" style="max-width: 500px;">
        <div style="margin-bottom: 15px;">
            <label style="display: block; margin-bottom: 5px; font-weight: bold;">Имя:</label>
            <asp:TextBox ID="txtFirstName" runat="server" Width="100%" MaxLength="50" CssClass="form-control" />
        </div>
        <div style="margin-bottom: 15px;">
            <label style="display: block; margin-bottom: 5px; font-weight: bold;">Фамилия:</label>
            <asp:TextBox ID="txtLastName" runat="server" Width="100%" MaxLength="50" CssClass="form-control" />
        </div>
        <div style="margin-bottom: 20px;">
            <label style="display: block; margin-bottom: 5px; font-weight: bold;">Дата зачисления:</label>
            
            <asp:TextBox ID="txtEnrollmentDate" runat="server" TextMode="Date" Width="100%" CssClass="form-control" />
        </div>
        <div style="text-align: center;">
            <asp:Button ID="btnSave" runat="server" Text="💾 Сохранить" OnClick="btnSave_Click" CssClass="btn btn-success" style="margin-right: 10px;" />
            <asp:Button ID="btnCancel" runat="server" Text="❌ Отмена" OnClick="btnCancel_Click" CssClass="btn btn-secondary" />
        </div>
    </div>
    
    <asp:Label ID="lblMessage" runat="server" Visible="false" style="display: block; padding: 10px; margin-top: 15px; border-radius: 4px; text-align: center;" />
</asp:Content>