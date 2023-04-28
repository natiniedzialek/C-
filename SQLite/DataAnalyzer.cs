namespace lab8;
using System;
using System.Collections.Generic;

public static class DataAnalyzer
{
    public static List<ColumnInfo> AnalyzeData(List<List<string>> data, List<string> columnNames)
    {
        List<ColumnInfo> columnInfoList = new();

        for (int i = 0; i < columnNames.Count; i++)
        {
            ColumnInfo columnInfo = new ColumnInfo();
            columnInfo.Name = columnNames[i];
            columnInfo.DataType = "TEXT";
            columnInfo.IsNullable = false;

            bool allValuesAreInt = true;
            bool allValuesAreDouble = true;

            for (int j = 0; j < data.Count; j++)
            {
                string value = data[j][i];

                if (string.IsNullOrEmpty(value))
                {
                    columnInfo.IsNullable = true;
                }
                else
                {
                    int intValue;
                    double doubleValue;

                    if (!int.TryParse(value, out intValue))
                    {
                        allValuesAreInt = false;
                    }

                    if (!double.TryParse(value, out doubleValue))
                    {
                        allValuesAreDouble = false;
                    }
                }
            }

            if (allValuesAreInt)
            {
                columnInfo.DataType = "INTEGER";
            }
            else if (allValuesAreDouble)
            {
                columnInfo.DataType = "REAL";
            }

            columnInfoList.Add(columnInfo);
        }

        return columnInfoList;
    }
}