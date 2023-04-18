namespace lab5;

public class Producer
{
    Task1 parent;
    public int number = 0;
    public bool end = false; 
    int time;

    public Producer(Task1 parent, int number, int time)
    {
        this.parent = parent;
        this.number = number;
        this.time = time;
    }

    public void Start()
    {
        while (!end)
        {
            Console.WriteLine("Producent " + number + " czeka");
            parent.producerSemaphore.WaitOne();
            for (int i = 0; i < parent.criticalConsumerNumber; i++)
            {
                parent.consumerSemaphore.WaitOne();
            }

            parent.dataList.Add(new Data(this));

            parent.producerSemaphore.Release();
            parent.consumerSemaphore.Release(parent.criticalConsumerNumber);
            Thread.Sleep(time);
        }
    }
}