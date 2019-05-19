using FoodManager.DatabaseModels;
using FoodManager.Interfaces;
using System;
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
        private IEnumerable<RecipeIngredient> recipeIngredients;

        private List<Recipe> allRecipes = DatabaseHelper.GetListOfModels(Recipe.Create, Recipe.QuerySelectAll).ToList();

        public WindowAddRecipe()
        {
            InitializeComponent();
        }

        void IDatabaseAdd<Recipe>.AddToDatabase(Recipe item)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(Recipe.QueryInsert, connection))
                {
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = item.Name;
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = item.Description;

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        MessageBox.Show($"Added new item: {item.Name}, {item.Description}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                connection.Close();
            }
            allRecipes = DatabaseHelper.GetListOfModels(Recipe.Create, Recipe.QuerySelectAll).ToList();
        }

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

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxName.Text != "")
            {
                Recipe recipe = new Recipe(textBoxName.Text, textBoxDescription.Text);

                if (((IDatabaseAdd<Recipe>)this).ValidateItem(recipe))
                {
                    ((IDatabaseAdd<Recipe>)this).AddToDatabase(recipe);
                    Recipe addedRecipe = allRecipes.Find(x => x.Name == recipe.Name);

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

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        bool IDatabaseAdd<Recipe>.ValidateItem(Recipe item)
        {
            return !allRecipes.Exists(x => x.Name == item.Name);
        }

        bool IDatabaseAdd<RecipeIngredient>.ValidateItem(RecipeIngredient item)
        {
            // already validated
            return true;
        }

        private void ListBoxIngredients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WindowRecipeIngredients window = new WindowRecipeIngredients();
            window.ShowDialog();

            if (window.DialogResult.HasValue && window.DialogResult.Value)
            {
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
