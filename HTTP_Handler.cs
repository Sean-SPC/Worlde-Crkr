using System.Net;
using System.IDisposable;

class HTTP_Handler
{
    public static async Task Main()
    {
        using (var client = new WebRequest())
        {
            var contents = client.DownloadString("http://www.google.com");
            Console.WriteLine(contents);
        }
    }
}