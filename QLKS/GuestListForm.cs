using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace QLKS
{
    public partial class GuestListForm : UserControl
    {
        protected internal static GuestListForm _instance;
        private SqlDataReader rdr = null;
        private DataTable dtable;
        private SqlConnection con = null;
        private SqlDataAdapter adp;
        private DataSet ds;
        SqlCommand cmd = null;
        private DataTable dt = new DataTable();
        private string cs = "Data Source=DESKTOP-ASUKO;Initial Catalog=QLKS;Integrated Security=True";
        private ListViewColumnSorter lvwColumnSorter;

        private string tempHoDem = "";
        private string tempTen = "";

        public GuestListForm()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewColumnSorter();
            this.listView1.ListViewItemSorter = lvwColumnSorter;
        }
        protected internal static GuestListForm Instance()
        {
            if (_instance == null)
            {
                _instance = new GuestListForm();
            }
            return _instance;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            load_GuestList();

        }

        public void load_GuestList()
        {
            listView1.Items.Clear();
            dt = load_dataTable();
            foreach (DataRow row in dt.Rows)
            {
                addItemToList(row);
            }
        }

        private void GuestListForm_Load(object sender, EventArgs e)
        {
            load_GuestList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            load_GuestList();
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
            DialogResult dr = MessageBox.Show("Bạn có muốn cập nhật thông tin khách hàng ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                HomeForm hf = HomeForm.Instance();
                GuestForm gf = GuestForm.Instance();

                tempHoDem = listView1.SelectedItems[0].SubItems[1].Text.ToString();
                tempTen = listView1.SelectedItems[0].SubItems[2].Text.ToString();
                if (!hf.panel3.Controls.Contains(gf))
                {
                    hf.panel3.Controls.Add(gf);
                    gf.Dock = DockStyle.Fill;
                    gf.BringToFront();
                    gf.Visible = true;

                    gf.txtLastName.Text = tempHoDem.ToString();
                    gf.txtFirstName.Text = tempTen.ToString();
                    gf.txtCMND.Text = listView1.SelectedItems[0].Text.ToString();
                    gf.rbMale.Checked = true;

                    tempHoDem = "";
                    tempTen = "";

                    this.Hide();
                }
                else
                {
                    gf.BringToFront();
                    gf.Visible = true;



                    gf.txtLastName.Text = tempHoDem.ToString();
                    gf.txtFirstName.Text = tempTen.ToString();
                    gf.txtCMND.Text = listView1.SelectedItems[0].SubItems[3].Text.ToString();
                    gf.rbMale.Checked = true;

                    tempHoDem = "";
                    tempTen = "";

                    this.Hide();
                }
            }
            else
            {
                return;
            }

        }

        private void nameSplit(string first)
        {

            var names = first.Split(' ');
            if (names.Length == 0)
            {
                tempTen = "";
            }
            else if (names.Length == 1)
            {
                tempTen = names[0].ToString();
            }
            else if (names.Length > 1)
            {
                tempTen = names[names.Length - 1].ToString();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < names.Length - 1; i++)
                {
                    sb.Append(names[i].ToString() + " ");
                }
                tempHoDem = sb.ToString();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            addItem();
        }

        private void addItem()
        {
            listView1.Items.Clear(); //Xoá dữ liệu trong list view
            dt = load_dataTable();
            foreach (DataRow row in dt.Rows)
            {
                string str = row["HoDem"].ToString() + " " + row["Ten"].ToString();
                //If the cell value is start with the value in the TextBox
                if (row["HoDem"].ToString().StartsWith(txtSearch.Text))
                {
                    addItemToList(row);
                }
                else if (row["Ten"].ToString().StartsWith(txtSearch.Text))
                {
                    addItemToList(row);
                }
                else if (str.StartsWith(txtSearch.Text))
                {
                    addItemToList(row);
                }
                else if (row["CMND"].ToString().StartsWith(txtSearch.Text))
                {
                    addItemToList(row);
                }
                else if (row["TrangThai"].ToString().StartsWith(txtSearch.Text))
                {
                    addItemToList(row);
                }

            }

        }

        private void addItemToList(DataRow row)
        {
            ListViewItem lvi = new ListViewItem(row["CMND"].ToString());
            lvi.SubItems.Add(row["HoDem"].ToString());
            lvi.SubItems.Add(row["Ten"].ToString());
            lvi.SubItems.Add(row["DiaChi"].ToString());
            lvi.SubItems.Add(row["QuocTich"].ToString());
            lvi.SubItems.Add(row["Email"].ToString());
            lvi.SubItems.Add(row["GioiTinh"].ToString());
            lvi.SubItems.Add(row["TrangThai"].ToString());
            listView1.Items.Add(lvi);
        }

        private DataTable load_dataTable()
        {
            con = new SqlConnection(cs);
            con.Open();
            DataTable data = new DataTable("KHACH_HANG");
            adp = new SqlDataAdapter("SELECT * FROM KHACH_HANG", con);
            adp.Fill(data);
            adp.Dispose();
            con.Close();
            return data;
        }
    }
}
