using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Threading;

namespace QLKS
{
    public partial class RegistrationForm : UserControl
    {
        protected internal static RegistrationForm _instance;
        private SqlDataReader rdr = null;
        private DataTable dtable;
        private SqlConnection con = null;
        private SqlDataAdapter adp;
        private SqlTransaction tran;
        private DataSet ds;
        SqlCommand cmd = null;
        private DataTable dt = new DataTable();
        private string cs = "Data Source=DESKTOP-ASUKO;Initial Catalog=QLKS;Integrated Security=True";


        public RegistrationForm()
        {
            InitializeComponent();
        }
        protected internal static RegistrationForm Instance()
        {
            if (_instance == null)
            {
                _instance = new RegistrationForm();
            }
            return _instance;
        }
        public Boolean isEmailValid(string emailAddress) //KT EMAIL HOP LE
        {
            try
            {
                //string REGEX ="[\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})";
                if (!string.IsNullOrEmpty(emailAddress))
                {
                    MailAddress mailAddress = new MailAddress(emailAddress);
                    return true;
                }
                else
                    return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public Boolean isPhoneNumberValid(string number) //KT SDT HOP LE
        {
            Boolean phoneValid;
            string PhoneNumber = "(\\+84|0)\\d{9,10}"; //pattern :điện thoại có phần mở đầu có thể là +84 (ở Việt Nam) hoặc là 0, 
                                                       //nên ta cần đặt vào trong group và thêm dấu |
                                                       //1 số điện thoại bao gồm 10 hoặc 11 chữ số, 
                                                       //nhưng ta không tính phần đầu của số điện thoại nên chỉ còn khoảng 9 – 10 chữ số                                                                   
            Regex checkPhone = new Regex(PhoneNumber);
            if (!string.IsNullOrEmpty(number))
            {
                phoneValid = checkPhone.IsMatch(number);
            }
            else
                phoneValid = false;
            return phoneValid;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string str;
                if ((txtFullName.Text.Trim().Length) == 0)
                {
                    MessageBox.Show("Xin nhập họ và tên đầy đủ", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFullName.Focus();
                    return;
                }
                if ((txtUserName.Text.Trim().Length) == 0)
                {
                    MessageBox.Show("Xin nhập Tên Đăng Nhập", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserName.Focus();
                    return;
                }
                if ((txtPhoneNumber.Text.Trim().Length) == 0)
                {
                    MessageBox.Show("Xin nhập Số Điện Thoại", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPhoneNumber.Focus();
                    return;
                }
                if (!isPhoneNumberValid(txtPhoneNumber.Text))
                {
                    MessageBox.Show("Xin nhập Số Điện Thoại hợp lệ", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPhoneNumber.Focus();
                    return;
                }
                if ((txtEmail.Text.Trim().Length) == 0)
                {
                    MessageBox.Show("Xin nhập Email", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Focus();
                    return;
                }
                if ((txtCMND.Text.Trim().Length) == 0)
                {
                    MessageBox.Show("Xin nhập Chứng minh thư", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Focus();
                    return;
                }
                if (isEmailValid(txtEmail.Text) == false)
                {
                    MessageBox.Show("Xin nhập Email hợp lệ", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Focus();
                    return;
                }
                if (rbQuanly.Checked == true)
                {
                    str = "0";
                }
                else
                {
                    str = "1";
                }
                //Transaction(str);
                Proc_CreateAccount(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Proc_CreateAccount(string loai)
        {

            using (con = new SqlConnection(cs))
            {
                con.Open();
                try
                {
                    bool check = false;
                    string ct = "SELECT dbo.getTenDangNhap(@find)";
                    cmd = new SqlCommand(ct, con);
                    cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "TenDangNhap")).Value = txtUserName.Text;
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        if (rdr[0].ToString().Equals(txtUserName.Text.Trim()))
                        {
                            MessageBox.Show("Tên đăng nhập đã có người sử dụng", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtUserName.Clear();
                            check = true;
                            return;
                        }
                    }
                    rdr.Close();
                    cmd = new SqlCommand("SELECT dbo.getCMND_User(@cmnd)", con);
                    cmd.Parameters.AddWithValue("@cmnd", txtCMND.Text.Trim());
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        if (rdr[0].ToString().Equals(txtCMND.Text.Trim()))
                        {
                            MessageBox.Show("Chứng minh thư đã có người sử dụng", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtCMND.Clear();
                            check = true;
                            return;
                        }
                    }
                    rdr.Close();
                    
                    cmd = new SqlCommand("TaoTaiKhoan", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@loaind", loai);
                    cmd.Parameters.AddWithValue("@tendangnhap", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@matkhau", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@hoten", txtFullName.Text);
                    cmd.Parameters.AddWithValue("@sdt", txtPhoneNumber.Text);
                    cmd.Parameters.AddWithValue("@cmnd", txtCMND.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.ExecuteNonQuery();

                    if (check == false)
                    {
                        MessageBox.Show("Tạo tài khoản thành công", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetType() + "\n");
                    MessageBox.Show(ex.StackTrace + "\n");
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void Transaction(string str)
        {
            using (con = new SqlConnection(cs))
            {
                con.Open();
                using (tran = con.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        //Thread.Sleep(3000);
                        bool check = false;
                        string ct = "SELECT TenDangNhap FROM NGUOI_DUNG WHERE TenDangNhap=@find";
                        cmd = new SqlCommand(ct, con, tran);
                        cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "TenDangNhap")).Value = txtUserName.Text;
                        rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            MessageBox.Show("Tên đăng nhập đã có người sử dụng", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtUserName.Clear();
                            return;
                        }
                        rdr.Close();
                        cmd = new SqlCommand("SELECT CMND FROM NGUOI_DUNG WHERE CMND=@cmnd", con, tran);
                        cmd.Parameters.AddWithValue("@cmnd", txtCMND.Text.Trim());
                        rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            MessageBox.Show("Chứng minh thư đã có người sử dụng", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtCMND.Clear();
                            return;
                        }
                        rdr.Close();
                        string cb = "INSERT INTO NGUOI_DUNG(LoaiND,TenDangNhap,MatKhau,HoTen,SDT,CMND,Email) VALUES(@d1,@d2,@d3,@d4,@d5,@d6,@d7)";
                        cmd = new SqlCommand(cb, con, tran);
                        cmd.Connection = con;
                        cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NVarChar, 10, "LoaiND")).Value = str;
                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NVarChar, 50, "TenDangNhap")).Value = txtUserName.Text;
                        cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NVarChar, 50, "MatKhau")).Value = txtPassword.Text;
                        cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NVarChar, 50, "HoTen")).Value = txtFullName.Text;
                        cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NVarChar, 11, "SDT")).Value = txtPhoneNumber.Text;
                        cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NVarChar, 15, "CMND")).Value = txtCMND.Text;
                        cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 50, "Email")).Value = txtEmail.Text;
                        tran.Save("insertUser");
                        cmd.ExecuteNonQuery();
                        tran.Commit();

                        MessageBox.Show("Tạo tài khoản thành công", "User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFullName.Text = "";
                        txtPassword.Text = "";
                        txtUserName.Text = "";
                        txtEmail.Text = "";
                        txtPhoneNumber.Text = "";
                        txtCMND.Text = "";

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
    }
}

