using API.Helpers;

namespace API.Test
{
    public static class Tester
    {
        public static void Run()
        {
            string ip = "8.8.8.8";
            Console.WriteLine($"IP: {ip}");
            for (int i = 0; i < 5; i++)
            {
                string key = RandomKeyHelper.GetIpKey(ip);
                Console.WriteLine($"Key {i + 1}: {key}");
            }
        }
    }
}
