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
using System.Data.Sql;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace QLKS
{
    public partial class GuestForm : UserControl
    {
        protected internal static GuestForm _instance;
        private SqlDataReader rdr = null;
        private Random random = new Random();
        private DataTable dtable;
        private SqlConnection con = null;
        private SqlDataAdapter adp;
        private DataSet ds;
        private SqlCommand cmd = null;
        private DataTable dt = new DataTable();
        private string cs = "Data Source=DESKTOP-ASUKO;Initial Catalog=QLKS;Integrated Security=True";
        private string gioitinh = "";
        public GuestForm()
        {
            InitializeComponent();
        }
        protected internal static GuestForm Instance()
        {
            if (_instance == null)
            {
                _instance = new GuestForm();
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
                if (checkEmptyBox())
                {
                    con = new SqlConnection(cs);
                    con.Open();

                    string ct = "SELECT CMND FROM KHACH_HANG WHERE CMND=@find";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 15, "CMND")).Value = txtCMND.Text;
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        MessageBox.Show("Đã có khách hàng này ", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult dialogResult = MessageBox.Show("Bạn có muốn cập nhật thông tin cho khách hàng ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dialogResult == DialogResult.Yes)
                        {
                            update_Guest();
                            rdr.Close();
                            return;
                        }
                        else
                        {
                            rdr.Close();
                            return;
                        }
                    }
                    else
                    {
                        checkin_Guest();

                        txtLastName.Text = "";
                        txtFirstName.Text = "";
                        txtCMND.Text = "";
                        txtAddress.Text = "";
                        txtNationality.Text = "";
                        txtEmail.Text = "";

                        con.Close();

                        GuestListForm glf = GuestListForm.Instance();
                        glf.load_GuestList();
                    }
                }
                else return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkin_Guest()
        {

            if (!checkCMND())
            {
                try
                {
                    SqlConnection con = new SqlConnection(cs);
                    con.Open();
                    string cb = "INSERT INTO KHACH_HANG(HoDem,Ten,CMND,DiaChi,QuocTich,Email,GioiTinh) VALUES(@d1,@d2,@d3,@d4,@d5,@d6,@d7)";
                    SqlCommand cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NVarChar, 50, "HoDem")).Value = txtLastName.Text;
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NVarChar, 50, "Ten")).Value = txtFirstName.Text;
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NVarChar, 15, "CMND")).Value = txtCMND.Text;
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NVarChar, 50, "DiaChi")).Value = txtAddress.Text;
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NVarChar, 50, "QuocTich")).Value = txtNationality.Text;
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NVarChar, 50, "Email")).Value = txtEmail.Text;
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NVarChar, 10, "GioiTinh")).Value = gioitinh.ToString();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Khách hàng đăng kí thành công", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Đã có khách hàng này!!!", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn cập nhật ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    update_Guest();
                }
            }
        }
        private bool checkCMND()
        {
            con = new SqlConnection(cs);
            con.Open();
            cmd = new SqlCommand("SELECT CMND FROM KHACH_HANG WHERE CMND=@find", con);
            cmd.Parameters.Add(new SqlParameter("@find", SqlDbType.NVarChar, 15)).Value = txtCMND.Text.ToString();
            rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }
        private bool checkGStatus()
        {
            con = new SqlConnection(cs);
            con.Open();
            cmd = new SqlCommand("SELECT TrangThai FROM KHACH_HANG WHERE CMND=@find", con);
            cmd.Parameters.Add(new SqlParameter("@find", SqlDbType.NVarChar, 50, "CMND")).Value = txtCMND.Text.ToString();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr["TrangThai"].ToString() == "Checkin(Temporary)" || rdr["TrangThai"].ToString() == "Checkin")
                {
                    con.Close();
                    rdr.Close();
                    return true;
                }
            }
            con.Close();
            rdr.Close();
            return false;
        }
        private void update_Guest()
        {
            if (checkCMND())
            {
                if (checkGStatus()) //KT TRẠNG THÁI =>> NẾU LÀ Checkin hoặc Checkin(Temporary) thì set lại thành Checkin
                {
                    try
                    {
                        SqlConnection con = new SqlConnection(cs);
                        con.Open();
                        string cb = "UPDATE KHACH_HANG SET HoDem=@d1,Ten=@d2,DiaChi=@d4,QuocTich=@d5,Email=@d6,GioiTinh=@d7,TrangThai=@d8 WHERE CMND=@d3";
                        SqlCommand cmd = new SqlCommand(cb);
                        cmd.Connection = con;
                        cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NVarChar, 50, "HoDem")).Value = txtLastName.Text;
                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NVarChar, 50, "Ten")).Value = txtFirstName.Text;
                        cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NVarChar, 15, "CMND")).Value = txtCMND.Text;
                        cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NVarChar, 50, "DiaChi")).Value = txtAddress.Text;
                        cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NVarChar, 50, "QuocTich")).Value = txtNationality.Text;
                        cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NVarChar, 50, "Email")).Value = txtEmail.Text;
                        cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NVarChar, 10, "GioiTinh")).Value = gioitinh.ToString();
                        cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.NVarChar, 50, "TrangThai")).Value = "Checkin";
                        cmd.ExecuteReader();
                        con.Close();
                        MessageBox.Show("Cập nhật thành công", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.ToString(), "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else //CHỈ CẬP NHẬT THÔNG TIN KHÔNG ĐĂNG KÍ TRẠNG THÁI
                {
                    try
                    {
                        SqlConnection con = new SqlConnection(cs);
                        con.Open();
                        string cb = "UPDATE KHACH_HANG SET HoDem=@d1,Ten=@d2,DiaChi=@d4,QuocTich=@d5,Email=@d6,GioiTinh=@d7 WHERE CMND=@d3";
                        SqlCommand cmd = new SqlCommand(cb);
                        cmd.Connection = con;
                        cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NVarChar, 50, "HoDem")).Value = txtLastName.Text;
                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NVarChar, 50, "Ten")).Value = txtFirstName.Text;
                        cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NVarChar, 15, "CMND")).Value = txtCMND.Text;
                        cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NVarChar, 50, "DiaChi")).Value = txtAddress.Text;
                        cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NVarChar, 50, "QuocTich")).Value = txtNationality.Text;
                        cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NVarChar, 50, "Email")).Value = txtEmail.Text;
                        cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NVarChar, 10, "GioiTinh")).Value = gioitinh.ToString();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Cập nhật thành công", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.ToString(), "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Không tồn tại khách hàng này!!!", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool checkEmptyBox()
        {
            if ((txtFirstName.Text.Trim().Length) == 0)
            {
                MessageBox.Show("Xin nhập tên ", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstName.Focus();
                return false;
            }
            if ((txtLastName.Text.Trim().Length) == 0)
            {
                MessageBox.Show("Xin nhập họ và tên đệm", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLastName.Focus();
                return false;
            }
            if ((txtAddress.Text.Trim().Length) == 0)
            {
                MessageBox.Show("Xin nhập địa chỉ ", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return false;
            }
            if ((txtCMND.Text.Trim().Length) == 0)
            {
                MessageBox.Show("Xin nhập Chứng minh nhân dân ", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCMND.Focus();
                return false;
            }

            if ((txtEmail.Text.Trim().Length) == 0) //NẾU EMAIL KHÔNG CÓ==>> SKIP
            {
                //MessageBox.Show("Xin nhập Email ", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //txtEmail.Focus();
                return true;
            }
            else if (isEmailValid(txtEmail.Text) == false) //CÓ EMAIL KT TÍNH HỢP LỆ
            {
                MessageBox.Show("Xin nhập Email hợp lệ", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return false;
            }
            return true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            update_Guest();
            GuestListForm.Instance().load_GuestList();
            CheckOutForm.Instance().load_All();
        }

        private void GuestForm_Load(object sender, EventArgs e)
        {
            rbMale.Checked = true;
            load_all();
        }

        private void load_all()
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

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMale.Checked == true)
            {
                gioitinh = rbMale.Text;
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFemale.Checked == true)
            {
                gioitinh = rbFemale.Text;
            }
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
    }
}
