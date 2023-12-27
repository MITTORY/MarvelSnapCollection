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
    public partial class cardInterface : UserControl
    {
        public event EventHandler EditClicked;
        public event EventHandler<CardSelectedEventArgs> CardSelected;

        public cardInterface()
        {
            InitializeComponent();
        }

        private void declineBox_Click(object sender, EventArgs e)
        {
            declineBox.Visible = false;
            acceptBox.Visible = true;
        }

        private void acceptBox_Click(object sender, EventArgs e)
        {
            acceptBox.Visible = false;
            declineBox.Visible = true;
        }

        private void cardInterface_Click(object sender, EventArgs e)
        {
            if (sender is cardInterface card)
            {
                MessageBox.Show($"{card.name.Text}\n" +
                                                  $"Pool: {card.Pool.Text}\n" +
                                                  $"Cost: {card.costText.Text}\n" +
                                                  $"Power: {card.powerText.Text}\n" +
                                                  $"Owned: {card.YesNo.Text}");
            }
        }

        private void name_Click(object sender, EventArgs e)
        {
            //NameForm nameForm = new NameForm();
            //nameForm.ShowDialog();
        }

        private void powerText_Click(object sender, EventArgs e)
        {

        }

        private void costText_Click(object sender, EventArgs e)
        {

        }

        private void editButton_Click(object sender, EventArgs e)
        {

        }
    }

    // Добавьте класс CardSelectedEventArgs
    public class CardSelectedEventArgs : EventArgs
    {
        public cardInterface SelectedCard { get; }

        public CardSelectedEventArgs(cardInterface selectedCard)
        {
            SelectedCard = selectedCard;
        }
    }
}
