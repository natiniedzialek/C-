// 2. Napisz program, który będzie monitorował w czasie rzeczywistym zmiany zachodzące w wybranym katalogu polegające na usuwaniu lub dodawaniu do niego plików (nie trzeba monitorować podkatalogów). Jeżeli w katalogu pojawi się nowy plik program ma wypisać: "dodano plik [nazwa pliku]" a jeśli usunięto plik "usunięto plik [nazwa pliku]". Program ma się zatrzymywać po wciśnięciu litery "q". Monitorowanie ma być w osobnym wątku niż oczekiwanie na wciśnięcie klawisza!
namespace lab5;

public class Task2
{
    String folderPath;
    
    public Task2(String folderPath) 
    {
        this.folderPath = folderPath;
    }

    public void Start()
    {
        Console.WriteLine($"Monitoring folder: {folderPath}. Press 'q' to exit.");

        MonitorDirectory monitorDirectory = new MonitorDirectory(folderPath); 
        Thread watcherThread = new Thread(new ThreadStart(monitorDirectory.Start));
        watcherThread.Start();

        WaitForQ waitForQ = new WaitForQ();
        Thread listenerThread = new Thread(new ThreadStart(waitForQ.Start));
        listenerThread.Start();

        while(!waitForQ.end) {}
        monitorDirectory.end = true;
    }

}