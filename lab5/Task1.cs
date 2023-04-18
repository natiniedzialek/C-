namespace lab5;

// Napisz program modelujący problem producent-konsument. Program ma uruchomić n wątków generujących dane oraz m wątków pobierających dane. Każdy z wątków ma przechowywać informację o swoim numerze porządkowym, załóżmy, że są numerowane od 0..n-1 i odpowiednio od 0..m-1. Generowanie i odpowiednio odczytywanie danych przez każdy wątek ma odbywać się w losowych przedziałach czasu, które będą podawane jako parametr dla danego wątku. Generowane dane mają być umieszczane na liście (lub innej strukturze), załóżmy, że dane to obiekty klasy, które będą miały identyfikator informujący o numerze porządkowym wątku, który je wygenerował. Wątek pobierający dane pobiera i usuwa zawsze pierwszy element ze struktury danych i "zapamiętuje", jaki był identyfikator wątku producenta, który te dane tam umieścił. Program ma zatrzymywać wszystkie wątki jeśli wciśniemy klawisz q i kończyć swoje działanie. Każdy zatrzymywany wątek ma wypisać ile "skonsumował" danych od poszczególnych producentów, np. Producent 0 - 4, Producent 1 - 5 itd.
public class Task1
{
    public List<Data> dataList = new();
    private int consumerNumber;
    private int producerNumber;
    public int criticalConsumerNumber;
    public Semaphore consumerSemaphore;
    public Semaphore producerSemaphore;
    List<Consumer> consumers = new();
    List<Producer> producers = new();
    public Random random = new Random(Environment.TickCount);


    public void Start()
    {

        for (int a = 0; a < producerNumber; a++)
        {
            Producer producer = new Producer(this, a, (int)random.NextInt64(1000, 10000));
            producers.Add(producer);
            Thread t = new Thread(new ThreadStart(producer.Start));
            t.Start();
        }

        for (int a = 0; a < consumerNumber; a++)
        {
            Consumer consumer = new Consumer(this, a, (int)random.NextInt64(500, 1000));
            consumers.Add(consumer);
            Thread t = new Thread(new ThreadStart(consumer.Start));
            t.Start();
        }

        while(Console.ReadKey(true).Key != ConsoleKey.Q) {}

        foreach(Producer p in producers)
            p.end = true;
        
        foreach(Consumer c in consumers)
            c.end = true;

        foreach(Consumer c in consumers)
        {
            Console.WriteLine("Consumer number " + c.number + " got access to following data : ");
            foreach(int i in c.memory.Keys) 
            {
                Console.WriteLine("Producer: " + i + " data count: " + c.memory[i]);
            }
            Console.WriteLine("");
        }
            

    }

    public Task1(int consumerNumber, int producerNumber, int criticalConsumerNumber)
    {
        this.consumerNumber = consumerNumber;
        this.producerNumber = producerNumber;
        this.criticalConsumerNumber = criticalConsumerNumber;

        consumerSemaphore = new Semaphore(initialCount: criticalConsumerNumber, maximumCount: criticalConsumerNumber);
        producerSemaphore = new Semaphore(initialCount: 1, maximumCount: 1);
    }

}