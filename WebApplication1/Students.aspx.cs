using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.UI.WebControls;
using WebApp;
using WebApplication1;

namespace WebApp
{
    public partial class Students : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.Button btnAddStudent;
        protected global::System.Web.UI.WebControls.GridView gvStudents;
        protected global::System.Web.UI.WebControls.Label lblMessage;

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
                LoadStudents();
            }
        }

        private void LoadStudents()
        {
            try
            {
                using (var context = new SchoolEntities())
                {
                    IQueryable<Person> query = context.Person
                        .Include(p => p.StudentGrade);

                    // Сортировка
                    switch (CurrentSortExpression)
                    {
                        case "PersonID":
                            query = (CurrentSortDirection == SortDirection.Ascending)
                                ? query.OrderBy(p => p.PersonID)
                                : query.OrderByDescending(p => p.PersonID);
                            break;
                        case "EnrollmentDate":
                            query = (CurrentSortDirection == SortDirection.Ascending)
                                ? query.OrderBy(p => p.EnrollmentDate)
                                : query.OrderByDescending(p => p.EnrollmentDate);
                            break;
                        case "LastName":
                        default:
                            query = (CurrentSortDirection == SortDirection.Ascending)
                                ? query.OrderBy(p => p.LastName)
                                : query.OrderByDescending(p => p.LastName);
                            break;
                    }

                    gvStudents.DataSource = query.ToList();
                    gvStudents.DataBind();
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Ошибка загрузки данных: {ex.Message}", "error");
            }
        }


        protected void gvStudents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStudents.PageIndex = e.NewPageIndex;
            LoadStudents();
        }

        protected void gvStudents_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (e.SortExpression == CurrentSortExpression)
            {
                CurrentSortDirection = (CurrentSortDirection == SortDirection.Ascending)
                    ? SortDirection.Descending
                    : SortDirection.Ascending;
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
                using (var context = new SchoolEntities())
                {
                    var studentToDelete = context.Person.Find(personId); // безопасно для PK

                    if (studentToDelete != null)
                    {
                        context.Person.Remove(studentToDelete);
                        context.SaveChanges();
                        ShowMessage("Студент успешно удалён.", "success");
                    }
                }
            }
            catch (DbUpdateException)
            {
                ShowMessage("Ошибка удаления: возможно, не настроено каскадное удаление.", "error");
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
