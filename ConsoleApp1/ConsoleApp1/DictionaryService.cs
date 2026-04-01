using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TextCorrector.Services {
  /// Service for working with error dictionary
  public static class DictionaryService {
  /// Load dictionary from JSON file
    public static Dictionary<string, string> LoadDictionary(string filePath) {
      try {
        if (File.Exists(filePath)) {
          string json = File.ReadAllText(filePath);
          return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
      } catch (Exception ex) {
        Console.WriteLine($"Error loading dictionary:{ex.Message}");
      }
      return null;
    }

    /// Save dictionary to JSON file
    public static void SaveDictionary(string filePath, Dictionary<string, string> dictionary) {
      try {
        string directory = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directory)) {
          Directory.CreateDirectory(directory);
        }

        string json = JsonSerializer.Serialize(dictionary, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
      } catch (Exception ex) {
        Console.WriteLine($"Error saving dictionary: {ex.Message}");
      }
    }

    public static Dictionary<string, string> GetDefaultDictionary() {
      return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
        { "привет", "привет" },
        { "првиет", "привет" },
        { "пирвет", "привет" },
        { "превед", "привет" },
        { "здраствуйте", "здравствуйте" },
        { "здрасте", "здравствуйте" },
        { "пока", "пока" },
        { "пака", "пока" },
        { "спс", "спасибо" },
        { "спасиба", "спасибо" },
        { "пожалста", "пожалуйста" },
        { "пожалуйста", "пожалуйста" },
        { "извините", "извините" },
        { "извените", "извините" },
        { "до свидания", "до свидания" },
        { "дасвидания", "до свидания" },
        { "как дела", "как дела" },
        { "какдила", "как дела" },
        { "хорошо", "хорошо" },
        { "харашо", "хорошо" },
        { "плохо", "плохо" },
        { "плоха", "плохо" },
        { "отлично", "отлично" },
        { "атлично", "отлично" },
        { "нормально", "нормально" },
        { "нармально", "нормально" },
        { "сегодня", "сегодня" },
        { "севодня", "сегодня" },
        { "завтра", "завтра" },
        { "завтро", "завтра" },
        { "вчера", "вчера" },
        { "вчера", "вчера" },
        { "телефон", "телефон" },
        { "тилифон", "телефон" },
        { "компьютер", "компьютер" },
        { "кампютер", "компьютер" },
        { "интернет", "интернет" },
        { "интернет", "интернет" },
        { "почта", "почта" },
        { "почьта", "почта" }
      };
    }
  }
}
