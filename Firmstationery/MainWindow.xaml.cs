using DBEntity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Firmstationery
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
        public decimal CostPrice { get; set; }

        public Product(int id, string name, string type, int count, decimal costPrice)
        {
            Id = id;
            Name = name;
            Type = type;
            Count = count;
            CostPrice = costPrice;
        }

        public Product(string name,string type)
        {
            Name = name;
            Type = type;
        }
    }



    public partial class MainWindow : Window
    {
        const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FirmStationery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection sqlConnection = new SqlConnection(ConnectionString);

        List<SqlCommand> commands = new List<SqlCommand>();

        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string SelectAllProducts = @"";
            commands.Add(new SqlCommand(SelectAllProducts,sqlConnection));
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
                MessageBox.Show("Successful disconnection");
            }
        }
        
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
                if (sqlConnection.State == ConnectionState.Open)
                {
                    MessageBox.Show("Successful connection");
                }
                else
                {
                    MessageBox.Show("Connection failed");
                }
            }         
        }

        private async void ShowAllAboutProducts(object sender, RoutedEventArgs e)
        {
            try
            {

                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SELECT Products.Id, Products.Name, Types.Name AS TypeName, Products.Count, Products.CostPrice FROM Products INNER JOIN Types ON Products.TypeId = Types.Id", sqlConnection))
                    {
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        if (reader.HasRows)
                        {
                            List<Product> products = new List<Product>();
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string name = reader.GetString(1);
                                string type = reader.GetString(2);
                                int count = reader.GetInt32(3);
                                decimal costPrice = reader.GetDecimal(4);

                                products.Add(new Product(id, name, type, count, costPrice));
                            }
                            DG.ItemsSource = products;
                        }
                        else
                        {
                            MessageBox.Show("No rows found.");
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }



        private void ShowTypesProducts(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Types", connection))
                    {
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        DG.ItemsSource = dataTable.DefaultView;

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void ShowManagers(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("ShowManagers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    DG.ItemsSource = dataTable.DefaultView;

                    reader.Close();
                }
            }
        }

        private void ShowProductWithMaxCount(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM Products ORDER BY Count DESC", connection))
                    {
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        DG.ItemsSource = dataTable.DefaultView;

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void ShowProductWithMinCount(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM Products ORDER BY Count ASC", connection))
                    {
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        DG.ItemsSource = dataTable.DefaultView;

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void ShowProductWithMaxCostPrice(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM Products ORDER BY CostPrice DESC", connection))
                    {
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        DG.ItemsSource = dataTable.DefaultView;

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void ShowProductWithMinCostPrice(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM Products ORDER BY CostPrice ASC", connection))
                    {
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        DG.ItemsSource = dataTable.DefaultView;

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void AddNewProduct(object sender, RoutedEventArgs e)
        {
            AddProduct addProduct = new AddProduct();
            addProduct.ShowDialog();
        }

        private async void AddNewType(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(Type_TextBox.Text))
            {
                try
                {
                    DBManager dbManager = new DBManager(ConnectionString);

                    string type = Type_TextBox.Text;

                    string query = $"INSERT INTO dbo.[Types] (Name) VALUES ('{type}')";

                    await dbManager.ExecuteQuery(query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
    }
}
