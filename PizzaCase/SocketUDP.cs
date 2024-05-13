using System.Net.Sockets;
using System.Net;
using System.Text;

public class SocketUDP : Socket
{

    private System.Net.Sockets.Socket s;
    //private System.Net.Sockets.Socket Acceptsocket;
    public string decodeddata;
    private bool connected;

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
            s.Bind(new IPEndPoint(IPAddress.Parse(ipAddress), port));
    }

    public void Send(byte[] byteArray)
    {
        s.Send(byteArray);

    }

    public void Recieve(byte[] byteArray)
    {
        byte[] bytes = new byte[s.SendBufferSize];

        int j = s.Receive(bytes);
        byte[] bytearray = new byte[j];
        for (int i = 0; i < j; i++)
            bytearray[i] = bytes[i];
        decodeddata = Encoding.UTF8.GetString(bytearray);
       
       

    }
}

