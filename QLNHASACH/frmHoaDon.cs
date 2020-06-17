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
namespace QLNHASACH
{
    public partial class frmCreateDonHang : Form
    {
        public frmCreateDonHang()
        {
            InitializeComponent();
        }

        DataTable tblKhachHang, tblSanPham, tblNhanVien;
        SqlDataAdapter daKhachHang, daSanPham, daNhanVien;
        string MaKH="";

        private void txtMaSP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                DataRow[] r = tblSanPham.Select("MaSP ='" + txtMaSP.Text + "'");
                if (r.Count() > 0)
                {
                    //Hien thi thong tin khach hang

                    txtTenSP.Text = r[0]["TenSP"].ToString();
                    nuSoLuong.Maximum = (int)r[0]["SoLuong"];
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm!!!");
                    txtTenSP.Text = "";
                    nuSoLuong.Maximum = 0;
                }
            }
        }

        private void btnAddSP_Click(object sender, EventArgs e)
        {

        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                DataRow[] r = tblKhachHang.Select("SoDT ='" + txtSDT.Text + "'");
                if(r.Count()>0)
                {
                    //Hien thi thong tin khach hang
                    MaKH = r[0]["MaKH"].ToString();
                    txtTenKH.Text = r[0]["TenKH"].ToString();
                    chkGioiTinh.Checked = (bool)r[0]["GioiTinh"];
                    txtDiaChi.Text = r[0]["DiaChi"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy SĐT của khách hàng, hãy tạo mới!!!");
                    MaKH = "";
                    txtTenKH.Text = "";
                    chkGioiTinh.Checked = false;
                    txtDiaChi.Text ="";

                }
            }
            
        }
        private void frmCreateDonHang_Load(object sender, EventArgs e)
        {
            daKhachHang = new SqlDataAdapter("Select * from KHACHHANG", Module.cnnStr);
            daSanPham = new SqlDataAdapter("Select * from SANPHAM", Module.cnnStr);
            daNhanVien = new SqlDataAdapter("Select * from NHANVIEN", Module.cnnStr);
            tblKhachHang = new DataTable();
            tblSanPham = new DataTable();
            tblNhanVien = new DataTable();
            try
            {
                daKhachHang.Fill(tblKhachHang);
                daSanPham.Fill(tblSanPham);
                daNhanVien.Fill(tblNhanVien);
            }catch(SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            loadCBNhanVien();
            createMaHD();
        }

        private void createMaHD()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Module.cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dbo.proc_CreateMaHD";
            cmd.Parameters.Add("@MaHDNew", SqlDbType.NVarChar, 10).Direction = ParameterDirection.Output;
            try
            {
                Module.cnn.Open();
                cmd.ExecuteScalar();
                txtMaHD.Text = cmd.Parameters["@MaHDNew"].Value.ToString();

            }
            catch (SqlException  ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Module.cnn.Close();
        }

        private void loadCBNhanVien()
        {
            cboNhanVien.DataSource = tblNhanVien;
            cboNhanVien.ValueMember = "MaNV";
            cboNhanVien.DisplayMember = "TenNV";
            cboNhanVien.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboNhanVien.AutoCompleteSource = AutoCompleteSource.ListItems;

        }
    }
}
    