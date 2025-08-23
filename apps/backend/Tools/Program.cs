namespace Tools
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is Main");
            Console.WriteLine("这是用于批量修改项目的脚本");
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("请选择执行的子方法");
                Console.WriteLine();
                Console.WriteLine("1. ReplaceByteArrayWithGuid.Start()");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        ReplaceByteArrayWithGuid.Start();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("无效的选择");
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("输入任意键继续");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
