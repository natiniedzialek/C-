//2. Proszę napisać programy, które będą działały analogicznie jak programy z punktu 1 z tą różnicą, że długość wiadomości nie będzie ograniczona do 1024 bajtów.
namespace lab6;
using System.Net;
using System.Net.Sockets;
using System.Text;

public static class ClientTask2 
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

        // send message length
        byte[] messageLength = BitConverter.GetBytes(message.Length);

        clientSocket.Send(messageLength);

        // send message        
        clientSocket.Send(messageBytes, SocketFlags.None);

        // receive message length
        byte[] lenBuffer = new byte[4];

        int received = clientSocket.Receive(lenBuffer, SocketFlags.None);
        int lenOfMessage = BitConverter.ToInt32(lenBuffer, 0);

        // receive message
        byte[] buffer = new byte[lenOfMessage];

        clientSocket.Receive(buffer, SocketFlags.None);
        
        string answer = Encoding.UTF8.GetString(buffer, 0, lenOfMessage);
        Console.WriteLine(answer);

        try
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
        catch {}
    }
}