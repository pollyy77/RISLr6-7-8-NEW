using System;
using System.Web.UI;
using WebApp; // !!! ИСПРАВЛЕНИЕ: Model1Entities находится здесь !!!

namespace WebApp
{
    public partial class StudentsAdd : System.Web.UI.Page
    {
        // 🔴 КЛЮЧЕВОЕ ИСПРАВЛЕНИЕ: Ручное объявление элементов управления
        protected global::System.Web.UI.WebControls.TextBox txtFirstName;
        protected global::System.Web.UI.WebControls.TextBox txtLastName;
        protected global::System.Web.UI.WebControls.TextBox txtEnrollmentDate;
        protected global::System.Web.UI.WebControls.Label lblMessage;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Устанавливаем сегодняшнюю дату по умолчанию
                txtEnrollmentDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
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

                using (var context = new Model1Entities()) // !!! ИСПОЛЬЗУЕМ Model1Entities !!!
                {
                    var newStudent = new Person // Person - сгенерированный класс
                    {
                        FirstName = txtFirstName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        EnrollmentDate = enrollmentDate,
                        // Важно, если в вашей БД используется TPH (Table Per Hierarchy)
                        Discriminator = "Student"
                    };

                    context.People.Add(newStudent);
                    context.SaveChanges();

                    ShowMessage($"Студент {newStudent.FirstName} {newStudent.LastName} успешно добавлен!", "success");

                    // Очистка формы
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