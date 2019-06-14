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
    public partial class ReservationListForm : UserControl
    {
        protected internal static ReservationListForm _instance;
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
        private string tempRoomType;
        private string tempRoomOccu;
        private string tempRoomCurr;
        private ListViewColumnSorter lvwColumnSorter;
        private SqlDataReader rdr = null;
        private DataTable dtable;
        private SqlConnection con = null;
        private SqlTransaction tran;
        private SqlDataAdapter adp;
        private DataSet ds;
        SqlCommand cmd = null;
        private DataTable dt = new DataTable();
        private string cs = "Data Source=DESKTOP-ASUKO;Initial Catalog=QLKS;Integrated Security=True";
        public ReservationListForm()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewColumnSorter();
            this.listView1.ListViewItemSorter = lvwColumnSorter;
        }
        protected internal static ReservationListForm Instance()
        {
            if (_instance == null)
            {
                _instance = new ReservationListForm();
            }
            return _instance;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            load_ReservationList();
        }

        private void ReservationListForm_Load(object sender, EventArgs e)
        {
            load_ReservationList();
        }

        public void load_ReservationList()
        {
            try
            {
                //con = new SqlConnection(cs);
                //con.Open();
                //dt = new DataTable("DAT_TRUOC");
                //adp = new SqlDataAdapter("SELECT * FROM DAT_TRUOC", con);
                //adp.Fill(dt);
                //listView1.Items.Clear();
                //foreach (DataRow row in dt.Rows)
                //{
                //    ListViewItem lvi = new ListViewItem(row["idDT"].ToString());
                //    lvi.SubItems.Add(row["tenKH"].ToString());
                //    lvi.SubItems.Add(row["CMND"].ToString());
                //    lvi.SubItems.Add(row["MaPhong"].ToString());
                //    lvi.SubItems.Add(row["NgayDat"].ToString());
                //    lvi.SubItems.Add(row["GioHen"].ToString());
                //    lvi.SubItems.Add(row["SoNguoi"].ToString());
                //    lvi.SubItems.Add(row["TrangThai"].ToString());
                //    listView1.Items.Add(lvi);
                //}
                //adp.Dispose();
                //con.Close();
                listView1.Items.Clear();
                dt = load_dataTable();
                foreach (DataRow row in dt.Rows)
                {
                    addItemToList(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
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
            this.listView1.Sort();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            int temp = checkStatus(this.listView1.SelectedItems[0].SubItems[7].Text.ToString());
            //0-Đang đặt 1-HUỶ 2-Đã Checkin
            if (temp == 0)
            {
                DialogResult dr = MessageBox.Show("Bạn có muốn CHECKIN ?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    HomeForm hf = HomeForm.Instance();
                    CheckinForm cf = CheckinForm.Instance();
                    if (!hf.panel3.Controls.Contains(cf))
                    {
                        hf.panel3.Controls.Add(cf);
                        cf.Dock = DockStyle.Fill;
                        cf.BringToFront();
                        cf.Visible = true;

                        getRoomData(listView1.SelectedItems[0].SubItems[4].Text.ToString());

                        cf.lblRoom.Text = listView1.SelectedItems[0].SubItems[4].Text.ToString();
                        cf.txtCMND.Text = listView1.SelectedItems[0].SubItems[3].Text.ToString();
                        cf.txtLastName.Text = listView1.SelectedItems[0].SubItems[1].Text.ToString();
                        cf.txtFirstName.Text = listView1.SelectedItems[0].SubItems[2].Text.ToString();
                        cf.lblOccupancy.Text = tempRoomOccu.ToString();
                        cf.lblCurrentPeopleCount.Text = tempRoomCurr.ToString();
                        cf.lblRoomType.Text = tempRoomType;

                        this.Hide();
                    }
                    else
                    {
                        cf.BringToFront();
                        cf.Visible = true;


                        getRoomData(listView1.SelectedItems[0].SubItems[4].Text.ToString());
                        cf.lblRoom.Text = listView1.SelectedItems[0].SubItems[4].Text.ToString();
                        cf.txtCMND.Text = listView1.SelectedItems[0].SubItems[3].Text.ToString();
                        cf.txtLastName.Text = listView1.SelectedItems[0].SubItems[1].Text.ToString();
                        cf.txtFirstName.Text = listView1.SelectedItems[0].SubItems[2].Text.ToString();
                        cf.lblOccupancy.Text = tempRoomOccu.ToString();
                        cf.lblCurrentPeopleCount.Text = tempRoomCurr.ToString();
                        cf.lblRoomType.Text = tempRoomType;

                        this.Hide();
                    }
                }
                else if (dr == DialogResult.No)
                {
                    DialogResult dialog = MessageBox.Show("Bạn muốn HUỶ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialog == DialogResult.Yes)
                    {
                        cancel_Reservation(listView1.SelectedItems[0].SubItems[4].Text.ToString(), listView1.SelectedItems[0].SubItems[3].Text.ToString());
                        RoomListForm.Instance().load_RoomList();
                        load_ReservationList();
                        GuestListForm.Instance().load_GuestList();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            else if (temp == 1) //HUỶ
            {
                MessageBox.Show("Khách hàng đã huỷ ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else // Đã checkin
            {
                MessageBox.Show("Khách hàng đã CHECKIN ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public void cancel_Reservation(string room, string CMND)
        {
            using (con = new SqlConnection(cs))
            {
                con.Open();
                using (tran = con.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        cmd = new SqlCommand("SELECT TrangThai FROM DAT_TRUOC WHERE MaPhong=@maphong AND CMND=@cmnd", con, tran);
                        cmd.Parameters.AddWithValue("@maphong", room);
                        cmd.Parameters.AddWithValue("@cmnd", CMND);
                        rdr = cmd.ExecuteReader();
                        string state = "";
                        while (rdr.Read())
                        {
                            state = rdr["TrangThai"].ToString();
                        }
                        rdr.Close();
                        if (state.Equals("HUỶ") || state.Equals("Đang đặt"))
                        {
                            cmd = new SqlCommand("DELETE FROM DAT_TRUOC WHERE MaPhong=@find AND CMND=@cmnd", con, tran);
                            cmd.Parameters.AddWithValue("@find", room);
                            cmd.Parameters.AddWithValue("@cmnd", CMND);
                            cmd.ExecuteNonQuery();

                            cmd = new SqlCommand("UPDATE PHONG SET TinhTrang=@d1 WHERE MaPhong=@find", con, tran);
                            cmd.Parameters.AddWithValue("@find", room);
                            cmd.Parameters.AddWithValue("@d1", "Trống");
                            cmd.ExecuteNonQuery();

                            cmd = new SqlCommand("UPDATE KHACH_HANG SET TrangThai=@d1 WHERE CMND=@cmnd", con, tran);
                            cmd.Parameters.AddWithValue("@cmnd", CMND);
                            cmd.Parameters.AddWithValue("@d1", "");
                            cmd.ExecuteNonQuery();
                        }
                        else if (state.Equals("Checkin") || state.Equals("Checkout"))
                        {
                            cmd = new SqlCommand("DELETE FROM DAT_TRUOC WHERE MaPhong=@find AND CMND=@cmnd", con, tran);
                            cmd.Parameters.AddWithValue("@find", room);
                            cmd.Parameters.AddWithValue("@cmnd", CMND);
                            cmd.ExecuteNonQuery();
                        }
                      


                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (tran != null)
                        {
                            tran.Rollback();
                        }
                        MessageBox.Show(ex.GetType() + "\n");
                        MessageBox.Show(ex.StackTrace);
                        throw;
                    }
                    finally
                    {

                        con.Close();
                    }
                }
            }
        }

        private int checkStatus(string temp) //0-Đang đặt 1-HUỶ 2-Đã Checkin
        {

            if (temp.Equals("HUỶ"))
            {
                return 1;
            }
            else if (temp.Equals("Đã Checkin"))
            {
                return 2;
            }
            else
                return 0;
        }

        private void getRoomData(string room)
        {
            try
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("SELECT * FROM PHONG WHERE MaPhong=@find", con);
                cmd.Parameters.AddWithValue("@find", room);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    tempRoomType = rdr["LoaiPhong"].ToString();
                    tempRoomCurr = rdr["SoNguoiHienCo"].ToString();
                    tempRoomOccu = rdr["GioiHan"].ToString();
                }
                rdr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            addItem();
        }
        private void addItem()
        {
            listView1.Items.Clear(); //Xoá dữ liệu trong list view
            dt = load_dataTable();
            foreach (DataRow row in dt.Rows)
            {
                var dateTime = "";
                if (!String.IsNullOrEmpty(row["NgayDat"].ToString()))
                {
                    dateTime = Convert.ToDateTime(row["NgayDat"].ToString()).ToString("dd-MM-yyyy");
                }
                //If the cell value is start with the value in the TextBox
                if (row["HoDem"].ToString().StartsWith(txtSearch.Text))
                {
                    addItemToList(row);
                }
                else if (row["Ten"].ToString().StartsWith(txtSearch.Text))
                {
                    addItemToList(row);
                }
                else if (row["CMND"].ToString().StartsWith(txtSearch.Text))
                {
                    addItemToList(row);
                }
                else if (row["MaPhong"].ToString().StartsWith(txtSearch.Text))
                {
                    addItemToList(row);
                }
                else if (dateTime.StartsWith(txtSearch.Text))
                {
                    addItemToList(row);
                }
                else if (row["GioHen"].ToString().StartsWith(txtSearch.Text))
                {
                    addItemToList(row);
                }
                else if (row["TrangThai"].ToString().StartsWith(txtSearch.Text))
                {
                    addItemToList(row);
                }

            }

        }

        private void addItemToList(DataRow row)
        {
            var dateTime = "";
            if (!String.IsNullOrEmpty(row["NgayDat"].ToString()))
            {
                dateTime = Convert.ToDateTime(row["NgayDat"].ToString()).ToString("dd-MM-yyyy");
            }

            ListViewItem lvi = new ListViewItem(row["idDT"].ToString());
            lvi.SubItems.Add(row["HoDem"].ToString());
            lvi.SubItems.Add(row["Ten"].ToString());
            lvi.SubItems.Add(row["CMND"].ToString());
            lvi.SubItems.Add(row["MaPhong"].ToString());
            lvi.SubItems.Add(dateTime.ToString());
            lvi.SubItems.Add(row["GioHen"].ToString());
            lvi.SubItems.Add(row["SoNguoi"].ToString());
            lvi.SubItems.Add(row["TrangThai"].ToString());
            listView1.Items.Add(lvi);
        }

        private DataTable load_dataTable()
        {
            con = new SqlConnection(cs);
            con.Open();
            DataTable data = new DataTable("DAT_TRUOC");
            adp = new SqlDataAdapter("SELECT * FROM DAT_TRUOC", con);
            adp.Fill(data);
            adp.Dispose();
            con.Close();
            return data;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            load_ReservationList();
        }
    }
}
