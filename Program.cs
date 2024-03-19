class Program {
    static int Main(string[] args) {
        int timeout = 5000;
        int len = args.Length;
        if (len == 2) {
        } else if (len == 3) {
        } else {
            Console.Error.WriteLine("Usage: <filename> <url> <filetosaveas> (optional)<timeout-milliseconds>");
            return 1;
        }
        string url = args[0];
        string fileName = args[1];
        if (len == 3) {
            string timeoutStr = args[2];
            if (!int.TryParse(timeoutStr, out timeout)) {
                Console.Error.WriteLine("Invalid timeout value");
                return 1;
            }
        }
        try {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMilliseconds(timeout);
            Task<HttpResponseMessage> task = client.GetAsync(url);
            task.Wait();
            HttpResponseMessage response = task.Result;
            Console.WriteLine(response.StatusCode);
            Task<string> content = response.Content.ReadAsStringAsync();
            content.Wait();
            Console.WriteLine(content.Result);
            File.WriteAllText(fileName, content.Result);
        } catch (Exception e) {
            Console.Error.WriteLine(e.Message);
            return 1;
        }
        return 0;
    }
}
