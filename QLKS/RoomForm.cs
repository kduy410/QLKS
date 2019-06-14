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
    public partial class RoomForm : UserControl
    {
        protected internal static RoomForm _instance;
        private SqlDataReader rdr = null;
        private DataTable dtable;
        private SqlConnection con = null;
        private SqlDataAdapter adp;
        private DataSet ds;
        SqlCommand cmd = null;
        private DataTable dt = new DataTable();
        private string cs = "Data Source=DESKTOP-ASUKO;Initial Catalog=QLKS;Integrated Security=True";
        public RoomForm()
        {
            InitializeComponent();
        }
        protected internal static RoomForm Instance()
        {
            if (_instance == null)
            {
                _instance = new RoomForm();
            }
            return _instance;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtRoomNo.Text.Trim().Length) == 0)
                {
                    MessageBox.Show("Xin nhập số phòng ", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRoomNo.Focus();
                    return;
                }
                if ((cbRoomType.Text.Trim().Length) == 0)
                {
                    MessageBox.Show("Xin chọn loại phòng", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbRoomType.Focus();
                    return;
                }
                if ((cbOccupancy.Text.Trim().Length) == 0)
                {
                    MessageBox.Show("Xin chọn số người ", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbOccupancy.Focus();
                    return;
                }
                con = new SqlConnection(cs);
                con.Open();
                string ct = "SELECT MaPhong FROM PHONG WHERE MaPhong=@find";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 10, "MaPhong")).Value = txtRoomNo.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    MessageBox.Show("Đã có phòng này ", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRoomNo.Text = "";
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    con = new SqlConnection(cs);
                    con.Open();
                    string cb = "INSERT INTO PHONG(MaPhong,LoaiPhong,TinhTrang,GioiHan,SoNguoiHienCo) VALUES(@d1,@d2,@d3,@d4,@d5)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NVarChar, 10, "MaPhong")).Value = txtRoomNo.Text;
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NVarChar, 10, "LoaiPhong")).Value = cbRoomType.Text;
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NVarChar, 50, "TinhTrang")).Value = "Trống";
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.Int, 10, "GioiHan")).Value = cbOccupancy.Text;
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.Int, 10, "SoNguoiHienCo")).Value = 0;
                    cmd.ExecuteReader();
                    MessageBox.Show("Tạo phòng thành công", "Room", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRoomNo.Text = "";
                    cbRoomType.Text = "";
                    cbOccupancy.Text = "";
                    con.Close();
                }
                RoomListForm.Instance().load_RoomList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RoomForm_Load(object sender, EventArgs e)
        {

        }
    }
}
