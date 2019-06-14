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
    public partial class DiscountForm : UserControl
    {
        protected internal static DiscountForm _instance;
        private Boolean update_Discount = false;
        private int id = 0;
        private SqlDataReader rdr = null;
        private DataTable dtable;
        private SqlConnection con = null;
        private SqlDataAdapter adp;
        private DataSet ds;
        SqlCommand cmd = null;
        private DataTable dt = new DataTable();
        private string cs = "Data Source=DESKTOP-ASUKO;Initial Catalog=QLKS;Integrated Security=True";
        public DiscountForm()
        {
            InitializeComponent();
        }
        protected internal static DiscountForm Instance()
        {
            if (_instance == null)
            {
                _instance = new DiscountForm();
            }
            return _instance;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            display_Discount();
            btnSubmit.Text = "&Lưu";
            update_Discount = false;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDiscountType.Text.Trim().Length == 0 || txtDiscountRate.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Hãy điền các ô trống", "Input", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Lưu thông tin khuyến mãi ...?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (update_Discount == false)
                        {
                            con = new SqlConnection(cs);
                            con.Open();
                            SqlCommand save =
                            new SqlCommand("INSERT INTO KHUYEN_MAI(LoaiKhuyenMai,MucKhuyenMai,TinhTrang) VALUES(N'" + txtDiscountType.Text.Trim() + "','" + txtDiscountRate.Text.Trim() + "',N'Đang khuyến mãi');", con);
                            save.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            con = new SqlConnection(cs);
                            con.Open();
                            SqlCommand update = new SqlCommand("UPDATE KHUYEN_MAI SET LoaiKhuyenMai=N'" + txtDiscountType.Text.Trim() + "',MucKhuyenMai='" + (Double.Parse(txtDiscountRate.Text.Trim()) / 100) + "' WHERE ID ='" + id + "'", con);
                            update.ExecuteNonQuery();
                            con.Close();
                        }
                        MessageBox.Show("Lưu thành công", "Discount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDiscountRate.Text = "";
                        txtDiscountType.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DiscountForm_Load(object sender, EventArgs e)
        {
            display_Discount();
        }

        private void display_Discount()
        {
            con = new SqlConnection(cs);
            con.Open();
            dt = new DataTable("KHUYEN_MAI");
            adp = new SqlDataAdapter("SELECT * FROM KHUYEN_MAI", con);
            adp.Fill(dt);
            listView1.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                //ListViewItem lvi = new ListViewItem(row["MaKhachHang"].ToString());
                ListViewItem lvi = new ListViewItem(row["idKhuyenMai"].ToString());
                //for (int i = 1; i < dt.Columns.Count; i++)
                //{
                //    lvi.SubItems.Add(row[i].ToString());
                //}
                //lvi.Text = row["ID"].ToString();
                lvi.SubItems.Add(row["LoaiKhuyenMai"].ToString());
                lvi.SubItems.Add(row["MucKhuyenMai"].ToString());
                lvi.SubItems.Add(row["TinhTrang"].ToString());
                listView1.Items.Add(lvi);
            }
            adp.Dispose();
            con.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            display_Discount();
        }
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Cập nhật thông tin khuyến mãi ...?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                id = Int32.Parse(listView1.SelectedItems[0].Text.ToString());
                txtDiscountType.Text = listView1.SelectedItems[0].SubItems[1].Text.ToString();
                if (listView1.SelectedItems[0].SubItems[2].Text == "")
                {
                    txtDiscountRate.Text = "";
                }
                else
                {
                    txtDiscountRate.Text = (Double.Parse(listView1.SelectedItems[0].SubItems[2].Text) * 100).ToString();
                }
                update_Discount = true;
                btnSubmit.Text = "&Cập Nhật";
            }

        }
    }
}
