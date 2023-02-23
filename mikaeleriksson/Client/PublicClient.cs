namespace mikaeleriksson.Client
{
    public class PublicClient
    {
        public HttpClient client { get; set; }

        public PublicClient(HttpClient client)
        {
            this.client = client;
        }

    }
}
