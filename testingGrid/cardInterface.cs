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
                MessageBox.Show($"{card.name.Text}\n"+
                                                  $"Pool: {card.Pool.Text}\n" +
                                                  $"Cost: {card.costText.Text}\n" +
                                                  $"Power: {card.powerText.Text}\n" +
                                                  $"Owned: {card.YesNo.Text}");
            }
        }
    }
}
