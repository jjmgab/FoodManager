using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FoodManager
{
    /// <summary>
    /// Helper class to simplify database communication
    /// </summary>
    class DatabaseHelper
    {
        /// <summary>
        /// Connection string
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Get a list of objects
        /// </summary>
        /// <typeparam name="T">selected object type</typeparam>
        /// <param name="BuildObject">object factory method</param>
        /// <param name="query">query to select objects</param>
        /// <returns></returns>
        public static IEnumerable<T> GetListOfModels<T>(Func<IDataRecord, T> BuildObject, string query)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        try
                        {
                            while (reader.Read())
                            {
                                yield return BuildObject(reader);
                            }
                        }
                        finally
                        {
                            reader.Close();
                            connection.Close();
                        }
                    }
                }
            }
        }
    }
}
