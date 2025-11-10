using System;
using System.Web.UI;
using WebApplication1;

namespace WebApp
{
    public partial class StudentsAdd : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.TextBox txtFirstName;
        protected global::System.Web.UI.WebControls.TextBox txtLastName;
        protected global::System.Web.UI.WebControls.TextBox txtEnrollmentDate;
        protected global::System.Web.UI.WebControls.Label lblMessage;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtEnrollmentDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                ShowMessage("Пожалуйста, заполните все обязательные поля.", "error");
                return;
            }

            try
            {
                if (!DateTime.TryParse(txtEnrollmentDate.Text, out DateTime enrollmentDate))
                {
                    ShowMessage("Некорректный формат даты зачисления.", "error");
                    return;
                }

                using (var context = new SchoolEntities())
                {
                    var newStudent = new Person
                    {
                        FirstName = txtFirstName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        EnrollmentDate = enrollmentDate,
                        HireDate = null  
                    };

                    context.Person.Add(newStudent);  
                    context.SaveChanges();

                    ShowMessage($"Студент {newStudent.FirstName} {newStudent.LastName} успешно добавлен!", "success");

                    
                    txtFirstName.Text = "";
                    txtLastName.Text = "";
                    txtEnrollmentDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Ошибка при сохранении данных: " + ex.Message, "error");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Students.aspx");
        }

        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;

            string baseClass = "alert ";
            switch (type)
            {
                case "success":
                    baseClass += "alert-success";
                    break;
                case "error":
                    baseClass += "alert-danger";
                    break;
                default:
                    baseClass += "alert-secondary";
                    break;
            }

            lblMessage.CssClass = baseClass + " show";
        }
    }
}