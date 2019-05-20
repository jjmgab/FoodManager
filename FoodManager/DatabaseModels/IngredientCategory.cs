using System.Data;

namespace FoodManager.DatabaseModels
{
    /// <summary>
    /// Class describing ingredient category model
    /// </summary>
    class IngredientCategory
    {
        /// <summary>
        /// A query to select all objects
        /// </summary>
        public static string QuerySelectAll { get; } = "SELECT * FROM IngredientCategories";

        /// <summary>
        /// Object id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Object name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Object factory to create an object from data record
        /// </summary>
        /// <param name="dataRecord"></param>
        /// <returns></returns>
        public static IngredientCategory Create(IDataRecord dataRecord)
        {
            return new IngredientCategory
            {
                Id = int.Parse(dataRecord["id"].ToString()),
                Name = dataRecord["name"].ToString()
            };
        }

        /// <summary>
        /// Private default constructor
        /// </summary>
        private IngredientCategory() { }

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
