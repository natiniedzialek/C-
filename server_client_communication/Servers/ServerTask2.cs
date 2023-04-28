namespace lab6;
using System.Net;
using System.Net.Sockets;
using System.Text;
public static class ServerTask2 
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

        // receive message length
        byte[] lenBuffer = new byte[4];

        int received = clientSocket.Receive(lenBuffer, SocketFlags.None);
        int lenOfMessage = BitConverter.ToInt32(lenBuffer, 0);

        // receive message
        byte[] buffer = new byte[lenOfMessage];

        clientSocket.Receive(buffer, SocketFlags.None);

        string message = Encoding.UTF8.GetString(buffer, 0, lenOfMessage);
        string echo = "i've received: " + message;

        // send message length
        byte[] echoLength = BitConverter.GetBytes(echo.Length);

        clientSocket.Send(echoLength);

        // send message
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