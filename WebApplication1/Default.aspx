<%@ Page Title="Главная" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApp.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="hero mb-5 p-5 rounded-4 text-white text-center" style="background: linear-gradient(135deg,#007bff,#00c3ff);">
        <h1 class="display-3 fw-bold">🎓 Contoso University</h1>
        <p class="lead fs-3">Современная система управления учебным процессом</p>
        <p>ASP.NET Web Forms • Entity Framework • База данных School</p>
    </div>

    <div class="row g-4">
        <div class="col-md-6">
            <div class="card h-100 shadow-sm">
                <div class="card-header bg-success text-white text-center">
                    <h4><i class="fas fa-users me-2"></i>Управление студентами</h4>
                </div>
                <div class="card-body">
                    <ul class="list-unstyled">
                        <li>✅ Просмотр списка студентов</li>
                        <li>✅ Добавление новых студентов</li>
                        <li>✅ Сортировка и поиск</li>
                        <li>✅ Постраничная навигация</li>
                        <li>✅ Удаление записей</li>
                    </ul>
                </div>
                <div class="card-footer text-center">
                    <a href="Students.aspx" class="btn btn-success btn-lg w-100"><i class="fas fa-arrow-right me-2"></i>Перейти к студентам</a>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="card h-100 shadow-sm">
                <div class="card-header bg-info text-white text-center">
                    <h4><i class="fas fa-book me-2"></i>Каталог курсов</h4>
                </div>
                <div class="card-body">
                    <ul class="list-unstyled">
                        <li>✅ Фильтрация по кафедрам</li>
                        <li>✅ Динамическая загрузка курсов</li>
                        <li>✅ Подробная информация о курсах</li>
                    </ul>
                </div>
                <div class="card-footer text-center">
                    <a href="Courses.aspx" class="btn btn-info btn-lg w-100 text-white"><i class="fas fa-arrow-right me-2"></i>Перейти к курсам</a>
                </div>
            </div>
        </div>
    </div>
    <div class="card shadow-sm mb-5">
    <div class="card-body text-center">
        <h4 class="text-primary mb-4"> Архитектура проекта</h4>
        <div class="row">
            <div class="col-md-4 mb-3">
                <h6>🌐 Frontend</h6>
                <p class="text-muted">ASP.NET Web Forms<br>Bootstrap 5<br>Адаптивный дизайн</p>
            </div>
            <div class="col-md-4 mb-3">
                <h6>⚙️ Backend</h6>
                <p class="text-muted">Entity Framework<br>Database First<br>SQL Server</p>
            </div>
            <div class="col-md-4 mb-3">
                <h6>📊 База данных</h6>
                <p class="text-muted">School Database<br>Таблицы: Students, Courses<br>Связи и отношения</p>
            </div>
        </div>
    </div>
</div>
</asp:Content>
