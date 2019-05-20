using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManager.DatabaseModels
{
    /// <summary>
    /// Class describing meal type model
    /// </summary>
    public class MealType
    {
        /// <summary>
        /// A query to select all objects
        /// </summary>
        public static string QuerySelectAll { get; } = "SELECT * FROM MealTypes";

        /// <summary>
        /// A query to insert an object
        /// </summary>
        public static string QueryInsert { get; } = @"INSERT INTO MealTypes(Name) VALUES(@Name)";

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
        public static MealType Create(IDataRecord dataRecord)
        {
            return new MealType
            {
                Id = int.Parse(dataRecord["id"].ToString()),
                Name = dataRecord["name"].ToString()
            };
        }

        /// <summary>
        /// Private default constructor
        /// </summary>
        private MealType() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ingredientCategoryId"></param>
        public MealType(string name, int ingredientCategoryId)
        {
            Name = name;
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
