using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient; 
using System.Data;

namespace Tugas2_API_KELOMPOK_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    class SiswaDAO
    {
        private MySqlCommand command = null;
        private MySqlConnection conn = new MySqlConnection();
        private string ServerConf = "server = localhost; userid=root; password=; Database=db_olimpiade"; 

        public SiswaDAO()
        {
            conn.ConnectionString = ServerConf;
        }

        public DataSet getData()
        {
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                command = new MySqlCommand();
                command.Connection = conn;
                command.CommandText = "SELECT * FROM siswa";
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                dataAdapter.Fill(ds, "DataSiswa");
                conn.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Connection Problems when get data: " + e.Message, "Error");
            }
            return ds;
        }
 
        public Boolean InsertData(Siswa s)
        {
            Boolean status = false;
            try
            {
                conn.Open();
                command = new MySqlCommand();
                command.Connection = conn;
                command.CommandText = "INSERT INTO siswa VALUE('" + s.Nomor_Peserta + "','" + s.Nama + "','" + s.Tanggal_Lahir + "'," +
                    "'" + s.Nomor_Hp + "','" + s.Jenis_Kelamin + "','" + s.Alamat + "','" + s.Kota_Asal + "','" + s.Email + "'," +
                    "'" + s.Asal_Sekolah + "','" + s.Nama_Ayah + "','" + s.Nomor_Hp_Ayah + "','" + s.Pekerjaan_Ayah + "'," +
                    "'" + s.Nama_Ibu + "','" + s.Nomor_Hp_Ibu + "','" + s.Pekerjaan_Ibu + "')";
                command.ExecuteNonQuery();
                status = true;
                conn.Close();
            }
            catch(MySqlException e)
            {
                MessageBox.Show("Connection Problems when insert data: " + e.Message, "Error");
            }
            return status;
        }
        public Boolean UpdateData(Siswa s, int nomor_peserta)
        {
            Boolean status = false;
            try
            {
                conn.Open();
                command = new MySqlCommand();
                command.Connection = conn;
                command.CommandText = "UPDATE siswa SET nomor_peserta = '" + s.Nomor_Peserta + "',nama = '" + s.Nama + "',tanggal_lahir = '" + s.Tanggal_Lahir + "'," +
                    "nomor_hp = '" + s.Nomor_Hp + "',jenis_kelamin = '" + s.Jenis_Kelamin + "',alamat = '" + s.Alamat + "',kota_asal = '" + s.Kota_Asal + "',email = '" + s.Email + "'," +
                    "asal_sekolah = '" + s.Asal_Sekolah + "',nama_ayah = '" + s.Nama_Ayah + "',nomor_hp_ayah = '" + s.Nomor_Hp_Ayah + "',pekerjaan_ayah = '" + s.Pekerjaan_Ayah + "'," +
                    "nama_ibu = '" + s.Nama_Ibu + "',nomor_hp_ibu = '" + s.Nomor_Hp_Ibu + "',pekerjaan_ibu = '" + s.Pekerjaan_Ibu + "' WHERE nomor_peserta = '" + nomor_peserta + "'";
                command.ExecuteNonQuery();
                status = true;
                conn.Close();
            }
            catch(MySqlException e)
            {
                MessageBox.Show("Connection Problems when update data: " + e.Message, "Error");
            }
            return status;
        }
        public Boolean DeleteData(int nomor_peserta)
        {
            Boolean status = false;
            try
            {
                conn.Open();
                command = new MySqlCommand();
                command.Connection = conn;
                command.CommandText = "DELETE FROM siswa WHERE nomor_peserta = '" + nomor_peserta + "'";
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch(MySqlException e)
            {
                MessageBox.Show("Connection Problems when delete data: " + e.Message, "Error");
            }
            return status;
        }
    }
    public partial class MainWindow : Window
    {
        SiswaDAO SDAO = new SiswaDAO();
        
        public MainWindow()
        {
            InitializeComponent();
            Jenis_Kelamin.Items.Add("Pria");
            Jenis_Kelamin.Items.Add("Wanita");
            initDataGrid();
        }
        public void initDataGrid()
        {
            DataSet data = new DataSet();
            data = SDAO.getData();
            dataGrid.DataContext = data;
        }
        public void InsertClick(object sender, RoutedEventArgs e)
        {
        if (np.Text != "" && ns.Text != "" && tl.Text != "" && nh.Text != "" && Jenis_Kelamin.SelectedItem != "" 
            && almt.Text !="" && ka.Text !="" && em.Text !="" && alsl.Text !="" && na.Text !="" && nha.Text !="" 
            && pa.Text !="" && ni.Text !="" && nhi.Text !="" && pi.Text != "")
            {
             Siswa s = new Siswa();
             s.Nomor_Peserta = Convert.ToInt32(np.Text);
             s.Nama = ns.Text;
             s.Tanggal_Lahir = tl.SelectedDate.Value.ToString("yyyy-MM-dd");
             s.Nomor_Hp = nh.Text;
             s.Jenis_Kelamin = Jenis_Kelamin.SelectedItem.ToString();
             s.Alamat = almt.Text;
             s.Kota_Asal = ka.Text;
             s.Email = em.Text;
             s.Asal_Sekolah = alsl.Text;
             s.Nama_Ayah = na.Text;
             s.Nomor_Hp_Ayah = nha.Text;
             s.Pekerjaan_Ayah = pa.Text;
             s.Nama_Ibu = ni.Text;
             s.Nomor_Hp_Ibu = nhi.Text;
             s.Pekerjaan_Ibu = pi.Text;

            SDAO.InsertData(s);
            initDataGrid();
                MessageBox.Show("Data Berhasil Ditambahkan", "Pesan", MessageBoxButton.OK, MessageBoxImage.Information);
                np.Text = "";
                ns.Text = "";
                tl.Text = "";
                nh.Text = "";
                Jenis_Kelamin.SelectedItem = "";
                almt.Text = "";
                ka.Text = "";
                em.Text = "";
                alsl.Text = "";
                na.Text = "";
                nha.Text = "";
                pa.Text = "";
                ni.Text = "";
                nhi.Text = "";
                pi.Text = "";
            }
        else
            {
                MessageBox.Show("Data Masih Ada Yang Kosong!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }        
        }
        public void ResetClick(object sender, RoutedEventArgs e)
        {
            np.Text = "";
            ns.Text = "";
            tl.Text = "";
            nh.Text = "";
            Jenis_Kelamin.SelectedItem = "";
            almt.Text = "";
            ka.Text = "";
            em.Text = "";
            alsl.Text = "";
            na.Text = "";
            nha.Text = "";
            pa.Text = "";
            ni.Text = "";
            nhi.Text = "";
            pi.Text = "";
        }
        public void DeleteClick(object sender, RoutedEventArgs e)
        {
            SDAO.DeleteData(Convert.ToInt32(np.Text));
            initDataGrid();           
        }
        public void UpdateClick(object sender, RoutedEventArgs e)
        {
            Siswa s = new Siswa();
            if (np.Text != "" && ns.Text != "" && tl.Text != "" && nh.Text != "" && Jenis_Kelamin.SelectedItem != ""
            && almt.Text != "" && ka.Text != "" && em.Text != "" && alsl.Text != "" && na.Text != "" && nha.Text != ""
            && pa.Text != "" && ni.Text != "" && nhi.Text != "" && pi.Text != "")
            {
                s.Nomor_Peserta = Convert.ToInt32(np.Text);
                s.Nama = ns.Text;
                s.Tanggal_Lahir = tl.SelectedDate.Value.ToString("yyyy-MM-dd");
                s.Nomor_Hp = nh.Text;
                s.Jenis_Kelamin = Jenis_Kelamin.SelectedItem.ToString();
                s.Alamat = almt.Text;
                s.Kota_Asal = ka.Text;
                s.Email = em.Text;
                s.Asal_Sekolah = alsl.Text;
                s.Nama_Ayah = na.Text;
                s.Nomor_Hp_Ayah = nha.Text;
                s.Pekerjaan_Ayah = pa.Text;
                s.Nama_Ibu = ni.Text;
                s.Nomor_Hp_Ibu = nhi.Text;
                s.Pekerjaan_Ibu = pi.Text;

                SDAO.UpdateData(s, s.Nomor_Peserta);
                initDataGrid();

                np.Text = "";
                ns.Text = "";
                tl.Text = "";
                nh.Text = "";
                Jenis_Kelamin.SelectedItem = "";
                almt.Text = "";
                ka.Text = "";
                em.Text = "";
                alsl.Text = "";
                na.Text = "";
                nha.Text = "";
                pa.Text = "";
                ni.Text = "";
                nhi.Text = "";
                pi.Text = "";              
            }
            else
            {
                MessageBox.Show("Semua Data Harus Di isi!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                np.Text = "";
                ns.Text = "";
                tl.Text = "";
                nh.Text = "";
                Jenis_Kelamin.SelectedItem = "";
                almt.Text = "";
                ka.Text = "";
                em.Text = "";
                alsl.Text = "";
                na.Text = "";
                nha.Text = "";
                pa.Text = "";
                ni.Text = "";
                nhi.Text = "";
                pi.Text = "";
            }

        }
    }
    
}
