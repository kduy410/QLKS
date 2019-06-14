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
    public partial class SearchServiceForm : Form
    {
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
        public SearchServiceForm()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewColumnSorter();
            this.listView1.ListViewItemSorter = lvwColumnSorter;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void display_Service()
        {
            try
            {
                con = new SqlConnection(cs);
                con.Open();
                dt = new DataTable("DICH_VU");
                adp = new SqlDataAdapter("SELECT * FROM DICH_VU ", con);
                adp.Fill(dt);
                listView1.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    ListViewItem lvi = new ListViewItem(row["idDV"].ToString());
                    lvi.SubItems.Add(checkSVT(row["LoaiDV"].ToString()));
                    lvi.SubItems.Add(row["TenDV"].ToString());
                    lvi.SubItems.Add(row["Gia"].ToString());
                    listView1.Items.Add(lvi);
                }
                adp.Dispose();
                con.Close();
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string checkSVT(string v)
        {
            if (v.Equals("0"))
            {
                return "ĐỒ ĂN";
            }
            else if (v.Equals("1"))
            {
                return "THỨC UỐNG";
            }
            else if (v.Equals("2"))
            {
                return "DỊCH VỤ";
            }
            else return null;
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
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xoá dịch vụ này ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes) //ĐỒNG Ý XOÁ
            {
                if (delete_DV(listView1.SelectedItems[0].Text))
                {
                    display_Service();
                    MessageBox.Show("Xoá thành công", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }
            else //KHÔNG ĐỒNG Ý
            {
                return;
            }
        }

        private bool delete_DV(string item)
        {
            using (con = new SqlConnection(cs))
            {
                con.Open();
                using (tran = con.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        cmd = new SqlCommand("DELETE FROM DICH_VU WHERE idDV=@find", con, tran);
                        cmd.Parameters.AddWithValue("@find", int.Parse(item));
                        cmd.ExecuteNonQuery();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể xoá, Dịch vụ này đang sử dụng, Hãy chờ đến khi dịch vụ hết sử dụng");
                        if (tran != null)
                        {
                            tran.Rollback();
                        }
                        //MessageBox.Show(ex.GetType().ToString() + "\n");
                        //MessageBox.Show(ex.StackTrace + "\n");
                        return false;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            return true;
        }
        
        private void SearchServiceForm_Load(object sender, EventArgs e)
        {
            display_Service();
        }
    }
}
