using System.Net.Sockets;
using System.Net;
using System.Text;

public class SocketTCP : Socket
{
    private System.Net.Sockets.Socket s;
    private System.Net.Sockets.Socket Acceptsocket;
    public string decodeddata;




    public void Close() {
        Acceptsocket.Close();
    }

    public void Connect(string ipAddress) {
        s = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        s.Bind(new IPEndPoint(IPAddress.Parse(ipAddress), 12345));
     
        s.Listen(1);
        Acceptsocket = s.Accept();
        s.Close();

    }

    public void Send(byte[] byteArray) {

        Acceptsocket.Send(byteArray);
        //send oreder has been received.
    }

    public void Recieve(byte[] byteArray) {

        byte[] data;

        data = new byte[Acceptsocket.SendBufferSize];
        int j = Acceptsocket.Receive(data);
        byte[] adata = new byte[j];
        for (int i = 0; i < j; i++)
            adata[i] = data[i];
        decodeddata = Encoding.UTF8.GetString(adata);
        //make pizza order aan.
    }
}

