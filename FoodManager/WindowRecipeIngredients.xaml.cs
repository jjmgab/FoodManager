using FoodManager.DatabaseModels;
using System;
using System.Collections.Generic;
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

namespace FoodManager
{
    /// <summary>
    /// Interaction logic for WindowRecipeIngredients.xaml
    /// </summary>
    public partial class WindowRecipeIngredients : Window
    {
        public List<RecipeIngredient> RecipeIngredients { get; } = new List<RecipeIngredient>();

        private List<Ingredient> allIngredients;

        public WindowRecipeIngredients()
        {
            InitializeComponent();
            RefreshIngredientList();
        }

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

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            foreach (RecipeIngredient item in listBoxTarget.Items)
            {
                RecipeIngredients.Add(item);
            }
            this.DialogResult = true;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

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

        private void ButtonAddNewIngredient_Click(object sender, RoutedEventArgs e)
        {
            WindowAddIngredient window = new WindowAddIngredient();
            window.ShowDialog();

            RefreshIngredientList();
        }
    }
}
