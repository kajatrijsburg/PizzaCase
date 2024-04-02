public interface Socket
{
	void Close();

	void Connect(string ipAddress);

	void Send(byte[] byteArray);

	void Recieve(byte[] byteArray);

}

