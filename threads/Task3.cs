namespace lab5;
using System;
using System.Threading;

// 3. Napisz program, który począwszy od zadanego katalogu będzie wyszukiwał pliki, których nazwa będzie posiadała zadany napis (podnapis, np. makaron.txt posiada "ron"). Wyszukiwanie ma brać pod uwagę podkatalogi. Wyszukiwanie ma odbywać się w wątku. Kiedy wątek wyszukujący znajdzie plik pasujący do wzorca wątek główny ma wypisać nazwę tego pliku do konsoli (wątek wyszukujący ma nie zajmować się bezpośrednio wypisywaniem znalezionych plików do konsoli).

public class Task3 
{
    string substring;
    string folderPath;
    public List<String> files = new();
    public ManualResetEvent signal;

    public Task3(string substring, string folderPath)
    {
        this.substring = substring;
        this.folderPath = folderPath;
        signal = new ManualResetEvent(false);
    }
    
    public void Start()
    {
        SearchForFiles searchFiles = new SearchForFiles(folderPath, substring, this); 
        Thread searchThread = new Thread(new ThreadStart(searchFiles.Start));
        searchThread.Start();

        WriteFilename writeFilename = new WriteFilename(this);
        Thread writingThread = new Thread(new ThreadStart(writeFilename.Start));
        writingThread.Start();

        searchThread.Join();
        signal.Set();
        writeFilename.end = true;
    }
    
}