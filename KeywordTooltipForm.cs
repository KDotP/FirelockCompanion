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
    public partial class KeywordTooltipForm : Form
    {
        public KeywordTooltipForm()
        {
            InitializeComponent();
        }

        public void SetText(string title, string description)
        {
            label1.Text = $"{title.ToUpper()}\n\n{description}";

            this.ClientSize = new System.Drawing.Size(Math.Min(400, label1.Width + 16), label1.Height + 16);
        }
    }
}
