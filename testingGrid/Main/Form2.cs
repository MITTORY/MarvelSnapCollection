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
            DialogResult result = MessageBox.Show("This is a test function, please use with caution.\nErrors may appear!\n\nTo remove the image, you can simply delete the \"backgroundImage.png\" file from the root folder and restart the application.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            // Проверяем результат диалога
            if (result == DialogResult.OK)
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
            // Очистка BackgroundImage
            BackgroundImage = null;

            // Очистка пути к файлу BackgroundImage в настройках
            Properties.Settings.Default.BackgroundImageFilePath = string.Empty;

            // Сохранение изменений
            Properties.Settings.Default.Save();
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
