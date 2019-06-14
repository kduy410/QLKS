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
using System.Globalization;

namespace QLKS
{
    public partial class CheckOutForm : UserControl
    {
        protected internal static CheckOutForm _instance;
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
        private DataTable dtable;
        private SqlConnection con = null;
        private SqlDataAdapter adp;
        private DataSet ds;
        private SqlTransaction tran;
        SqlCommand cmd = null;
        private DataTable dt = new DataTable();
        private string cs = "Data Source=DESKTOP-ASUKO;Initial Catalog=QLKS;Integrated Security=True";
        private string datetime;
        private string format1 = "yyyy-MM-dd HH:mm:ss";
        private string format2 = "dd-MM-yyyy";
        private string format3 = "HH:mm:ss";
        private string numberformat = "N";
        public CheckOutForm()
        {
            InitializeComponent();
        }
        protected internal static CheckOutForm Instance()
        {
            if (_instance == null)
            {
                _instance = new CheckOutForm();
            }
            return _instance;
        }

        public void load_All()
        {
            using (con = new SqlConnection(cs))
            {
                con.Open();
                try
                {
                    cmd = new SqlCommand("SELECT A.HoDem,A.Ten,A.CMND,B.SoNgay,B.TraTruoc,B.NgayDangKi FROM " +
                    "KHACH_HANG A,CT_HOA_DON B,HOA_DON C " +
                    "WHERE C.CMND=A.CMND AND C.idHD=B.idHD AND C.MaPhong=@maphong ORDER BY C.NgayLap DESC", con);

                    cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                    rdr = cmd.ExecuteReader();
                    DateTime ngaydangki = DateTime.Now;
                    decimal tratruoc = 0;
                    if (rdr.Read())
                    {
                        lblLastName.Text = rdr["HoDem"].ToString();
                        lblFirstName.Text = rdr["Ten"].ToString();
                        lblCMND.Text = rdr["CMND"].ToString();
                        lblNumOfDays.Text = rdr["SoNgay"].ToString();

                        tratruoc = Convert.ToDecimal(rdr["TraTruoc"].ToString());
                        lblAdvance.Text = tratruoc.ToString(numberformat);
                        ngaydangki = Convert.ToDateTime(rdr["NgayDangKi"].ToString());
                    }
                    rdr.Close();
                    //LẤY ID HOÁ ĐƠN
                    cmd = new SqlCommand("SELECT idHD FROM HOA_DON WHERE CMND=@cmnd AND MaPhong=@maphong ORDER BY NgayLap DESC", con);
                    cmd.Parameters.AddWithValue("@cmnd", lblCMND.Text);
                    cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                    string idhd = "";
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        idhd = rdr["idHD"].ToString();
                    }

                    rdr.Close();

                    cmd = new SqlCommand("SELECT A.Gia,B.SoLuong FROM DICH_VU A,DS_DICH_VU B WHERE A.idDV=B.idDV AND B.idHD=@idhd", con);
                    cmd.Parameters.AddWithValue("@idhd", idhd);
                    rdr = cmd.ExecuteReader();
                    decimal totalDV = 0;
                    while (rdr.Read())
                    {
                        totalDV += (decimal)Val(rdr["Gia"].ToString()) * (decimal)Val(rdr["SoLuong"].ToString());
                    }
                    rdr.Close();

                    lblServicePrice.Text = totalDV.ToString(numberformat);
                    decimal roomPrice = calculate_Roomprice(ngaydangki, DateTime.Now, lblRoomType.Text, 80000, 20000);
                    lblRoomPrice.Text = roomPrice.ToString(numberformat);
                    //lblRoomPrice.Text = Math.Round(roomPrice, 1, MidpointRounding.AwayFromZero).ToString(numberformat);
                    lblTotal.Text = (totalDV + roomPrice - tratruoc).ToString(numberformat);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show(ex.StackTrace);
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }

        }
        public decimal calculate_Roomprice(DateTime ndk, DateTime nt, string roomtype, decimal fhprice, decimal ahprice)
        {
            DateTime x = Convert.ToDateTime(ndk);
            DateTime y = Convert.ToDateTime(nt);
            TimeSpan time;
            int check = DateTime.Compare(x, y);
            if (check < 0)
            {
                time = y.Subtract(x);
            }
            else time = x.Subtract(y);
            // time = y.Subtract(x);
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
            if ( time.TotalHours <= 1)
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
            return total;
        }

