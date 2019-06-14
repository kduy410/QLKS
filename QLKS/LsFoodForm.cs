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
    public partial class LsFoodForm : UserControl
    {
        protected internal static LsFoodForm _instance;
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
        private SqlConnection con = null;
        private SqlDataAdapter adp;
        private DataSet ds;
        private SqlCommand cmd = null;
        private DataTable dt = new DataTable();
        private string cs = "Data Source=DESKTOP-ASUKO;Initial Catalog=QLKS;Integrated Security=True";

        private ListViewColumnSorter lvwColumnSorter;
        public LsFoodForm()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewColumnSorter();
            this.listViewSV.ListViewItemSorter = lvwColumnSorter;
        }
        protected internal static LsFoodForm Instance()
        {
            if (_instance == null)
            {
                _instance = new LsFoodForm();
            }
            return _instance;
        }
        private DataTable loadData(int type)
        {
            try
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("SELECT * FROM DICH_VU WHERE LoaiDV=@type", con);
                cmd.Parameters.AddWithValue("@type", type);
                adp = new SqlDataAdapter(cmd);
                dt = new DataTable("DICH_VU");
                adp.Fill(dt);
                adp.Dispose();
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
        public void loadListType(int type)
        {
            listViewSV.Items.Clear();
            dt = loadData(type);
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem lvi = new ListViewItem(row["idDV"].ToString());
                lvi.SubItems.Add(row["TenDV"].ToString());
                lvi.SubItems.Add(row["Gia"].ToString());
                lvi.SubItems.Add("0");
                listViewSV.Items.Add(lvi);
            }
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
            PopupForm pf = PopupForm.Intance();
            pf.lblID.Text = listViewSV.SelectedItems[0].Text;
            pf.txtNumber.Text = listViewSV.SelectedItems[0].SubItems[3].Text;
            pf.lblForm.Text = this.ToString();
            pf.ShowDialog();
        }

        public void LsFoodForm_Load(object sender, EventArgs e)
        {
            loadListType(0);
        }
    }
}
