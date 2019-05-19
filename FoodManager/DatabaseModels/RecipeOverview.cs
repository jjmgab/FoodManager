using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManager.DatabaseModels
{
    public class RecipeOverview
    {
        public static string QuerySelectAll { get; } = "SELECT * FROM RecipesOverview";

        public string Name { get; set; }
        public string Description { get; set; }
        public int IngredientCount { get; set; }
        public int TimesUsed { get; set; }

        public static RecipeOverview Create(IDataRecord dataRecord)
        {
            return new RecipeOverview
            {
                Name = dataRecord["name"].ToString(),
                Description = dataRecord["description"].ToString(),
                IngredientCount = int.Parse(dataRecord["ingredientcount"].ToString()),
                TimesUsed = int.Parse(dataRecord["timesused"].ToString())
            };
        }

        private RecipeOverview() { }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
