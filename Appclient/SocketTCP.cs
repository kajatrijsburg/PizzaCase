using System.Net.Sockets;
using System.Net;
using System.Text;

public class SocketTCP : Socket
{
    private System.Net.Sockets.Socket s;
    public string decodeddata;

    //todo add get

    public SocketTCP()
    {
        s = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    ~SocketTCP() 
    {
        Close();
    }

    public void Close() {
        s.Close();
    }

    public void Connect(string ipAddress, int port) {

        s.Connect(IPAddress.Parse(ipAddress), port);

    }

    public void Send(byte[] byteArray) {

        s.Send(byteArray);
        //send oreder has been received.
    }


    public void Recieve(byte[] byteArray) {
    }
}

