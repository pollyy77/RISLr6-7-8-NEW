<%@ Page Title="Курсы" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="WebApp.Courses" %>
<%-- Убедитесь, что Inherits="WebApp.Courses" --%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-header">Курсы по кафедрам (Filtering)</h2>
    <div class="card">
        <div style="margin-bottom: 20px;">
            <strong style="display: block; margin-bottom: 10px; font-size: 16px;">Выберите кафедру:</strong>
            <asp:DropDownList ID="ddlDepartments" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartments_SelectedIndexChanged" CssClass="form-control" style="width: 300px;">
                <%-- Элементы будут заполнены из БД в Code-Behind --%>
            </asp:DropDownList>
            <asp:Label ID="lblSelectedDepartment" runat="server" style="margin-left: 15px; font-weight: bold; color: #2c3e50;" />
        </div>
        
        <asp:GridView ID="gvCourses" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-striped" EmptyDataText="Выберите кафедру для отображения курсов" GridLines="None">
            <Columns>
                <asp:BoundField DataField="CourseID" HeaderText="ID курса" />
                <asp:BoundField DataField="Title" HeaderText="Название курса" />
                <asp:BoundField DataField="Credits" HeaderText="Кредиты" />
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblMessage" runat="server" Visible="false" style="display: block; padding: 10px; margin-top: 15px; border-radius: 4px; text-align: center;" />
    </div>
</asp:Content>