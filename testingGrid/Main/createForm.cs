using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testingGrid
{
    public partial class createForm : Form
    {
        private List<cardInterface> cardInterfaces = new List<cardInterface>();

        public createForm()
        {
            InitializeComponent();
        }

        public List<cardInterface> GetCardInterfaces()
        {
            return cardInterfaces;
        }

        public void SetSelectedCard(cardInterface selectedCard)
        {
            // Обработайте выбранную карточку и обновите соответствующий элемент flowLayoutPanel1
            // Пример: cardInterfaces[0].SomeMethod(selectedCard);
            // Замените SomeMethod на метод, который обновляет ваш интерфейс
        }
    }
}
