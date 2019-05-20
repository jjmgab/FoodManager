using System.Data;

namespace FoodManager.DatabaseModels
{
    /// <summary>
    /// Class describing recipe model
    /// </summary>
    class Recipe
    {
        /// <summary>
        /// A query to select all objects
        /// </summary>
        public static string QuerySelectAll { get; } = "SELECT * FROM Recipes";

        /// <summary>
        /// A query to insert an object
        /// </summary>
        public static string QueryInsert { get; } = @"INSERT INTO Recipes(Name, Description) 
                            VALUES(@Name, @Description)";

        /// <summary>
        /// Object id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Object name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Object description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Object factory to create an object from data record
        /// </summary>
        /// <param name="dataRecord"></param>
        /// <returns></returns>
        public static Recipe Create(IDataRecord dataRecord)
        {
            return new Recipe
            {
                Id = int.Parse(dataRecord["id"].ToString()),
                Name = dataRecord["name"].ToString(),
                Description = dataRecord["description"].ToString()
            };
        }

        /// <summary>
        /// Private default constructor
        /// </summary>
        private Recipe() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public Recipe(string name, string description)
        {
            Name = name;
            Description = description;
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
