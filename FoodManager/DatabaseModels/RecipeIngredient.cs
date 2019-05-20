using System.Data;

namespace FoodManager.DatabaseModels
{
    /// <summary>
    /// Class describing recipe ingredient model
    /// </summary>
    public class RecipeIngredient
    {
        /// <summary>
        /// A query to select all objects
        /// </summary>
        public static string QuerySelectAll { get; } = "SELECT ri.*, i.Name as IngredientName FROM RecipeIngredients ri LEFT JOIN Ingredients i ON ri.IngredientId = i.Id";

        /// <summary>
        /// A query to insert an object
        /// </summary>
        public static string QueryInsert { get; } = @"INSERT INTO RecipeIngredients(Amount, Unit, RecipeId, IngredientId) 
                            VALUES(@Amount, @Unit, @RecipeId, @IngredientId)";

        /// <summary>
        /// Object id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Object amount
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Object unit
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Object foreign key id of recipe
        /// </summary>
        public int RecipeId { get; set; }

        /// <summary>
        /// Object foreign key id of ingredient
        /// </summary>
        public int IngredientId { get; set; }

        /// <summary>
        /// Object ingredient name
        /// </summary>
        public string IngredientName { get; set; }

        /// <summary>
        /// Object factory to create an object from data record
        /// </summary>
        /// <param name="dataRecord"></param>
        /// <returns></returns>
        public static RecipeIngredient Create(IDataRecord dataRecord)
        {
            return new RecipeIngredient
            {
                Id = int.Parse(dataRecord["id"].ToString()),
                Amount = int.Parse(dataRecord["amount"].ToString()),
                Unit = dataRecord["unit"].ToString(),
                RecipeId = int.Parse(dataRecord["recipeid"].ToString()),
                IngredientId = int.Parse(dataRecord["ingredientid"].ToString()),
                IngredientName = dataRecord["ingredientname"].ToString()

            };
        }

        /// <summary>
        /// Private default constructor
        /// </summary>
        private RecipeIngredient() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="unit"></param>
        /// <param name="recipeId"></param>
        /// <param name="ingredientId"></param>
        /// <param name="ingredientName"></param>
        public RecipeIngredient(int amount, string unit, int recipeId, int ingredientId, string ingredientName)
        {
            Amount = amount;
            Unit = unit;
            RecipeId = recipeId;
            IngredientId = ingredientId;
            IngredientName = ingredientName;
        }

        /// <summary>
        /// ToString method override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{IngredientName} ({Amount} {Unit})";
        }
    }
}
