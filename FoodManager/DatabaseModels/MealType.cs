using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManager.DatabaseModels
{
    public class MealType
    {
        public static string QuerySelectAll { get; } = "SELECT * FROM MealTypes";
        public static string QueryInsert { get; } = @"INSERT INTO MealTypes(Name) VALUES(@Name)";

        public int Id { get; set; }
        public string Name { get; set; }

        public static MealType Create(IDataRecord dataRecord)
        {
            return new MealType
            {
                Id = int.Parse(dataRecord["id"].ToString()),
                Name = dataRecord["name"].ToString()
            };
        }

        private MealType() { }

        public MealType(string name, int ingredientCategoryId)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