        public void load_Room()
        {
            con = new SqlConnection(cs);
            con.Open();
            cmd = new SqlCommand("SELECT MaPhong FROM PHONG", con);
            rdr = cmd.ExecuteReader();
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            while (rdr.Read())
            {
                MyCollection.Add(rdr.GetString(0));
            }
            //txtRoom.AutoCompleteCustomSource = MyCollection;
            con.Close();
        }
        public void load_RoomType()
        {
            con = new SqlConnection(cs);
            con.Open();
            cmd = new SqlCommand("SELECT LoaiPhong FROM PHONG", con);
            rdr = cmd.ExecuteReader();
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            while (rdr.Read())
            {
                MyCollection.Add(rdr.GetString(0));
            }
            //txtRoomType.AutoCompleteCustomSource = MyCollection;
            con.Close();
        }
        private void btnBack_Click_1(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void CheckOutForm_Load(object sender, EventArgs e)
        {
            lblDiscount.Visible = false;
            lblGuestID.Visible = false;
            lblOccupancy.Visible = true;
            lblCurrentPeopleCount.Visible = true;
            lblRoom.Visible = true;
            lblGD.Visible = true;

            getCheckinDay();
            getCheckoutDay();

            load_All();
            //load_Room();
            //load_RoomType();
        }
        public void getCheckinDay()
        {
            string format2 = "dd-MM-yyyy";
            con = new SqlConnection(cs);
            con.Open();
            string query = "SELECT * FROM GIAO_DICH WHERE MaPhong = @find ORDER BY idGD ASC";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.Add(new SqlParameter("@find", SqlDbType.NVarChar, 10, "MaPhong")).Value = lblRoom.Text.ToString();
            rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                lblCheckinday.Text = Convert.ToDateTime(rdr["NgayDangKi"]).ToString("dd-MM-yyyy HH:mm:ss");
            }
            else
            {
                lblCheckinday.Text = "0";
            }

            rdr.Close();
            con.Close();

        }

        private void btnSearchGuest_Click(object sender, EventArgs e)
        {
            SearchGuestForm sgf = new SearchGuestForm();
            sgf.ShowDialog();
        }

        private void btnSearchRoom_Click(object sender, EventArgs e)
        {
            SearchRoomForm srf = new SearchRoomForm();
            srf.ShowDialog();
        }

