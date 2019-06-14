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
    public partial class RoomOption : Form
    {
        private SqlDataReader rdr = null;
        private DataTable dtable;
        private SqlConnection con = null;
        private SqlDataAdapter adp;
        private DataSet ds;
        SqlCommand cmd = null;
        private DataTable dt = new DataTable();
        private string cs = "Data Source=DESKTOP-ASUKO;Initial Catalog=QLKS;Integrated Security=True";
        public RoomOption()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCheckin_Click(object sender, EventArgs e)
        {
            HomeForm hf = HomeForm.Instance();
            CheckinForm cf = CheckinForm.Instance();
            if (!hf.panel3.Controls.Contains(cf))
            {
                hf.panel3.Controls.Add(cf);
                cf.Dock = DockStyle.Fill;
                cf.BringToFront();
                cf.Visible = true;

                cf.lblRoom.Text = lblRoom.Text.ToString();
                cf.lblRoomType.Text = lblRoomType.Text.ToString();

                cf.lblOccupancy.Text = lblPCount.Text.ToString();
                cf.lblCurrentPeopleCount.Text = lblCurrentP.Text.ToString();

                checkReserve();

                this.Hide();
            }
            else
            {
                cf.BringToFront();
                cf.Visible = true;
                cf.lblRoom.Text = lblRoom.Text.ToString();
                cf.lblRoomType.Text = lblRoomType.Text.ToString();

                cf.lblOccupancy.Text = lblPCount.Text.ToString();
                cf.lblCurrentPeopleCount.Text = lblCurrentP.Text.ToString();
                checkReserve();

                this.Hide();
            }
        }

        private void checkReserve()
        {
            try
            {
                using (con = new SqlConnection(cs))
                using (cmd = new SqlCommand("SELECT HoDem,Ten,CMND FROM DAT_TRUOC WHERE MaPhong=@maphong AND TrangThai=@state", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@maphong", lblRoom.Text);
                    cmd.Parameters.AddWithValue("@state", "Đang đặt");
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        CheckinForm.Instance().txtLastName.Text = rdr["HoDem"].ToString().Trim();
                        CheckinForm.Instance().txtFirstName.Text = rdr["Ten"].ToString().Trim();
                        CheckinForm.Instance().txtCMND.Text = rdr["CMND"].ToString().Trim();
                    }
                    else
                    {
                        CheckinForm.Instance().txtLastName.Text = "";
                        CheckinForm.Instance().txtFirstName.Text = "";
                        CheckinForm.Instance().txtCMND.Text = "";
                    }
                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            lblCurrentP.Visible = false;
            lblPCount.Visible = false;

            lblRoom.Visible = false;
            lblRoomType.Visible = false;



        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            int check = checkRoomState();
            if (check == 1)//Đang sử dụng -> checkout
            {
                HomeForm hf = HomeForm.Instance();
                CheckOutForm cof = CheckOutForm.Instance();
                if (!hf.panel3.Controls.Contains(cof))
                {
                    hf.panel3.Controls.Add(cof);
                    cof.Dock = DockStyle.Fill;
                    cof.BringToFront();
                    cof.Visible = true;


                    cof.lblRoom.Text = lblRoom.Text.ToString();
                    cof.lblRoomType.Text = lblRoomType.Text.ToString();


                    cof.lblOccupancy.Text = lblPCount.Text.ToString();
                    cof.lblCurrentPeopleCount.Text = lblCurrentP.Text.ToString();

                    CheckOutForm._instance.getCheckinDay();
                    CheckOutForm._instance.getTransID();
                    CheckOutForm._instance.load_All();
                    CheckOutForm._instance.txtCASH.Clear();
                    CheckOutForm._instance.txtChanges.Clear();

                    this.Hide();
                }
                else
                {
                    cof.BringToFront();
                    cof.Visible = true;
                    cof.lblRoom.Text = lblRoom.Text.ToString();
                    cof.lblRoomType.Text = lblRoomType.Text.ToString();

                    cof.lblOccupancy.Text = lblPCount.Text.ToString();
                    cof.lblCurrentPeopleCount.Text = lblCurrentP.Text.ToString();

                    CheckOutForm._instance.getCheckinDay();
                    CheckOutForm._instance.getTransID();
                    CheckOutForm._instance.load_All();
                    CheckOutForm._instance.txtCASH.Clear();
                    CheckOutForm._instance.txtChanges.Clear();
                    this.Hide();
                }
            }
            else if (check == 2) //đã đặt trước ->không được checkout
            {
                MessageBox.Show("Phòng đã đặt trước!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Phòng trống!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnServices_Click(object sender, EventArgs e)
        {
            int check = checkRoomState();
            if (check == 1) //đang sử dụng -> sử dụng dịch vụ
            {
                HomeForm hf = HomeForm.Instance();
                ServiceForm sf = ServiceForm.Instance();
                if (!hf.panel3.Controls.Contains(sf))
                {
                    hf.panel3.Controls.Add(sf);
                    sf.Dock = DockStyle.Fill;
                    sf.BringToFront();
                    sf.Visible = true;
                    sf.lblRoom.Text = lblRoom.Text;
                    sf.loadDV();
                    this.Hide();
                }
                else
                {

                    sf.BringToFront();
                    sf.Visible = true;
                    sf.lblRoom.Text = lblRoom.Text;
                    sf.loadDV();
                    this.Hide();
                }
            }
            else if (check == 2) //phòng đặt trước không được sử dụng dịch vụ cho đến khi khách checkin
            {
                MessageBox.Show("Phòng đang đặt trước!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Phòng trống!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnReserve_Click(object sender, EventArgs e)
        {
            int check = checkRoomState();
            if (check != 2) //Chưa đặt trước
            {
                if (check != 1) //trống
                {
                    HomeForm hf = HomeForm.Instance();
                    ReservationForm rf = ReservationForm.Instance();
                    if (!hf.panel3.Controls.Contains(rf))
                    {
                        hf.panel3.Controls.Add(rf);
                        rf.Dock = DockStyle.Fill;
                        rf.BringToFront();
                        rf.Visible = true;

                        rf.lblRoom.Text = lblRoom.Text;
                        rf.lblRoomType.Text = lblRoomType.Text;
                        rf.lblOccu.Text = lblPCount.Text;

                        this.Hide();
                    }
                    else
                    {
                        rf.BringToFront();
                        rf.Visible = true;
                        rf.lblRoom.Text = lblRoom.Text;
                        rf.lblRoomType.Text = lblRoomType.Text;
                        rf.lblOccu.Text = lblPCount.Text;
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Phòng đang được sử dụng", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Phòng đã được đặt truóc", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult dr = MessageBox.Show("Bạn có muốn huỷ đặt trước", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    cancel_Reservation(lblRoom.Text);
                    resetRoomState();
                    RoomListForm.Instance().load_RoomList();
                    ReservationListForm.Instance().load_ReservationList();
                }
                return;
            }
        }

        private void resetRoomState()
        {
            try
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("UPDATE PHONG SET TinhTrang=@d1 WHERE MaPhong=@find", con);
                cmd.Parameters.AddWithValue("@find", lblRoom.Text);
                cmd.Parameters.AddWithValue("@d1", "Trống");
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                throw;
            }
        }

        public void cancel_Reservation(string room)
        {
            try
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("UPDATE DAT_TRUOC SET TrangThai=@d1 WHERE MaPhong=@find", con);
                cmd.Parameters.AddWithValue("@find", room);
                cmd.Parameters.AddWithValue("@d1", "HUỶ");
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                throw;
            }
        }

        //private bool checkRoomAvailable()
        //{
        //    try
        //    {
        //        con = new SqlConnection(cs);
        //        con.Open();
        //        cmd = new SqlCommand("SELECT TinhTrang FROM PHONG WHERE MaPhong=@find", con);
        //        cmd.Parameters.Add(new SqlParameter("@find", SqlDbType.NVarChar, 10, "MaPhong")).Value = lblRoom.Text.ToString();
        //        rdr = cmd.ExecuteReader();
        //        if (rdr.Read())
        //        {
        //            if (rdr["TinhTrang"].ToString().Equals("Đang sử dụng"))
        //            {
        //                rdr.Close();
        //                con.Close();
        //                return false;
        //            }
        //        }
        //        rdr.Close();
        //        con.Close();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        throw;
        //    }
        //}
        //private bool checkRoomReserve()
        //{
        //    try
        //    {
        //        con = new SqlConnection(cs);
        //        con.Open();
        //        cmd = new SqlCommand("SELECT TinhTrang FROM PHONG WHERE MaPhong=@find", con);
        //        cmd.Parameters.Add(new SqlParameter("@find", SqlDbType.NVarChar, 10, "MaPhong")).Value = lblRoom.Text.ToString();
        //        rdr = cmd.ExecuteReader();
        //        if (rdr.Read())
        //        {
        //            if (rdr["TinhTrang"].ToString().Equals("Đặt trước"))
        //            {
        //                rdr.Close();
        //                con.Close();
        //                return false;
        //            }
        //        }
        //        rdr.Close();
        //        con.Close();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        throw;
        //    }
        //}
        private int checkRoomState() //0-Trống 1-Đang sử dụng 2-Đặt trước
        {
            try
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("SELECT TinhTrang FROM PHONG WHERE MaPhong=@find", con);
                cmd.Parameters.Add(new SqlParameter("@find", SqlDbType.NVarChar, 10, "MaPhong")).Value = lblRoom.Text.ToString();
                rdr = cmd.ExecuteReader();
                string state = "";
                int i = -1;
                if (rdr.Read())
                {
                    state = rdr["TinhTrang"].ToString();
                }
                if (state.Equals("Trống"))
                {
                    i = 0;
                }
                else if (state.Equals("Đang sử dụng"))
                {
                    i = 1;
                }
                else i = 2;
                rdr.Close();
                con.Close();
                return i;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                throw;
            }
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            HomeForm hf = HomeForm.Instance();
            StateForm cf = StateForm.Instance();
            if (!hf.panel3.Controls.Contains(cf))
            {
                hf.panel3.Controls.Add(cf);
                cf.Dock = DockStyle.Fill;
                cf.BringToFront();
                cf.Visible = true;

                cf.lblRoom.Text = lblRoom.Text;
                cf.lblRoomType.Text = lblRoomType.Text;
                cf.load_listGuest();
                cf.load_all();

                this.Hide();
            }
            else
            {
                cf.lblRoom.Text = lblRoom.Text;
                cf.lblRoomType.Text = lblRoomType.Text;
                cf.load_listGuest();
                cf.load_all();

                cf.BringToFront();
                cf.Visible = true;

                this.Hide();
            }
        }
    }
}

