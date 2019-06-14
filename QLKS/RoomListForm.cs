using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLKS
{
    public partial class RoomListForm : UserControl
    {
        protected internal static RoomListForm _instance;
        private SqlDataReader rdr = null;
        private DataTable dtable;
        private SqlConnection con = null;
        private SqlDataAdapter adp;
        private DataSet ds;
        SqlCommand cmd = null;
        private DataTable dt = new DataTable();
        private string cs = "Data Source=DESKTOP-ASUKO;Initial Catalog=QLKS;Integrated Security=True";
        private ListViewItem lvi;
        private ListViewColumnSorter lvwColumnSorter;
        public RoomListForm()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewColumnSorter();
            this.listView1.ListViewItemSorter = lvwColumnSorter;
        }
        protected internal static RoomListForm Instance()
        {
            if (_instance == null)
            {
                _instance = new RoomListForm();
            }
            return _instance;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            load_RoomList();
        }

        private void RoomListForm_Load(object sender, EventArgs e)
        {
            load_RoomList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            load_RoomList();
        }

        public void load_RoomList()
        {
            listView1.Items.Clear();
            dt = load_dataTable();
            lblRoomCount.Text = dt.Rows.Count.ToString();
            foreach (DataRow row in dt.Rows)
            {
                addItemToList(row);
            }
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
            if (!listView1.SelectedItems.Equals(null))
            {
                RoomOption roomOption = new RoomOption();
                roomOption.Show();
                roomOption.lblRoom.Text = listView1.SelectedItems[0].Text.ToString();
                roomOption.lblRoomType.Text = listView1.SelectedItems[0].SubItems[1].Text.ToString();
                roomOption.lblPCount.Text = listView1.SelectedItems[0].SubItems[3].Text.ToString();
                roomOption.lblCurrentP.Text = listView1.SelectedItems[0].SubItems[4].Text.ToString();
            }
            else return;
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

                //If the cell value is start with the value in the TextBox
                if (row["MaPhong"].ToString().StartsWith(txtSearch.Text))
                {
                    addItemToList(row);
                }
                else if (row["LoaiPhong"].ToString().StartsWith(txtSearch.Text))
                {
                    addItemToList(row);
                }
                else if (row["TinhTrang"].ToString().StartsWith(txtSearch.Text))
                {
                    addItemToList(row);
                }
                else if (row["GioiHan"].ToString().StartsWith(txtSearch.Text))
                {
                    addItemToList(row);
                }
                //else if (row["SoNguoiHienCo"].ToString().StartsWith(txtSearch.Text))
                //{
                //    addItemToList(row);
                //}
            }

        }

        private void addItemToList(DataRow row)
        {
            ListViewItem lvi = new ListViewItem(row["MaPhong"].ToString());
            lvi.SubItems.Add(row["LoaiPhong"].ToString());
            lvi.SubItems.Add(row["TinhTrang"].ToString());
            lvi.SubItems.Add(row["GioiHan"].ToString());
            lvi.SubItems.Add(row["SoNguoiHienCo"].ToString());
            listView1.Items.Add(lvi);
        }

        private DataTable load_dataTable()
        {
            con = new SqlConnection(cs);
            con.Open();
            DataTable data = new DataTable("PHONG");
            adp = new SqlDataAdapter("SELECT * FROM PHONG", con);
            adp.Fill(data);
            adp.Dispose();
            con.Close();
            return data;
        }
    }
}
