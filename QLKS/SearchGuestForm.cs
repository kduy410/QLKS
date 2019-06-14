using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKS
{
    public partial class SearchGuestForm : Form
    {
        private ListViewColumnSorter lvwColumnSorter;
        private SqlDataReader rdr = null;
        private DataTable dtable;
        private SqlConnection con = null;
        private SqlDataAdapter adp;
        private DataSet ds;
        SqlCommand cmd = null;
        private DataTable dt = new DataTable();
        private string cs = "Data Source=DESKTOP-ASUKO;Initial Catalog=QLKS;Integrated Security=True";
        public SearchGuestForm()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewColumnSorter();
            this.listView1.ListViewItemSorter = lvwColumnSorter;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void SearchGuestForm_Load(object sender, EventArgs e)
        {
            display_Guest();
        }

        private void display_Guest()
        {
            con = new SqlConnection(cs);
            con.Open();
            dt = new DataTable("KHACH_HANG");
            adp = new SqlDataAdapter("SELECT * FROM KHACH_HANG ", con);
            adp.Fill(dt);
            listView1.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem lvi = new ListViewItem(row["idKhachHang"].ToString());
                lvi.SubItems.Add(row["HoVaTenDem"].ToString());
                lvi.SubItems.Add(row["Ten"].ToString());
                lvi.SubItems.Add(row["CMND"].ToString());
                lvi.SubItems.Add(row["TrangThai"].ToString());
                listView1.Items.Add(lvi);
            }
            adp.Dispose();
            con.Close();
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                if (lvwColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }
            }
            else
            {
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
            }
            this.listView1.Sort();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (CheckinForm._instance != null)
            {
                CheckinForm checkinForm = CheckinForm._instance;
                checkinForm.lblGuestID.Text = listView1.SelectedItems[0].Text.ToString();
                checkinForm.txtLastName.Text = listView1.SelectedItems[0].SubItems[1].Text.ToString() + " " + listView1.SelectedItems[0].SubItems[2].Text.ToString();
                checkinForm.txtCMND.Text = listView1.SelectedItems[0].SubItems[3].Text.ToString();
            }
            else
            {
                CheckOutForm checkOutForm = CheckOutForm._instance;
                checkOutForm.lblGuestID.Text = listView1.SelectedItems[0].Text.ToString();
                //checkOutForm.txtLastName.Text = listView1.SelectedItems[0].SubItems[1].Text.ToString() + " " + listView1.SelectedItems[0].SubItems[2].Text.ToString();
                //checkOutForm.txtCMND.Text = listView1.SelectedItems[0].SubItems[3].Text.ToString();
            }



            this.Close();
        }
    }
}
