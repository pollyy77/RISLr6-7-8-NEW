using System;
using System.Web.UI;

namespace WebApp
{
    public partial class StudentsAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Устанавливаем текущую дату по умолчанию для поля Date
            if (!IsPostBack)
            {
                txtEnrollmentDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверка входных данных
                if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
                {
                    ShowMessage("Пожалуйста, заполните Имя и Фамилию.", "error");
                    return;
                }

                if (!DateTime.TryParse(txtEnrollmentDate.Text, out DateTime enrollmentDate))
                {
                    ShowMessage("Некорректный формат даты зачисления.", "error");
                    return;
                }

                [cite_start]// Вставка в БД через EF [cite: 25]
                using (var context = new SchoolEntities())
                {
                    // Предполагаем, что Person - это класс студента
                    var newStudent = new Person
                    {
                        FirstName = txtFirstName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        EnrollmentDate = enrollmentDate,
                        // Устанавливаем Discriminator, если таблица Person используется для разных типов
                        Discriminator = "Student"
                    };

                    context.People.Add(newStudent); // People - DbSet для таблицы Person
                    context.SaveChanges();

                    [cite_start] ShowMessage($"Студент {newStudent.FirstName} {newStudent.LastName} успешно добавлен в базу данных!", "success"); [cite: 31]

                    // Очистка формы
                    txtFirstName.Text = "";
                    txtLastName.Text = "";
                    txtEnrollmentDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                }
            }
            catch (Exception ex)
            {
                [cite_start] ShowMessage("Ошибка при сохранении данных: " + ex.Message, "error"); [cite: 52]
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
            // ... (Ваш код стилизации сообщений)
        }
    }
}