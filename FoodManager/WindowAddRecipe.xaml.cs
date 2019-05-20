using FoodManager.DatabaseModels;
using FoodManager.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FoodManager
{
    /// <summary>
    /// Interaction logic for WindowAddRecipe.xaml
    /// </summary>
    public partial class WindowAddRecipe : Window, IDatabaseAdd<Recipe>, IDatabaseAdd<RecipeIngredient>
    {
        /// <summary>
        /// Recipe ingredient list
        /// </summary>
        private IEnumerable<RecipeIngredient> recipeIngredients;

        /// <summary>
        /// List of all recipes in the database
        /// </summary>
        private List<Recipe> allRecipes = DatabaseHelper.GetListOfModels(Recipe.Create, Recipe.QuerySelectAll).ToList();

        /// <summary>
        /// Constructor
        /// </summary>
        public WindowAddRecipe()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add a recipe to the database
        /// </summary>
        /// <param name="item"></param>
        void IDatabaseAdd<Recipe>.AddToDatabase(Recipe item)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(Recipe.QueryInsert, connection))
                {
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = item.Name.ToLower();
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = item.Description;

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        MessageBox.Show($"Dodano nowy przepis: {item.Name.ToLower()}", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                connection.Close();
            }
            allRecipes = DatabaseHelper.GetListOfModels(Recipe.Create, Recipe.QuerySelectAll).ToList();
        }

        /// <summary>
        /// Add a recipe ingredient to the database
        /// </summary>
        /// <param name="item"></param>
        void IDatabaseAdd<RecipeIngredient>.AddToDatabase(RecipeIngredient item)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(RecipeIngredient.QueryInsert, connection))
                {
                    command.Parameters.Add("@Amount", SqlDbType.Int).Value = item.Amount;
                    command.Parameters.Add("@Unit", SqlDbType.NVarChar).Value = item.Unit;
                    command.Parameters.Add("@RecipeId", SqlDbType.Int).Value = item.RecipeId;
                    command.Parameters.Add("@IngredientId", SqlDbType.Int).Value = item.IngredientId;

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        MessageBox.Show($"Added new item!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                connection.Close();
            }
        }

        /// <summary>
        /// Accept changes so the recipe can be added to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxName.Text != "")
            {
                // create recipe
                Recipe recipe = new Recipe(textBoxName.Text, textBoxDescription.Text);

                if (((IDatabaseAdd<Recipe>)this).ValidateItem(recipe))
                {
                    ((IDatabaseAdd<Recipe>)this).AddToDatabase(recipe);
                    Recipe addedRecipe = allRecipes.Find(x => x.Name == recipe.Name);

                    // add list of ingredients to the database
                    foreach (RecipeIngredient recipeIngredient in recipeIngredients)
                    {
                        recipeIngredient.RecipeId = addedRecipe.Id;
                        ((IDatabaseAdd<RecipeIngredient>)this).AddToDatabase(recipeIngredient);
                    }
                }
            }

            this.DialogResult = true;
            this.Close();
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

        /// <summary>
        /// Validate correctness of a recipe object
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool IDatabaseAdd<Recipe>.ValidateItem(Recipe item)
        {
            return !allRecipes.Exists(x => x.Name == item.Name);
        }

        /// <summary>
        /// Validate correctness of a recipe ingredient object
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool IDatabaseAdd<RecipeIngredient>.ValidateItem(RecipeIngredient item)
        {
            // already validated
            return true;
        }

        /// <summary>
        /// On double-click on a listbox of ingredients, open a window to add ingredients
        /// to the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxIngredients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WindowRecipeIngredients window = new WindowRecipeIngredients();
            window.ShowDialog();

            // check if OK clicked
            if (window.DialogResult.HasValue && window.DialogResult.Value)
            {
                // add to ingredient list
                recipeIngredients = window.RecipeIngredients;
                if (recipeIngredients.Count() > 0)
                {
                    listBoxIngredients.Items.Clear();
                    foreach (RecipeIngredient item in recipeIngredients)
                    {
                        listBoxIngredients.Items.Add(item);
                    }
                }
            }
        }
    }
}
