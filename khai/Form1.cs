using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace khai
{
    public partial class Form1 : Form
    {
        private DataTable dataTable;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            {
                // Tạo DataTable và thêm cột
                dataTable = new DataTable();
                dataTable.Columns.Add("masp", typeof(string));
                dataTable.Columns.Add("tensp", typeof(string));
                dataTable.Columns.Add("ngaynhap", typeof(DateTime));
                dataTable.Columns.Add("loaisp", typeof(string));

                // Hiển thị dữ liệu lên DataGridView
                dataGridView1.DataSource = dataTable;
            }
        }

        private void btthoat_Click(object sender, EventArgs e)
        {
            {
                // Hiển thị hộp thoại cảnh báo hỏi đáp trước khi thoát
                DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Kiểm tra người dùng đã chọn Yes (Có) hay No (Không)
                if (result == DialogResult.Yes)
                {
                    // Nếu người dùng chọn Yes, thoát chương trình
                    Application.Exit();
                }
                // Nếu người dùng chọn No, không làm gì cả (và chương trình tiếp tục chạy)
            }
        }

        private void btluu_Click(object sender, EventArgs e)
        {
            {

                DialogResult result = MessageBox.Show("Bạn có chắc muốn lưu dữ liệu?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (result == DialogResult.Yes)
                {

                    Application.Exit();
                }

            }
        }

        private void btthem_Click(object sender, EventArgs e)
        {
            {
                // Lấy thông tin từ các điều khiển nhập liệu
                string masp = txtMaSP.Text;
                string tensp = txtTenSP.Text;
                DateTime ngaynhap = dtpNgayNhap.Value;
                string loaisp = txtLoaiSP.Text;

                // Kiểm tra nếu có bất kỳ trường nào chưa được nhập
                if (string.IsNullOrEmpty(masp) || string.IsNullOrEmpty(tensp) || string.IsNullOrEmpty(loaisp))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                    return;
                }

                // Thêm dữ liệu vào DataTable
                
                dataTable.Rows.Add(masp, tensp, ngaynhap, loaisp);

                // Xóa nội dung của các điều khiển nhập liệu sau khi đã thêm dữ liệu
                txtMaSP.Text = "";
                txtTenSP.Text = "";
                dtpNgayNhap.Value = DateTime.Now;
                txtLoaiSP.Text = "";
            }
        }

        private void btsua_Click(object sender, EventArgs e)
        {
            {
                // Lấy chỉ mục của dòng được chọn trong DataGridView
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;

                // Kiểm tra nếu người dùng chưa chọn dòng để sửa
                if (selectedRowIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn một dòng để sửa dữ liệu.");
                    return;
                }

                // Lấy thông tin từ các điều khiển nhập liệu
                string masp = txtMaSP.Text;
                string tensp = txtTenSP.Text;
                DateTime ngaynhap = dtpNgayNhap.Value;
                string loaisp = txtLoaiSP.Text;

                // Kiểm tra nếu có bất kỳ trường nào chưa được nhập
                if (string.IsNullOrEmpty(masp) || string.IsNullOrEmpty(tensp) || string.IsNullOrEmpty(loaisp))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                    return;
                }

                // Sửa dữ liệu trong DataTable tại chỉ mục được chọn
                dataTable.Rows[selectedRowIndex]["masp"] = masp;
                dataTable.Rows[selectedRowIndex]["tensp"] = tensp;
                dataTable.Rows[selectedRowIndex]["ngaynhap"] = ngaynhap;
                dataTable.Rows[selectedRowIndex]["loaisp"] = loaisp;

                // Cập nhật lại DataGridView
                dataGridView1.Refresh();
            }
        }

        private void btxoa_Click(object sender, EventArgs e)
        {
            {
                // Lấy chỉ mục của dòng được chọn trong DataGridView
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;

                // Kiểm tra nếu người dùng chưa chọn dòng để xóa
                if (selectedRowIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn một dòng để xóa dữ liệu.");
                    return;
                }

                // Xác nhận việc xóa dữ liệu
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa dòng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Nếu người dùng chọn Yes (Có), thực hiện xóa dữ liệu trong DataTable
                if (result == DialogResult.Yes)
                {
                    dataTable.Rows.RemoveAt(selectedRowIndex);

                    // Cập nhật lại DataGridView
                    dataGridView1.Refresh();
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ TextBox tìm kiếm
            string keyword = txtTimKiem.Text;

            // Nếu không có từ khóa tìm kiếm, hiển thị lại toàn bộ dữ liệu
            if (string.IsNullOrEmpty(keyword))
            {
                dataGridView1.DataSource = dataTable;
            }
            else
            {
                // Tạo bảng mới để lưu kết quả tìm kiếm
                DataTable searchResult = dataTable.Clone();

                // Lặp qua tất cả các dòng trong DataTable để tìm kiếm dữ liệu
                foreach (DataRow row in dataTable.Rows)
                {
                    // Nếu tên sản phẩm chứa từ khóa tìm kiếm, thêm vào bảng kết quả
                    if (row["tensp"].ToString().ToLower().Contains(keyword.ToLower()))
                    {
                        searchResult.Rows.Add(row.ItemArray);
                    }
                }

                // Hiển thị kết quả tìm kiếm trong DataGridView
                dataGridView1.DataSource = searchResult;
            }
        }
    }
}
