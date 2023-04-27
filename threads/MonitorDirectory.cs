namespace lab5;

public class MonitorDirectory {
    string folderPath;
    public bool end = false;

    public MonitorDirectory(string folderPath) {
        this.folderPath = folderPath;
    }

    public void Start()
    {
        FileSystemWatcher watcher = new FileSystemWatcher();
        watcher.Path = folderPath;

        watcher.Created += OnCreated;
        watcher.Deleted += OnDeleted;

        watcher.EnableRaisingEvents = true;

        while(!end) {}
    }

    public void OnCreated(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"Added file: {e.Name}");
    }

    public void OnDeleted(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"Deleted file: {e.Name}");
    }
}