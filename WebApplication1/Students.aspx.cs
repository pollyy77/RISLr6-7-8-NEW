using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WebApp; // !!! ИСПРАВЛЕНИЕ: Model1Entities находится здесь !!!

namespace WebApp
{
    public partial class Students : System.Web.UI.Page
    {
        // 🔴 КЛЮЧЕВОЕ ИСПРАВЛЕНИЕ: Ручное объявление элементов управления 
        // Если designer-файлы не генерируются, это обязательно для устранения ошибок.
        protected global::System.Web.UI.WebControls.Button btnAddStudent;
        protected global::System.Web.UI.WebControls.GridView gvStudents;
        protected global::System.Web.UI.WebControls.Label lblMessage;

        private string CurrentSortExpression
        {
            get { return ViewState["SortExpression"] as string ?? "LastName"; }
            set { ViewState["SortExpression"] = value; }
        }
        // ... (CurrentSortDirection)
        private SortDirection CurrentSortDirection
        {
            get { return (SortDirection)(ViewState["SortDirection"] ?? SortDirection.Ascending); }
            set { ViewState["SortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStudents();
            }
        }

        private void LoadStudents()
        {
            try
            {
                using (var context = new Model1Entities()) // !!! ИСПОЛЬЗУЕМ Model1Entities !!!
                {
                    IQueryable<Person> query = context.People
                        .Include(p => p.StudentGrades)
                        .Include(p => p.Courses);

                    // Реализация сортировки (Sorting)
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
                ShowMessage($"Критическая ошибка загрузки данных: {ex.Message}", "error");
            }
        }

        // ... (gvStudents_PageIndexChanging, gvStudents_Sorting)
        protected void gvStudents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStudents.PageIndex = e.NewPageIndex;
            LoadStudents();
        }

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

        protected void gvStudents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteStudent")
            {
                DeleteStudent(Convert.ToInt32(e.CommandArgument));
            }
        }

        private void DeleteStudent(int personId)
        {
            try
            {
                using (var context = new Model1Entities())
                {
                    var studentToDelete = context.People.FirstOrDefault(p => p.ID == personId);
                    if (studentToDelete != null)
                    {
                        context.People.Remove(studentToDelete);
                        context.SaveChanges();
                        ShowMessage("Студент и все связанные записи успешно удалены.", "success");
                    }
                }
            }
            catch (DbUpdateException)
            {
                ShowMessage($"Ошибка удаления: Убедитесь, что настроено каскадное удаление в Model1.edmx.", "error");
            }
            catch (Exception ex)
            {
                ShowMessage($"Критическая ошибка: {ex.Message}", "error");
            }
            finally
            {
                LoadStudents();
            }
        }

        protected void btnAddStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StudentsAdd.aspx");
        }

        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            lblMessage.Visible = !string.IsNullOrEmpty(message);

            switch (type)
            {
                case "success":
                    lblMessage.Style["background-color"] = "#d4edda";
                    lblMessage.Style["color"] = "#155724";
                    lblMessage.Style["border"] = "1px solid #c3e6cb";
                    break;
                case "error":
                    lblMessage.Style["background-color"] = "#f8d7da";
                    lblMessage.Style["color"] = "#721c24";
                    lblMessage.Style["border"] = "1px solid #f5c6cb";
                    break;
                case "reset":
                    lblMessage.Style.Clear();
                    break;
            }
        }
    }
}