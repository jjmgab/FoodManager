using FoodManager.DatabaseModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace FoodManager
{
    /// <summary>
    /// Interaction logic for WindowRecipeIngredients.xaml
    /// </summary>
    public partial class WindowRecipeIngredients : Window
    {
        /// <summary>
        /// List of target recipe igredients
        /// </summary>
        public List<RecipeIngredient> RecipeIngredients { get; } = new List<RecipeIngredient>();

        /// <summary>
        /// List of all ingredients
        /// </summary>
        private List<Ingredient> allIngredients;

        /// <summary>
        /// Constructor
        /// </summary>
        public WindowRecipeIngredients()
        {
            InitializeComponent();
            RefreshIngredientList();
        }

        /// <summary>
        /// Refresh list of ingredients in the source list
        /// </summary>
        private void RefreshIngredientList()
        {
            allIngredients = DatabaseHelper.GetListOfModels(Ingredient.Create, Ingredient.QuerySelectAll).OrderBy(x => x.IngredientCategoryId).ThenBy(x => x.Name).ToList();

            foreach (RecipeIngredient item in listBoxTarget.Items)
            {
                allIngredients.Remove(allIngredients.Find(x => x.Name == item.IngredientName));
            }

            listBoxSource.Items.Clear();
            foreach (Ingredient item in allIngredients)
            {
                listBoxSource.Items.Add(item);
            }
        }

        /// <summary>
        /// Move an ingredient from the source list to the target list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMoveRight_Click(object sender, RoutedEventArgs e)
        {
            if (!listBoxSource.Items.IsEmpty && listBoxSource.SelectedItem != null)
            {
                if (!int.TryParse(textBoxAmount.Text, out int amount))
                {
                    MessageBox.Show("Please enter valid number for amount.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (textBoxUnit.Text == "")
                {
                    MessageBox.Show("Please enter unit.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Ingredient sourceIngredient = (Ingredient)listBoxSource.SelectedItem;
                RecipeIngredient ingredient = new RecipeIngredient(amount, textBoxUnit.Text, -1, sourceIngredient.Id, sourceIngredient.Name);
                listBoxTarget.Items.Add(ingredient);
                listBoxSource.Items.Remove(listBoxSource.SelectedItem);
            }
        }

        /// <summary>
        /// Accept changes and add ingredients to the target list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            foreach (RecipeIngredient item in listBoxTarget.Items)
            {
                RecipeIngredients.Add(item);
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
        /// Move an ingredient from the target list back to the source list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMoveLeft_Click(object sender, RoutedEventArgs e)
        {
            if (!listBoxTarget.Items.IsEmpty && listBoxTarget.SelectedItem != null)
            {
                RecipeIngredient sourceIngredient = (RecipeIngredient)listBoxTarget.SelectedItem;
                Ingredient ingredient = allIngredients.Find(x => x.Name == sourceIngredient.IngredientName);
                listBoxSource.Items.Add(ingredient);
                listBoxTarget.Items.Remove(listBoxTarget.SelectedItem);
            }
        }

        /// <summary>
        /// Open a window to add a new ingredient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddNewIngredient_Click(object sender, RoutedEventArgs e)
        {
            WindowAddIngredient window = new WindowAddIngredient();
            window.ShowDialog();

            RefreshIngredientList();
        }
    }
}
