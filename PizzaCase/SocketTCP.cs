using System.Net.Sockets;
using System.Net;
using System.Text;

public class SocketTCP : Socket
{
    private System.Net.Sockets.Socket s;
    private System.Net.Sockets.Socket Acceptsocket;
    public string decodeddata;

    private int listensockets = 1;

    public SocketTCP()
    {
        s = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    ~SocketTCP()
    {
        Close();
    }


    public void Close() {
        if (Acceptsocket != null)
        {
            Acceptsocket.Close();

        }

    }

    public void Connect(string ipAddress, int port) {
        try
        {
            s.Bind(new IPEndPoint(IPAddress.Parse(ipAddress), port));
           
            s.Listen(listensockets);
            Acceptsocket = s.Accept();
            s.Close();
        }
        catch (Exception ex) { }

    }

    public void Send(byte[] byteArray) {

        Acceptsocket.Send(byteArray);
        //send oreder has been received.
    }


    public void Recieve(byte[] byteArray) {
        if (Acceptsocket == null) {
            return;
        }

        byte[] bytes = new byte[Acceptsocket.SendBufferSize];

        int j = Acceptsocket.Receive(bytes);
        byte[] bytearray = new byte[j]; //todo change to bytearray
        for (int i = 0; i < j; i++)
            bytearray[i] = bytes[i];
        decodeddata = Encoding.UTF8.GetString(bytearray);
        

    }
}

