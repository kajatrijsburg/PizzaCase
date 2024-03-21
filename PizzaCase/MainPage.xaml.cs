namespace PizzaCase
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public SocketTCP tCP;
        public byte[] data;

        public MainPage()
        {
            InitializeComponent();
            tCP = new SocketTCP();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            tCP.Connect("127.0.0.1");
            tCP.Recieve(data);
            CounterBtn.Text = tCP.decodeddata;
            tCP.Close();

            //SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
