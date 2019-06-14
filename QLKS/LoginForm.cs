using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Threading;

namespace QLKS
{
    public partial class LoginForm : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader rdr;
        SqlTransaction tran;
        string cs = "Data Source=DESKTOP-ASUKO;Initial Catalog=QLKS;Integrated Security=True";
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim().Length == 0)
            {
                MessageBox.Show("Xin nhập tên đăng nhập", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Focus();
            }
            if (txtPassword.Text.Trim().Length == 0)
            {
                MessageBox.Show("Xin nhập mật khẩu", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Focus();
            }
            string username = "";
            string idND = "";


            try
            {
                //Thread.Sleep(5000);
                using (con = new SqlConnection(cs))
                {
                    con.Open();

                    try
                    {
                        //
                        //cmd = new SqlCommand("SELECT TenDangNhap,idND FROM NGUOI_DUNG WHERE TenDangNhap=@username AND MatKhau=@password", con, tran);
                        //cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                        //cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                        //rdr = cmd.ExecuteReader();
                        ////0-inactive 1-active
                        //while (rdr.Read())
                        //{
                        //    username = rdr["TenDangNhap"].ToString();
                        //    idND = rdr["idND"].ToString();
                        //}
                        //rdr.Close();

                        //FUNCTION SQL
                        cmd = new SqlCommand("SELECT * FROM dbo.getTK(@username,@password)", con);
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                        rdr = cmd.ExecuteReader();
                        //0-inactive 1-active
                        
                        while (rdr.Read())
                        {
                            username = rdr["TenDangNhap"].ToString();
                            idND = rdr["idND"].ToString();
                        }
                        rdr.Close();
                        if (username.Equals(""))
                        {
                            MessageBox.Show("Đăng nhập thất bại...,Tên đăng nhâp hoặc mật khẩu sau. \nXin hãy thử lại!", "Wrong Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtPassword.Clear();
                            txtUsername.Clear();
                            txtUsername.Focus();
                            return;
                        }
                        else
                        {
                            HomeForm homeForm = HomeForm.Instance();
                            homeForm.lblIDUser.Text = idND.ToString();
                            homeForm.lblUser.Text = username.ToString();
                            homeForm.Show();
                            this.Hide();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Rollback exeption type :" + ex.GetType());
                        MessageBox.Show("Message :" + ex.Message);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
