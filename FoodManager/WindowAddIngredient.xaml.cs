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
        /// <summary>
        /// List of ingredients
        /// </summary>
        private List<Ingredient> listOfIngredients = DatabaseHelper.GetListOfModels(Ingredient.Create, Ingredient.QuerySelectAll).ToList();

        /// <summary>
        /// Constructor
        /// </summary>
        public WindowAddIngredient()
        {
            InitializeComponent();

            // fill up ingredient category combobox
            foreach (IngredientCategory item in DatabaseHelper.GetListOfModels(IngredientCategory.Create, IngredientCategory.QuerySelectAll).OrderBy(x => x.Name))
            {
                comboBoxCategory.Items.Add(item);
            }
        }

        /// <summary>
        /// Add an ingredient to the database
        /// </summary>
        /// <param name="item"></param>
        public void AddToDatabase(Ingredient item)
        {
            if (ValidateItem(item))
            {
                using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(Ingredient.QueryInsert, connection))
                    {
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = item.Name.ToLower();
                        command.Parameters.Add("@IngredientCategoryId", SqlDbType.Int).Value = item.IngredientCategoryId;

                        connection.Open();
                    }
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Check object correctness
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool ValidateItem(Ingredient item)
        {
            return !listOfIngredients.Exists(x => x.Name == item.Name);
        }

        /// <summary>
        /// Accept changes and add an item to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Discard changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
