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
    public partial class PopupForm : Form
    {
        protected internal static PopupForm _instance;
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
        public PopupForm()
        {
            InitializeComponent();
        }
        protected internal static PopupForm Intance()
        {
            if (_instance == null)
            {
                _instance = new PopupForm();
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
            int temp = getName();
            int num = (int)Val(txtNumber.Text);
            if (num != 0)
            {
                if (temp == 0)
                {
                    LsFoodForm.Instance().listViewSV.SelectedItems[0].SubItems[3].Text = num.ToString();
                    addItemToOrder(LsFoodForm.Instance().listViewSV, LsOrderForm.Instance().listViewSV);
                }
                else if (temp == 1)
                {
                    LsDrinkForm.Instance().listViewSV.SelectedItems[0].SubItems[3].Text = num.ToString();
                    addItemToOrder(LsDrinkForm.Instance().listViewSV, LsOrderForm.Instance().listViewSV);
                }
                else if (temp == 2)
                {
                    LsItemForm.Instance().listViewSV.SelectedItems[0].SubItems[3].Text = num.ToString();
                    addItemToOrder(LsItemForm.Instance().listViewSV, LsOrderForm.Instance().listViewSV);
                }
            }
            else
            {
                MessageBox.Show("Số lượng không thể bằng 0, Xin chọn số lượng", "Question", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            return;

        }

        private int getName()
        {
            int x = -1;
            if (lblForm.Text.Equals(LsFoodForm.Instance().ToString()))
            {
                x = 0;
            }
            else if (lblForm.Text.Equals(LsDrinkForm.Instance().ToString()))
            {
                x = 1;
            }
            else if (lblForm.Text.Equals(LsItemForm.Instance().ToString()))
            {
                x = 2;
            }
            return x;
        }

        private void addItemToOrder(ListView source, ListView target)

        {
            foreach (ListViewItem i in source.SelectedItems)
            {
                if (target.Items.Count > 0)
                {
                    foreach (ListViewItem j in target.Items)
                    {
                        if (j.Text.Equals(i.Text))
                        {
                            int numj = (int)Val(j.SubItems[3].Text);
                            int numi = (int)Val(i.SubItems[3].Text);
                            int total = numj+ numi;
                            //j.SubItems[3].Text = i.SubItems[3].Text;
                            j.SubItems[3].Text = total.ToString();
                            return;
                        }
                    }
                    target.Items.Add((ListViewItem)i.Clone());
                    return;
                }
                else
                {
                    target.Items.Add((ListViewItem)i.Clone());
                    return;
                }

            }

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
    }
}
