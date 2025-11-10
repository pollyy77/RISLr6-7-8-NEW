<%@ Page Title="Студенты" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Students.aspx.cs" Inherits="WebApp.Students" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2>Список студентов</h2>
        <asp:Button ID="btnAddStudent" runat="server" 
            Text="➕ Добавить студента" 
            CssClass="btn btn-outline-primary btn-lg" 
            PostBackUrl="~/StudentsAdd.aspx" />
    </div>

    <asp:Label ID="lblMessage" runat="server" CssClass="alert" Visible="false" />

    <div class="table-responsive">
        <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="8" AllowSorting="True" DataKeyNames="PersonID"
            EmptyDataText="Нет данных о студентах" 
            PagerStyle-CssClass="pagination"
            HeaderStyle-CssClass="table-light" 
            CssClass="table table-hover"
            OnPageIndexChanging="gvStudents_PageIndexChanging"
            OnRowCommand="gvStudents_RowCommand">
            <Columns>
                <asp:BoundField DataField="PersonID" HeaderText="ID" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                
                <asp:BoundField DataField="LastName" HeaderText="Фамилия" />
                
                <asp:BoundField DataField="FirstName" HeaderText="Имя" />
                
                <asp:BoundField DataField="EnrollmentDate" HeaderText="Дата зачисления" 
                    DataFormatString="{0:dd.MM.yyyy}" 
                    ItemStyle-CssClass="text-center" 
                    HeaderStyle-CssClass="text-center" />
                
                <asp:TemplateField HeaderText="Действия" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Button ID="btnDelete" runat="server" Text="🗑️ Удалить"
                            CommandName="DeleteStudent" 
                            CommandArgument='<%# Eval("PersonID") %>'
                            CssClass="btn btn-sm btn-outline-danger"
                            OnClientClick="return confirm('Удалить этого студента?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>