        public void getTransID()
        {
            con = new SqlConnection(cs);
            con.Open();
            string query = "SELECT idGD FROM GIAO_DICH WHERE MaPhong = @find AND TrangThai=@trangthai ORDER BY idGD ASC";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.Add(new SqlParameter("@find", SqlDbType.NVarChar, 10, "MaPhong")).Value = lblRoom.Text.ToString();
            cmd.Parameters.AddWithValue("@trangthai", "Active");
            rdr = cmd.ExecuteReader();
            StringBuilder sb = new StringBuilder();
            while (rdr.Read())
            {
                sb.Append(rdr["idGD"].ToString() + ",");
            }
            sb.Append(".");
            lblGD.Text = sb.ToString();
            rdr.Close();
            con.Close();
        }
        private void getCheckoutDay()
        {
            string format1 = "yyyy-MM-dd HH:mm:ss";
            string format2 = "dd-MM-yyyy";

            DateTime date = DateTime.Now;
            lblCurrentDay.Text = date.ToString(format2);
            datetime = Convert.ToDateTime(date).ToString(format1);
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (txtCASH.Text.Length == 0)
            {
                MessageBox.Show("Xin nhập số tiền", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DialogResult dr = MessageBox.Show("Bạn muốn trả phòng", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    Transaction();
                }
                else
                {
                    return;
                }
            }
        }

        private void Transaction()
        {
            using (con = new SqlConnection(cs))
            {
                con.Open();
                using (tran = con.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        int flag = 0;

                        //KIỂM TRA TÌNH TRẠNG KHÁCH HÀNG
                        cmd = new SqlCommand("SELECT CMND,TrangThai FROM KHACH_HANG WHERE CMND = @cmnd", con, tran);
                        cmd.Parameters.AddWithValue("@cmnd", lblCMND.Text);
                        rdr = cmd.ExecuteReader();
                        string stateKH = "";
                        string cmnd = "";
                        while (rdr.Read())
                        {
                            stateKH = rdr["TrangThai"].ToString();
                            cmnd = rdr["CMND"].ToString();
                        }
                        rdr.Close();

                        if (stateKH.Equals("") || stateKH.Equals("Checkout") || stateKH.Equals("Reserve"))
                        {
                            flag = 1;
                        }
                        //KIỂM TRA TÌNH TRẠNG PHÒNG
                        cmd = new SqlCommand("SELECT TinhTrang FROM PHONG WHERE MaPhong = @maphong", con, tran);
                        cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                        rdr = cmd.ExecuteReader();
                        string stateRoom = "";
                        while (rdr.Read())
                        {
                            stateRoom = rdr["TinhTrang"].ToString();
                        }
                        rdr.Close();
                        if (stateRoom.Equals("") || stateRoom.Equals("Đặt trước") || stateRoom.Equals("Trống"))
                        {
                            flag = 1;
                        }
                        //KIỂM TRA GIAO_DỊCH
                        cmd = new SqlCommand("  ", con, tran);
                        cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                        cmd.Parameters.AddWithValue("@trangthai", "Active");
                        rdr = cmd.ExecuteReader();
                        string stateGD = "";
                        List<string> listCMND = new List<string>();
                        while (rdr.Read())
                        {
                            stateGD = rdr["TrangThai"].ToString();
                            listCMND.Add(rdr["CMND"].ToString());
                        }
                        rdr.Close();
                        if (stateGD.Equals(""))
                        {
                            flag = 1;
                        }
                        //KIỂM TRA HOÁ ĐƠN
                        cmd = new SqlCommand("SELECT idHD,NgayLap FROM HOA_DON WHERE CMND = @cmnd AND MaPhong=@maphong ORDER BY NgayLap DESC", con, tran);
                        cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                        cmd.Parameters.AddWithValue("@cmnd", cmnd);
                        rdr = cmd.ExecuteReader();
                        string idHD = "";
                        DateTime ngaydk = DateTime.Now;

                        if (rdr.Read())
                        {
                            idHD = rdr["idHD"].ToString();
                            ngaydk = Convert.ToDateTime(rdr["NgayLap"].ToString());
                        }
                        
                        rdr.Close();
                        tran.Save("updateGuest");

                        foreach (string item in listCMND)
                        {
                            cmd = new SqlCommand("UPDATE KHACH_HANG SET TrangThai=@trangthai WHERE CMND=@cmnd", con, tran);
                            cmd.Parameters.AddWithValue("@trangthai", "Checkout");
                            cmd.Parameters.AddWithValue("@cmnd", item);
                            cmd.ExecuteNonQuery();
                        }
                        //KIỂM TRA DS_DICH VU
                        cmd = new SqlCommand("SELECT A.Gia,B.SoLuong,B.idDSDV FROM DICH_VU A,DS_DICH_VU B WHERE A.idDV=B.idDV AND idHD=@idhd", con, tran);
                        cmd.Parameters.AddWithValue("@idhd", idHD);
                        rdr = cmd.ExecuteReader();
                        decimal totalDV = 0;
                        List<string> listDSDV = new List<string>();
                        while (rdr.Read())
                        {
                            totalDV += (decimal)Val(rdr["Gia"].ToString()) * (decimal)Val(rdr["SoLuong"].ToString());
                            listDSDV.Add(rdr["idDSDV"].ToString());
                        }
                        rdr.Close();
                        //TRẠNG THÁI
                        if (flag == 1)
                        {
                            MessageBox.Show("Đã checkout!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            return;
                        }
                        //LẤY TIỀN TRẢ TRƯỚC TỪ CT_HOA_DON
                        decimal tratruoc = 0;
                        cmd = new SqlCommand("SELECT TraTruoc FROM CT_HOA_DON WHERE idHD=@idhd", con, tran);
                        cmd.Parameters.AddWithValue("@idhd", idHD);
                        rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            tratruoc = decimal.Parse(rdr["TraTruoc"].ToString());
                        }
                        rdr.Close();
                       
                        //TÍNH TIỀN

                        decimal total = ((calculate_Roomprice(ngaydk, DateTime.Now, lblRoomType.Text, 80000, 20000) + totalDV) - tratruoc);
                        decimal cash = Convert.ToDecimal(txtCASH.Text);
                        decimal changes = cash - total;

                        if (total < 0)
                        {
                            txtChanges.Text = total.ToString(numberformat);
                        }

                        if (changes < 0)
                        {
                            MessageBox.Show("Không đủ tiền!!!,\n" + total.ToString(numberformat), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            txtChanges.Text = changes.ToString(numberformat);
                        }
                        //TÍNH SỐ NGÀY THỰC TẾ
                        TimeSpan ts = DateTime.Now.Subtract(ngaydk);
                        decimal songay = Convert.ToDecimal(ts.Days);
                        //CẬP NHẬT DANH SÁCH ĐẶT TRƯỚC
                        cmd = new SqlCommand("UPDATE DAT_TRUOC SET TrangThai=@trangthai WHERE MaPhong=@maphong AND CMND=@cmnd", con, tran);
                        cmd.Parameters.AddWithValue("@trangthai", "Checkout");
                        cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                        cmd.Parameters.AddWithValue("@cmnd", lblCMND.Text);
                        tran.Save("updateReserve");
                        cmd.ExecuteNonQuery();
                        //CẬP NHẬT PHÒNG
                        cmd = new SqlCommand("UPDATE PHONG SET TinhTrang=@trangthai,SoNguoiHienCo=@snhc WHERE MaPhong=@maphong", con, tran);
                        cmd.Parameters.AddWithValue("@trangthai", "Trống");
                        cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                        cmd.Parameters.AddWithValue("@snhc", 0);
                        tran.Save("updateRoom");
                        cmd.ExecuteNonQuery();
                        //CẬP NHẬT GIAO DỊCH
                        foreach (string item in listCMND)
                        {
                            cmd = new SqlCommand("UPDATE GIAO_DICH SET TrangThai=@afterState,NgayTra=@ngaytra WHERE CMND=@cmnd AND MaPhong=@maphong AND TrangThai=@firstState", con, tran);
                            cmd.Parameters.AddWithValue("@afterState", "");
                            cmd.Parameters.AddWithValue("@firstState", "Active");
                            cmd.Parameters.AddWithValue("@cmnd", item);
                            cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                            cmd.Parameters.AddWithValue("@ngaytra", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                        tran.Save("updateGD");


                        //CẬP NHẬT HOÁ ĐƠN
                        //cmd = new SqlCommand("UPDATE HOA_DON SET NgayTra=@ngaytra WHERE MaPhong=@maphong AND CMND=@cmnd AND idHD=@idHD", con, tran);
                        //cmd.Parameters.AddWithValue("@afterState", "1");
                        //cmd.Parameters.AddWithValue("@total", total);
                        //cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                        //cmd.Parameters.AddWithValue("@cmnd", cmnd);
                        //cmd.Parameters.AddWithValue("@idhd", idHD);
                        //cmd.Parameters.AddWithValue("@sn", songay);
                        //cmd.Parameters.AddWithValue("@ngaytra", DateTime.Now);
                        //tran.Save("updateHD");
                        //cmd.ExecuteNonQuery();

                        //CẬP NHẬT CT_HOÁ ĐƠN
                        cmd = new SqlCommand("UPDATE CT_HOA_DON SET NgayTra=@ngaytra,TongTien=@total,TienDichVu=@tiendv,SoNgay=@sn WHERE idHD=@idHD", con, tran);
                        cmd.Parameters.AddWithValue("@tiendv", totalDV);
                        cmd.Parameters.AddWithValue("@total", total);
                        cmd.Parameters.AddWithValue("@sn", songay);
                        cmd.Parameters.AddWithValue("@idhd", idHD);
                        cmd.Parameters.AddWithValue("@ngaytra", DateTime.Now);

                        tran.Save("updateCTHD");
                        cmd.ExecuteNonQuery();

                        //CẬP NHẬT DANH SÁCH DỊCH VỤ
                        tran.Save("deleteDSDV");
                        foreach (string item in listDSDV)
                        {
                            cmd = new SqlCommand("DELETE DS_DICH_VU WHERE idDSDV=@idds", con, tran);
                            cmd.Parameters.AddWithValue("@idds", item);
                            cmd.ExecuteNonQuery();
                        }
                        listCMND.Clear();
                        listDSDV.Clear();
                        tran.Commit();
                        MessageBox.Show("Trả phòng thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Commit exeption type: " + ex.GetType());
                        MessageBox.Show("Stack trade: " + ex.StackTrace);
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

                        ReservationListForm.Instance().load_ReservationList();
                        GuestListForm.Instance().load_GuestList();
                        RoomListForm.Instance().load_RoomList();

                    }
                }
            }
        }   

        private void txtNumOfDay_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtAdvancePay_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtSubTotal_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtTotal_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtGuestName_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void txtCMND_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
      (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtCASH_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
      (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtChanges_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
      (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
