using System.Net.Sockets;
using System.Net;
using System.Text;

public class SocketTCP : Socket
{
    private System.Net.Sockets.Socket s;
    private System.Net.Sockets.Socket acceptsocket;
    private string decodeddata = "";
    private readonly int timeout = 1000;

    private int listensockets = 1;

    public SocketTCP()
    {
        s = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    ~SocketTCP()
    {
        Close();
    }

    /// <summary>
    /// close the socket.
    /// </summary>
    public void Close() {
        if (acceptsocket != null)
        {
            acceptsocket.Close();
        }
    }

    /// <summary>
    /// bind the server socket to endpoint, and put it in listening state for any connections
    /// </summary>
    /// <param name="ipAddress">the ipadress that will be connected</param>
    /// <param name="port">the port that will be listend on</param>
    public void Connect(string ipAddress, int port) {
        try
        {
            s.Bind(new IPEndPoint(IPAddress.Parse(ipAddress), port));
           
            s.Listen(listensockets);
        }
        catch (Exception ex) { }

    }

    /// <summary>
    /// receive data from client.
    /// </summary>
    /// <param name="byteArray">the bytearray to which the received data will be copied</param>
    public void Recieve(byte[] byteArray) {
        acceptsocket = s.Accept();

        if (acceptsocket == null) {
            return;
        }

        acceptsocket.ReceiveTimeout = timeout;

        byte[] bytes = new byte[acceptsocket.SendBufferSize];
        int j;
        try
        {
           j = acceptsocket.Receive(bytes);
        } catch { return;  }
        

        byte[] bytearray = new byte[j];
        for (int i = 0; i < j; i++)
            bytearray[i] = bytes[i];
        decodeddata = Encoding.UTF8.GetString(bytearray);
        

    }


    public string GetDecodedData() { return decodeddata; }

    public void SetDecodedData(string decodeddata) { this.decodeddata = decodeddata; }
}

