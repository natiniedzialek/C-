namespace lab5;
public class Consumer
{
    Task1 parent;
    public bool end = false;
    public int number;
    public Dictionary<int, int> memory = new();
    int time;

    public Consumer(Task1 parent, int number, int time)
    {
        this.parent = parent;
        this.number = number;
        this.time = time;
    }

    public void Start() 
    {
        while (!end) 
        {
            Console.WriteLine("Konsument " + number + " czeka");
            parent.consumerSemaphore.WaitOne();
            if (parent.dataList.Count > 0) 
            {
                int producerNumber = parent.dataList[0].producerNumber;
                // jeśli mamy już zapisany numer producenta
                if (memory.ContainsKey(producerNumber)) 
                {
                    memory[producerNumber] += 1;
                } else 
                {
                    memory[producerNumber] = 1;
                }
                parent.dataList.RemoveAt(0);
            }

            parent.consumerSemaphore.Release(); 
            Thread.Sleep(time);      
        }
    }
}