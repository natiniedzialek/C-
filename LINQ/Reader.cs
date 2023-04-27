namespace lab4;

public class Reader<T> {
    public static List<T> Read(String path, Func<String[], T> generate) {
        var data = File.ReadAllLines(path)
                    .Skip(1)
                    .Select(a => a.Split(','))
                    .Select(generate)
                    .ToList();
        return data;
    }
}