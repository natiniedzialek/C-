namespace lab8;
using System;
using System.Collections.Generic;
using System.IO;

public static class CSVReader
{
    public static (List<List<string>>, List<string>) ReadCsvFile(string fileName, char separator)
    {
        List<List<string>> data = new List<List<string>>();
        List<string> columnNames = new List<string>();

        using (var reader = new StreamReader(fileName))
        {
            bool isFirstLine = true;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(separator);

                if (isFirstLine)
                {
                    columnNames.AddRange(values);
                    isFirstLine = false;
                } 
                else
                {
                    data.Add(new List<string>(values));
                }
            }
        }

        return (data, columnNames);
    }
}