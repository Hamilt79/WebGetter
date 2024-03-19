class Program {
    static void Main(string[] args) {
        if (args.Length != 2) {
            Console.WriteLine("Usage: <filename> <url> <filetosaveas>");
            return;
        }

        string url = args[0];
        string fileName = args[1];
        HttpClient client = new HttpClient();
        Task<HttpResponseMessage> task = client.GetAsync(url);
        task.Wait();
        HttpResponseMessage response = task.Result;
        Console.WriteLine(response.StatusCode);
        Task<string> content = response.Content.ReadAsStringAsync();
        content.Wait();
        Console.WriteLine(content.Result);
        File.WriteAllText(fileName, content.Result);
    }
}
