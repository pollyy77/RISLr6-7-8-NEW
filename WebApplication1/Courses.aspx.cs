using System;
using System.Linq;
using System.Web.UI.WebControls;
using WebApplication1;

namespace WebApp
{
    public partial class Courses : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.DropDownList ddlDepartments;
        protected global::System.Web.UI.WebControls.Label lblSelectedDepartment;
        protected global::System.Web.UI.WebControls.GridView gvCourses;
        protected global::System.Web.UI.WebControls.Label lblMessage;

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
                    var departments = context.Department  
                        .OrderBy(d => d.Name)
                        .ToList();

                    ddlDepartments.DataSource = departments;
                    ddlDepartments.DataTextField = "Name";
                    ddlDepartments.DataValueField = "DepartmentID";  
                    ddlDepartments.DataBind();

                    ddlDepartments.Items.Insert(0, new ListItem("-- Выберите кафедру --", ""));
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Ошибка загрузки списка кафедр: {ex.Message}", "error");
            }
        }

        protected void ddlDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddlDepartments.SelectedValue, out int departmentId) && departmentId > 0)
            {
                lblSelectedDepartment.Text = $"Выбрана кафедра: {ddlDepartments.SelectedItem.Text}";

                try
                {
                    using (var context = new SchoolEntities())
                    {
                        var courses = context.Course  
                            .Where(c => c.DepartmentID == departmentId)
                            .OrderBy(c => c.Title)
                            .ToList();

                        gvCourses.DataSource = courses;
                        gvCourses.DataBind();
                        ShowMessage(string.Empty, "reset");
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage($"Ошибка загрузки курсов: {ex.Message}", "error");
                    gvCourses.DataSource = null;
                    gvCourses.DataBind();
                }
            }
            else
            {
                lblSelectedDepartment.Text = "";
                gvCourses.DataSource = null;
                gvCourses.DataBind();
                ShowMessage(string.Empty, "reset");
            }
        }

        private void ShowMessage(string message, string type)
        {
            if (lblMessage != null)
            {
                lblMessage.Text = message;
                lblMessage.Visible = !string.IsNullOrEmpty(message);

                switch (type)
                {
                    case "error":
                        lblMessage.Style["background-color"] = "#f8d7da";
                        lblMessage.Style["color"] = "#721c24";
                        lblMessage.Style["border"] = "1px solid #f5c6cb";
                        break;
                    case "reset":
                    default:
                        lblMessage.Style.Clear();
                        break;
                }
            }
        }
    }
}