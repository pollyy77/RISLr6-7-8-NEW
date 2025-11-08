using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class Courses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDepartments();
            }
        }

        private void LoadDepartments()
        {
            try
            {
                using (var context = new SchoolEntities())
                {
                    // Загрузка списка кафедр (Departments) из EF
                    var departments = context.Departments
                        .OrderBy(d => d.Name)
                        .ToList();

                    // Привязка данных (Предполагаем поля Name и ID)
                    ddlDepartments.DataSource = departments;
                    ddlDepartments.DataTextField = "Name";
                    ddlDepartments.DataValueField = "ID";
                    ddlDepartments.DataBind();

                    // Добавление первого элемента
                    ddlDepartments.Items.Insert(0, new ListItem("-- Выберите кафедру --", ""));
                }
            }
            catch (Exception ex)
            {
                [cite_start] ShowMessage($"Ошибка загрузки списка кафедр: {ex.Message}", "error"); [cite: 52]
            }
        }

        protected void ddlDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Пытаемся распарсить ID. departmentId > 0 означает, что выбран реальный элемент
            if (int.TryParse(ddlDepartments.SelectedValue, out int departmentId) && departmentId > 0)
            {
                [cite_start] lblSelectedDepartment.Text = $"Выбрана кафедра: {ddlDepartments.SelectedItem.Text}"; [cite: 32]
                
                try
                {
                    using (var context = new SchoolEntities())
                    {
                        [cite_start]// ТРЕБОВАНИЕ: Фильтрация курсов по выбранной кафедре [cite: 26]
                        var courses = context.Courses
                            .Where(c => c.DepartmentID == departmentId) // Предполагаем, что Course имеет поле DepartmentID
                            .OrderBy(c => c.Title)
                            .ToList();

                        gvCourses.DataSource = courses;
                        gvCourses.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    [cite_start] ShowMessage($"Ошибка загрузки курсов: {ex.Message}", "error"); [cite: 52]
                    gvCourses.DataSource = null;
                    gvCourses.DataBind();
                }

            }
            else
            {
                // Если выбран пустой элемент
                lblSelectedDepartment.Text = "";
                gvCourses.DataSource = null;
                gvCourses.DataBind();
            }
        }

        private void ShowMessage(string message, string type)
        {
            // Используем lblMessage, если он есть, или lblSelectedDepartment в качестве заглушки
            Label targetLabel = (Page.FindControl("MainContent") as ContentPlaceHolder)?.FindControl("lblMessage") as Label;
            if (targetLabel != null)
            {
                targetLabel.Text = message;
                targetLabel.Visible = true;
                // ... (Ваш код стилизации сообщений)
            }
        }
    }
}