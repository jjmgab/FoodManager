using System;
using System.Data;

namespace FoodManager.DatabaseModels
{
    /// <summary>
    /// Class describing ongoing menu entry model
    /// </summary>
    class OngoingMenuEntry
    {
        /// <summary>
        /// A query to select all objects
        /// </summary>
        public static string QuerySelectAll { get; } = "SELECT * FROM OngoingMenuEntries";

        /// <summary>
        /// Entry date
        /// </summary>
        public DateTime EntryDate { get; set; }

        /// <summary>
        /// Meal type name
        /// </summary>
        public string MealType { get; set; }

        /// <summary>
        /// Meal name
        /// </summary>
        public string MealName { get; set; }

        /// <summary>
        /// Object factory to create an object from data record
        /// </summary>
        /// <param name="dataRecord"></param>
        /// <returns></returns>
        public static OngoingMenuEntry Create(IDataRecord dataRecord)
        {
            return new OngoingMenuEntry
            {
                EntryDate = DateTime.Parse(dataRecord["entrydate"].ToString()),
                MealType = dataRecord["mealtype"].ToString(),
                MealName = dataRecord["mealname"].ToString()
            };
        }

        /// <summary>
        /// Private default constructor
        /// </summary>
        private OngoingMenuEntry() { }

        /// <summary>
        /// ToString method override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"data: {EntryDate.ToShortDateString()}{Environment.NewLine}" +
                $"typ: {MealType}{Environment.NewLine}" +
                $"nazwa: {MealName}";
        }
    }
}
