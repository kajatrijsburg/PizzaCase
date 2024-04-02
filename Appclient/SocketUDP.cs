using System.Net.Sockets;
using System.Net;
using System.Text;

public class SocketUDP : Socket
{

    private System.Net.Sockets.Socket s;
    public string decodeddata;

    public void Close()
    {
        s.Close();

    }

    public void Connect(string ipAddress)
    {
        s = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        s.Connect(new IPEndPoint(IPAddress.Parse(ipAddress), 12345));

    }

    public void Send(byte[] byteArray)
    {
        s.Send(byteArray);

    }

    public void Recieve(byte[] byteArray)
    {
        byte[] bytes = new byte[s.SendBufferSize];
        int j = s.Receive(bytes);
        byte[] bytearray = new byte[j]; //todo change to bytearray
        for (int i = 0; i < j; i++)
            bytearray[i] = bytes[i];
        decodeddata = Encoding.UTF8.GetString(bytearray);
        //make pizza order aan.}

    }
}

