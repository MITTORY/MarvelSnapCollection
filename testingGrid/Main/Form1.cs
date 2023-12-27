using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace testingGrid
{
    public partial class Form1 : Form
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        private List<CardInfo> cardInfoList = new List<CardInfo>();
        private createForm createForm;
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();


        //===========Действия с формой=================
        public Form1()
        {
            InitializeComponent();

            createForm = new createForm();
            // Добавьте обработчик события CardSelected для передачи выбранной карточки в createForm
            foreach (var card in createForm.GetCardInterfaces())
            {
                card.CardSelected += (sender, args) =>
                {
                    var selectedCard = args.SelectedCard;
                    // Передайте выбранную карточку в createForm
                    createForm.SetSelectedCard(selectedCard);
                };
            }

            searchBox1.TextChanged += searchBox1_TextChanged;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCardInfo();

            // Загрузка пути к файлу BackgroundImage из настроек
            string backgroundImageFilePath = Properties.Settings.Default.BackgroundImageFilePath;

            if (!string.IsNullOrEmpty(backgroundImageFilePath) && File.Exists(backgroundImageFilePath))
            {
                // Загрузка BackgroundImage из файла, только если путь не пустой и файл существует
                LoadBackgroundImageFromFile(backgroundImageFilePath);
            }

            // Загрузка BackColor
            int backColorArgb = Properties.Settings.Default.FormBackColor;
            if (backColorArgb != 0)
            {
                BackColor = Color.FromArgb(backColorArgb);
            }

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
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveCardInfo();

            // Сохранение BackgroundImage в файл, только если он существует
            string backgroundImageFilePath = "backgroundImage.png";
            SaveBackgroundImageToFile(backgroundImageFilePath);

            // Сохранение пути к файлу BackgroundImage в настройках
            Properties.Settings.Default.BackgroundImageFilePath = this.BackgroundImage != null ? backgroundImageFilePath : string.Empty;

            // Сохранение BackColor
            Properties.Settings.Default.FormBackColor = BackColor.ToArgb();

            // Сохранение всех настроек
            Properties.Settings.Default.Save();
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
        private void label6_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
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


                pictureBox1.Image = null;
                nameBox.Text = "";
            }
            else
            {
                MessageBox.Show("Не вся информация указана!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                    // Удаляем карту из списка и из панели только для Owned карт
                    if (!isNotOwned)
                    {
                        cardInfoList.RemoveAt(flowLayoutPanel1.Controls.Count - 1);
                    }

                    flowLayoutPanel1.Controls.Remove(selectedCard);
                }
            }
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
        private void untappedgg_Click(object sender, EventArgs e)
        {
            OpenUrl("https://snap.untapped.gg/ru/cards");
        }
        private void marvelsnappro_Click(object sender, EventArgs e)
        {
            OpenUrl("https://marvelsnap.pro/cards");
        }
        private void showInfoButton_Click(object sender, EventArgs e)
        {
            // Ваш код для вывода информации о количестве карт в каждом пуле
            StringBuilder poolInfo = new StringBuilder();

            poolInfo.AppendLine("Pool Information:");

            foreach (var pool in poolBox.Items)
            {
                string poolName = pool.ToString();
                int cardsInPool = cardInfoList.Count(card => card.Pool == poolName);

                poolInfo.AppendLine($"{poolName}: {cardsInPool} cards");
            }

            // Добавление информации об общем количестве карт
            int totalCards = cardInfoList.Count;
            poolInfo.AppendLine($"Total: {totalCards} cards");

            // Отображение информации (пример: в MessageBox)
            MessageBox.Show(poolInfo.ToString(), "Pool Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private async void cardCostBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Очистите панель перед загрузкой новых карточек
            flowLayoutPanel1.Controls.Clear();

            // Получите выбранную стоимость
            string selectedCost = cardCostBox.SelectedItem.ToString().ToLower();

            // Отфильтруйте карточки по выбранной стоимости
            var filteredCards = selectedCost == "all"
                ? cardInfoList.ToList() // Если выбрана "All", показываем все карты
                : cardInfoList.Where(card => card.Cost.ToLower().Contains(selectedCost)).ToList();

            // Подготовка ProgressBar
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
            progressBar1.Visible = true;

            // Подождите некоторое время (загрузите данные асинхронно)
            await Task.Delay(1000); // Замените на ваш реальный код загрузки данных

            // Удалите ProgressBar и отобразите карточки после загрузки
            progressBar1.Visible = false;

            foreach (var cardInfo in filteredCards)
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
            }
        }
        private async void cardPowerBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Очистите панель перед загрузкой новых карточек
            flowLayoutPanel1.Controls.Clear();

            // Получите выбранную силу
            string selectedPowerText = cardPowerBox.SelectedItem.ToString().ToLower();

            // Отфильтруйте карточки по выбранной силе
            if (selectedPowerText == "all")
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
            else if (int.TryParse(selectedPowerText, out int selectedPower))
            {
                // Отфильтруйте карточки по выбранной силе (числовое значение)
                var filteredCards = cardInfoList.Where(card => Int32.TryParse(card.Power, out int cardPower) && cardPower == selectedPower).ToList();

                // Подготовка ProgressBar
                progressBar1.Style = ProgressBarStyle.Marquee;
                progressBar1.MarqueeAnimationSpeed = 30;
                progressBar1.Visible = true;

                // Подождите некоторое время (загрузите данные асинхронно)
                await Task.Delay(1000); // Замените на ваш реальный код загрузки данных

                // Удалите ProgressBar и отобразите карточки после загрузки
                progressBar1.Visible = false;

                foreach (var cardInfo in filteredCards)
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
                }
            }
        }
        private async void cardPoolBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Очистите панель перед загрузкой новых карточек
            flowLayoutPanel1.Controls.Clear();

            // Получите выбранный пул
            string selectedPool = cardPoolBox.SelectedItem.ToString().ToLower();

            // Отфильтруйте карточки по выбранному пулу
            var filteredCards = selectedPool == "all"
                ? cardInfoList.ToList() // Если выбран "All", показываем все карты
                : cardInfoList.Where(card => card.Pool.ToLower() == selectedPool).ToList();

            // Подготовка ProgressBar
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
            progressBar1.Visible = true;

            // Подождите некоторое время (загрузите данные асинхронно)
            await Task.Delay(1000); // Замените на ваш реальный код загрузки данных

            // Удалите ProgressBar и отобразите карточки после загрузки
            progressBar1.Visible = false;

            foreach (var cardInfo in filteredCards)
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


        //========Остальное==========================
        public void SaveCardInfo()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<CardInfo>));
            using (TextWriter writer = new StreamWriter("CardInfo.xml"))
            {
                serializer.Serialize(writer, cardInfoList);
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
        private void OpenUrl(string url)
        {
            try
            {
                // Используем Process.Start для открытия ссылки в браузере по умолчанию
                Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при открытии ссылки: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveBackgroundImageToFile(string filePath)
        {
            if (this.BackgroundImage != null)
            {
                this.BackgroundImage.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
        private void LoadBackgroundImageFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                this.BackgroundImage = Image.FromFile(filePath);
            }
        }
        //===========================================


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
