namespace FoodManager.Interfaces
{
    /// <summary>
    /// An interface containing methods crucial for adding items to the database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IDatabaseAdd<T>
    {
        /// <summary>
        /// Add item to the database
        /// </summary>
        /// <param name="item"></param>
        void AddToDatabase(T item);

        /// <summary>
        /// Verify object correctness
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool ValidateItem(T item);
    }
}
