using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Serialization;

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


        //===========Действия с формой=================
        public Form1()
        {
            InitializeComponent();

            searchBox1.TextChanged += searchBox1_TextChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000; // Интервал в миллисекундах (1 секунда)
            timer1.Tick += Timer_Tick;
            timer1.Start();

            LoadCardInfo();
            LoadProgress();

            poolBox.Items.Add("Starter");
            poolBox.Items.Add("Collection Level 1-14");
            poolBox.Items.Add("Recruit");
            poolBox.Items.Add("Series 1");
            poolBox.Items.Add("Series 2");
            poolBox.Items.Add("Series 3");
            poolBox.Items.Add("Series 4");
            poolBox.Items.Add("Series 5");
            poolBox.SelectedItem = null;

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
            costBox.Items.Add("8");
            costBox.SelectedItem = null;

            powerBox.Items.Add("-8");
            powerBox.Items.Add("-3");
            powerBox.Items.Add("-1");
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
            powerBox.SelectedItem = null;

            cardCostBox.Items.Add("0");
            cardCostBox.Items.Add("1");
            cardCostBox.Items.Add("2");
            cardCostBox.Items.Add("3");
            cardCostBox.Items.Add("4");
            cardCostBox.Items.Add("5");
            cardCostBox.Items.Add("6");
            cardCostBox.Items.Add("8");
            cardCostBox.SelectedItem = null;

            cardPowerBox.Items.Add("-8");
            cardPowerBox.Items.Add("-3");
            cardPowerBox.Items.Add("-1");
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

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveCardInfo();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        //===========================================


        //===========Кнопки на форме===================
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

                if (yesnoBox.SelectedIndex == 0)
                {
                    customControl.YesNo.Text = yesnoBox.SelectedItem.ToString();
                }
                else
                {
                    customControl.YesNo.Text = yesnoBox.SelectedItem.ToString();
                    customControl.BackColor = Color.Gray;
                    customControl.name.ForeColor = Color.White;
                    customControl.YesNo.ForeColor = Color.White;
                }

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

                    // Обновление прогресса в зависимости от выбранного пула
                    UpdateProgress(poolBox.SelectedItem.ToString());
                

                pictureBox1.Image = null;
                nameBox.Text = "";
            }
            else
            {
                MessageBox.Show("Не вся информация указана!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //===========================================


        //===========Сортировка=======================
        private void searchBox1_MouseClick(object sender, MouseEventArgs e)
        {
            searchBox1.Clear();
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
        //===========================================


        //=========Вид окна======================
        private void CLOSE_Click(object sender, EventArgs e)
        {
            Close();
            SaveCardInfo();
        }

        private void MINIMIZE_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }
        //======================================


        //=======Кнопки настроек====================
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
        //===========================================


        //========Кнопки связанные со списком===========
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
                    // Проверка, является ли карта "Not Owned"
                    bool isNotOwned = selectedCard.YesNo.Text == "Not Owned";

                    // Уменьшаем значение соответствующего прогресс-бара только для Owned карт
                    if (!isNotOwned)
                    {
                        DecrementProgressBar(selectedCard.Pool.Text);
                    }

                    // Удаляем карту из списка и из панели только для Owned карт
                    if (!isNotOwned)
                    {
                        cardInfoList.RemoveAt(flowLayoutPanel1.Controls.Count - 1);
                    }

                    flowLayoutPanel1.Controls.Remove(selectedCard);
                }
            }
        }
        //===========================================


        //=========ProgressBar====================================
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

            // Обновите значения меток с процентами
            starterProcent.Text = $"{((double)starterProgressValue / starterProgress.Maximum * 100):F2}%";
            collectionProcent.Text = $"{((double)collectionProgressValue / CollectionProgress.Maximum * 100):F2}%";
            recruitProcent.Text = $"{((double)recruitProgressValue / recruitProgress.Maximum * 100):F2}%";
            series1Procent.Text = $"{((double)series1ProgressValue / series1Progress.Maximum * 100):F2}%";
            series2Procent.Text = $"{((double)series2ProgressValue / series2Progress.Maximum * 100):F2}%";
            series3Procent.Text = $"{((double)series3ProgressValue / series3Progress.Maximum * 100):F2}%";
            series4Procent.Text = $"{((double)series4ProgressValue / series4Progress.Maximum * 100):F2}%";
            series5Procent.Text = $"{((double)series5ProgressValue / series5Progress.Maximum * 100):F2}%";

            // Обновите значение метки totalProcent
            totalProcent.Text = $"{((double)totalProgressValue / totalProgress.Maximum * 100):F2}%";
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

        private void UpdateProgress(string selectedPool)
        {
            int progressBarValue = 0;
            int progressBarMaxValue = 0;
            Label progressBarLabel = null;

            switch (selectedPool)
            {
                case "Starter":
                    progressBarMaxValue = starterProgress.Maximum;
                    if (starterProgressValue + 1 > progressBarMaxValue)
                    {
                        MessageBox.Show("Коллекция заполнена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    progressBarValue = ++starterProgressValue;
                    progressBarLabel = starterProcent;
                    starterProgress.Value = progressBarValue;
                    break;

                case "Collection Level 1-14":
                    progressBarMaxValue = CollectionProgress.Maximum;
                    if (collectionProgressValue + 1 > progressBarMaxValue)
                    {
                        MessageBox.Show("Коллекция заполнена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    progressBarValue = ++collectionProgressValue;
                    progressBarLabel = collectionProcent;
                    CollectionProgress.Value = progressBarValue;
                    break;

                case "Recruit":
                    progressBarMaxValue = recruitProgress.Maximum;
                    if (recruitProgressValue + 1 > progressBarMaxValue)
                    {
                        MessageBox.Show("Коллекция заполнена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    progressBarValue = ++recruitProgressValue;
                    progressBarLabel = recruitProcent;
                    recruitProgress.Value = progressBarValue;
                    break;

                case "Series 1":
                    progressBarMaxValue = series1Progress.Maximum;
                    if (series1ProgressValue + 1 > progressBarMaxValue)
                    {
                        MessageBox.Show("Коллекция заполнена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    progressBarValue = ++series1ProgressValue;
                    progressBarLabel = series1Procent;
                    series1Progress.Value = progressBarValue;
                    break;

                case "Series 2":
                    progressBarMaxValue = series2Progress.Maximum;
                    if (series2ProgressValue + 1 > progressBarMaxValue)
                    {
                        MessageBox.Show("Коллекция заполнена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    progressBarValue = ++series2ProgressValue;
                    progressBarLabel = series2Procent;
                    series2Progress.Value = progressBarValue;
                    break;

                case "Series 3":
                    progressBarMaxValue = series3Progress.Maximum;
                    if (series3ProgressValue + 1 > progressBarMaxValue)
                    {
                        MessageBox.Show("Коллекция заполнена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    progressBarValue = ++series3ProgressValue;
                    progressBarLabel = series3Procent;
                    series3Progress.Value = progressBarValue;
                    break;

                case "Series 4":
                    progressBarMaxValue = series4Progress.Maximum;
                    if (series4ProgressValue + 1 > progressBarMaxValue)
                    {
                        MessageBox.Show("Коллекция заполнена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    progressBarValue = ++series4ProgressValue;
                    progressBarLabel = series4Procent;
                    series4Progress.Value = progressBarValue;
                    break;

                case "Series 5":
                    progressBarMaxValue = series5Progress.Maximum;
                    if (series5ProgressValue + 1 > progressBarMaxValue)
                    {
                        MessageBox.Show("Коллекция заполнена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    progressBarValue = ++series5ProgressValue;
                    progressBarLabel = series5Procent;
                    series5Progress.Value = progressBarValue;
                    break;

                default:
                    break;
            }

            // Проверка, не превышено ли максимальное значение прогресс-бара
            if (progressBarValue > progressBarMaxValue)
            {
                MessageBox.Show("Коллекция заполнена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Обновление процента
            int percentage = (int)((double)progressBarValue / progressBarMaxValue * 100);
            progressBarLabel.Text = $"{percentage}%";

            // Обновление totalProgressValue
            totalProgressValue = starterProgressValue + collectionProgressValue + recruitProgressValue +
                                 series1ProgressValue + series2ProgressValue + series3ProgressValue +
                                 series4ProgressValue + series5ProgressValue;
            totalProgress.Value = totalProgressValue;
        }
        //======================================================


        //========Остальное==========================
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

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Обновление даты и времени
            dateLabel.Text = DateTime.Now.ToString("dd.MM.yyyy");
            timeLabel.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.Title = "Save an Excel File";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToExcel(saveFileDialog.FileName);
            }
        }
        private void ExportToExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("CardData");

                // Добавьте заголовки
                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "Pool";
                worksheet.Cells[1, 3].Value = "Cost";
                worksheet.Cells[1, 4].Value = "Power";
                worksheet.Cells[1, 5].Value = "Yes/No";

                int row = 2;

                // Добавьте данные о картах
                foreach (var cardInfo in cardInfoList)
                {
                    worksheet.Cells[row, 1].Value = cardInfo.Name;
                    worksheet.Cells[row, 2].Value = cardInfo.Pool;
                    worksheet.Cells[row, 3].Value = cardInfo.Cost;
                    worksheet.Cells[row, 4].Value = cardInfo.Power;
                    worksheet.Cells[row, 5].Value = cardInfo.YesnoBox ? "Yes" : "No";

                    row++;
                }

                // Центрирование текста в ячейках
                for (int i = 1; i <= 5; i++)
                {
                    worksheet.Column(i).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }

                // Установка жирного шрифта для первой строки
                worksheet.Cells["1:1"].Style.Font.Bold = true;

                // Установка шрифта Bahnschrift для первой строки
                worksheet.Cells["1:1"].Style.Font.SetFromFont("Bahnschrift", 12);

                // Сохраните файл
                package.SaveAs(new FileInfo(filePath));
            }
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To import the database, simply notice the \"CardInfo.xml\" file in the root folder, but be careful!\nDon't forget to make a backup!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        //===========================================
    }
}
