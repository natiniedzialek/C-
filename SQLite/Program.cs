namespace lab8;
using System;
using Microsoft.Data.Sqlite;

public static class Program
{
    public static void Main()
    {
        var data = Task1();
        var columnInfo = Task2(data);
        Task3(data, columnInfo);
        Task4(data, columnInfo);
        Task5();
    }

    public static (List<List<string>>, List<string>) Task1() 
    {
        Console.WriteLine("--------------- TASK 1 ----------------\n");
        var result = CSVReader.ReadCsvFile("Book1.csv", ',');
        Console.WriteLine(string.Join(" ", result.Item2));
        foreach(List<string> l in result.Item1)
            Console.WriteLine(string.Join(" ", l));
        Console.WriteLine("");
        return result;
    }

    public static List<ColumnInfo> Task2((List<List<string>>, List<string>) data)
    {
        Console.WriteLine("--------------- TASK 2 ----------------\n");
        var result = DataAnalyzer.AnalyzeData(data.Item1, data.Item2);

        foreach(ColumnInfo columnInfo in result)
            Console.WriteLine("Column Name: " + columnInfo.Name + " Data Type: " + columnInfo.DataType + " Is Nullable: " + columnInfo.IsNullable);
        Console.WriteLine("");

        return result;
    }

    public static void Task3((List<List<string>>, List<string>) data, List<ColumnInfo> columnInfo)
    {
        SqliteConnection connection = new SqliteConnection(@"Data Source=D:\databases\sqlite\cs lab\database.db;");
        connection.Open();
        SQLiteManager.CreateTable(
            columnInfo, 
            "MYTABLE", 
            connection
        );
        connection.Close();
    }

    public static void Task4((List<List<string>>, List<string>) data, List<ColumnInfo> columnInfo)
    {
        SqliteConnection connection = new SqliteConnection(@"Data Source=D:\databases\sqlite\cs lab\database.db;");
        connection.Open();
        SQLiteManager.InsertData(
            "MYTABLE",
            columnInfo, 
            data.Item1, 
            connection
        );
        connection.Close();
    }

    public static void Task5()
    {
        Console.WriteLine("--------------- TASK 5 ----------------\n");
        SqliteConnection connection = new SqliteConnection(@"Data Source=D:\databases\sqlite\cs lab\database.db;");
        connection.Open();
        SQLiteManager.SelectData(
            connection,
            "MYTABLE"
        );
        connection.Close();
    }
}