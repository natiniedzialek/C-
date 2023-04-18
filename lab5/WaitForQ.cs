namespace lab5;

public class WaitForQ {
    public bool end = false;
    public void Start() {
        while (Console.ReadKey(true).Key != ConsoleKey.Q) {}
        end = true;
    }
}