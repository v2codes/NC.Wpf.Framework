using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace Microsoft.Extensions.Configuration;

public static class ConfigurationExtensions
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sections">按照 IConfiguration 获取配置项约定，使用 ':' 分割节点层级，例如 Configuration["Logging:LogLevel:Default"]</param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    //public static async Task SetValue<T>(string sections, string key, T value)
    //{
    //    var ssonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
    //    var file = "appsettings.json";
    //    var jsonString = await File.ReadAllTextAsync(file);
    //    var document = JsonNode.Parse(jsonString);

    //    var path = sections.Split(";");
    //    var recursionNode = document;
    //    foreach (var item in path)
    //    {
    //        recursionNode = recursionNode![item];
    //    }

    //    recursionNode = new JsonObject();
    //}
}


//async Task Main()
//{
//    await SetValue("", "Culture", "en-US");
//}

///// <summary>
///// 
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="path">按照 IConfiguration 获取配置项约定，使用 ':' 分割节点层级，例如 Configuration["Logging:LogLevel:Default"]</param>
///// <param name="key"></param>
///// <param name="value"></param>
///// <returns></returns>
//public static async Task SetValue<T>(string path, string key, T value)
//{
//    var ssonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
//    //var file = "appsettings.json";
//    //var jsonString = await File.ReadAllTextAsync(file);
//    var jsonStr = @"{
//					  // 支持的语言详见：https://learn.microsoft.com/en-us/windows/apps/publish/publish-your-app/supported-languages?pivots=store-installer-msix
//					  ""Culture"": ""zh-CN"",
//					  ""ModuleA"": {
//							""Address"": ""http://localhost"",
//					    	""Port"": 5000
//					  },
//					  ""ConnectionStrings"": {
//							""NCService"": ""Server=localhost;Database=NCWpfSample;User Id=sa;Password=Password01!;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=False;""
//					  }
//					}";
//    var jsonRemoveComment = RemoveComments(jsonStr);
//    //jsonRemoveComment.Dump();
//    var obj = JsonSerializer.Deserialize<dynamic>(jsonRemoveComment);
//    var json = JsonSerializer.Serialize(obj);
//    var rootNode = JsonNode.Parse(json);

//    var sections = path.Split(";");
//    var currentNode = rootNode;

//    for (int i = 0; i < sections.Length; i++)
//    {
//        var section = sections[i];
//        if (i == sections.Length - 1)
//        {
//            // Last section, set the value
//            if (currentNode is JsonObject jsonObject)
//            {
//                jsonObject[section] = JsonValue.Create(value);
//            }
//        }
//        else
//        {
//            // Intermediate section, navigate to the next level
//            if (currentNode[section] == null)
//            {
//                currentNode[section] = new JsonObject();
//            }
//            currentNode = currentNode[section];
//        }
//    }

//    rootNode.ToJsonString(new JsonSerializerOptions { WriteIndented = true }).Dump();

//    //File.WriteAllText(ConfigFilePath, jsonNode.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
//}

//private static string RemoveComments(string json)
//{
//    //var regex = new Regex(@"\/\/.*?$|\/\*.*?\*\/", RegexOptions.Singleline | RegexOptions.Multiline);
//    //return regex.Replace(json, string.Empty);

//    // Remove single-line comments
//    var regexSingleLine = new Regex(@"\/\/.*$", RegexOptions.Multiline);
//    json = regexSingleLine.Replace(json, string.Empty);

//    // Remove multi-line comments
//    var regexMultiLine = new Regex(@"\/\*.*?\*\/", RegexOptions.Singleline);
//    json = regexMultiLine.Replace(json, string.Empty);

//    // Normalize whitespace characters to ensure valid JSON
//    json = Regex.Replace(json, @"\t|\n|\r", "");

//    // Trim whitespace
//    json = json.Trim();

//    // Remove trailing commas (could cause issues in JSON)
//    var regexTrailingCommas = new Regex(@",\s*([\]}])");
//    json = regexTrailingCommas.Replace(json, "$1");

//    return json;
//}