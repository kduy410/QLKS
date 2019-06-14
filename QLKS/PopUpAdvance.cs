using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKS
{
    public partial class PopUpFormAdvance : Form
    {
        protected internal static PopUpFormAdvance _instance;

        public static double Val(string expression)
        {
            if (expression == null)
                return 0;

            //try the entire string, then progressively smaller substrings to replicate the behavior of VB's 'Val', which ignores trailing characters after a recognizable value:
            for (int size = expression.Length; size > 0; size--)
            {
                double testDouble;
                if (double.TryParse(expression.Substring(0, size), out testDouble))
                    return testDouble;
            }

            //no value is recognized, so return 0:
            return 0;
        }
        public static double Val(object expression)
        {
            if (expression == null)
                return 0;

            double testDouble;
            if (double.TryParse(expression.ToString(), out testDouble))
                return testDouble;

            //VB's 'Val' function returns -1 for 'true':
            bool testBool;
            if (bool.TryParse(expression.ToString(), out testBool))
                return testBool ? -1 : 0;

            //VB's 'Val' function returns the day of the month for dates:
            DateTime testDate;
            if (DateTime.TryParse(expression.ToString(), out testDate))
                return testDate.Day;

            //no value is recognized, so return 0:
            return 0;
        }
        public static int Val(char expression)
        {
            int testInt;
            if (int.TryParse(expression.ToString(), out testInt))
                return testInt;
            else
                return 0;
        }
        public PopUpFormAdvance()
        {
            InitializeComponent();
        }
        protected internal static PopUpFormAdvance Intance()
        {
            if (_instance == null)
            {
                _instance = new PopUpFormAdvance();
            }
            return _instance;
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            int num = (int)Val(txtNumber.Text);
            if (num != 99)
            {
                num++;
                txtNumber.Text = (num).ToString();
            }
            else
            {
                txtNumber.Text = num.ToString();
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            int num = (int)Val(txtNumber.Text);
            if (num != 0)
            {
                num--;
                txtNumber.Text = (num).ToString();
            }
            else
            {
                txtNumber.Text = num.ToString();
            }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (lblForm.Text.Equals(LsOrderForm.Instance().ToString()))
            {
                int num = (int)Val(txtNumber.Text);
                if (num != 0)
                {
                    LsOrderForm.Instance().listViewSV.SelectedItems[0].SubItems[3].Text = num.ToString();
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Số lượng không thể bằng 0, bạn có muốn xoá nó!!!", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        LsOrderForm.Instance().listViewSV.SelectedItems[0].Remove();
                        StateForm.Instance().load_all();
                        this.Hide();
                    }
                    else
                        return;
                }
            }
            return;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void txtNumber_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
      (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            lblID.Visible = false;
            lblForm.Visible = false;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn muốn huỷ dịch vụ hiện tại?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (lblForm.Text.Equals(LsOrderForm.Instance().ToString()))
                {
                    LsOrderForm.Instance().listViewSV.SelectedItems[0].Remove();
                    LsOrderForm.Instance().removeItemSQL(lblID.Text);
                    StateForm.Instance().load_all();
                    this.Hide();
                }
            }
            else return;
        }

        private void PopUpFormAdvance_Load(object sender, EventArgs e)
        {
            LsOrderForm of = LsOrderForm.Instance();
            txtNumber.Text = of.listViewSV.SelectedItems[0].SubItems[3].Text;
        }
    }
}
