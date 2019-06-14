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
    public partial class ServiceCreateBoxForm : Form
    {
        private SqlDataReader rdr = null;
        private Random random = new Random();
        private DataTable dtable;
        private SqlConnection con = null;
        private SqlTransaction tran;
        private SqlDataAdapter adp;
        private DataSet ds;
        private SqlCommand cmd = null;
        private DataTable dt = new DataTable();
        private string cs = "Data Source=DESKTOP-ASUKO;Initial Catalog=QLKS;Integrated Security=True";
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
        protected internal static ServiceCreateBoxForm _instance;
        protected internal static ServiceCreateBoxForm Instance()
        {
            if (_instance == null)
            {
                _instance = new ServiceCreateBoxForm();
            }
            return _instance;
        }

        public ServiceCreateBoxForm()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbServiceType.Text == "" || txtPrice.Text == "" || txtServiceName.Text == "")
                {
                    MessageBox.Show("Xin hãy nhập các ô trống", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtServiceName.Focus();
                    return;
                }
                else if (Double.TryParse(txtPrice.Text.ToString(), out double x) == false)
                {
                    MessageBox.Show("Xin hãy nhập số!!!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Focus();
                    return;
                }
                if (checkServiceName(txtServiceName.Text.ToString())) //KT CÓ TÊN DV ===> ĐÃ CÓ 
                {
                    DialogResult dialogResult = MessageBox.Show("Bạn có muốn cập nhật dịch vụ này ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes) //ĐỒNG Ý CẬP NHẬT
                    {
                        if (update_DV())
                        {
                            cbServiceType.Text = "";
                            txtServiceName.Text = "";
                            txtPrice.Text = "";

                            MessageBox.Show("Cập nhật thành công", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ServiceForm sf = ServiceForm._instance;
                            sf.loadDV();
                        }
                        else
                        {
                            return;
                        }

                    }
                    else //KHÔNG ĐỒNG Ý
                    {
                        return;
                    }
                }
                else //CHƯA CÓ TÊN DV ĐÓ
                {
                    if (createService())
                    {
                        MessageBox.Show("Tạo thành công", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbServiceType.Text = "";
                        txtServiceName.Text = "";
                        txtPrice.Text = "";
                        con.Close();
                        //ServiceForm sf = ServiceForm._instance;
                        //sf.displayData();
                    }
                    else return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool createService()
        {
            try
            {
                int temp = checkServiceType();
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("INSERT INTO DICH_VU(LoaiDV,TenDV,Gia) VALUES(@d1,@d2,@d3)", con);
                cmd.Parameters.AddWithValue("@d1", temp);
                cmd.Parameters.AddWithValue("@d2", txtServiceName.Text.ToString());
                cmd.Parameters.AddWithValue("@d3", stringToPrice());
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

        }

        private string stringToPrice()
        {
            try
            {
                return Math.Round(Decimal.Parse(txtPrice.Text), 1, MidpointRounding.AwayFromZero).ToString("F3");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private int checkServiceType()
        {
            if (cbServiceType.Text.Equals("ĐỒ ĂN"))
            {
                return 0;
            }
            else if (cbServiceType.Text.Equals("THỨC UỐNG"))
            {
                return 1;
            }
            else if (cbServiceType.Text.Equals("ĐỒ DÙNG"))
            {
                return 2;
            }
            return -1;
        }

        private bool checkServiceName(string name)
        {
            con = new SqlConnection(cs);
            con.Open();
            cmd = new SqlCommand("SELECT idDV,TenDV FROM DICH_VU WHERE TenDV=@find", con);
            cmd.Parameters.Add(new SqlParameter("@find", SqlDbType.NVarChar, 50)).Value = name;
            rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                MessageBox.Show("Tên dịch vụ đã có", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtIDDV.Text = rdr["idDV"].ToString();
                rdr.Close();
                return true;
            }
            else
            {
                rdr.Close();
                return false;
            }
        }

        private bool update_DV()
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
                            if (txtIDDV.Text.Length == 0)
                            {
                                MessageBox.Show("Không có mã dịch vụ");
                                tran.Rollback();
                                return false;
                            }
                            else
                            {
                                cmd = new SqlCommand("UPDATE DICH_VU SET LoaiDV=@loai,Gia=@gia WHERE TenDV=@ten AND idDV=@id", con, tran);
                                cmd.Parameters.AddWithValue("@loai", checkServiceType());
                                cmd.Parameters.AddWithValue("@ten", txtServiceName.Text);
                                cmd.Parameters.AddWithValue("@gia", stringToPrice());
                                cmd.Parameters.AddWithValue("@id", txtIDDV.Text.Trim());
                                cmd.ExecuteNonQuery();
                                tran.Commit();
                            }

                        }
                        catch (Exception ex)
                        {
                            if (tran != null)
                            {
                                tran.Rollback();
                                return false;
                            }
                            MessageBox.Show(ex.GetType().ToString() + "\n");
                            MessageBox.Show(ex.StackTrace.ToString());
                        }
                        finally
                        {
                            con.Close();
                        }
                        return true;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            SearchServiceForm ssf = new SearchServiceForm();
            ssf.ShowDialog();
        }

        private void loadSV()
        {
            try
            {
                con = new SqlConnection(cs);
                cmd = new SqlCommand("SELECT idDV,TenDV FROM DICH_VU", con);
                con.Open();
                rdr = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection1 = new AutoCompleteStringCollection();
                AutoCompleteStringCollection MyCollection2 = new AutoCompleteStringCollection();
                while (rdr.Read())
                {
                    MyCollection1.Add(rdr.GetInt32(0).ToString());
                    MyCollection2.Add(rdr.GetString(1));
                }
                txtServiceName.AutoCompleteCustomSource = MyCollection2;
                txtIDDV.AutoCompleteCustomSource = MyCollection1;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void ServiceCreateBoxForm_Load(object sender, EventArgs e)
        {
            loadSV();
        }

        private void txtPrice_TextChanged(object sender, KeyPressEventArgs e)
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

        private void txtIDDV_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
      (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
