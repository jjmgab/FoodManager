using System.Data;

namespace FoodManager.DatabaseModels
{
    public class RecipeIngredient
    {
        public static string QuerySelectAll { get; } = "SELECT ri.*, i.Name as IngredientName FROM RecipeIngredients ri LEFT JOIN Ingredients i ON ri.IngredientId = i.Id";
        public static string QueryInsert { get; } = @"INSERT INTO RecipeIngredients(Amount, Unit, RecipeId, IngredientId) 
                            VALUES(@Amount, @Unit, @RecipeId, @IngredientId)";

        public int Id { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }

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

        private RecipeIngredient() { }

        public RecipeIngredient(int amount, string unit, int recipeId, int ingredientId, string ingredientName)
        {
            Amount = amount;
            Unit = unit;
            RecipeId = recipeId;
            IngredientId = ingredientId;
            IngredientName = ingredientName;
        }

        public override string ToString()
        {
            return $"{Amount} {Unit} {IngredientName}";
        }
    }
}
