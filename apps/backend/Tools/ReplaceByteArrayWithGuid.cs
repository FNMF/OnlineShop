using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tools
{
    public static class ReplaceByteArrayWithGuid
    {
        public static void Start()
        {
            // 默认扫描目录
            string targetDirectory = @"D:\Project\OnlineShop\apps\backend\API\Domain\Entities\Models";

            // 模式选择： lowercase / uppercase / ignorecase
            Console.WriteLine("请选择替换模式：lowercase / uppercase / ignorecase（默认：ignorecase）");
            string mode = "ignorecase";
            string str = Console.ReadLine();
            if (!string.IsNullOrEmpty(str))
            {
                mode = str.ToLower();
            }
            if (mode != "lowercase" && mode != "uppercase" && mode != "ignorecase")
            {
                Console.WriteLine("无效的模式，使用默认的 ignorecase 模式。");
                mode = "ignorecase";
            }
            if (str == null)
            {
                Console.WriteLine("未输入模式，使用默认的 ignorecase 模式。");
            }

            // === 备份地址：Tools 项目的 BackUps 文件夹 ===
            string solutionDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            string backupDir = Path.Combine(solutionDir, "BackUps");

            if (!Directory.Exists(backupDir))
            {
                Directory.CreateDirectory(backupDir);
            }

            RegexOptions regexOptions = RegexOptions.None;
            string pattern;

            switch (mode)
            {
                case "lowercase":
                    pattern = @"byte\[\]\s+(\w*uuid\w*)";
                    break;
                case "uppercase":
                    pattern = @"byte\[\]\s+(\w*Uuid\w*)";
                    break;
                default: // ignorecase
                    pattern = @"byte\[\]\s+(\w*uuid\w*)";
                    regexOptions = RegexOptions.IgnoreCase;
                    break;
            }

            // 遍历所有 cs 文件
            foreach (var file in Directory.GetFiles(targetDirectory, "*.cs", SearchOption.AllDirectories))
            {
                string code = File.ReadAllText(file);
                string original = code;

                // ======== 类型替换 ========
                code = Regex.Replace(code, pattern, @"Guid $1", regexOptions);
                code = Regex.Replace(code, pattern.Replace("byte\\[\\]", "byte\\[\\]\\?"), @"Guid? $1", regexOptions);

                // ======== 移除 "= null!;" ========
                // 仅移除非 virtual 的情况
                code = Regex.Replace(code,
                    @"(public\s+(?!virtual)\w+.*\{[^}]*\})\s*=\s*null!;",
                    "$1");

                // ======== 如果有修改，先备份再覆盖 ========
                if (code != original)
                {
                    string relativePath = Path.GetRelativePath(targetDirectory, file);
                    string backupFilePath = Path.Combine(backupDir, relativePath);

                    Directory.CreateDirectory(Path.GetDirectoryName(backupFilePath));
                    File.Copy(file, backupFilePath, true); // 覆盖式备份
                    File.WriteAllText(file, code);

                    Console.WriteLine($"[UPDATED] {file}");
                }
            }

            Console.WriteLine($"替换完成！（模式：{mode}，备份目录：{backupDir}）");
        }
    }
}
