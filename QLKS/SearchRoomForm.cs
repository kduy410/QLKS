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
    public partial class SearchRoomForm : Form
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
        public SearchRoomForm()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewColumnSorter();
            this.listView1.ListViewItemSorter = lvwColumnSorter;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (CheckinForm._instance != null)
            {
                CheckinForm checkinForm = CheckinForm._instance;
                checkinForm.lblRoom.Text = listView1.SelectedItems[0].Text.ToString();
                checkinForm.lblRoomType.Text = listView1.SelectedItems[0].SubItems[1].Text.ToString();
                checkinForm.lblOccupancy.Text = listView1.SelectedItems[0].SubItems[3].Text.ToString();
                //checkinForm.updateCurrentPeople();
            }
            else
            {
                CheckOutForm checkOutForm = CheckOutForm._instance;
                checkOutForm.lblRoom.Text = listView1.SelectedItems[0].Text.ToString();
                checkOutForm.lblRoomType.Text = listView1.SelectedItems[0].SubItems[1].Text.ToString();
                checkOutForm.lblOccupancy.Text = listView1.SelectedItems[0].SubItems[3].Text.ToString();
            }
            this.Close();
        }

        private void SearchRoomForm_Load(object sender, EventArgs e)
        {
            display_Room();
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
        private void display_Room()
        {
            con = new SqlConnection(cs);
            con.Open();
            dt = new DataTable("PHONG");
            //adp = new SqlDataAdapter("SELECT * FROM PHONG WHERE TinhTrang=N'trống'", con);
            adp = new SqlDataAdapter("SELECT * FROM PHONG", con);
            adp.Fill(dt);
            listView1.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem lvi = new ListViewItem(row["MaPhong"].ToString());
                //lvi.Text = row["MaPhong"].ToString();

                lvi.SubItems.Add(row["LoaiPhong"].ToString());
                lvi.SubItems.Add(row["TinhTrang"].ToString());
                lvi.SubItems.Add(row["SoLuongNguoi"].ToString());
                lvi.SubItems.Add(row["SoNguoiHienCo"].ToString());
                listView1.Items.Add(lvi);
            }
            adp.Dispose();
            con.Close();
        }
    }
}
