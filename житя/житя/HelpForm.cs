using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace житя
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
        }

        private void basButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void SetTextBoxText1(string filePath)
        {
            string text = File.ReadAllText(filePath);
            textBox1.Text = text;

        }
        public void SetTextBoxText2(string filePath)
        {
            string text = File.ReadAllText(filePath);
            textBox2.Text = text;
        }
    }
}
