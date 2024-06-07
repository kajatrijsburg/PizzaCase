using System.Net.Sockets;
using System.Net;
using System.Text;

public class SocketUDP : Socket
{

    private System.Net.Sockets.Socket s;
    private string decodeddata = "";
    private readonly int timeout = 1000;

    public SocketUDP()
    {
        s = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        s.ReceiveTimeout = timeout;
    }

    ~SocketUDP() {

        Close();
    }
    /// <summary>
    /// close server socket
    /// </summary>
    public void Close()
    {
        s.Close();
    }

    /// <summary>
    /// bind the server socket to endpoint.
    /// </summary>
    /// <param name="ipAddress">the ipadress that will be connected</param>
    /// <param name="port">the port that will be listend on</param>
    public void Connect(string ipAddress, int port)
    {
            s.Bind(new IPEndPoint(IPAddress.Parse(ipAddress), port));
    }

    /// <summary>
    /// receive data from client.
    /// </summary>
    /// <param name="byteArray">the bytearray to which the received data will be copied</param>
    public void Recieve(byte[] byteArray)
    {
        byte[] bytes = new byte[s.SendBufferSize];
        int j;
        try
        {
            j = s.Receive(bytes);
        }
        catch { return; }

        byte[] bytearray = new byte[j];
        for (int i = 0; i < j; i++)
            bytearray[i] = bytes[i];
        decodeddata = Encoding.Unicode.GetString(bytearray);
    }

    public string GetDecodedData() { return decodeddata; }

    public void SetDecodedData(string decodeddata) { this.decodeddata = decodeddata; }
}

