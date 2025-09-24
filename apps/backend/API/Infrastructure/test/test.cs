using SKIT.FlurlHttpClient.Wechat.TenpayV3;
using System.Reflection;

namespace API.Infrastructure.test
{
    public class test
    {
        public static void DumpWechatClientSignatures(WechatTenpayClient client)
        {
            var type = client.GetType();
            Console.WriteLine("Type: " + type.FullName);

            var methodNames = new[] { "VerifyEventSignature", "DecryptEventResource", "GenerateParametersForJsapiPayRequest" };
            foreach (var name in methodNames)
            {
                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                  .Where(m => m.Name == name).ToArray();
                Console.WriteLine($"Found {methods.Length} methods named {name}:");
                foreach (var m in methods)
                {
                    Console.WriteLine("  " + m);
                    foreach (var p in m.GetParameters())
                        Console.WriteLine($"    param: {p.ParameterType.FullName} {p.Name}");
                    Console.WriteLine("    IsGenericMethodDefinition: " + m.IsGenericMethodDefinition);
                }
            }
        }
    }
}
