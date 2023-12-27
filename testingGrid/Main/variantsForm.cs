using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static testingGrid.Form1;
using static testingGrid.Main.variantsForm;

namespace testingGrid.Main
{
    public partial class variantsForm : Form
    {
        private List<VariantsInfo> variantInfoList = new List<VariantsInfo>();

        public variantsForm()
        {
            InitializeComponent();
        }
        private void variantsForm_Load(object sender, EventArgs e)
        {
            LoadVariantInfo();
        }
        private void variantsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveCardInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && !string.IsNullOrEmpty(nameBox.Text))
            {
                variantsInterface variantsControl = new variantsInterface();

                variantsControl.Image.Image = pictureBox1.Image;
                variantsControl.name.Text = nameBox.Text;

                flowLayoutPanel1.Controls.Add(variantsControl);

                variantInfoList.Add(new VariantsInfo
                {
                    Name = nameBox.Text,
                    ImageBytes = ImageToByteArray(pictureBox1.Image),
                });
                pictureBox1.Image = null;
                nameBox.Text = "";
            }
            else
            {
                MessageBox.Show("Не вся информация указана!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения (*.png;*.jpg)|*.png;*.jpg|Все файлы (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedImagePath = openFileDialog.FileName;
                    try
                    {
                        // Загружаем изображение в pictureBox1
                        pictureBox1.Image = Image.FromFile(selectedImagePath);
                        pictureBox1.BackgroundImage = null;

                        // Извлекаем имя файла без расширения и устанавливаем его в nameBox
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(selectedImagePath);
                        nameBox.Text = fileNameWithoutExtension;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }


        public void SaveCardInfo()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<VariantsInfo>));
            using (TextWriter writer = new StreamWriter("variantInfo.xml"))
            {
                serializer.Serialize(writer, variantInfoList);
            }
        }
        private void LoadVariantInfo()
        {
            if (File.Exists("variantInfo.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<VariantsInfo>));
                using (TextReader reader = new StreamReader("variantInfo.xml"))
                {
                    variantInfoList = (List<VariantsInfo>)serializer.Deserialize(reader);
                }

                foreach (var variantinfo in variantInfoList)
                {
                    variantsInterface variantsControl = new variantsInterface();

                    variantsControl.name.Text = variantsControl.Name;
                    variantsControl.Image.Image = ByteArrayToImage(variantinfo.ImageBytes);

                    flowLayoutPanel1.Controls.Add(variantsControl);
                }
            }
        }


        [Serializable]
        public class VariantsInfo
        {
            public string Name { get; set; }
            public byte[] ImageBytes { get; set; }
        }
        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                return Image.FromStream(stream);
            }
        }

        private void searchBox1_TextChanged(object sender, EventArgs e)
        {
            // Получаем текст из текстового поля поиска
            string searchText = searchBox1.Text.ToLower();

            // Проходим по всем элементам в flowLayoutPanel1
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                // Проверяем, является ли текущий элемент картой (CardInterface)
                if (control is variantsInterface card)
                {
                    // Скрываем или отображаем карту в зависимости от соответствия поисковому запросу
                    card.Visible = card.name.Text.ToLower().Contains(searchText);
                }
            }
        }
    }
}
