using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool
{
    public partial class DepthViewWindow : Form
    {
        public DepthViewWindow(Image image)
        {
            InitializeComponent();
            pbDepthMap.Image = image;
        }

        private void DepthViewWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
