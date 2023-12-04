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
using System.Windows.Shapes;

namespace Firmstationery
{
    public partial class AddProduct : Window
    {

        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FirmStationery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public AddProduct()
        {
           
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DBManager dbManager = new DBManager(connectionString);

                string query = "SELECT DISTINCT Name FROM Types";

                var types = await dbManager.ExecuteQuery(query);

                if (types != null && types.Rows.Count > 0)
                {
                    foreach (DataRow row in types.Rows)
                    {
                        string type = row["Name"].ToString();
                        TypeComboBox.Items.Add(type);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async Task<int> GetTypeIdByName(string typeName)
        {
            int typeId = -1;

            try
            {
                DBManager dbManager = new DBManager(connectionString);

                string query = $"SELECT Id FROM Types WHERE Name = '{typeName}'";
                var result = await dbManager.ExecuteQuery(query);

                if (result != null && result.Rows.Count > 0)
                {
                    typeId = Convert.ToInt32(result.Rows[0]["Id"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            return typeId;
        }




        private async void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DBManager dbManager = new DBManager(connectionString);

                string name = NameTextBox.Text;
                string selectedType = TypeComboBox.SelectedItem.ToString();

                int typeId = await GetTypeIdByName(selectedType);

                int count = Convert.ToInt32(CountTextBox.Text);
                decimal costPrice = decimal.Parse(CostPriceTextBox.Text);

                string query = $"INSERT INTO Products (Name, TypeId, Count, CostPrice) " +
                               $"VALUES ('{name}', {typeId}, {count}, {costPrice})";

                await dbManager.ExecuteNonQuery(query);
                MessageBox.Show("Product added successfully to the database.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


    }
}
