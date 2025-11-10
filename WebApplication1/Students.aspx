<%@ Page Title="Студенты" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="Students.aspx.cs" Inherits="WebApp.Students" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-header">Список студентов</h2>
    
    <div style="margin-bottom: 20px;">
        <asp:Button ID="btnAddStudent" runat="server" Text="➕ Добавить студента" 
            CssClass="btn btn-success" OnClick="btnAddStudent_Click" />
    </div>

    <div class="card">
        <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="False"
            CssClass="table" AllowPaging="True" PageSize="10" 
            OnPageIndexChanging="gvStudents_PageIndexChanging"
            AllowSorting="True" OnSorting="gvStudents_Sorting" 
            DataKeyNames="PersonID" OnRowCommand="gvStudents_RowCommand"
            EmptyDataText="В базе данных нет записей о студентах">
            
            <Columns>
                <asp:TemplateField HeaderText="ФИО" SortExpression="LastName">
                    <ItemTemplate>
                        <%# Eval("LastName") %> <%# Eval("FirstName") %>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField DataField="PersonID" HeaderText="ID" SortExpression="PersonID" ReadOnly="True" />
                
                <asp:BoundField DataField="EnrollmentDate" HeaderText="Дата зачисления" 
                    SortExpression="EnrollmentDate" DataFormatString="{0:dd.MM.yyyy}" />

                <asp:TemplateField HeaderText="Кол-во оценок">
                    <ItemTemplate>
                        <%# GetGradesCount(Container.DataItem) %>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Действия">
                    <ItemTemplate>
                        <asp:Button ID="btnDelete" runat="server" Text="🗑️ Удалить" 
                            CommandName="DeleteStudent" CommandArgument='<%# Eval("PersonID") %>'
                            CssClass="btn btn-danger" 
                            OnClientClick="return confirm('Удалить этого студента?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <asp:Label ID="lblMessage" runat="server" Visible="false" 
        style="display: block; padding: 10px; margin-top: 15px; border-radius: 4px; text-align: center;" />
</asp:Content>