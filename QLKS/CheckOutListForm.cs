using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKS
{
    public partial class CheckOutListForm : UserControl
    {
        protected internal static CheckOutListForm _instance;
        public CheckOutListForm()
        {
            InitializeComponent();
        }
        protected internal static CheckOutListForm Instance()
        {
            if (_instance == null)
            {
                _instance = new CheckOutListForm();
            }
            return _instance;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
