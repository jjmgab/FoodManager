using System.Data;

namespace FoodManager.DatabaseModels
{
    class Recipe
    {
        public static string QuerySelectAll { get; } = "SELECT * FROM Recipes";
        public static string QueryInsert { get; } = @"INSERT INTO Recipes(Name, Description) 
                            VALUES(@Name, @Description)";

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static Recipe Create(IDataRecord dataRecord)
        {
            return new Recipe
            {
                Id = int.Parse(dataRecord["id"].ToString()),
                Name = dataRecord["name"].ToString(),
                Description = dataRecord["description"].ToString()
            };
        }

        private Recipe() { }

        public Recipe(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
