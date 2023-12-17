using System;
using System.Drawing;
using System.Windows.Forms;

namespace testingGrid
{
    public partial class Form2 : Form
    {
        private Form1 form1;

        public Form2(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void imageBTN_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения (*.png;*.jpg;)|*.png;*.jpg|Все файлы (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedImagePath = openFileDialog.FileName;

                    try
                    {
                        form1.BackgroundImage = Image.FromFile(selectedImagePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void colorBTN_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = Color.Black;

            DialogResult result = colorDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                Color selectedColor = colorDialog.Color;

                // Изменяем BackColor в Form1
                form1.BackColor = selectedColor;
            }
        }

        private void resetBTN_Click(object sender, EventArgs e)
        {
            form1.BackColor = Color.FromArgb(224, 224, 224);
        }

        private void resetimageBTN_Click(object sender, EventArgs e)
        {
            form1.BackgroundImage = null;
        }

        private void codeColorConfirm_Click(object sender, EventArgs e)
        {
            // Получаем значение из TextBox codeColor
            string hexColorCode = codeColor.Text;

            try
            {
                // Пытаемся преобразовать введенный код цвета в Color
                Color selectedColor = ColorTranslator.FromHtml(hexColorCode);

                // Изменяем BackColor в Form1
                form1.BackColor = selectedColor;
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный формат кода цвета! Пример: #192544", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
