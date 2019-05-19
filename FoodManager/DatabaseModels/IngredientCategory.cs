using System.Data;

namespace FoodManager.DatabaseModels
{
    class IngredientCategory
    {
        public static string QuerySelectAll { get; } = "SELECT * FROM IngredientCategories";

        public int Id { get; set; }
        public string Name { get; set; }

        public static IngredientCategory Create(IDataRecord dataRecord)
        {
            return new IngredientCategory
            {
                Id = int.Parse(dataRecord["id"].ToString()),
                Name = dataRecord["name"].ToString()
            };
        }

        private IngredientCategory() { }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
