<%@ Page Title="Добавить студента" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentsAdd.aspx.cs" Inherits="WebApp.StudentsAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card shadow-sm mt-4">
        <div class="card-header bg-secondary text-white">
            <h2>Добавить студента</h2>
        </div>
        <div class="card-body">
            <asp:Label ID="lblMessage" runat="server" CssClass="alert" Visible="false" />

            <div class="mb-3">
                <label class="form-label">Имя:</label>
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control form-control-lg" />
            </div>
            <div class="mb-3">
                <label class="form-label">Фамилия:</label>
                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control form-control-lg" />
            </div>
            <div class="mb-3">
                <label class="form-label">Дата зачисления:</label>
                <asp:TextBox ID="txtEnrollmentDate" runat="server" TextMode="Date" CssClass="form-control form-control-lg" />
            </div>

            <div class="d-flex gap-2 mb-3">
                <asp:Button ID="btnSave" runat="server" Text="Сохранить" CssClass="btn btn-success btn-lg" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Назад" CssClass="btn btn-outline-secondary btn-lg" OnClick="btnCancel_Click" />
            </div>
        </div>
    </div>
</asp:Content>