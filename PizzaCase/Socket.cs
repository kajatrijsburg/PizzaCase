public interface Socket
{
	void Close();

	void Connect(string ipAddress, int port);

	void Recieve(byte[] byteArray);

}

