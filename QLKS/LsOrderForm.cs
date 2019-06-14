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
    public partial class LsOrderForm : UserControl
    {
        protected internal static LsOrderForm _instance;
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
        private Random random = new Random();
        private DataTable dtable;
        private SqlTransaction tran;
        private SqlConnection con = null;
        private SqlDataAdapter adp;
        private DataSet ds;
        private SqlCommand cmd = null;
        private DataTable dt = new DataTable();
        private string cs = "Data Source=DESKTOP-ASUKO;Initial Catalog=QLKS;Integrated Security=True";

        private ListViewColumnSorter lvwColumnSorter;
        public LsOrderForm()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewColumnSorter();
            this.listViewSV.ListViewItemSorter = lvwColumnSorter;
        }
        protected internal static LsOrderForm Instance()
        {
            if (_instance == null)
            {
                _instance = new LsOrderForm();
            }
            return _instance;
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

        private void listViewSV_DoubleClick(object sender, EventArgs e)
        {
            PopUpFormAdvance pop = PopUpFormAdvance.Intance();
            pop.lblID.Text = listViewSV.SelectedItems[0].Text;
            pop.txtNumber.Text = listViewSV.SelectedItems[0].SubItems[3].Text;
            pop.lblForm.Text = this.ToString();
            pop.ShowDialog();
        }

        private void LsItemForm_Load(object sender, EventArgs e)
        {
            loadDSDV();
        }
        public void loadDSDV()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    //LẤY CMND KHÁCH HÀNG
                    cmd = new SqlCommand("SELECT CMND FROM GIAO_DICH WHERE MaPhong=@maphong AND TrangThai=@trangthai AND LoaiDK=@loaidk", con);
                    cmd.Parameters.AddWithValue("@maphong", ServiceForm.Instance().lblRoom.Text);
                    cmd.Parameters.AddWithValue("@trangthai", "Active");
                    cmd.Parameters.AddWithValue("@loaidk", "0");
                    rdr = cmd.ExecuteReader();
                    string cmnd = "";
                    if (rdr.Read())
                    {
                        cmnd = rdr["CMND"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy CMND khách hàng");
                        return;
                    }
                    rdr.Close();

                    //LẤY ID HOÁ ĐƠN
                    cmd = new SqlCommand("SELECT idHD FROM HOA_DON WHERE CMND=@cmnd AND MaPhong=@maphong ORDER BY NgayLap DESC", con);
                    cmd.Parameters.AddWithValue("@cmnd", cmnd);
                    cmd.Parameters.AddWithValue("@maphong", ServiceForm.Instance().lblRoom.Text);
                    string idhd = "";
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        idhd = rdr["idHD"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy hoá đơn!!!");
                        return;
                    }
                    rdr.Close();

                    cmd = new SqlCommand("SELECT A.idDV,A.LoaiDV,A.TenDV,A.Gia,B.SoLuong FROM DICH_VU A, DS_DICH_VU B WHERE A.idDV=B.idDV AND B.idHD=@idhd", con);
                    cmd.Parameters.AddWithValue("@idhd", idhd);
                    dt = new DataTable("DICH_VU");
                    adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    adp.Dispose();
                    listViewSV.Items.Clear();
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ListViewItem lvi = new ListViewItem(row["idDV"].ToString());
                            lvi.SubItems.Add(row["TenDV"].ToString());
                            lvi.SubItems.Add(row["Gia"].ToString());
                            lvi.SubItems.Add(row["SoLuong"].ToString());
                            listViewSV.Items.Add(lvi);
                        }
                    }
                    else return;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            finally { con.Close(); }
        }
        public void removeItemSQL(string id)
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    using (tran = con.BeginTransaction(IsolationLevel.Serializable))
                    {
                        try
                        {
                            //LẤY CMND KHÁCH HÀNG
                            cmd = new SqlCommand("SELECT CMND FROM GIAO_DICH WHERE MaPhong=@maphong AND TrangThai=@trangthai AND LoaiDK=@loaidk", con, tran);
                            cmd.Parameters.AddWithValue("@maphong", ServiceForm.Instance().lblRoom.Text);
                            cmd.Parameters.AddWithValue("@trangthai", "Active");
                            cmd.Parameters.AddWithValue("@loaidk", 0);
                            rdr = cmd.ExecuteReader();
                            string cmnd = "";
                            if (rdr.Read())
                            {
                                cmnd = rdr["CMND"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy CMND khách hàng");
                                return;
                            }
                            rdr.Close();
                            //LẤY ID HOÁ ĐƠN
                            cmd = new SqlCommand("SELECT idHD FROM HOA_DON WHERE CMND=@cmnd AND MaPhong=@maphong ORDER BY NgayLap DESC", con, tran);
                            cmd.Parameters.AddWithValue("@cmnd", cmnd);
                            cmd.Parameters.AddWithValue("@maphong", ServiceForm.Instance().lblRoom.Text);
                            rdr = cmd.ExecuteReader();
                            string idhd = "";
                            if (rdr.Read())
                            {
                                idhd = rdr["idHD"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy hoá đơn!!!");
                                return;
                            }
                            rdr.Close();

                            cmd = new SqlCommand("DELETE FROM DS_DICH_VU WHERE idHD=@idhd AND idDV=@iddv", con, tran);
                            cmd.Parameters.AddWithValue("@idHD", idhd);
                            cmd.Parameters.AddWithValue("@idDV", id);
                            tran.Save("deleteDSDV");
                            cmd.ExecuteNonQuery();
                            tran.Commit();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

    }
}
