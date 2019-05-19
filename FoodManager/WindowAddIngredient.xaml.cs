using FoodManager.DatabaseModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FoodManager.Interfaces;
using System.Data.SqlClient;
using System.Data;

namespace FoodManager
{
    /// <summary>
    /// Interaction logic for WindowAddIngredient.xaml
    /// </summary>
    public partial class WindowAddIngredient : Window, IDatabaseAdd<Ingredient>
    {
        private List<Ingredient> listOfIngredients = DatabaseHelper.GetListOfModels(Ingredient.Create, Ingredient.QuerySelectAll).ToList();

        public WindowAddIngredient()
        {
            InitializeComponent();

            foreach (IngredientCategory item in DatabaseHelper.GetListOfModels(IngredientCategory.Create, IngredientCategory.QuerySelectAll).OrderBy(x => x.Name))
            {
                comboBoxCategory.Items.Add(item);
            }
        }

        public void AddToDatabase(Ingredient item)
        {
            if (ValidateItem(item))
            {
                using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(Ingredient.QueryInsert, connection))
                    {
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = item.Name;
                        command.Parameters.Add("@IngredientCategoryId", SqlDbType.Int).Value = item.IngredientCategoryId;

                        connection.Open();
                        if (command.ExecuteNonQuery() > 0)
                            MessageBox.Show($"Added new item: {item.Name}, {item.IngredientCategoryId}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    connection.Close();
                }
            }
        }

        public bool ValidateItem(Ingredient item)
        {
            return !listOfIngredients.Exists(x => x.Name == item.Name);
        }

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxName.Text;
            int categoryId = comboBoxCategory.SelectedIndex != -1 ? ((IngredientCategory)comboBoxCategory.SelectedItem).Id : -1;

            if (name != "" && categoryId != -1)
            {
                AddToDatabase(new Ingredient(name, categoryId));
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid data", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
