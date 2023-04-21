// cryptography
namespace lab7;

public static class Program 
{
    public static void Main(string [] args) 
    {
        Task1();
        Task2();
        Task3();
        Task4();
    }

    public static void Task1() 
    {
        do 
        {
            Console.WriteLine("Enter task number: 0 - generate keys, 1 - encrypt file, 2 - decrypt file.");
            int taskNumber = Convert.ToInt32(Console.ReadLine());
            while(!(taskNumber == 0 || taskNumber == 1 || taskNumber == 2)) 
            {
                Console.WriteLine("Incorrect task number! Choose between 0, 1, 2.");
                taskNumber = Convert.ToInt32(Console.ReadLine());
            }

            RSAProgram rsaProgram = new();
            switch(taskNumber)
            {
                case 0:
                    rsaProgram.GenerateKeys();
                    break;
                case 1:
                    rsaProgram.EncryptData();
                    break;
                case 2:
                    rsaProgram.DecryptData();
                    break;
            }
            Console.WriteLine("Press q to quit or anything else to continue.");
        } while (Console.ReadKey(true).Key != ConsoleKey.Q);
    }

    public static void Task2() 
    {
        Console.WriteLine("Enter file with data:");
        string dataFilePath = Console.ReadLine();
        Console.WriteLine("Enter file with hash:");
        string hashFilePath = Console.ReadLine();
        Console.WriteLine("Enter file with algorithm name:");
        string algorithmFilePath = Console.ReadLine();

        while(!File.Exists(dataFilePath) || !File.Exists(algorithmFilePath)) 
        {
            Console.WriteLine("Files do not exist! Enter correct filenames:");
            Console.WriteLine("Enter file with data:");
            dataFilePath = Console.ReadLine();
            Console.WriteLine("Enter file with hash:");
            hashFilePath = Console.ReadLine();
            Console.WriteLine("Enter file with algorithm name:");
            algorithmFilePath = Console.ReadLine();
        }

        HashProgram hashProgram = new(dataFilePath, hashFilePath, algorithmFilePath);

        if(File.Exists(hashFilePath)) 
        {
            Console.WriteLine(String.Format(
                "Is the hash value correct? {0}",
                hashProgram.CheckHash()
            ));
        } else 
        {
            hashProgram.ToFile();
        }
    }

    public static void Task3() 
    {
        Console.WriteLine("Enter file with data:");
        string ?dataFilePath = Console.ReadLine();
        Console.WriteLine("Enter file with signature:");
        string ?signatureFilePath = Console.ReadLine();

        SignatureProgram signatureProgram = new(dataFilePath, signatureFilePath);

        if(File.Exists(signatureFilePath)) 
        {
            Console.WriteLine(String.Format(
                "Is the signature valid? {0}",
                signatureProgram.CheckSignature()
            ));
        } else 
        {
            signatureProgram.GenerateSignature();
        }
    }

    public static void Task4() 
    {
        Console.WriteLine("Enter password:");
        string password = Console.ReadLine();

        PasswordProgram passwordProgram = new PasswordProgram(password);

        passwordProgram.EncryptWithPassword();
        passwordProgram.DecryptWithPassword();
    }
}