namespace lab8;
using System;
using Microsoft.Data.Sqlite;

public static class SQLiteManager {
    public static void CreateTable(List<ColumnInfo> columnInfoList, string tableName, SqliteConnection connection)
    {
        SqliteCommand delTableCmd = connection.CreateCommand();
        
        delTableCmd.CommandText = "DROP TABLE IF EXISTS " + tableName;
        delTableCmd.ExecuteNonQuery();

        // build SQL query
        string query = "CREATE TABLE " + tableName + " (";
        foreach (ColumnInfo columnInfo in columnInfoList)
        {
            query += columnInfo.Name + " " + columnInfo.DataType;
            if (!columnInfo.IsNullable)
            {
                query += " NOT NULL";
            }
            query += ",";
        }
        query = query.TrimEnd(',') + ");";
        Console.WriteLine("--------------- TASK 3 ----------------\n\n" + query + "\n");

        // Create a table
        SqliteCommand createTableCommand = connection.CreateCommand();
        createTableCommand.CommandText = query;
        createTableCommand.ExecuteNonQuery();
    }

    public static void InsertData(string tableName, List<ColumnInfo> columnInfo, List<List<String>> data, SqliteConnection connection)
    {

        // build SQL query
        string query = "INSERT INTO " + tableName + " (";

        foreach (ColumnInfo c in columnInfo)
            query += c.Name + ",";
        
        query = query.TrimEnd(',') + ") VALUES ";

        int intValue;
        double doubleValue;
        foreach (List<String> row in data)
        {
            query += "(";
            foreach(string value in row)
            {
                if(value == "")
                {
                    query += "@value,";
                    continue;
                }
                else if(!int.TryParse(value, out intValue) & !double.TryParse(value, out doubleValue))
                {
                    query += "\"" + value + "\",";
                    continue;
                }
                query += value + ",";
            }
            query = query.TrimEnd(',') + "),";
        }
        query = query.TrimEnd(',');
        Console.WriteLine("--------------- TASK 4 ----------------\n\n" + query + "\n");

        // insert data
        using (var transaction = connection.BeginTransaction())
        {
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@value", DBNull.Value);
            command.ExecuteNonQuery();
            transaction.Commit();
        }
    }

    public static void SelectData(SqliteConnection connection, string tableName)
    {
        SqliteCommand selectCmd = connection.CreateCommand();
        selectCmd.CommandText = "SELECT * FROM " + tableName;
        using (SqliteDataReader reader = selectCmd.ExecuteReader())
            {
                bool firstRow = true;
                while (reader.Read())
                {
                    if (firstRow)
                    {
                        for (int a = 0; a < reader.FieldCount; a++)
                        {
                            Console.Write(reader.GetName(a));
                            Console.Write(",");
                        }
                        firstRow = false;
                        Console.WriteLine("");
                    }
                    for (int a = 0; a < reader.FieldCount; a++)
                    {
                        String?val = null;
                        try {
                            val = reader.GetString(a);
                        } catch {}
                        Console.Write(val != null ? val : "NULL");
                        Console.Write(",");
                    }
                    Console.WriteLine("");
                }
                reader.Close();
            }
    }
}