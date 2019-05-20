using System.Data;

namespace FoodManager.DatabaseModels
{
    /// <summary>
    /// Class describing ingredient model
    /// </summary>
    public class Ingredient
    {
        /// <summary>
        /// A query to select all objects
        /// </summary>
        public static string QuerySelectAll { get; } = "SELECT * FROM Ingredients";

        /// <summary>
        /// A query to insert an object
        /// </summary>
        public static string QueryInsert { get; } = @"INSERT INTO Ingredients(Name, IngredientCategoryId) 
                            VALUES(@Name, @IngredientCategoryId)";

        /// <summary>
        /// Object id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Object name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Object foreign key id of ingredient category
        /// </summary>
        public int IngredientCategoryId { get; set; }

        /// <summary>
        /// Object factory to create an object from data record
        /// </summary>
        /// <param name="dataRecord"></param>
        /// <returns></returns>
        public static Ingredient Create(IDataRecord dataRecord)
        {
            return new Ingredient
            {
                Id = int.Parse(dataRecord["id"].ToString()),
                Name = dataRecord["name"].ToString(),
                IngredientCategoryId = int.Parse(dataRecord["ingredientcategoryid"].ToString())
            };
        }

        /// <summary>
        /// Private default constructor
        /// </summary>
        private Ingredient() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ingredientCategoryId"></param>
        public Ingredient(string name, int ingredientCategoryId)
        {
            Name = name;
            IngredientCategoryId = ingredientCategoryId;
        }

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
