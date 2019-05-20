using FoodManager.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FoodManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DatabaseHelper.ConnectionString = @"Data Source=JAKUBOMPUTER\SQLEXPRESS; database=MealPlanner; Trusted_Connection=yes;";

            MessageBox.Show(Properties.Resources.AppStillInDevBody, 
                Properties.Resources.AppStillInDevTitle, 
                MessageBoxButton.OK, 
                MessageBoxImage.Exclamation);
        }

        /// <summary>
        /// Opens add ingredient window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            WindowAddIngredient window = new WindowAddIngredient();
            window.ShowDialog();
        }

        /// <summary>
        /// Opens add recipe window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            WindowAddRecipe window = new WindowAddRecipe();
            window.ShowDialog();
        }

        /// <summary>
        /// Shows a list of recipes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonShowRecipies_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<RecipeOverview> recipes = DatabaseHelper.GetListOfModels(RecipeOverview.Create, RecipeOverview.QuerySelectAll).OrderBy(x => x.Name);
            listBoxItems.Items.Clear();
            foreach (RecipeOverview item in recipes)
            {
                listBoxItems.Items.Add(item);
            }
        }

        /// <summary>
        /// Show selected recipe info.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // if nothing selected
            if (listBoxItems.SelectedIndex == -1)
                return;

            // clear all contents
            stackPanelInfo.Children.Clear();

            // get selected recipe
            RecipeOverview recipeSelected = (RecipeOverview)listBoxItems.SelectedItem;

            // since all recipe names are unique, select recipe object
            Recipe recipe = DatabaseHelper.GetListOfModels(Recipe.Create, Recipe.QuerySelectAll)
                .First(x => x.Name == recipeSelected.Name && x.Description == recipeSelected.Description);

            // select recipe ingredients
            IEnumerable<RecipeIngredient> ingredients = DatabaseHelper.GetListOfModels(RecipeIngredient.Create, $"{RecipeIngredient.QuerySelectAll} WHERE ri.RecipeId = {recipe.Id} ORDER BY i.IngredientCategoryId, i.Name");

            // create title label
            Label labelTitle = new Label
            {
                Content = recipe.Name,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = stackPanelInfo.Width - 10
            };

            // info about how many times was the meal planned
            string timesPlanned = $"Przepis zaplanowany {recipeSelected.TimesUsed} {(recipeSelected.TimesUsed != 1 ? "razy" : "raz")}{Environment.NewLine}{Environment.NewLine}";

            // create description textbox
            TextBox textBoxDescription = new TextBox
            {
                Text = timesPlanned + recipe.Description,
                TextWrapping = TextWrapping.Wrap,
                IsReadOnly = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Height = 200,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = stackPanelInfo.Width - 10
            };

            // generate ingredient list
            StringBuilder stringBuilder = new StringBuilder();

            foreach (RecipeIngredient item in ingredients)
                stringBuilder.AppendLine(item.ToString());

            // create ingredient textbox
            TextBox labelIngredients = new TextBox
            {
                Text = stringBuilder.ToString(),
                TextWrapping = TextWrapping.Wrap,
                IsReadOnly = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Height = 150,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = stackPanelInfo.Width - 10
            };

            // add all three controls to the stackpanel
            stackPanelInfo.Children.Add(labelTitle);
            stackPanelInfo.Children.Add(textBoxDescription);
            stackPanelInfo.Children.Add(labelIngredients);
        }
    }
}
