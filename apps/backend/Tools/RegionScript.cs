using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tools
{
    internal class RegionScript
    {
        private static readonly string InputDir = @"D:\0文件\快捷使用\RigionSQL";
        private static readonly string OutputDir = @"D:\0文件\快捷使用\OutPutRigion";

        private static readonly Dictionary<string, int> ProvinceCodeMap = new();
        private static readonly Dictionary<string, int> CityCodeMap = new();
        public static void Start()
        {
            Directory.CreateDirectory(OutputDir);

            // 顺序非常重要
            ConvertProvince();
            ConvertCity();
            ConvertCounty();

            Console.WriteLine("全部转换完成");
        }

        // ===================== Province =====================
        static void ConvertProvince()
        {
            var file = Path.Combine(InputDir, "province.sql");
            if (!File.Exists(file)) return;

            var sql = File.ReadAllText(file);
            var sb = new StringBuilder();

            var matches = Regex.Matches(sql,
                @"VALUES\s*\('(?<id>\d+)'\s*,\s*'(?<name>[^']+)'\s*,\s*'(?<code>\d+)'");

            foreach (Match m in matches)
            {
                int id = int.Parse(m.Groups["id"].Value);
                string name = m.Groups["name"].Value;
                string code = m.Groups["code"].Value;

                ProvinceCodeMap[code] = id;

                sb.AppendLine(
                    $"INSERT INTO provinces (id, name) VALUES ({id}, '{name}');"
                );
            }

            WriteOut("provinces_converted.sql", sb);
        }

        // ===================== City =====================
        static void ConvertCity()
        {
            var file = Path.Combine(InputDir, "city.sql");
            if (!File.Exists(file)) return;

            var sql = File.ReadAllText(file);
            var sb = new StringBuilder();

            var matches = Regex.Matches(sql,
                @"VALUES\s*\('(?<id>\d+)'\s*,\s*'(?<name>[^']+)'\s*,\s*'(?<cityCode>\d+)'\s*,\s*'(?<provinceCode>\d+)'");

            foreach (Match m in matches)
            {
                int id = int.Parse(m.Groups["id"].Value);
                string name = m.Groups["name"].Value;
                string cityCode = m.Groups["cityCode"].Value;
                string provinceCode = m.Groups["provinceCode"].Value;

                if (!ProvinceCodeMap.TryGetValue(provinceCode, out int provinceId))
                    continue; // 防御性处理

                CityCodeMap[cityCode] = id;

                sb.AppendLine(
                    $"INSERT INTO cities (id, name, province_id) VALUES ({id}, '{name}', {provinceId});"
                );
            }

            WriteOut("cities_converted.sql", sb);
        }

        // ===================== County / District =====================
        static void ConvertCounty()
        {
            var file = Path.Combine(InputDir, "county.sql");
            if (!File.Exists(file)) return;

            var sql = File.ReadAllText(file);
            var sb = new StringBuilder();

            var matches = Regex.Matches(sql,
                @"VALUES\s*\('(?<id>\d+)'\s*,\s*'(?<name>[^']+)'\s*,\s*'(?<countyCode>\d+)'\s*,\s*'(?<cityCode>\d+)'");

            foreach (Match m in matches)
            {
                int id = int.Parse(m.Groups["id"].Value);
                string name = m.Groups["name"].Value;
                string cityCode = m.Groups["cityCode"].Value;

                if (!CityCodeMap.TryGetValue(cityCode, out int cityId))
                    continue;

                sb.AppendLine(
                    $"INSERT INTO districts (id, name, city_id) VALUES ({id}, '{name}', {cityId});"
                );
            }

            WriteOut("districts_converted.sql", sb);
        }

        // ===================== 输出 =====================
        static void WriteOut(string fileName, StringBuilder content)
        {
            File.WriteAllText(
                Path.Combine(OutputDir, fileName),
                content.ToString(),
                Encoding.UTF8
            );
        }
    }
}
