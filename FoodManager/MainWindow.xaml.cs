using FoodManager.DatabaseModels;
using System.Collections.Generic;
using System.Windows;

namespace FoodManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DatabaseHelper.ConnectionString = @"Data Source=JAKUBOMPUTER\SQLEXPRESS; database=MealPlanner; Trusted_Connection=yes;";

            
            
        }

        private void ButtonAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            WindowAddIngredient window = new WindowAddIngredient();
            window.ShowDialog();
        }

        private void ButtonAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            WindowAddRecipe window = new WindowAddRecipe();
            window.ShowDialog();
        }

        private void ButtonShowRecipies_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<RecipeOverview> recipes = DatabaseHelper.GetListOfModels(RecipeOverview.Create, RecipeOverview.QuerySelectAll);
            listBoxItems.Items.Clear();
            foreach (RecipeOverview item in recipes)
            {
                listBoxItems.Items.Add(item);
            }
        }
    }
}
