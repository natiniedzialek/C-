namespace lab6;
using System.Net;
using System.Net.Sockets;
using System.Text;
public static class ServerTask1 
{
    public static void Run() 
    {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress iPAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(iPAddress, 9000);

        Socket serverSocket = new
        (
            localEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp
        );

        serverSocket.Bind(localEndPoint);
        serverSocket.Listen(100);

        Socket clientSocket = serverSocket.Accept();
        byte[] buffer = new byte[1024];

        int received = clientSocket.Receive(buffer, SocketFlags.None);
        received = received <= 1_024 ? received : 1_024;

        string message = Encoding.UTF8.GetString(buffer, 0, received);

        string echo = "i've received: " + message;
        byte[] echoBytes = Encoding.UTF8.GetBytes(echo);
        clientSocket.Send(echoBytes, 0);

        try 
        {
            serverSocket.Shutdown(SocketShutdown.Both);
            serverSocket.Close();
        }
        catch {}
        
    }
}