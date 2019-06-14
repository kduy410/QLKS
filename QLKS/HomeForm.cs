using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKS
{
    public partial class HomeForm : Form
    {
        protected internal static HomeForm _instance;

        private string status = "";
        private SqlDataReader rdr = null;
        private SqlConnection con = null;
        private SqlDataAdapter adp;
        private SqlTransaction tran;

        private DataSet ds;
        SqlCommand cmd = null;
        private DataTable dt = new DataTable();
        private string cs = "Data Source=DESKTOP-ASUKO;Initial Catalog=QLKS;Integrated Security=True";
        private string tempdatetime;

        public HomeForm()
        {
            InitializeComponent();
        }
        protected internal static HomeForm Instance()
        {
            if (_instance == null)
            {
                _instance = new HomeForm();
            }
            return _instance;
        }
        private void checkStatus(string username)
        {
            con = new SqlConnection(cs);
            cmd = new SqlCommand("SELECT LoaiND FROM NGUOI_DUNG WHERE TenDangNhap=@find", con);
            cmd.Parameters.Add(new SqlParameter("@find", SqlDbType.NVarChar)).Value = username;
            cmd.Connection.Open();
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                rdr.Read();
                if (rdr["LoaiND"].ToString().Equals("0"))
                {
                    status = "QL";
                }
                else
                    status = "NV";
            }
            con.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm login = new LoginForm();
            login.Show();
        }

        private void btnCheckin_Click(object sender, EventArgs e)
        {
            if (!panel3.Controls.Contains(CheckinForm.Instance()))
            {
                panel3.Controls.Add(CheckinForm._instance);
                CheckinForm._instance.Dock = DockStyle.Fill;
                CheckinForm._instance.BringToFront();
                CheckinForm._instance.Visible = true;
            }
            CheckinForm._instance.BringToFront();
            CheckinForm._instance.Visible = true;
        }

        private void btnCheckinList_Click(object sender, EventArgs e)
        {
            if (!panel3.Controls.Contains(CheckinListForm.Instance()))
            {
                panel3.Controls.Add(CheckinListForm._instance);
                CheckinListForm._instance.Dock = DockStyle.Fill;
                CheckinListForm._instance.BringToFront();
                CheckinListForm._instance.Visible = true;
                CheckinListForm._instance.load_CheckinList();
            }
            else
            {
                CheckinListForm._instance.BringToFront();
                CheckinListForm._instance.Visible = true;
                CheckinListForm._instance.load_CheckinList();
            }
        }

        private void btnGuest_Click(object sender, EventArgs e)
        {
            if (!panel3.Controls.Contains(GuestForm.Instance()))
            {
                panel3.Controls.Add(GuestForm._instance);
                GuestForm._instance.Dock = DockStyle.Fill;
                GuestForm._instance.BringToFront();
                GuestForm._instance.Visible = true;
            }
            GuestForm._instance.BringToFront();
            GuestForm._instance.Visible = true;
        }

        private void btnGuestList_Click(object sender, EventArgs e)
        {
            if (!panel3.Controls.Contains(GuestListForm.Instance()))
            {
                panel3.Controls.Add(GuestListForm._instance);
                GuestListForm._instance.Dock = DockStyle.Fill;
                GuestListForm._instance.BringToFront();
                GuestListForm._instance.Visible = true;
                GuestListForm._instance.load_GuestList();
            }
            else
            {
                GuestListForm._instance.BringToFront();
                GuestListForm._instance.Visible = true;
                GuestListForm._instance.load_GuestList();
            }
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            if (!panel3.Controls.Contains(RoomForm.Instance()))
            {
                panel3.Controls.Add(RoomForm._instance);
                RoomForm._instance.Dock = DockStyle.Fill;
                RoomForm._instance.BringToFront();
                RoomForm._instance.Visible = true;
            }
            RoomForm._instance.BringToFront();
            RoomForm._instance.Visible = true;
        }

        private void btnRoomList_Click(object sender, EventArgs e)
        {
            if (!panel3.Controls.Contains(RoomListForm.Instance()))
            {
                panel3.Controls.Add(RoomListForm._instance);
                RoomListForm._instance.Dock = DockStyle.Fill;
                RoomListForm._instance.BringToFront();
                RoomListForm._instance.Visible = true;
            }
            else
            {
                RoomListForm._instance.BringToFront();
                RoomListForm._instance.Visible = true;
            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (!panel3.Controls.Contains(CheckOutForm.Instance()))
            {
                panel3.Controls.Add(CheckOutForm._instance);
                CheckOutForm._instance.Dock = DockStyle.Fill;
                CheckOutForm._instance.BringToFront();
                CheckOutForm._instance.Visible = true;
            }
            CheckOutForm._instance.BringToFront();
            CheckOutForm._instance.Visible = true;
        }

        private void btnCheckOutList_Click(object sender, EventArgs e)
        {
            if (!panel3.Controls.Contains(CheckOutListForm.Instance()))
            {
                panel3.Controls.Add(CheckOutListForm._instance);
                CheckOutListForm._instance.Dock = DockStyle.Fill;
                CheckOutListForm._instance.BringToFront();
                CheckOutListForm._instance.Visible = true;
            }
            CheckOutListForm._instance.BringToFront();
            CheckOutListForm._instance.Visible = true;
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            if (!panel3.Controls.Contains(DiscountForm.Instance()))
            {
                panel3.Controls.Add(DiscountForm._instance);
                DiscountForm._instance.Dock = DockStyle.Fill;
                DiscountForm._instance.BringToFront();
                DiscountForm._instance.Visible = true;
            }
            DiscountForm._instance.BringToFront();
            DiscountForm._instance.Visible = true;
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            if (!panel3.Controls.Contains(RegistrationForm.Instance()))
            {
                panel3.Controls.Add(RegistrationForm._instance);
                RegistrationForm._instance.Dock = DockStyle.Fill;
                RegistrationForm._instance.BringToFront();
                RegistrationForm._instance.Visible = true;
            }
            RegistrationForm._instance.BringToFront();
            RegistrationForm._instance.Visible = true;
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            if (!panel3.Controls.Contains(RoomListForm.Instance()))
            {
                panel3.Controls.Add(RoomListForm._instance);
                RoomListForm._instance.Dock = DockStyle.Fill;
                RoomListForm._instance.BringToFront();
                RoomListForm._instance.Visible = true;
            }
            else
            {
                RoomListForm._instance.BringToFront();
                RoomListForm._instance.Visible = true;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            checkStatus(lblUser.Text);
            if (status == "NV")
            {
                btnRegistration.Visible = false;
                btnRoom.Visible = false;
                ServiceForm sf = ServiceForm.Instance();
                sf.btnAdd.Visible = false;
            }
            else
            {
                btnRegistration.Visible = true;
                btnRoom.Visible = true;
            }
        }

        private void btnReservation_Click(object sender, EventArgs e)
        {
            if (!panel3.Controls.Contains(ReservationListForm.Instance()))
            {
                panel3.Controls.Add(ReservationListForm._instance);
                ReservationListForm._instance.Dock = DockStyle.Fill;
                ReservationListForm._instance.BringToFront();
                ReservationListForm._instance.Visible = true;
            }
            ReservationListForm._instance.BringToFront();
            ReservationListForm._instance.Visible = true;
        }
    }
}
