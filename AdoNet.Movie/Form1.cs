using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AdoNet.Movie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Server=MSI;initial catalog = MovieDB; integrated security = true");

        private void btnCategoryList_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Select * From TblCategory", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dtgCategory.DataSource = dt;
            connection.Close();
        }

        private void btnCategorySave_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Insert Into TblCategory (CategoryName) values (@p1)", connection);
            command.Parameters.AddWithValue("@p1", txtCategoryName.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Kategori Başarılı Bir Şekilde Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
        }

        private void btnCategoryDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Delete From TblCategory Where CategoryID=@p1", connection);
            command.Parameters.AddWithValue("@p1", txtCategoryID.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Kategori Başarılı Bir Şekilde Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            connection.Close();
        }

        private void btnCategoryUpdate_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Update TblCategory Set CategoryName=@p1 where CategoryID=@p2", connection);
            command.Parameters.AddWithValue("@p1", txtCategoryName.Text);
            command.Parameters.AddWithValue("@p2", txtCategoryID.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Kategori Başarılı Bir Şekilde Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            connection.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region
            connection.Open();
            SqlCommand command = new SqlCommand("Select * From TblCategory", connection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            cmbCategory.ValueMember = "CategoryID";
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.DataSource = dt;
            connection.Close();
            #endregion

            #region
            connection.Open();
            SqlCommand categoryCount = new SqlCommand("Select Count(*) From TblCategory", connection);
            SqlDataReader categoryReader = categoryCount.ExecuteReader();
            while (categoryReader.Read())
            {
                lblCategoryCount.Text = categoryReader[0].ToString();
            }
            connection.Close();
            #endregion

            #region
            connection.Open();
            SqlCommand movieCount = new SqlCommand("Select Count(*) From TblMovie", connection);
            SqlDataReader movieReader = movieCount.ExecuteReader();
            while (movieReader.Read())
            {
                lblMovieCount.Text = movieReader[0].ToString();
            }
            connection.Close();
            #endregion
        }

        private void btnMovie_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Select MovieName, MovieDuration, MovieIMDB, CategoryName From TblMovie Inner Join TblCategory On TblMovie.MovieCategory = TblCategory.CategoryID", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dtgMovie.DataSource = dt;
            connection.Close();
        }

        private void btnMovieProduce_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Exec MovieListWithCategory", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dtgMovie.DataSource = dt;
            connection.Close();
        }

        private void btnMovieEkle_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Insert Into TblMovie (MovieName, MovieIMDB, MovieDuration, MovieCategory) Values (@p1,@p2,@p3,@p4)", connection);
            command.Parameters.AddWithValue("@p1", txtMovieName.Text);
            command.Parameters.AddWithValue("@p2", txtMoviePuan.Text);
            command.Parameters.AddWithValue("@p3", txtMovieTime.Text);
            command.Parameters.AddWithValue("@p4", cmbCategory.SelectedValue);
            command.ExecuteNonQuery();
            MessageBox.Show("Filminiz Başarılı Bir Şekilde Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
        }

        private void btnMovieDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Delete From TblMovie Where MovieID=@p1", connection);
            command.Parameters.AddWithValue("@p1", txtMovieID.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Film Başarılı Bir Şekilde Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            connection.Close();
        }

        private void btnMovieUpdate_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Update TblMovie Set MovieName=@p1, MovieIMDB=@p2, MovieDuration=@p3, MovieCategory=@p4 Where MovieID=@p5", connection);
            command.Parameters.AddWithValue("@p1", txtMovieName.Text);
            command.Parameters.AddWithValue("@p2", txtMoviePuan.Text);
            command.Parameters.AddWithValue("@p3", txtMovieTime.Text);
            command.Parameters.AddWithValue("@p4", cmbCategory.SelectedValue);
            command.Parameters.AddWithValue("@p5", txtMovieID.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Filminiz Başarılı Bir Şekilde Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            connection.Close();
        }
    }
}
