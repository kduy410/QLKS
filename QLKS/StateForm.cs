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
    public partial class StateForm : UserControl
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
        private ListViewColumnSorter lvwColumnSorter;

        private int numDays;
        private SqlDataReader rdr = null;
        private DataTable dtable;
        private SqlConnection con = null;
        private SqlDataAdapter adp;
        private DataSet ds;
        SqlCommand cmd = null;
        private DataTable dt = new DataTable();
        private string cs = "Data Source=DESKTOP-ASUKO;Initial Catalog=QLKS;Integrated Security=True";
        private SqlTransaction tran;
        private string datetime;
        private string format_dateHour = "yyyy-MM-dd HH:mm:ss";
        private string format_date = "dd-MM-yyyy";
        private string numberformat = "N";
        protected internal static StateForm _instance;
        public static StateForm Instance()
        {
            if (_instance == null)
            {
                _instance = new StateForm();
            }
            return _instance;
        }
        public StateForm()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewColumnSorter();
            this.listViewGuest.ListViewItemSorter = lvwColumnSorter;
            this.listViewSV.ListViewItemSorter = lvwColumnSorter;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void StateForm_Load(object sender, EventArgs e)
        {
            load_all();
            load_listGuest();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            load_all();
            load_listGuest();
        }

        public void load_listGuest()
        {
            using (con = new SqlConnection(cs))
            {
                con.Open();
                try
                {
                    cmd = new SqlCommand("SELECT A.HoDem,A.Ten,A.CMND,B.LoaiDK,B.NgayDangKi FROM KHACH_HANG A,GIAO_DICH B WHERE MaPhong=@maphong AND A.CMND=B.CMND AND B.TrangThai=@state", con);
                    cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                    cmd.Parameters.AddWithValue("@state", "Active");
                    dt = new DataTable();
                    adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    adp.Dispose();
                    listViewGuest.Items.Clear();
                    if (dt.Rows.Count > 0)
                    {
                        int i = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            ListViewItem lvi = new ListViewItem(i.ToString());
                            string loaidk = "";
                            if (row["LoaiDK"].ToString().Equals("0"))
                            {
                                loaidk = "Đăng kí chính";
                            }
                            else
                            {
                                loaidk = "Đăng kí phụ";
                            }
                            lvi.SubItems.Add(loaidk);
                            lvi.SubItems.Add(row["CMND"].ToString());
                            lvi.SubItems.Add(row["HoDem"].ToString());
                            lvi.SubItems.Add(row["Ten"].ToString());
                            lvi.SubItems.Add(row["NgayDangKi"].ToString());
                            listViewGuest.Items.Add(lvi);
                        }
                    }
                    else return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    throw;
                }
                con.Close();
            }
        }

        public decimal calculate_Roomprice(DateTime ndk, DateTime nt, string roomtype, decimal fhprice, decimal ahprice)
        {
            DateTime x = Convert.ToDateTime(ndk);
            DateTime y = Convert.ToDateTime(nt);
            TimeSpan time;
            //int check = DateTime.Compare(x, y);
            //if (check < 0)
            //{
            //    time = y.Subtract(x);
            //}
            //else time = x.Subtract(y);
            time = y.Subtract(x);
            decimal total = 0;
            decimal type = -1;
            if (roomtype.Equals("Đơn"))
            {
                type = 0;
            }
            else
            {
                type = 30000;
            }
            if (time.TotalHours <= 1)
            {
                total = fhprice;
            }
            else if (time.TotalHours > 1)
            {
                //total = ((Convert.ToDecimal(time.TotalHours.ToString()) - 1) * (fhprice + ahprice)) + fhprice;
                //total = ((Convert.ToDecimal(time.TotalHours.ToString())) * (fhprice + ahprice)) - ahprice;
                //total = ((Convert.ToDecimal(Math.Round(time.TotalHours, 1, MidpointRounding.AwayFromZero)) - 1) * (fhprice + ahprice)) + fhprice;
                total = ((Convert.ToDecimal(Math.Round(time.TotalHours, 1, MidpointRounding.AwayFromZero))) * (fhprice + ahprice)) - ahprice;
            }
            total = total + type;
            //MessageBox.Show(Convert.ToDecimal(time.TotalHours.ToString()).ToString());
            return total;
        }
        public void load_all()
        {
            using (con = new SqlConnection(cs))
            {
                con.Open();
                //LẤY SỐ NGƯỜI HIỆN CÓ
                cmd = new SqlCommand("SELECT SoNguoiHienCo FROM PHONG WHERE MaPhong=@maphong", con);
                cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    lblCurrentPeople.Text = rdr["SoNguoiHienCo"].ToString();
                }
                else
                {
                    lblCurrentPeople.Text = "0";
                }
                rdr.Close();
                //LẤY NGAY ĐĂNG KÍ SỐ NGƯỜI CMND TỪ GIAO DỊCH
                cmd = new SqlCommand("SELECT CMND,NgayDangKi,SoNguoi FROM GIAO_DICH WHERE MaPhong=@maphong AND TrangThai=@state", con);
                cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                cmd.Parameters.AddWithValue("@state", "Active");
                string cmnd = "";
                rdr = cmd.ExecuteReader();

                DateTime ngaydangki = DateTime.Now;
                decimal roomPrice = -1;

                if (rdr.Read())
                {
                    ngaydangki = Convert.ToDateTime(rdr["NgayDangKi"].ToString());
                    roomPrice = calculate_Roomprice(ngaydangki, DateTime.Now, lblRoomType.Text, 80000, 20000);
                    lblReg.Text = rdr["SoNguoi"].ToString();
                    cmnd = rdr["CMND"].ToString();
                }
                else
                {
                    roomPrice = 0;
                    lblReg.Text = "0";
                }
                rdr.Close();
                lblRoomPrice.Text = roomPrice.ToString(numberformat);

                //LẤY ID HOÁ ĐƠN
                cmd = new SqlCommand("SELECT idHD FROM HOA_DON WHERE CMND =@cmnd AND MaPhong=@maphong ORDER BY NgayLap DESC", con);
                cmd.Parameters.AddWithValue("@cmnd", cmnd);
                cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                rdr = cmd.ExecuteReader();
                string idHD = "";
                if (rdr.Read())
                {
                    idHD = rdr["idHD"].ToString();
                }
                rdr.Close();
                //LẤY TIỀN TRẢ TRƯỚC
                cmd = new SqlCommand("SELECT TraTruoc FROM CT_HOA_DON WHERE idHD=@idhd", con);
                cmd.Parameters.AddWithValue("@idhd", idHD);
                rdr = cmd.ExecuteReader();
                decimal tratruoc = 0;
                if (rdr.Read())
                {
                    tratruoc = Convert.ToDecimal(rdr["TraTruoc"].ToString());
                    lblAdvance.Text = tratruoc.ToString(numberformat);
                }
                else
                {
                    lblAdvance.Text = "0";
                }
                rdr.Close();

                //LÁY DS DỊCH VỤ TÍNH TIỀN DỊCH VỤ
                cmd = new SqlCommand("SELECT A.idDV,A.SoLuong,B.LoaiDV,B.TenDV,B.Gia FROM DICH_VU B,DS_DICH_VU A WHERE A.idHD=@idhd AND A.idDV=B.idDV", con);
                cmd.Parameters.AddWithValue("@idhd", idHD);
                dt = new DataTable();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                adp.Dispose();
                listViewSV.Items.Clear();
                decimal totalDV = 0;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ListViewItem lvi = new ListViewItem(row["idDV"].ToString());
                        string loaidv = "";
                        if (row["LoaiDV"].ToString().Equals("0"))
                        {
                            loaidv = "ĐỒ ĂN";
                        }
                        else if (row["LoaiDV"].ToString().Equals("1"))
                        {
                            loaidv = "THỨC UỐNG";
                        }
                        else
                        {
                            loaidv = "ĐỒ DÙNG";
                        }
                        lvi.SubItems.Add(loaidv);
                        lvi.SubItems.Add(row["TenDV"].ToString());
                        lvi.SubItems.Add(row["SoLuong"].ToString());
                        lvi.SubItems.Add(row["Gia"].ToString());
                        listViewSV.Items.Add(lvi);
                        totalDV += (decimal)Val(row["Gia"].ToString()) * (decimal)Val(row["SoLuong"].ToString());
                    }
                }
                rdr.Close();
                lblSVprice.Text = totalDV.ToString(numberformat);
                con.Close();
            }
        }

        private void listViewSV_ColumnClick(object sender, ColumnClickEventArgs e)
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
            this.listViewSV.Sort();
        }

        private void listViewGuest_ColumnClick(object sender, ColumnClickEventArgs e)
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
            this.listViewGuest.Sort();
        }
    }
}
