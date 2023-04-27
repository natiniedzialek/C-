namespace lab5;

public class SearchForFiles
{
    public bool end = false;
    string folderPath;
    string substring;
    Task3 parent;

    public SearchForFiles(string folderPath, string substring, Task3 parent) {
        this.folderPath = folderPath;
        this.substring = substring;
        this.parent = parent;
    }

    public void Start() {
        foreach (string file in Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories)) {
            if (file.Contains(substring)) {
                parent.files.Add(file);
                parent.signal.Set();
            }
        }
        end = true;
    }
}