using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirelockCompanion
{
    public partial class TextDisplay : Form
    {
        public TextDisplay(string lines)
        {
            InitializeComponent();
            richTextBox1.Text = lines;
        }
    }
}
