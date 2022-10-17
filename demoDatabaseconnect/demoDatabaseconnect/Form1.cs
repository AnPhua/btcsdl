using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace demoDatabaseconnect
{
    public partial class Form1 : Form
    {
        SqlConnection con ;
        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=LAPTOP-AUDFFK8M;Initial Catalog=QLyCL;Integrated Security=True");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
  
        
            DataTable dtChatLieu = ProcessDataBase.DocBang("select * from tblChatLieu");
            dataGridView1.DataSource = dtChatLieu;
            //Định dạng dataGrid
            dataGridView1.Columns[0].HeaderText = "Mã chất liệu";//dgvChatLieu
            dataGridView1.Columns[1].HeaderText = "Tên chất liệu";
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 250;
            dataGridView1.BackgroundColor = Color.Black;
            dtChatLieu.Dispose();//Giải phóng bộ nhớ cho DataTable

            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            btnThemMoi.Enabled = true;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtMaCL.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtTenCL.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            txtMaCL.Enabled = false;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = true;
            btnThemMoi.Enabled = false;

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtTenCL.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên chất liệu", "Thông báo",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenCL.Focus();

            }
            else
            {
                ProcessDataBase.CapNhatDuLieu("update tblChatLieu set TenChatLieu=N'" + txtTenCL.Text + "' where MaChatLieu='" + txtMaCL.Text + "'");
                ResetValue();//Xóa dữ liệu ở các ô nhập TextBox
                             //Sau khi update cần lấy lại dữ liệu để hiển thị lên lưới
                dataGridView1.DataSource = ProcessDataBase.DocBang("select * from tblChatLieu");

                btnXoa.Enabled = false;
                btnSua.Enabled = false;
                btnLuu.Enabled = false;
                btnBoQua.Enabled = false;
                btnThemMoi.Enabled = true;
            }

        }
        public void ResetValue()
        {
            txtMaCL.Text = "";
            txtTenCL.Text = "";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa chất liệu có mã là:" +
               txtMaCL.Text + " không?", "Thông báo",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
               System.Windows.Forms.DialogResult.Yes)
            {
                ProcessDataBase.CapNhatDuLieu("delete tblChatLieu where MaChatLieu='" + txtMaCL.Text + "'");
                dataGridView1.DataSource = ProcessDataBase.DocBang("Select * from tblChatLieu");
                ResetValue();
                btnXoa.Enabled = false;
                btnSua.Enabled = false;
                btnLuu.Enabled = false;
                btnBoQua.Enabled = false;
                btnThemMoi.Enabled = true;
            }

        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            ResetValue();
            txtMaCL.Enabled = true;
            txtMaCL.Focus();

            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            btnThemMoi.Enabled = true;

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaCL.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã chất liệu");
                txtMaCL.Focus();
            }
            else
            {
                if (txtTenCL.Text == "")
                {
                    MessageBox.Show("Bạn phải nhập tên chất liệu");
                    txtTenCL.Focus();
                }
                else
                {
                    DataTable dtChatLieu = ProcessDataBase.DocBang("Select * from tblChatLieu where" + " MaChatLieu='" + (txtMaCL.Text).Trim() + "'");
                    if (dtChatLieu.Rows.Count > 0)
                    {
                        MessageBox.Show("Mã chất liệu này đã có, hãy nhập mã khác!");
                        txtMaCL.Focus();
                    }
                    else
                    {
                        ProcessDataBase.CapNhatDuLieu("insert into tblChatLieu values(N'" + txtMaCL.Text + "',N'" + txtTenCL.Text + "')");
                        MessageBox.Show("Bạn đã thêm mới thành công");
                        dataGridView1.DataSource = ProcessDataBase.DocBang("select * from tblChatLieu");
                        ResetValue();
                        btnXoa.Enabled = false;
                        btnSua.Enabled = false;
                        btnLuu.Enabled = false;
                        btnBoQua.Enabled = false;
                        btnThemMoi.Enabled = true;
                    }
                }
            }

        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            btnThemMoi.Enabled = true;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == System.Windows.Forms.DialogResult.Yes)
            {
                if (btnLuu.Enabled == true)
                {
                    if (MessageBox.Show("Bạn có muốn Lưu lại chất liệu đang thêm mới không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        btnLuu_Click(sender, e);
                    else
                        this.Close();

                }
                else
                    this.Close();
            }

        }
    }
}
