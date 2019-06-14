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
    public partial class ServiceForm : UserControl
    {
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
        private SqlDataReader rdr = null;
        private Random random = new Random();
        private DataTable dtable;
        private SqlTransaction tran;
        private SqlConnection con = null;
        private SqlDataAdapter adp;
        private DataSet ds;
        private SqlCommand cmd = null;
        private DataTable dt = new DataTable();
        private string cs = "Data Source=DESKTOP-ASUKO;Initial Catalog=QLKS;Integrated Security=True";



        protected internal static ServiceForm _instance;
        protected internal static ServiceForm Instance()
        {
            if (_instance == null)
            {
                _instance = new ServiceForm();
            }
            return _instance;
        }
        public ServiceForm()
        {
            InitializeComponent();
        }

        private void ServiceForm_Load(object sender, EventArgs e)
        {
            if (!pnList.Controls.Contains(LsFoodForm.Instance()))
            {
                pnList.Controls.Add(LsFoodForm._instance);
                LsFoodForm._instance.Dock = DockStyle.Fill;
                LsFoodForm._instance.BringToFront();
                LsFoodForm._instance.Visible = true;
                LsFoodForm._instance.loadListType(0);
            }
            else
            {
                LsFoodForm._instance.BringToFront();
                LsFoodForm._instance.Visible = true;
            }
        }

        private void btnFood_Click(object sender, EventArgs e)
        {
            if (!pnList.Controls.Contains(LsFoodForm.Instance()))
            {
                pnList.Controls.Add(LsFoodForm._instance);
                LsFoodForm._instance.Dock = DockStyle.Fill;
                LsFoodForm._instance.BringToFront();
                LsFoodForm._instance.Visible = true;
            }
            else
            {
                LsFoodForm._instance.BringToFront();
                LsFoodForm._instance.Visible = true;
            }
        }

        private void btnDrink_Click(object sender, EventArgs e)
        {
            if (!pnList.Controls.Contains(LsDrinkForm.Instance()))
            {
                pnList.Controls.Add(LsDrinkForm._instance);
                LsDrinkForm._instance.Dock = DockStyle.Fill;
                LsDrinkForm._instance.BringToFront();
                LsDrinkForm._instance.Visible = true;
            }
            else
            {
                LsDrinkForm._instance.BringToFront();
                LsDrinkForm._instance.Visible = true;
            }
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            if (!pnList.Controls.Contains(LsItemForm.Instance()))
            {
                pnList.Controls.Add(LsItemForm._instance);
                LsItemForm._instance.Dock = DockStyle.Fill;
                LsItemForm._instance.BringToFront();
                LsItemForm._instance.Visible = true;
            }
            else
            {
                LsItemForm._instance.BringToFront();
                LsItemForm._instance.Visible = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ServiceCreateBoxForm scbf = ServiceCreateBoxForm.Instance();
            scbf.ShowDialog();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            loadDV();
        }

        public void loadDV()
        {

            LsFoodForm.Instance().loadListType(0);

            LsDrinkForm.Instance().loadListType(1);

            LsItemForm.Instance().loadListType(2);

            LsOrderForm.Instance().loadDSDV();

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkEmptyList())
                {
                    DialogResult dr = MessageBox.Show("Xác nhận dịch vụ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        listToSQL();
                        MessageBox.Show("Cập nhật dịch vụ thành công.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDV();
                        StateForm.Instance().load_all();
                    }
                    else return;
                }
                else
                {
                    MessageBox.Show("Order của bạn đang trống, Xin hãy chọn dịch vụ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private bool checkEmptyList()
        {
            LsOrderForm lsOrderForm = LsOrderForm.Instance();
            if (lsOrderForm.listViewSV.Items.Count > 0)
            {
                return true;
            }
            else return false;
        }

        private void listToSQL()
        {
            try
            {
                LsOrderForm lsOrderForm = LsOrderForm.Instance();
                int id, sl;
                foreach (ListViewItem item in lsOrderForm.listViewSV.Items)
                {
                    id = (int)Val(item.Text);
                    sl = (int)Val(item.SubItems[3].Text);
                    string idDSDV = checkDV(id);
                    if (idDSDV.Equals(""))
                    {
                        createDV(id, sl);
                    }
                    else
                    {
                        updateDV(idDSDV, id, sl);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void updateDV(string iddsdv, int id, int sl)
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    using (tran = con.BeginTransaction(IsolationLevel.Serializable))
                    {
                        try
                        {
                            //LẤY CMND KHÁCH HÀNG
                            cmd = new SqlCommand("SELECT CMND FROM GIAO_DICH WHERE MaPhong=@maphong AND TrangThai=@trangthai AND LoaiDK=@loaidk", con, tran);
                            cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                            cmd.Parameters.AddWithValue("@trangthai", "Active");
                            cmd.Parameters.AddWithValue("@loaidk", 0);
                            rdr = cmd.ExecuteReader();
                            string cmnd = "";
                            if (rdr.Read())
                            {
                                cmnd = rdr["CMND"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy CMND khách hàng");
                                return;
                            }
                            rdr.Close();
                            //LẤY ID HOÁ ĐƠN
                            cmd = new SqlCommand("SELECT idHD FROM HOA_DON WHERE CMND=@cmnd AND MaPhong=@maphong ORDER BY NgayLap DESC", con, tran);
                            cmd.Parameters.AddWithValue("@cmnd", cmnd);
                            cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                            rdr = cmd.ExecuteReader();
                            string idhd = "";
                            if (rdr.Read())
                            {
                                idhd = rdr["idHD"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy hoá đơn!!!");
                                return;
                            }
                            rdr.Close();

                            cmd = new SqlCommand("UPDATE DS_DICH_VU SET SoLuong=@sl WHERE  idDSDV=@iddsdv AND idHD=@idhd AND idDV=@iddv", con, tran);
                            cmd.Parameters.AddWithValue("@idhd", idhd);
                            cmd.Parameters.AddWithValue("@iddsdv", iddsdv);
                            cmd.Parameters.AddWithValue("@sl", sl);
                            cmd.Parameters.AddWithValue("@iddv", id);
                            tran.Save("updateDV");
                            cmd.ExecuteNonQuery();
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Commit exeption type: " + ex.GetType());
                            MessageBox.Show("Message :" + ex.Message);
                            try
                            {
                                if (tran != null)
                                {
                                    tran.Rollback();
                                }
                            }
                            catch (Exception ex2)
                            {
                                MessageBox.Show("Rollback exeption type :" + ex2.GetType());
                                MessageBox.Show("Message :" + ex2.Message);
                            }
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void createDV(int id, int sl)
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    using (tran = con.BeginTransaction(IsolationLevel.Serializable))
                    {
                        try
                        {
                            //LẤY CMND KHÁCH HÀNG
                            cmd = new SqlCommand("SELECT CMND FROM GIAO_DICH WHERE MaPhong=@maphong AND TrangThai=@trangthai AND LoaiDK=@loaidk", con, tran);
                            cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                            cmd.Parameters.AddWithValue("@trangthai", "Active");
                            cmd.Parameters.AddWithValue("@loaidk", 0);
                            rdr = cmd.ExecuteReader();
                            string cmnd = "";
                            if (rdr.Read())
                            {
                                cmnd = rdr["CMND"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy CMND khách hàng");
                                return;
                            }
                            rdr.Close();

                            //LẤY ID HOÁ ĐƠN
                            cmd = new SqlCommand("SELECT idHD FROM HOA_DON WHERE CMND=@cmnd AND MaPhong=@maphong ORDER BY NgayLap DESC", con, tran);
                            cmd.Parameters.AddWithValue("@cmnd", cmnd);
                            cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                            rdr = cmd.ExecuteReader();
                            string idhd = "";
                            if (rdr.Read())
                            {
                                idhd = rdr["idHD"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy hoá đơn!!!");
                                return;
                            }
                            rdr.Close();

                            cmd = new SqlCommand("INSERT INTO DS_DICH_VU(idDV,idHD,SoLuong) VALUES(@d1,@d2,@d3)", con, tran);
                            cmd.Parameters.AddWithValue("@d1", id);
                            cmd.Parameters.AddWithValue("@d2", idhd);
                            cmd.Parameters.AddWithValue("@d3", sl);
                            tran.Save("insertDV");
                            cmd.ExecuteNonQuery();
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Commit exeption type: " + ex.GetType());
                            MessageBox.Show("Message :" + ex.Message);
                            try
                            {
                                if (tran != null)
                                {
                                    tran.Rollback();
                                }
                            }
                            catch (Exception ex2)
                            {
                                MessageBox.Show("Rollback exeption type :" + ex2.GetType());
                                MessageBox.Show("Message :" + ex2.Message);
                            }
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private string checkDV(int id)
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT CMND FROM GIAO_DICH WHERE MaPhong=@maphong AND TrangThai=@tt AND LoaiDK=@loaidk", con);
                    cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                    cmd.Parameters.AddWithValue("@tt", "Active");
                    cmd.Parameters.AddWithValue("@loaidk", 0);
                    rdr = cmd.ExecuteReader();
                    string cmnd = "";
                    while (rdr.Read())
                    {
                        cmnd = rdr["CMND"].ToString();
                    }
                    rdr.Close();
                    cmd = new SqlCommand("SELECT idHD FROM HOA_DON WHERE CMND=@cmnd AND MaPhong=@maphong ORDER BY NgayLap DESC", con);
                    cmd.Parameters.AddWithValue("@cmnd", cmnd);
                    cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                    rdr = cmd.ExecuteReader();
                    string idHD = "";
                    if (rdr.Read())
                    {
                        idHD = rdr["idHD"].ToString();
                    }
                    rdr.Close();
                    cmd = new SqlCommand("SELECT idDSDV FROM DS_DICH_VU WHERE idHD=@idhd AND idDV=@idDV", con);
                    cmd.Parameters.AddWithValue("@idhd", idHD);
                    cmd.Parameters.AddWithValue("@idDV", id);
                    rdr = cmd.ExecuteReader();
                    string idDSDV = "";
                    if (rdr.Read())
                    {
                        idDSDV = rdr["idDSDV"].ToString();
                    }
                    rdr.Close();
                    return idDSDV;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (!pnList.Controls.Contains(LsOrderForm.Instance()))
            {
                pnList.Controls.Add(LsOrderForm._instance);
                LsOrderForm._instance.Dock = DockStyle.Fill;
                LsOrderForm._instance.BringToFront();
                LsOrderForm._instance.Visible = true;
            }
            else
            {
                LsOrderForm._instance.BringToFront();
                LsOrderForm._instance.Visible = true;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
