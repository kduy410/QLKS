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
using System.Threading;
using System.Globalization;

namespace QLKS
{

    public partial class CheckinForm : UserControl
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
        protected internal static CheckinForm _instance;
        private decimal advance;
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
        public CheckinForm()
        {
            InitializeComponent();
        }
        protected internal static CheckinForm Instance()
        {
            if (_instance == null)
            {
                _instance = new CheckinForm();
            }
            return _instance;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnSearchGuest_Click(object sender, EventArgs e)
        {
            SearchGuestForm searchGuestForm = new SearchGuestForm();
            searchGuestForm.ShowDialog();
        }

        private void CheckinForm_Load(object sender, EventArgs e)
        {

            lblGuestID.Visible = false;
            lblOccupancy.Visible = true;
            lblCurrentPeopleCount.Visible = true;

            DateTime date = DateTime.Now;
            lblCheckinDate.Text = date.ToString(format_date);
            datetime = Convert.ToDateTime(date).ToString(format_dateHour);

            trans_ID();
            load_all();
        }

        private void trans_ID()
        {
            con = new SqlConnection(cs);
            con.Open();
            SqlCommand sql = new SqlCommand("SELECT * FROM GIAO_DICH ORDER BY idGD DESC", con);
            rdr = sql.ExecuteReader();
            if (!rdr.Read())
            {
                lblTransID.Text = "1";
            }
            else
            {
                lblTransID.Text = rdr["idGD"].ToString();
            }
            con.Close();
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            int sndangki = (int)Val(txtPeopleCount.Text.Trim().ToString());
            int sngioihan = (int)Val(lblOccupancy.Text);
            int snhientai = (int)Val(lblCurrentPeopleCount.Text);

            if (txtAdvance.Text.Length > 0)
            {
                advance = Math.Round(Decimal.Parse(txtAdvance.Text), 1, MidpointRounding.AwayFromZero);
            }
            else
            {
                advance = 0;
            }
            if (txtNumOfDay.Text.Length > 0)
            {
                numDays = (int)Val(txtNumOfDay.Text);
            }
            else
            {
                numDays = 0;
            }
            if (txtCMND.Text.Trim().Length > 15)
            {
                MessageBox.Show("Chứng minh thư không hợp lệ", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Transact();
            }
        }

        private void Transact()
        {

            tran = null;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                using (tran = con.BeginTransaction(IsolationLevel.Serializable))
                {
                    //Thread.Sleep(5000);
                    try
                    {
                        //TÌM KHÁCH HÀNG TRONG DB
                        string cmnd = "";
                        string stateKH = "";
                        cmd = new SqlCommand("SELECT CMND,TrangThai FROM KHACH_HANG WHERE CMND = @cmnd", con, tran);
                        cmd.Parameters.AddWithValue("@cmnd", txtCMND.Text.Trim());
                        rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            cmnd = rdr["CMND"].ToString();
                            stateKH = rdr["TrangThai"].ToString();
                        }
                        rdr.Close();
                        //KHÔNG CÓ KHÁCH HÀNG TRONG DB
                        if (cmnd.Equals(""))
                        {
                            //THÊM THÔNG TIN KHÁCH HÀNG VÀO DB
                            cmd = new SqlCommand("INSERT INTO KHACH_HANG(HoDem,Ten,CMND,TrangThai) VALUES(@hodem,@ten,@cmnd,@trangthaikh)", con, tran);
                            cmd.Parameters.AddWithValue("@hodem", txtLastName.Text.Trim());
                            cmd.Parameters.AddWithValue("@ten", txtFirstName.Text.Trim());
                            cmd.Parameters.AddWithValue("@cmnd", txtCMND.Text.Trim());
                            cmd.Parameters.AddWithValue("@trangthaikh", "Checkin");
                            tran.Save("insertGuest");
                            cmd.ExecuteNonQuery();
                        }
                        else //CÓ KHÁCH HÀNG TRONG DB
                        {
                            //KIỂM TRA XEM KHÁCH HÀNG CÓ ĐANG CHECKIN
                            if (stateKH.Equals("Checkin")) //CÓ
                            {
                                MessageBox.Show("Khách hàng này đang Checkin", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tran.Rollback();
                                return;
                            }
                            else if (stateKH.Equals("Reserve")) //KHÁCH HÀNG CÓ ĐẶT TRƯỚC
                            {
                                //CẬP NHẬT TRẠNG THÁI SANG CHECKIN,
                                cmd = new SqlCommand("UPDATE KHACH_HANG SET TrangThai=@trangthaikh WHERE CMND=@cmnd", con, tran);
                                cmd.Parameters.AddWithValue("@trangthaikh", "Checkin");
                                cmd.Parameters.AddWithValue("@cmnd", txtCMND.Text.Trim());
                                tran.Save("updateGuestState");
                                cmd.ExecuteNonQuery();
                                //CẬP NHẬT DS ĐẶT TRƯỚC SANG ĐÃ CHECKIN,
                                cmd = new SqlCommand("UPDATE DAT_TRUOC SET TrangThai=@trangthaidt WHERE CMND=@cmnd AND MaPhong=@maphong", con, tran);
                                cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                                cmd.Parameters.AddWithValue("@cmnd", txtCMND.Text.Trim());
                                cmd.Parameters.AddWithValue("@trangthaidt", "Checkin");
                                tran.Save("updateReserve");
                                cmd.ExecuteNonQuery();
                            }
                            else //KHÔNG
                            {
                                //CẬP NHẬT TRẠNG THÁI SANG CHECKIN
                                cmd = new SqlCommand("UPDATE KHACH_HANG SET TrangThai=@trangthaikh WHERE CMND =@cmnd", con, tran);
                                cmd.Parameters.AddWithValue("@cmnd", txtCMND.Text.Trim());
                                cmd.Parameters.AddWithValue("@trangthaikh", "Checkin");
                                tran.Save("updateGuest");
                                cmd.ExecuteNonQuery();
                            }
                        }
                        //KIỂM TRA PHÒNG
                        cmd = new SqlCommand("SELECT GioiHan,SoNguoiHienCo,TinhTrang FROM PHONG WHERE MaPhong=@maphong", con, tran);
                        cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                        rdr = cmd.ExecuteReader();
                        int gioihan = 0;
                        int snhienco = 0;
                        int sndangki = (int)Val(txtPeopleCount.Text.Trim());
                        string stateRoom = "";
                        if (rdr.Read())
                        {
                            gioihan = (int)Val(rdr["GioiHan"].ToString());
                            snhienco = (int)Val(rdr["SoNguoiHienCo"].ToString());
                            stateRoom = rdr["TinhTrang"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm được phòng này");
                            tran.Rollback();
                            return;
                        }
                        rdr.Close();
                        //SO SÁNH SỐ NGƯỜI ĐĂNG KÍ VỚI SỐ NGƯỜI GIỚI HẠN
                        if (gioihan == snhienco)
                        {
                            MessageBox.Show("Phòng đầy!!!,Xin chọn phòng khác");
                            tran.Rollback();
                            return;
                        }
                        else if (snhienco + sndangki > gioihan)
                        {
                            MessageBox.Show("Đăng kí vượt quá số người qui định " + ((sndangki + snhienco) - gioihan) + " người." + ",Xin chọn phòng khác");
                            tran.Rollback();
                            return;
                        }
                        //KIỂM TRA NGƯỜI CHECKIN CHÍNH, LƯU VÀO HOÁ ĐƠN, LƯU GIAO DỊCH
                        if (snhienco == 0 && (stateRoom.Equals("Trống") || stateRoom.Equals("Đặt trước")))
                        {
                            string idkh = "";
                            //TÌM KHÁCH HÀNG
                            cmd = new SqlCommand("SELECT CMND FROM KHACH_HANG WHERE CMND=@cmnd", con, tran);
                            cmd.Parameters.AddWithValue("@cmnd", txtCMND.Text.Trim());
                            rdr = cmd.ExecuteReader();
                            while (rdr.Read())
                            {
                                idkh = rdr["CMND"].ToString();
                            }
                            rdr.Close();
                            if (idkh.Equals(""))
                            {
                                MessageBox.Show("Xảy ra lỗi...,Xin thử lại");
                                tran.Rollback();
                                return;
                            }
                            ////KIỂM TRA NGƯỜI DÙNG CÓ ĐANG ĐĂNG NHẬP
                            //cmd = new SqlCommand("SELECT TrangThai FROM NGUOI_DUNG WHERE idND=@find", con, tran);
                            //cmd.Parameters.AddWithValue("@find", HomeForm.Instance().lblIDUser.Text);
                            //rdr = cmd.ExecuteReader();
                            //if (rdr.Read())
                            //{
                            //    if (rdr["TrangThai"].Equals("0"))
                            //    {
                            //        this.Hide();
                            //        LoginForm login = new LoginForm();
                            //        login.Show();
                            //        tran.Rollback();
                            //        return;
                            //    }
                            //}
                            //rdr.Close();
                            //TẠO HOÁ ĐƠN
                            cmd = new SqlCommand("INSERT INTO HOA_DON(idND,CMND,MaPhong,NgayLap) VALUES(@idnd,@cmnd,@maphong,@ngaylap)", con, tran);
                            cmd.Parameters.AddWithValue("@idnd", HomeForm.Instance().lblIDUser.Text);
                            cmd.Parameters.AddWithValue("@cmnd", txtCMND.Text.Trim());
                            cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                             cmd.Parameters.AddWithValue("@ngaylap", datetime);
                          
                            tran.Save("insertHD");
                            cmd.ExecuteNonQuery();

                            //LẤY ID HD, TẠO CTHD
                            string idhd = "";
                            cmd.CommandText = "SELECT idHD FROM HOA_DON WHERE MaPhong=@maphong AND CMND=@cmnd ORDER BY NgayLap DESC";
                            //cmd.Parameters.AddWithValue("@idkh", idkh);
                            //cmd.Parameters.AddWithValue("@trangthaihd", 0);
                            rdr = cmd.ExecuteReader();
                            if (rdr.Read())
                            {
                                idhd = rdr["idHD"].ToString();
                            }
                            rdr.Close();
                            if (idhd.Equals(""))
                            {
                                MessageBox.Show("Xảy ra lỗi...,Xin thử lại");
                                tran.Rollback();
                                return;
                            }
                            cmd.CommandText = "INSERT INTO CT_HOA_DON(idHD,SoNgay,SoNguoi,NgayDangKi,TraTruoc) VALUES(@idhd,@songay,@songuoi,@ngaydangki,@tratruoc)";
                            cmd.Parameters.AddWithValue("@idhd", idhd);
                            cmd.Parameters.AddWithValue("@songuoi", (int)Val(txtPeopleCount.Text));
                            cmd.Parameters.AddWithValue("@songay", (int)Val(txtNumOfDay.Text));
                            cmd.Parameters.AddWithValue("@ngaydangki", datetime);
                            cmd.Parameters.AddWithValue("@tratruoc", decimal.Parse(txtAdvance.Text));
                            tran.Save("insertCTHD");
                            cmd.ExecuteNonQuery();

                            //TẠO GIAO DỊCH
                            cmd.CommandText = "INSERT INTO GIAO_DICH(LoaiDK,CMND,MaPhong,NgayDangKi,SoNguoi,TrangThai) VALUES(@loaidk,@cmnd,@maphong,@ngaydangki,@songuoi,@trangthaigd)";
                            cmd.Parameters.AddWithValue("@loaidk", 0); //0-người đăng kí chính
                            cmd.Parameters.AddWithValue("@trangthaigd", "Active");
                            //cmd.Parameters.AddWithValue("@cmnd", txtCMND.Text.Trim());
                            tran.Save("insertGD");
                            cmd.ExecuteNonQuery();

                            //CẬP NHẬT TÌNH TRẠNG PHÒNG
                            cmd.CommandText = "UPDATE PHONG SET TinhTrang=@tinhtrangphong,SoNguoiHienCo+=@snhienco WHERE MaPhong=@maphong";
                            cmd.Parameters.AddWithValue("@tinhtrangphong", "Đang sử dụng"); //0-người đăng kí chính
                            cmd.Parameters.AddWithValue("@snhienco", 1);
                            tran.Save("updateRoom");
                            cmd.ExecuteNonQuery();

                        }
                        //NGƯỜI PHỤ, KHÔNG LƯU HOÁ ĐƠN, CẬP NHẬT PHÒNG, CẬP NHẬT GIAO DỊCH
                        else
                        {
                            string CMND = "";
                            //TÌM KHÁCH HÀNG
                            cmd = new SqlCommand("SELECT CMND FROM KHACH_HANG WHERE CMND=@cmnd", con, tran);
                            cmd.Parameters.AddWithValue("@cmnd", txtCMND.Text.Trim());
                            rdr = cmd.ExecuteReader();
                            while (rdr.Read())
                            {
                                CMND = rdr["CMND"].ToString();
                            }
                            rdr.Close();
                            if (CMND.Equals(""))
                            {
                                MessageBox.Show("Xảy ra lỗi...,Xin thử lại");
                                tran.Rollback();
                                return;
                            }
                            //TẠO GIAO DỊCH
                            cmd.CommandText = "INSERT INTO GIAO_DICH(LoaiDK,CMND,MaPhong,NgayDangKi,SoNguoi,TrangThai) VALUES(@loaidk,@cmnd,@maphong,@ngaydangki,@songuoi,@trangthaigd)";
                            cmd.Parameters.AddWithValue("@loaidk", 1); //1-người đăng kí phụ
                            cmd.Parameters.AddWithValue("@trangthaigd", "Active");
                            cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                            cmd.Parameters.AddWithValue("@ngaydangki", datetime);
                            cmd.Parameters.AddWithValue("@songuoi", 1);
                            tran.Save("insertGD");
                            cmd.ExecuteNonQuery();

                            //CẬP NHẬT TÌNH TRẠNG PHÒNG
                            cmd.CommandText = "UPDATE PHONG SET TinhTrang=@tinhtrangphong,SoNguoiHienCo+=@snhienco WHERE MaPhong=@maphong";
                            cmd.Parameters.AddWithValue("@tinhtrangphong", "Đang sử dụng"); //0-người đăng kí chính
                            cmd.Parameters.AddWithValue("@snhienco", 1);
                            tran.Save("updateRoom");
                            cmd.ExecuteNonQuery();
                        }
                        tran.Commit();
                        MessageBox.Show("CHECKIN thành công.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Commit exeption type: " + ex.GetType());
                        MessageBox.Show("Commit exeption type: " + ex.StackTrace);
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
                        lblCurrentPeopleCount.Text = loadCurrentPeople();
                        ReservationListForm.Instance().load_ReservationList();
                        GuestListForm.Instance().load_GuestList();
                        RoomListForm.Instance().load_RoomList();

                    }
                }
            }
        }

        private void updateReservation()
        {
            try
            {
                using (con = new SqlConnection(cs))
                using (cmd = new SqlCommand("UPDATE DAT_TRUOC SET TrangThai=@d1 WHERE CMND=@find", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@d1", "Checkin");
                    cmd.Parameters.AddWithValue("@find", txtCMND.Text.Trim());
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private bool checkGuestState()
        {
            try
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("SELECT TrangThai FROM KHACH_HANG WHERE CMND=@find", con);
                cmd.Parameters.AddWithValue("@find", txtCMND.Text.Trim());
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    if (rdr["TrangThai"].ToString().Equals("Reserve"))
                    {
                        rdr.Close();
                        con.Close();
                        return true;
                    }
                }
                rdr.Close();
                con.Close();
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private string loadCurrentPeople()
        {
            try
            {
                using (con = new SqlConnection(cs))
                using (cmd = new SqlCommand("SELECT SoNguoiHienCo FROM PHONG WHERE MaPhong=@find", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@find", lblRoom.Text); //0: chưa thanh toán,1: đã thanh toán
                    using (rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            string temp = rdr["SoNguoiHienCo"].ToString();
                            return temp;
                        }
                        else return "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void createCTHD()
        {
            try
            {
                string idKH = getIDKH(lblRoom.Text);
                string idHD = getIDHD(lblRoom.Text, idKH);
                using (con = new SqlConnection(cs))
                using (cmd = new SqlCommand("INSERT INTO CT_HOA_DON(idHD,MaPhong,NgayDangKi,TraTruoc) " +
                    "VALUES(@d1,@d2,@d3,@d4)", con))
                {
                    con.Open();
                    if (idHD.Equals(""))
                    {
                        MessageBox.Show("Không có hoá đơn này.");
                        return;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@d1", idHD);
                    }
                    cmd.Parameters.AddWithValue("@d2", lblRoom.Text);
                    cmd.Parameters.AddWithValue("@d3", datetime);
                    cmd.Parameters.AddWithValue("@d4", Math.Round(Decimal.Parse(txtAdvance.Text), 1, MidpointRounding.AwayFromZero)); //0: chưa thanh toán,1: đã thanh toán
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private string getIDHD(string room, string idKH)
        {
            try
            {
                using (con = new SqlConnection(cs))
                using (cmd = new SqlCommand("SELECT idHD FROM HOA_DON WHERE idKH=@d1 AND MaPhong=@d2 AND TrangThai=@d3", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@d1", idKH);
                    cmd.Parameters.AddWithValue("@d2", room);
                    cmd.Parameters.AddWithValue("@d3", 0); //0:chưa thanh toán, 1:đã thanh toán
                    using (rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            string temp = rdr["idHD"].ToString();
                            return temp;
                        }
                        else return "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void createHD()
        {
            try
            {
                string idKH = getIDKH(txtCMND.Text);
                using (con = new SqlConnection(cs))
                using (cmd = new SqlCommand("INSERT INTO HOA_DON(idND,idKH,MaPhong,NgayDangKi,TrangThai,TraTruoc) " +
                    "VALUES(@d1,@d2,@d3,@d4,@d5,@d6)", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@d1", HomeForm.Instance().lblIDUser.Text);
                    if (idKH.Equals(""))
                    {
                        MessageBox.Show("Không có khách hàng này.");
                        return;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@d2", idKH);
                    }
                    cmd.Parameters.AddWithValue("@d3", lblRoom.Text);
                    cmd.Parameters.AddWithValue("@d4", datetime);
                    cmd.Parameters.AddWithValue("@d5", "0"); //0: chưa thanh toán,1: đã thanh toán
                    cmd.Parameters.AddWithValue("@d6", Math.Round(Decimal.Parse(txtAdvance.Text.Trim()), 1, MidpointRounding.AwayFromZero));
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private string getIDKH(string cmnd)
        {
            try
            {
                using (con = new SqlConnection(cs))
                using (cmd = new SqlCommand("SELECT idKH FROM KHACH_HANG WHERE CMND=@find", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@find", cmnd.Trim());
                    using (rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            string temp = rdr["idKH"].ToString();
                            return temp;
                        }
                        else
                        {
                            return "";
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

        private void updateRoom()
        {
            try
            {
                using (con = new SqlConnection(cs))
                using (cmd = new SqlCommand("UPDATE PHONG SET TinhTrang=@d1,SoNguoiHienCo+=@d2 WHERE MaPhong=@find", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@d1", "Đang sử dụng");
                    cmd.Parameters.AddWithValue("@d2", 1);
                    cmd.Parameters.AddWithValue("@find", lblRoom.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void createGD()
        {
            try
            {
                using (con = new SqlConnection(cs))
                using (cmd = new SqlCommand("INSERT INTO GIAO_DICH(CMND,MaPhong,NgayDangKi,SoNguoi,TrangThai,LoaiDK) " +
                    "VALUES(@d1,@d2,@d3,@d4,@d5,@d6)", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@d1", txtCMND.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", lblRoom.Text);
                    cmd.Parameters.AddWithValue("@d3", datetime);
                    cmd.Parameters.AddWithValue("@d4", txtPeopleCount.Text.Trim());
                    cmd.Parameters.AddWithValue("@d5", "Active");
                    //LOẠI ĐĂNG KÍ XÁC ĐỊNH NGƯỜI ĐĂNG KÍ CHÍNH
                    cmd.Parameters.AddWithValue("@d6", 0);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
        private void createGD(int x)
        {
            try
            {
                using (con = new SqlConnection(cs))
                using (cmd = new SqlCommand("INSERT INTO GIAO_DICH(CMND,MaPhong,NgayDangKi,TrangThai,LoaiDK) " +
                    "VALUES(@d1,@d2,@d3,@d4,@d5)", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@d1", txtCMND.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", lblRoom.Text);
                    cmd.Parameters.AddWithValue("@d3", datetime);
                    cmd.Parameters.AddWithValue("@d4", "Active");
                    cmd.Parameters.AddWithValue("@d5", x);
                    //LOẠI ĐĂNG KÍ XÁC ĐỊNH NGƯỜI ĐĂNG KÍ CHÍNH 0-người ĐK CHÍNH 1-phụ
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private bool checkRoomState()
        {
            try
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("SELECT TinhTrang FROM PHONG WHERE MaPhong=@find", con);
                cmd.Parameters.AddWithValue("@find", lblRoom.Text);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    if (rdr["TinhTrang"].ToString().Equals("Trống"))
                    {
                        rdr.Close();
                        con.Close();
                        return true;
                    }
                }
                rdr.Close();
                con.Close();
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void insertGuest()
        {
            try
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("INSERT INTO KHACH_HANG(HoDem,Ten,CMND,TrangThai) VALUES(@d1,@d2,@d3,@d4)", con);
                cmd.Parameters.AddWithValue("@d1", txtLastName.Text.Trim());
                cmd.Parameters.AddWithValue("@d2", txtFirstName.Text.Trim());
                cmd.Parameters.AddWithValue("@d3", txtCMND.Text.Trim());
                cmd.Parameters.AddWithValue("@d4", "");
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
        private void updateGuest()
        {
            try
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("UPDATE KHACH_HANG SET TrangThai=@d1 WHERE CMND=@find", con);
                cmd.Parameters.AddWithValue("@find", txtCMND.Text);
                cmd.Parameters.AddWithValue("@d1", "Checkin");
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private int checkCurrentPeople(string maphong)
        {
            try
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("SELECT SoNguoiHienCo FROM PHONG WHERE MaPhong = @find", con);
                cmd.Parameters.AddWithValue("@find", maphong);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (rdr.Read())
                {
                    return (int)Val(rdr["SoNguoiHienCo"]);
                }
                return -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private bool findCMND(string cmnd)
        {
            try
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("SELECT CMND FROM KHACH_HANG WHERE CMND = @find", con);
                cmd.Parameters.AddWithValue("@find", cmnd);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (rdr.Read())
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        public void load_all()
        {
            con = new SqlConnection(cs);
            con.Open();
            cmd = new SqlCommand("SELECT HoDem,Ten,CMND FROM KHACH_HANG", con);
            rdr = cmd.ExecuteReader();
            AutoCompleteStringCollection MyCollection1 = new AutoCompleteStringCollection();
            AutoCompleteStringCollection MyCollection2 = new AutoCompleteStringCollection();
            AutoCompleteStringCollection MyCollection3 = new AutoCompleteStringCollection();
            while (rdr.Read())
            {
                MyCollection1.Add(rdr["Ten"].ToString());
                MyCollection2.Add(rdr["HoDem"].ToString());
                MyCollection3.Add(rdr["CMND"].ToString());
            }
            txtFirstName.AutoCompleteCustomSource = MyCollection1;
            txtLastName.AutoCompleteCustomSource = MyCollection2;
            txtCMND.AutoCompleteCustomSource = MyCollection3;
            con.Close();
        }


        private void btnPlus_Click(object sender, EventArgs e)
        {
            int temp = (int)Val(txtPeopleCount.Text.ToString());
            if (temp == 0)
            {
                txtPeopleCount.Text = 1.ToString();
            }
            else
            {
                if (Val(lblOccupancy.Text) == temp)
                {
                    txtPeopleCount.Text = temp.ToString();
                }
                else
                {
                    temp++;
                    txtPeopleCount.Text = temp.ToString();
                }
            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            int temp = (int)Val(txtPeopleCount.Text.ToString());
            if (temp == 0)
            {
                txtPeopleCount.Text = 0.ToString();
            }
            else
            {
                temp--;
                txtPeopleCount.Text = temp.ToString();
            }
        }

        private void btnSearchRoom_Click(object sender, EventArgs e)
        {
            SearchRoomForm searchRoomForm = new SearchRoomForm();
            searchRoomForm.ShowDialog();
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

        private void txtPeopleCount_TextChanged(object sender, KeyPressEventArgs e)
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

        private void txtAdvance_TextChanged(object sender, KeyPressEventArgs e)
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

        private void txtSubtotal_TextChanged(object sender, KeyPressEventArgs e)
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

        private void txtName_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }
    }
}
