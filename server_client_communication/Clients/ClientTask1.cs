// 1. Proszę napisać program serwera oraz program klienta. Serwer będzie oczekiwał na połączenie jednego klienta. Klient po połączeniu z serwerem ma wysłać na serwer wpisaną z klawiatury tekstową wiadomość. Jeżeli długość wiadomości przekroczy 1024 bajty proszę ograniczyć ją do 1024 bajtów. Serwer ma odebrać wiadomość i wypisać ją w postaci zdekodowanego napisu (String-a, a nie tablicy bajtów) do konsoli. Następnie ma wysłać do klienta wiadomość "odczytalem: " i przesłany przez klienta napis. Jeżeli długość wiadomości przekroczy 1024 bajty proszę ograniczyć ją do 1024 bajtów. Po przesłaniu program serwera ma zakończyć działanie. Klient ma odebrać wiadomość od serwera, wypisać ją na ekran w postaci napisu (String) i zakończyć działanie.
namespace lab6;
using System.Net;
using System.Net.Sockets;
using System.Text;

public static class ClientTask1 
{
    public static void Run()
    {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 9000);

        Socket clientSocket = new
        (
            localEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp
        );

        clientSocket.Connect(localEndPoint);

        Console.WriteLine("Enter clients message: ");
        string message = Console.ReadLine();
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        
        clientSocket.Send(messageBytes, SocketFlags.None);

        var buffer = new byte[1_024];
        int received = clientSocket.Receive(buffer, SocketFlags.None);
        received = received <= 1_024 ? received : 1_024;
        
        string answer = Encoding.UTF8.GetString(buffer, 0, received);
        Console.WriteLine(answer);

        try
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
        catch {}
    }
}