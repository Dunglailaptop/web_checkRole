using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace webapp_manageSupperBrain.Controllers
{
    public class SettingConnect
    {
        public static string connectionString = @"Data Source=dungct\SQLEXPRESS;Initial Catalog=webapp_manageSupperBrain;Integrated Security=True;";
        public static List<T> Select<T>(string query) where T : new()
        {
        
            List<T> data = new List<T>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    T item = new T();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string columnName = reader.GetName(i);
                        object value = reader[i];

                        // Using reflection to set the property value of the object
                        var property = typeof(T).GetProperty(columnName);
                        if (property != null && value != DBNull.Value)
                        {
                            property.SetValue(item, value);
                        }
                    }

                    data.Add(item);
                }

                reader.Close();
            }

            return data;
        }

        public static T SelectSingle<T>(string query) where T : new()
        {
        

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                T item = default(T);

                if (reader.Read())
                {
                    item = new T();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string columnName = reader.GetName(i);
                        object value = reader[i];

                        // Using reflection to set the property value of the object
                        var property = typeof(T).GetProperty(columnName);
                        if (property != null && value != DBNull.Value)
                        {
                            property.SetValue(item, value);
                        }
                    }
                }

                reader.Close();

                return item;
            }
        }

    }
}