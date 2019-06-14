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
    public partial class ReservationForm : UserControl
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
        protected internal static ReservationForm _instance;
        protected internal static ReservationForm Instance()
        {
            if (_instance == null)
            {
                _instance = new ReservationForm();
            }
            return _instance;
        }
        private SqlDataReader rdr = null;
        private DataTable dtable;
        private SqlConnection con = null;
        private SqlDataAdapter adp;
        private DataSet ds;
        SqlCommand cmd = null;
        private DataTable dt = new DataTable();
        private string cs = "Data Source=DESKTOP-ASUKO;Initial Catalog=QLKS;Integrated Security=True";
        private string tempdatetime;
        private string appointment;
        public ReservationForm()
        {
            InitializeComponent();
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ReservationForm_Load(object sender, EventArgs e)
        {
            string format1 = "yyyy-MM-dd HH:mm:ss";
            string format2 = "dd-MM-yyyy";
            string format = "HH:mm:ss";

            DateTime date = DateTime.Now;
            lblCurrentDate.Text = DateTime.Now.ToString(format2);
            tempdatetime = Convert.ToDateTime(date).ToString(format1);


            appointment = DateTime.Parse(timePicker.Value.ToString()).ToString(format);

            timePicker.Format = DateTimePickerFormat.Time;
            timePicker.ShowUpDown = true;

            loadAll();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!checkEmpty()) //ĐẦY ĐỦ
            {
                int x = checkCMND();
                if (x != 0) //KT CMND KH NÀY CÓ ĐANG Checkin =>>không có CHECKIN HOẶC ĐÃ CHECKOUT, hoặc k có trng db
                {
                    if (x == 1)
                    {
                        if (!checkNumOfPeople())
                        {
                            if (checkRes() == 0)
                            {
                                updateGuest();
                                reservation();
                                update_Room();
                                MessageBox.Show("Đặt trước thành công", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                txtLastName.Text = "";
                                txtFirstName.Text = "";
                                txtCMND.Text = "";
                                txtNumOfPeople.Text = "";

                                ReservationListForm.Instance().load_ReservationList();
                                RoomListForm.Instance().load_RoomList();
                                GuestListForm.Instance().load_GuestList();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Khách hàng này đã đặt trước", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Vượt quá số người quy định", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        if (!checkNumOfPeople())
                        {
                            if (checkRes() == 0)
                            {
                                insertGuest();
                                reservation();
                                update_Room();
                                MessageBox.Show("Đặt trước thành công", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                txtLastName.Text = "";
                                txtFirstName.Text = "";
                                txtCMND.Text = "";
                                txtNumOfPeople.Text = "";

                                ReservationListForm.Instance().load_ReservationList();
                                RoomListForm.Instance().load_RoomList();
                                GuestListForm.Instance().load_GuestList();

                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Khách hàng này đã đặt trước", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Vượt quá số người quy định", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Khách hàng này đã Checkin", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else //CHƯA ĐIỀN ĐẦY ĐỦ Ô TRỐNG
            {
                return;
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
                cmd.Parameters.AddWithValue("@d4", "Reserve");
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
                cmd.Parameters.AddWithValue("@d1", "Reserve");
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private int checkRes()
        {
            try
            {
                int i = -1;
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("SELECT TrangThai FROM DAT_TRUOC WHERE CMND=@find", con);
                cmd.Parameters.AddWithValue("@find", txtCMND.Text.Trim());
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    if (rdr["TrangThai"].Equals("HUỶ") || rdr["TrangThai"].Equals("Checkout"))
                        i = 0;
                    else
                    {
                        i = 1;
                    }
                }else
                {
                    i = 0;
                }

                return i;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            }

        }

        private void update_Room()
        {
            try
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("UPDATE PHONG SET TinhTrang=N'Đặt trước' WHERE MaPhong=@find", con);
                cmd.Parameters.Add(new SqlParameter("@find", SqlDbType.NVarChar, 10, "MaPhong")).Value = lblRoom.Text.ToString();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void reservation()
        {
            try
            {
                con = new SqlConnection(cs);
                con.Open();

                cmd = new SqlCommand("SELECT TrangThai FROM DAT_TRUOC WHERE CMND=@cmnd AND MaPhong=@maphong", con);
                cmd.Parameters.AddWithValue("@cmnd", txtCMND.Text.Trim());
                cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                rdr = cmd.ExecuteReader();
                int state = -1;
                bool check = false;
                if (rdr.Read())
                {
                    check = true;
                    if (rdr["TrangThai"].Equals("Checkout") || rdr["TrangThai"].Equals("HUỶ"))
                    {
                        state = 0;
                    }
                    else state = 1;
                }
                rdr.Close();
                if (check == false)
                {
                    cmd = new SqlCommand("INSERT INTO DAT_TRUOC(Ten,CMND,MaPhong,NgayDat,GioHen,SoNguoi,TrangThai,HoDem) VALUES(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8)", con);
                    cmd.Parameters.AddWithValue("@d1", txtFirstName.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", txtCMND.Text);
                    cmd.Parameters.AddWithValue("@d3", lblRoom.Text);
                    cmd.Parameters.AddWithValue("@d4", tempdatetime);
                    cmd.Parameters.AddWithValue("@d5", appointment);
                    cmd.Parameters.AddWithValue("@d6", (int)Val(txtNumOfPeople.Text));
                    string tinhtrang = "Đang đặt";
                    cmd.Parameters.AddWithValue("@d7", tinhtrang);
                    cmd.Parameters.AddWithValue("@d8", txtLastName.Text.Trim());
                    cmd.ExecuteNonQuery();
                }

                if (state == 0)
                {
                    cmd = new SqlCommand("UPDATE DAT_TRUOC SET TrangThai=@trangthai,NgayDat=@ngaydat,GioHen=@giohen WHERE CMND=@cmnd AND MaPhong=@maphong", con);
                    cmd.Parameters.AddWithValue("@cmnd", txtCMND.Text.Trim());
                    cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                    cmd.Parameters.AddWithValue("@ngaydat", tempdatetime);
                    cmd.Parameters.AddWithValue("@giohen", appointment);
                    cmd.Parameters.AddWithValue("@trangthai", "Đang đặt");
                    cmd.ExecuteNonQuery();
                }
                rdr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool checkNumOfPeople()
        {
            int x = (int)Val(txtNumOfPeople.Text.ToString()); //sô lượng đặt trước
            int y = (int)Val(lblOccu.Text.ToString()); //số lượng tối đa
            if (x > y)
            {
                return true;
            }
            else
                return false;
        }

        private int checkCMND()
        {
            try
            //CHECKIN : 0, CHECKOUT : 1, KHÔNG CÓ: 2
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("SELECT TrangThai FROM KHACH_HANG WHERE CMND=@find", con);
                cmd.Parameters.Add(new SqlParameter("@find", SqlDbType.NVarChar, 15, "CMND")).Value = txtCMND.Text.Trim();
                rdr = cmd.ExecuteReader();
                if (rdr.Read()) //có kh trong db
                {
                    if (rdr["TrangThai"].Equals("Checkin") || rdr["TrangThai"].Equals("Checkin(Temporary)")) //trạng thái checkin, checkin(tạm)
                    {
                        rdr.Close();
                        con.Close();
                        return 0;
                    }
                    else
                    { //trạng thái checkout
                        rdr.Close();
                        con.Close();
                        return 1;
                    }
                }
                else
                { //k có kh

                    rdr.Close();
                    con.Close();
                    return 2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private bool checkEmpty()
        {
            if (txtLastName.Text.Trim().Length == 0 || txtFirstName.Text.Trim().Length == 0 || txtCMND.Text.Trim().Length == 0 || txtNumOfPeople.Text.Trim().Length == 0)
            {
                MessageBox.Show("Xin nhập các ô trống ", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return true;
            }
            if (txtCMND.Text.Trim().Length > 15)
            {
                MessageBox.Show("Chứng minh thư chỉ 9 hoặc 12 số ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;
        }
        private void loadAll()
        {
            con = new SqlConnection(cs);
            cmd = new SqlCommand("SELECT CMND,Ten,HoDem FROM KHACH_HANG", con);
            con.Open();
            rdr = cmd.ExecuteReader();
            AutoCompleteStringCollection MyCollection1 = new AutoCompleteStringCollection();
            AutoCompleteStringCollection MyCollection2 = new AutoCompleteStringCollection();
            AutoCompleteStringCollection MyCollection3 = new AutoCompleteStringCollection();
            while (rdr.Read())
            {
                MyCollection1.Add(rdr.GetString(0));
                MyCollection2.Add(rdr.GetString(1));
                MyCollection3.Add(rdr.GetString(2));
            }
            txtCMND.AutoCompleteCustomSource = MyCollection1;
            txtFirstName.AutoCompleteCustomSource = MyCollection2;
            txtLastName.AutoCompleteCustomSource = MyCollection3;
            con.Close();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string format = "HH:mm:ss";
            appointment = DateTime.Parse(timePicker.Value.ToString()).ToString(format);

        }

        private void txtLastName_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void txtFirstName_TextChanged(object sender, KeyPressEventArgs e)
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

        private void txtNumOfPeople_TextChanged(object sender, KeyPressEventArgs e)
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
