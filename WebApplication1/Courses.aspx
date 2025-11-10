<%@ Page Title="Курсы" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="WebApp.Courses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-3"><i class="fas fa-book me-2"></i>Каталог курсов</h2>

    <div class="card shadow-sm mb-4">
        <div class="card-body">
            <div class="row align-items-center">
                <div class="col-md-6">
                    <label class="form-label fw-bold fs-5">Выберите кафедру:</label>
                    <asp:DropDownList ID="ddlDepartments" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlDepartments_SelectedIndexChanged"
                        CssClass="form-select form-select-lg">
                    </asp:DropDownList>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblSelectedDepartment" runat="server" CssClass="fs-4 text-muted fw-bold" />                
                </div>
            </div>
        </div>
    </div>

    <asp:GridView ID="gvCourses" runat="server" AutoGenerateColumns="False" CssClass="table table-hover"
        EmptyDataText='<div class="text-center py-4 text-muted"><i class="fas fa-book fa-3x mb-3"></i><h5>Выберите кафедру для отображения курсов</h5></div>'
        HeaderStyle-CssClass="table-light">
        <Columns>
            <asp:BoundField DataField="CourseID" HeaderText="Код курса" />
            <asp:BoundField DataField="Title" HeaderText="Название курса" />
            <asp:TemplateField HeaderText="Кредиты" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <span class="badge bg-secondary text-white fs-6 p-2"><%# Eval("Credits") %> кредит(-а, -ов)</span></ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Label ID="lblMessage" runat="server" CssClass="alert mt-3" />
</asp:Content>