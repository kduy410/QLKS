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
    public partial class CheckinListForm : UserControl
    {
        private ListViewItem lvi;
        private ListViewColumnSorter lvwColumnSorter;
        private SqlDataReader rdr = null;
        private DataTable dtable;
        private SqlConnection con = null;
        private SqlDataAdapter adp;
        private DataSet ds;
        SqlCommand cmd = null;
        private DataTable dt = new DataTable();
        private string cs = "Data Source=DESKTOP-ASUKO;Initial Catalog=QLKS;Integrated Security=True";
        protected internal static CheckinListForm _instance;
        public CheckinListForm()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewColumnSorter();
            this.listView1.ListViewItemSorter = lvwColumnSorter;
        }
        protected internal static CheckinListForm Instance()
        {
            if (_instance == null)
            {
                _instance = new CheckinListForm();
            }
            return _instance;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void CheckinListForm_Load(object sender, EventArgs e)
        {
            load_CheckinList();
        }

        public void load_CheckinList()
        {
            try
            {
                listView1.Items.Clear();
                dt = load_dataTable();
                foreach (DataRow row in dt.Rows)
                {
                    addItemToList(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            load_CheckinList();
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
                var dateTime1 = "";
                var dateTime2 = "";

                if (!String.IsNullOrEmpty(row["NgayDangKi"].ToString()))
                {
                    dateTime1 = Convert.ToDateTime(row["NgayDangKi"].ToString()).ToString("dd-MM-yyyy HH:mm:ss");
                }
                if (!String.IsNullOrEmpty(row["NgayTra"].ToString()))
                {
                    dateTime2 = Convert.ToDateTime(row["NgayTra"].ToString()).ToString("dd-MM-yyyy HH:mm:ss");
                }

                //If the cell value is start with the value in the TextBox
                if (row["CMND"].ToString().StartsWith(txtSearch.Text))
                {
                    addItemToList(row);
                }
                else if (row["MaPhong"].ToString().StartsWith(txtSearch.Text))
                {
                    addItemToList(row);
                }
                else if (dateTime1.StartsWith(txtSearch.Text)) //NGÀY ĐĂNG KÍ
                {
                    addItemToList(row);
                }
                else if (dateTime2.StartsWith(txtSearch.Text)) //NGÀY TRẢ
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
            var dateTime1 = "";
            var dateTime2 = "";

            if (!String.IsNullOrEmpty(row["NgayDangKi"].ToString()))
            {
                dateTime1 = Convert.ToDateTime(row["NgayDangKi"].ToString()).ToString("dd-MM-yyyy HH:mm:ss");
            }
            if (!String.IsNullOrEmpty(row["NgayTra"].ToString()))
            {
                dateTime2 = Convert.ToDateTime(row["NgayTra"].ToString()).ToString("dd-MM-yyyy HH:mm:ss");
            }
            string loaidk = "";
            if (row["LoaiDK"].Equals(0))
            {
                loaidk = "Đăng kí chính";
            }else
            {
                loaidk = "Đăng kí phụ";
            }


            ListViewItem lvi = new ListViewItem(row["idGD"].ToString());
            lvi.SubItems.Add(loaidk);
            lvi.SubItems.Add(row["CMND"].ToString());
            lvi.SubItems.Add(row["MaPhong"].ToString());

            lvi.SubItems.Add(dateTime1.ToString()); //NGÀY ĐĂNG KÍ
            lvi.SubItems.Add(dateTime2.ToString()); //NGÀY TRẢ

            lvi.SubItems.Add(row["SoNguoi"].ToString());
            lvi.SubItems.Add(row["TrangThai"].ToString());
            listView1.Items.Add(lvi);
        }

        private DataTable load_dataTable()
        {
            con = new SqlConnection(cs);
            con.Open();
            DataTable data = new DataTable("GIAO_DICH");
            adp = new SqlDataAdapter("SELECT * FROM GIAO_DICH", con);
            adp.Fill(data);
            adp.Dispose();
            con.Close();
            return data;
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
