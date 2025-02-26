namespace CQRS.DesignPattern.CancellationPattern
{
    public class TestCancellationTask
    {
        public async static Task MainMethod()
        {
            LongRunningClass longRunningClass = new LongRunningClass();
            TaskRnd taskRnd = new TaskRnd();
            try
            {
                //var task1= taskRnd.method1Async();
                //var task2 = taskRnd.method2Async();
                //await Task.WhenAll(task1, task2);
                //await taskRnd.method3Async();
                //await taskRnd.method4Async();
                //var task3= taskRnd.method3Async();
                //var task4= taskRnd.method4Async();
                //await taskRnd.method4Async();
                //await taskRnd.method4Async();

                //await ValueTask.wh(task3, task4);
                //await longRunningClass.MainAsync();
                //var aa= taskRnd.Method5Async(4);
                //var xx = taskRnd.Method6Async(5);
                //var output1 = await aa;
                //Console.WriteLine(output1);
                //var output2 = await aa;
                //var output3 = await xx;
                //Console.WriteLine(xx);
                //var output4 = await xx;
                await taskRnd.method1Async();
                Console.WriteLine("First Method complete");
                await taskRnd.method2Async();
                Console.WriteLine("Second Method complete");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
    }
    public class TaskRnd
    {
        public async Task method11Async()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine($"method11Async {i}");
                    
                    Task.Delay(100);
                }

            });
        }
        public async Task method12Async()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine($"method12Async {i}");
                    Task.Delay(100);
                }
            });
            
        }
        public async Task method1Async()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"method1Async {i}");
               // var aa = 5 / i;
                await Task.Delay(100);
            }
        }
        public async Task method2Async()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"method2Async {i}");
                await Task.Delay(100);
            }
        }
        public async ValueTask method3Async()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"method3Async {i}");
                await Task.Delay(100);
                await Task.Delay(1);
            }
        }
        public async ValueTask method4Async()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"method4Async {i}");
                await Task.Delay(100);
            }
        }
        public async ValueTask<int> Method5Async(int a)
        {
            await Task.Delay(10);
            return a;
        }
        public async Task<int> Method6Async(int a)
        {
            await Task.Delay(10);
            return a;
        }
    }
}
