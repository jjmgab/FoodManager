using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManager.DatabaseModels
{
    class OngoingMenuEntry
    {
        public static string QuerySelectAll { get; } = "SELECT * FROM OngoingMenuEntries";

        public DateTime EntryDate { get; set; }
        public string MealType { get; set; }
        public string MealName { get; set; }

        public static OngoingMenuEntry Create(IDataRecord dataRecord)
        {
            return new OngoingMenuEntry
            {
                EntryDate = DateTime.Parse(dataRecord["entrydate"].ToString()),
                MealType = dataRecord["mealtype"].ToString(),
                MealName = dataRecord["mealname"].ToString()
            };
        }

        private OngoingMenuEntry() { }

        public override string ToString()
        {
            return $"data: {EntryDate.ToShortDateString()}{Environment.NewLine}" +
                $"typ: {MealType}{Environment.NewLine}" +
                $"nazwa: {MealName}";
        }
    }
}
