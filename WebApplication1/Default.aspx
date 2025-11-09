<%@ Page Title="Главная" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApp.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="jumbotron">
        <h1>Система Contoso: Управление Учебным Процессом</h1>
        <p class="lead">Данное веб-приложение создано на основе ASP.NET Web Forms и Entity Framework (Database First) для управления данными студентов и курсов.</p>
        <p><a href="https://www.microsoft.com/en-us/microsoft-365/business/office-365-for-education" class="btn btn-primary btn-lg">Узнать больше о Contoso &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Студенты</h2>
            <p>
                Просмотр, добавление и удаление записей студентов. Список поддерживает сортировку и постраничную навигацию.
            </p>
            <p><a class="btn btn-default" href="Students.aspx">Перейти к списку студентов &raquo;</a></p>
        </div>
        <div class="col-md-4">
            <h2>Курсы</h2>
            <p>
                Просмотр списка курсов с возможностью фильтрации по кафедре.
            </p>
            <p><a class="btn btn-default" href="Courses.aspx">Перейти к курсам &raquo;</a></p>
        </div>
        <div class="col-md-4">
            <h2>О Проекте</h2>
            <p>
                Решение разработано в рамках лабораторной работы 6-7-8 по требованиям.
            </p>
            <p><a class="btn btn-default" href="https://github.com/pollyy77/RISLr6-7-8-NEW.git">GitHub проекта &raquo;</a></p>
        </div>
    </div>

</asp:Content>