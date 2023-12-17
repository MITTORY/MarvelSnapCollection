using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace testingGrid
{
    public partial class Form1 : Form
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        private List<CardInfo> cardInfoList = new List<CardInfo>();

        private int starterProgressValue = 0;
        private int collectionProgressValue = 0;
        private int recruitProgressValue = 0;
        private int series1ProgressValue = 0;
        private int series2ProgressValue = 0;
        private int series3ProgressValue = 0;
        private int series4ProgressValue = 0;
        private int series5ProgressValue = 0;
        private int totalProgressValue = 0;


        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public Form1()
        {
            InitializeComponent();

            searchBox1.TextChanged += searchBox1_TextChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCardInfo();

            poolBox.Items.Add("Starter");
            poolBox.Items.Add("Collection Level 1-14");
            poolBox.Items.Add("Recruit");
            poolBox.Items.Add("Series 1");
            poolBox.Items.Add("Series 2");
            poolBox.Items.Add("Series 3");
            poolBox.Items.Add("Series 4");
            poolBox.Items.Add("Series 5");
            poolBox.SelectedIndex = 0;

            yesnoBox.Items.Add("Owned");
            yesnoBox.Items.Add("Not Owned");
            yesnoBox.SelectedIndex = 0;

            costBox.Items.Add("0");
            costBox.Items.Add("1");
            costBox.Items.Add("2");
            costBox.Items.Add("3");
            costBox.Items.Add("4");
            costBox.Items.Add("5");
            costBox.Items.Add("6");
            costBox.SelectedIndex = 0;

            powerBox.Items.Add("0");
            powerBox.Items.Add("1");
            powerBox.Items.Add("2");
            powerBox.Items.Add("3");
            powerBox.Items.Add("4");
            powerBox.Items.Add("5");
            powerBox.Items.Add("6");
            powerBox.Items.Add("7");
            powerBox.Items.Add("8");
            powerBox.Items.Add("9");
            powerBox.Items.Add("10");
            powerBox.Items.Add("11");
            powerBox.Items.Add("12");
            powerBox.Items.Add("14");
            powerBox.Items.Add("15");
            powerBox.Items.Add("20");
            powerBox.SelectedIndex = 0;

            cardCostBox.Items.Add("All");
            cardCostBox.Items.Add("0");
            cardCostBox.Items.Add("1");
            cardCostBox.Items.Add("2");
            cardCostBox.Items.Add("3");
            cardCostBox.Items.Add("4");
            cardCostBox.Items.Add("5");
            cardCostBox.Items.Add("6");
            cardCostBox.SelectedItem = null;

            cardPowerBox.Items.Add("All");
            cardPowerBox.Items.Add("0");
            cardPowerBox.Items.Add("1");
            cardPowerBox.Items.Add("2");
            cardPowerBox.Items.Add("3");
            cardPowerBox.Items.Add("4");
            cardPowerBox.Items.Add("5");
            cardPowerBox.Items.Add("6");
            cardPowerBox.Items.Add("7");
            cardPowerBox.Items.Add("8");
            cardPowerBox.Items.Add("9");
            cardPowerBox.Items.Add("10");
            cardPowerBox.Items.Add("11");
            cardPowerBox.Items.Add("12");
            cardPowerBox.Items.Add("14");
            cardPowerBox.Items.Add("15");
            cardPowerBox.Items.Add("20");
            cardPowerBox.SelectedItem = null;

            cardPoolBox.Items.Add("All");
            cardPoolBox.Items.Add("Starter");
            cardPoolBox.Items.Add("Collection Level 1-14");
            cardPoolBox.Items.Add("Recruit");
            cardPoolBox.Items.Add("Series 1");
            cardPoolBox.Items.Add("Series 2");
            cardPoolBox.Items.Add("Series 3");
            cardPoolBox.Items.Add("Series 4");
            cardPoolBox.Items.Add("Series 5");
            cardPoolBox.SelectedItem = null;
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && !string.IsNullOrEmpty(nameBox.Text))
            {
                cardInterface customControl = new cardInterface();

                customControl.Image.Image = pictureBox1.Image;
                customControl.name.Text = nameBox.Text;
                customControl.Pool.Text = poolBox.SelectedItem.ToString();
                customControl.costText.Text = costBox.SelectedItem.ToString();
                customControl.powerText.Text = powerBox.SelectedItem.ToString();

                // Установка видимости acceptBox и declineBox
                customControl.acceptBox.Visible = yesnoBox.SelectedIndex == 0;
                customControl.declineBox.Visible = yesnoBox.SelectedIndex == 1;

                flowLayoutPanel1.Controls.Add(customControl);

                cardInfoList.Add(new CardInfo
                {
                    Name = nameBox.Text,
                    ImageBytes = ImageToByteArray(pictureBox1.Image),
                    Pool = poolBox.SelectedItem.ToString(),
                    YesnoBox = yesnoBox.SelectedIndex == 0,
                    Cost = costBox.SelectedItem.ToString(),
                    Power = powerBox.SelectedItem.ToString(),
                });

                pictureBox1.Image = null;
                nameBox.Text = "";

                switch (poolBox.SelectedItem.ToString())
                {
                    case "Starter":
                        starterProgressValue++;
                        starterProgress.Value = starterProgressValue;
                        break;
                    case "Collection Level 1-14":
                        collectionProgressValue++;
                        CollectionProgress.Value = collectionProgressValue;
                        break;
                    case "Recruit":
                        recruitProgressValue++;
                        recruitProgress.Value = recruitProgressValue;
                        break;
                    case "Series 1":
                        series1ProgressValue++;
                        series1Progress.Value = series1ProgressValue;
                        break;
                    case "Series 2":
                        series2ProgressValue++;
                        series2Progress.Value = series2ProgressValue;
                        break;
                    case "Series 3":
                        series3ProgressValue++;
                        series3Progress.Value = series3ProgressValue;
                        break;
                    case "Series 4":
                        series4ProgressValue++;
                        series4Progress.Value = series4ProgressValue;
                        break;
                    case "Series 5":
                        series5ProgressValue++;
                        series5Progress.Value = series5ProgressValue;
                        break;
                    default:
                        break;
                }

                // Обновляем totalProgressValue
                totalProgressValue = starterProgressValue + collectionProgressValue + recruitProgressValue +
                                     series1ProgressValue + series2ProgressValue + series3ProgressValue +
                                     series4ProgressValue + series5ProgressValue;
                totalProgress.Value = totalProgressValue;
            }
            else
            {
                MessageBox.Show("Не вся информация указана!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchBox1_MouseClick(object sender, MouseEventArgs e)
        {
            searchBox1.Clear();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void CLOSE_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MINIMIZE_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            chooseBackCard chooseBackCard = new chooseBackCard(this);
            chooseBackCard.ShowDialog();
        }

        private void settingsDefaultButton_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            form2.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveCardInfo();
        }

        public void SaveCardInfo()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<CardInfo>));
            using (TextWriter writer = new StreamWriter("CardInfo.xml"))
            {
                serializer.Serialize(writer, cardInfoList);
                SaveProgress();
            }
        }

        private void LoadCardInfo()
        {
            if (File.Exists("CardInfo.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<CardInfo>));
                using (TextReader reader = new StreamReader("CardInfo.xml"))
                {
                    cardInfoList = (List<CardInfo>)serializer.Deserialize(reader);
                }

                foreach (var cardInfo in cardInfoList)
                {
                    cardInterface customControl = new cardInterface();

                    customControl.name.Text = cardInfo.Name;
                    customControl.Image.Image = ByteArrayToImage(cardInfo.ImageBytes);
                    customControl.Pool.Text = cardInfo.Pool;
                    customControl.powerText.Text = cardInfo.Power;
                    customControl.costText.Text = cardInfo.Cost;

                    // Устанавливаем видимость acceptBox и declineBox
                    customControl.acceptBox.Visible = cardInfo.YesnoBox;
                    customControl.declineBox.Visible = !cardInfo.YesnoBox;

                    flowLayoutPanel1.Controls.Add(customControl);
                    LoadProgress();
                }
            }
        }

        private void discardBTN_Click(object sender, EventArgs e)
        {
            // Specify the file path
            string filePath = "CardInfo.xml";

            try
            {
                // Check if the file exists before attempting to delete
                if (File.Exists(filePath))
                {
                    // Delete the file
                    File.Delete(filePath);

                    // Optionally, clear the cardInfoList and update the UI as needed
                    cardInfoList.Clear();
                    flowLayoutPanel1.Controls.Clear();
                }
                else
                {
                    // Display a message if the file doesn't exist
                    MessageBox.Show("Сохранение не найдено!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during file deletion
                MessageBox.Show($"Ошибка удаления файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteCard_Click(object sender, EventArgs e)
        {
            // Проверяем, есть ли выбранная карта
            if (flowLayoutPanel1.Controls.Count > 0)
            {
                // Получаем выбранную карту
                cardInterface selectedCard = flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1] as cardInterface;

                if (selectedCard != null)
                {
                    // Уменьшаем значение соответствующего прогресс-бара
                    DecrementProgressBar(selectedCard.Pool.Text);

                    // Удаляем карту из списка и из панели
                    cardInfoList.RemoveAt(flowLayoutPanel1.Controls.Count - 1);
                    flowLayoutPanel1.Controls.Remove(selectedCard);
                }
            }
        }

        private void DecrementProgressBar(string pool)
        {
            switch (pool)
            {
                case "Starter":
                    starterProgressValue--;
                    starterProgress.Value = Math.Max(0, starterProgressValue);
                    break;
                case "Collection Level 1-14":
                    collectionProgressValue--;
                    CollectionProgress.Value = Math.Max(0, collectionProgressValue);
                    break;
                case "Recruit":
                    recruitProgressValue--;
                    recruitProgress.Value = Math.Max(0, recruitProgressValue);
                    break;
                case "Series 1":
                    series1ProgressValue--;
                    series1Progress.Value = Math.Max(0, series1ProgressValue);
                    break;
                case "Series 2":
                    series2ProgressValue--;
                    series2Progress.Value = Math.Max(0, series2ProgressValue);
                    break;
                case "Series 3":
                    series3ProgressValue--;
                    series3Progress.Value = Math.Max(0, series3ProgressValue);
                    break;
                case "Series 4":
                    series4ProgressValue--;
                    series4Progress.Value = Math.Max(0, series4ProgressValue);
                    break;
                case "Series 5":
                    series5ProgressValue--;
                    series5Progress.Value = Math.Max(0, series5ProgressValue);
                    break;
                default:
                    break;
            }

            // Обновляем totalProgressValue
            totalProgressValue = starterProgressValue + collectionProgressValue + recruitProgressValue +
                                 series1ProgressValue + series2ProgressValue + series3ProgressValue +
                                 series4ProgressValue + series5ProgressValue;
            totalProgress.Value = Math.Max(0, totalProgressValue);
        }

        private void editCard_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming Soon!");
        }

        private void searchBox1_TextChanged(object sender, EventArgs e)
        {
            // Получаем текст из текстового поля поиска
            string searchText = searchBox1.Text.ToLower();

            // Проходим по всем элементам в flowLayoutPanel1
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                // Проверяем, является ли текущий элемент картой (CardInterface)
                if (control is cardInterface card)
                {
                    // Скрываем или отображаем карту в зависимости от соответствия поисковому запросу
                    card.Visible = card.name.Text.ToLower().Contains(searchText);
                }
            }
        }

        public void SaveProgress()
        {
            Properties.Settings.Default.StarterProgress = starterProgressValue;
            Properties.Settings.Default.CollectionProgress = collectionProgressValue;
            Properties.Settings.Default.RecruitProgress = recruitProgressValue;
            Properties.Settings.Default.Series1Progress = series1ProgressValue;
            Properties.Settings.Default.Series2Progress = series2ProgressValue;
            Properties.Settings.Default.Series3Progress = series3ProgressValue;
            Properties.Settings.Default.Series4Progress = series4ProgressValue;
            Properties.Settings.Default.Series5Progress = series5ProgressValue;
            Properties.Settings.Default.TotalProgress = totalProgressValue;

            Properties.Settings.Default.Save();
        }

        public void LoadProgress()
        {
            starterProgressValue = Properties.Settings.Default.StarterProgress;
            collectionProgressValue = Properties.Settings.Default.CollectionProgress;
            recruitProgressValue = Properties.Settings.Default.RecruitProgress;
            series1ProgressValue = Properties.Settings.Default.Series1Progress;
            series2ProgressValue = Properties.Settings.Default.Series2Progress;
            series3ProgressValue = Properties.Settings.Default.Series3Progress;
            series4ProgressValue = Properties.Settings.Default.Series4Progress;
            series5ProgressValue = Properties.Settings.Default.Series5Progress;
            totalProgressValue = Properties.Settings.Default.TotalProgress;

            // Обновите значения прогресс-баров
            starterProgress.Value = starterProgressValue;
            CollectionProgress.Value = collectionProgressValue;
            recruitProgress.Value = recruitProgressValue;
            series1Progress.Value = series1ProgressValue;
            series2Progress.Value = series2ProgressValue;
            series3Progress.Value = series3ProgressValue;
            series4Progress.Value = series4ProgressValue;
            series5Progress.Value = series5ProgressValue;
            totalProgress.Value = totalProgressValue;
        }

        private void ApplyFilters()
        {
            // Проходим по всем элементам в flowLayoutPanel1
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                // Проверяем, является ли текущий элемент картой (CardInterface)
                if (control is cardInterface card)
                {
                    // Проверка по стоимости
                    string cardCost = cardCostBox.SelectedItem?.ToString().ToLower();
                    bool costFilter = string.IsNullOrEmpty(cardCost) || card.costText.Text.ToLower().Contains(cardCost);

                    // Проверка по силе
                    int selectedPower;
                    bool powerFilter = int.TryParse(cardPowerBox.SelectedItem?.ToString(), out selectedPower) &&
                                       int.TryParse(card.powerText.Text, out int cardPowerValue) &&
                                       cardPowerValue == selectedPower;

                    // Проверка по пулу
                    string selectedPool = cardPoolBox.SelectedItem?.ToString().ToLower();
                    bool poolFilter = selectedPool == "all" || card.Pool.Text.ToLower() == selectedPool;

                    // Комбинирование фильтров
                    card.Visible = costFilter && powerFilter && poolFilter;
                }
            }
        }

        private void cardCostBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
            // Получаем текст из выбранного элемента в выпадающем списке
            string cardCost = cardCostBox.SelectedItem.ToString().ToLower();

            // Проходим по всем элементам в flowLayoutPanel1
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                // Проверяем, является ли текущий элемент картой (CardInterface)
                if (control is cardInterface card)
                {
                    if (cardCost == "all" || card.costText.Text.ToLower().Contains(cardCost))
                    {
                        // Выводим все карты или только те, которые соответствуют выбранной стоимости
                        card.Visible = true;
                    }
                    else
                    {
                        card.Visible = false;
                    }
                }
            }
        }

        private void cardPowerBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
            // Получаем число из выбранного элемента в выпадающем списке
            int selectedPower;

            if (cardPowerBox.SelectedItem.ToString().ToLower() == "all")
            {
                // Выводим все карты, если выбран "All" в cardPowerBox
                foreach (Control control in flowLayoutPanel1.Controls)
                {
                    if (control is cardInterface card)
                    {
                        card.Visible = true;
                    }
                }
            }
            else if (int.TryParse(cardPowerBox.SelectedItem.ToString(), out selectedPower))
            {
                // Проходим по всем элементам в flowLayoutPanel1
                foreach (Control control in flowLayoutPanel1.Controls)
                {
                    // Проверяем, является ли текущий элемент картой (CardInterface)
                    if (control is cardInterface card)
                    {
                        // Преобразуем текстовое представление силы карты в число
                        if (int.TryParse(card.powerText.Text, out int cardPowerValue))
                        {
                            // Скрываем или отображаем карту в зависимости от соответствия поисковому запросу
                            card.Visible = cardPowerValue == selectedPower;
                        }
                        else
                        {
                            // Обработка ситуации, если значение силы карты не является числом
                            card.Visible = false;
                        }
                    }
                }
            }
        }

        private void cardPoolBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
            // Получаем текст из выбранного элемента в выпадающем списке
            string selectedPool = cardPoolBox.SelectedItem.ToString().ToLower();

            // Проходим по всем элементам в flowLayoutPanel1
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                // Проверяем, является ли текущий элемент картой (CardInterface)
                if (control is cardInterface card)
                {
                    if (selectedPool == "all" || card.Pool.Text.ToLower() == selectedPool)
                    {
                        // Выводим все карты или только те, которые соответствуют выбранному пулу
                        card.Visible = true;
                    }
                    else
                    {
                        card.Visible = false;
                    }
                }
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            cardCostBox.Text = "Card Cost";
            cardPowerBox.Text = "Card Power";
            cardPoolBox.Text = "Card Pool";
        }

        private void starterProgress_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы собрали " + starterProgressValue.ToString() + " карт из " + starterProgress.Maximum.ToString());
        }

        private void CollectionProgress_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы собрали " + collectionProgressValue.ToString() + " карт из " + CollectionProgress.Maximum.ToString());
        }

        private void recruitProgress_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы собрали " + recruitProgressValue.ToString() + " карт из " + recruitProgress.Maximum.ToString());
        }

        private void series1Progress_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы собрали " + series1ProgressValue.ToString() + " карт из " + series1Progress.Maximum.ToString());
        }

        private void series2Progress_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы собрали " + series2ProgressValue.ToString() + " карт из " + series2Progress.Maximum.ToString());
        }

        private void series3Progress_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы собрали " + series3ProgressValue.ToString() + " карт из " + series3Progress.Maximum.ToString());
        }

        private void series4Progress_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы собрали " + series4ProgressValue.ToString() + " карт из " + series4Progress.Maximum.ToString());
        }

        private void series5Progress_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы собрали " + series5ProgressValue.ToString() + " карт из " + series5Progress.Maximum.ToString());
        }

        private void totalProgress_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы собрали " + totalProgressValue.ToString() + " карт из " + totalProgress.Maximum.ToString());
        }


        [Serializable]
        public class CardInfo
        {
            public string Name { get; set; }
            public byte[] ImageBytes { get; set; }
            public string Pool { get; set; }
            public string Power { get; set; }
            public string Cost { get; set; }
            public bool YesnoBox { get; set; }
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
    }
}
