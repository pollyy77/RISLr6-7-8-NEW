<%@ Page Title="Студенты" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="Students.aspx.cs" Inherits="WebApp.Students" %>
<%-- Убедитесь, что Inherits="WebApp.Students" соответствует вашему пространству имен/классу --%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-header">Список студентов (CRUD, Paging, Sorting, Include)</h2>
    
    <div style="margin-bottom: 20px;">
        <asp:Button ID="btnAddStudent" runat="server" Text="➕ Добавить студента" 
            CssClass="btn btn-success" OnClick="btnAddStudent_Click" />
    </div>

    <div class="card">
        <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="False"
            CssClass="table table-hover table-striped" 
            AllowPaging="True" PageSize="10" 
            OnPageIndexChanging="gvStudents_PageIndexChanging"
            AllowSorting="True" 
            OnSorting="gvStudents_Sorting" 
            DataKeyNames="ID"
            OnRowCommand="gvStudents_RowCommand"
            EmptyDataText="В базе данных нет записей о студентах."
            GridLines="None">
            
            <Columns>
                <asp:TemplateField HeaderText="ФИО" SortExpression="LastName">
                    <ItemTemplate>
                        <%# Eval("LastName") + " " + Eval("FirstName") %>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                
                <asp:BoundField DataField="EnrollmentDate" HeaderText="Дата зачисления" 
                    SortExpression="EnrollmentDate"
                    DataFormatString="{0:dd.MM.yyyy}" />

                <asp:TemplateField HeaderText="Кол-во оценок (Include)">
                    <ItemTemplate>
                        <%# Eval("StudentGrades") != null ? ((System.Collections.ICollection)Eval("StudentGrades")).Count : 0 %>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Действия">
                    <ItemTemplate>
                        <asp:Button ID="btnDelete" runat="server" Text="🗑️" 
                            CommandName="DeleteStudent" CommandArgument='<%# Eval("ID") %>'
                            CssClass="btn btn-sm btn-danger" ToolTip="Удалить"
                            OnClientClick="return confirm('Удалить этого студента и связанные записи?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>
    </div>

    <asp:Label ID="lblMessage" runat="server" Visible="false" 
        style="display: block; padding: 10px; margin-top: 15px; border-radius: 4px; text-align: center;" />
</asp:Content>