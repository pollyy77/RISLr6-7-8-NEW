using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data.Entity; // Для метода Include [cite: 24]
using System.Data.Entity.Infrastructure; // Для обработки ошибок БД

namespace WebApp
{
    // Убедитесь, что класс Person (генерируется EF из таблицы Person) доступен.
    public partial class Students : System.Web.UI.Page
    {
        private string CurrentSortExpression
        {
            get { return ViewState["SortExpression"] as string ?? "LastName"; }
            set { ViewState["SortExpression"] = value; }
        }

        private SortDirection CurrentSortDirection
        {
            get { return (SortDirection)(ViewState["SortDirection"] ?? SortDirection.Ascending); }
            set { ViewState["SortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvStudents.SortExpression = CurrentSortExpression;
                gvStudents.SortDirection = CurrentSortDirection;
                LoadStudents();
            }
        }

        private void LoadStudents()
        {
            try
            {
                using (var context = new SchoolEntities())
                {
                    [cite_start]// ТРЕБОВАНИЕ ВАРИАНТА 8: Include="StudentGrades, Courses" [cite: 68]
                    // Загружаем связанные данные за один запрос (N+1 check)
                    IQueryable<Person> query = context.People // People - стандартное название DbSet для таблицы Person
                        .Include(p => p.StudentGrades) // Оценки
                        .Include(p => p.Courses);      // Курсы (если связь многие-ко-многим настроена)

                    [cite_start]// Реализация сортировки (Sorting) [cite: 22]
                    switch (CurrentSortExpression)
                    {
                        case "ID":
                            query = (CurrentSortDirection == SortDirection.Ascending) ? query.OrderBy(p => p.ID) : query.OrderByDescending(p => p.ID);
                            break;
                        case "EnrollmentDate":
                            query = (CurrentSortDirection == SortDirection.Ascending) ? query.OrderBy(p => p.EnrollmentDate) : query.OrderByDescending(p => p.EnrollmentDate);
                            break;
                        case "LastName":
                        default:
                            query = (CurrentSortDirection == SortDirection.Ascending) ? query.OrderBy(p => p.LastName) : query.OrderByDescending(p => p.LastName);
                            break;
                    }

                    gvStudents.DataSource = query.ToList();
                    gvStudents.DataBind();
                }
            }
            catch (Exception ex)
            {
                [cite_start] ShowMessage($"Критическая ошибка загрузки данных: {ex.Message}", "error"); [cite: 52]
            }
        }

        [cite_start]// ОБРАБОТЧИКИ ПЕЙДЖИНГА (Paging) [cite: 22]
        protected void gvStudents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStudents.PageIndex = e.NewPageIndex;
            LoadStudents();
        }

        [cite_start]// ОБРАБОТЧИК СОРТИРОВКИ (Sorting) [cite: 22]
        protected void gvStudents_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (e.SortExpression == CurrentSortExpression)
            {
                CurrentSortDirection = (CurrentSortDirection == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending;
            }
            else
            {
                CurrentSortExpression = e.SortExpression;
                CurrentSortDirection = SortDirection.Ascending;
            }
            LoadStudents();
        }

        [cite_start]// ОБРАБОТЧИК КОМАНД (Удаление/Редактирование) [cite: 22]
        protected void gvStudents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int personId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "DeleteStudent")
            {
                DeleteStudent(personId);
            }
            else if (e.CommandName == "EditStudent")
            {
                // Перенаправление на StudentsAdd для редактирования (функция не реализована в рамках базового CRUD)
                ShowMessage($"Редактирование студента ID: {personId} (функция в разработке)", "warning");
                // Response.Redirect($"~/StudentsAdd.aspx?ID={personId}");
            }
        }

        private void DeleteStudent(int personId)
        {
            try
            {
                using (var context = new SchoolEntities())
                {
                    var studentToDelete = context.People.FirstOrDefault(p => p.ID == personId);

                    if (studentToDelete != null)
                    {
                        [cite_start]// Удаление: Сработает каскадное удаление StudentGrades благодаря настройке EDMX 
                        context.People.Remove(studentToDelete);
                        context.SaveChanges();

                        ShowMessage("Студент и все связанные записи успешно удалены.", "success");
                    }
                }
            }
            // Ловим ошибки БД, если каскадное удаление не сработало
            catch (DbUpdateException)
            {
                [cite_start] ShowMessage($"Ошибка удаления: Убедитесь, что настроено каскадное удаление в SchoolModel.edmx.", "error"); [cite: 33]
            }
            catch (Exception ex)
            {
                [cite_start] ShowMessage($"Критическая ошибка: {ex.Message}", "error"); [cite: 52]
            }
            finally
            {
                LoadStudents(); // Перезагрузка списка [cite: 31]
            }
        }

        protected void btnAddStudent_Click(object sender, EventArgs e)
        {
            [cite_start] Response.Redirect("~/StudentsAdd.aspx"); [cite: 25]
        }

        [cite_start]// Метод для вывода пользовательских сообщений [cite: 52]
        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
            // Стилизация (используем Bootstrap цвета)
            switch (type)
            {
                case "success":
                    lblMessage.Style["background-color"] = "#d4edda";
                    lblMessage.Style["color"] = "#155724";
                    lblMessage.Style["border"] = "1px solid #c3e6cb";
                    break;
                case "warning":
                    lblMessage.Style["background-color"] = "#fff3cd";
                    lblMessage.Style["color"] = "#856404";
                    lblMessage.Style["border"] = "1px solid #ffeaa7";
                    break;
                case "error":
                    lblMessage.Style["background-color"] = "#f8d7da";
                    lblMessage.Style["color"] = "#721c24";
                    lblMessage.Style["border"] = "1px solid #f5c6cb";
                    break;
            }
        }
    }
}