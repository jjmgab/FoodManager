using System.Data;

namespace FoodManager.DatabaseModels
{
    public class Ingredient
    {
        public static string QuerySelectAll { get; } = "SELECT * FROM Ingredients";
        public static string QueryInsert { get; } = @"INSERT INTO Ingredients(Name, IngredientCategoryId) 
                            VALUES(@Name, @IngredientCategoryId)";

        public int Id { get; set; }
        public string Name { get; set; }
        public int IngredientCategoryId { get; set; }

        public static Ingredient Create(IDataRecord dataRecord)
        {
            return new Ingredient
            {
                Id = int.Parse(dataRecord["id"].ToString()),
                Name = dataRecord["name"].ToString(),
                IngredientCategoryId = int.Parse(dataRecord["ingredientcategoryid"].ToString())
            };
        }

        private Ingredient() { }

        public Ingredient(string name, int ingredientCategoryId)
        {
            Name = name;
            IngredientCategoryId = ingredientCategoryId;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
