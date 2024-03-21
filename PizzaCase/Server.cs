using System.Net;
using System.Net.Sockets;
using System.Text;


public class Server
{
	public int port;

	public List<Order> ordersRecieved;

	private List<Socket> sockets;

	private int maxSockets;

	public Socket Listen()
	{
		return null;
	}

}

