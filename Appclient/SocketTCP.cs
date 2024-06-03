using System.Net.Sockets;
using System.Net;
using System.Text;

public class SocketTCP : Socket
{
    private System.Net.Sockets.Socket s;

    public SocketTCP()
    {
        s = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    ~SocketTCP() 
    {
        Close();
    }

    /// <summary>
    /// close socket
    /// </summary>
    public void Close() {
        s.Close();
    }

    /// <summary>
    /// connect socket to endpoint
    /// </summary>
    /// <param name="ipAddress">ipadress to which the endpoint will be connected</param>
    /// <param name="port">port to which the endpoint will be connected</param>
    public void Connect(string ipAddress, int port) {

        s.Connect(IPAddress.Parse(ipAddress), port);

    }

    /// <summary>
    /// send data
    /// </summary>
    /// <param name="byteArray">data to be send</param>
    public void Send(byte[] byteArray) {

        s.Send(byteArray);
    }


}

