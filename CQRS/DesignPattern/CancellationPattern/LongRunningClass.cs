namespace CQRS.DesignPattern.CancellationPattern
{
    public class LongRunningClass
    {
        public async Task LongRunningOperationAsync(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 100; i++)
            {
                // Periodically check if cancellation is requested
                cancellationToken.ThrowIfCancellationRequested();

                // Simulate some work
                await Task.Delay(100);
            }
        }
        void DoWork(CancellationToken token)
        {
            for (int i = 0; i < 10; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Cancellation requested.");
                    return;
                }
                // Simulate work
                Thread.Sleep(1000);
            }
        }
        static async Task LongRunningTaskAsync(CancellationToken token)
        {
            for (int i = 0; i < 10; i++)
            {
                // Check if cancellation is requested
                if (token.IsCancellationRequested)
                {
                    //can do after cancellation
                }
                token.ThrowIfCancellationRequested();
                Console.WriteLine($"Working... {i}");
                await Task.Delay(1000);  // Simulate work
            }
            Console.WriteLine("Task completed successfully.");
        }
        static async Task Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;
            // Simulate user cancellation after 2 seconds
            await Task.Run(() =>
            {
                Thread.Sleep(2000);
                cancellationTokenSource.Cancel();
            });
            try
            {
                Console.WriteLine("Starting long-running task...");
                await LongRunningTaskAsync(token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Task was cancelled.");
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }
            Console.WriteLine("Program completed.");
        }
        public async Task MainAsync()
        {
            using var cts = new CancellationTokenSource();

            // Cancel the operation after 2 seconds
            cts.CancelAfter(TimeSpan.FromSeconds(2));

            try
            {
                await LongRunningOperationAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation was canceled.");
            }
        }
        //Implementing Task Cancellation in .NET Core
        public async Task DownloadFileAsync(string url, CancellationToken token)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url, token);
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("File downloaded.");
            }
        }
        public async Task Run()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            try
            {
                Task downloadTask = DownloadFileAsync("https://example.com/file", cts.Token);
                // Simulate user cancellation
                cts.CancelAfter(3000); // Cancel after 3 seconds
                await downloadTask;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Download cancelled.");
            }
        }
        //Handling Timeouts with Cancellation Tokens
        public async Task DownloadWithTimeoutAsync(string url, int timeout)
        {
            using (CancellationTokenSource cts = new CancellationTokenSource(timeout))
            {
                try
                {
                    await DownloadFileAsync(url, cts.Token);
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Download timed out.");
                }
            }
        }
        public async Task Run1()
        {
            await DownloadWithTimeoutAsync("https://example.com/file", 5000); // 5-second timeout
        }
    }
}
