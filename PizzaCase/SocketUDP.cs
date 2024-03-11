public class SocketUDP : Socket
{
	public abstract void Close();

	public abstract void Connect(string ipAddress);

	public abstract void Send(byte[] byteArray);

	public abstract void Recieve(byte[] byteArray[]);

}

