using System.Net.Sockets;
using System.Net;
using System.Text;

public class SocketUDP : Socket
{

    private System.Net.Sockets.Socket s;
    public string decodeddata;

    public SocketUDP()
    {
        s = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    }

    ~SocketUDP() {
        Close();
    }

    public void Close()
    {
        s.Close();

    }

    public void Connect(string ipAddress, int port)
    {
        s = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        s.Connect(new IPEndPoint(IPAddress.Parse(ipAddress), port));

    }

    public void Send(byte[] byteArray)
    {
        s.Send(byteArray);

    }

    public void Recieve(byte[] byteArray)
    {
    }
}

