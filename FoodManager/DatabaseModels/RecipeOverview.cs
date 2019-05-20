using System.Data;

namespace FoodManager.DatabaseModels
{
    /// <summary>
    /// Class describing recipe overview model
    /// </summary>
    public class RecipeOverview
    {
        /// <summary>
        /// A query to select all objects
        /// </summary>
        public static string QuerySelectAll { get; } = "SELECT * FROM RecipesOverview";

        /// <summary>
        /// Object name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Object description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Object ingredient count
        /// </summary>
        public int IngredientCount { get; set; }

        /// <summary>
        /// Number of times an object was used in the database
        /// </summary>
        public int TimesUsed { get; set; }

        /// <summary>
        /// Object factory to create an object from data record
        /// </summary>
        /// <param name="dataRecord"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Private default constructor
        /// </summary>
        private RecipeOverview() { }

        /// <summary>
        /// ToString method override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
