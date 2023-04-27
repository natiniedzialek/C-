namespace lab5;

public class WriteFilename
{
    public bool end = false;
    Task3 parent;

    public WriteFilename(Task3 parent)
    {
        this.parent = parent;
    }

    public void Start()
    {
        while(!end) 
        {
            parent.signal.WaitOne();
            foreach(string file in parent.files) {
                Console.WriteLine(file);
            }
            parent.signal.Reset();
        }
    }

}