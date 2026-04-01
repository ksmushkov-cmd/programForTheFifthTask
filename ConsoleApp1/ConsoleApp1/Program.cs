using System;
using System.Collections.Generic;
using System.IO;
using TextCorrector.Models;
using TextCorrector.Services;
using TextCorrector.Utils;

namespace TextCorrector {
  class Program {
    static void Main(string[] args) {
      Console.OutputEncoding = System.Text.Encoding.UTF8;
      Console.WriteLine("TEXT CORRECTOR v1.0");

      // Load error dictionary
      string dictionaryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "error_dictionary.json");
      Dictionary<string, string> errorDictionary = DictionaryService.LoadDictionary(dictionaryPath);

      if (errorDictionary == null || errorDictionary.Count == 0) {
        Console.WriteLine("Dictionary not loaded. Using built-in dictionary.");
        errorDictionary = DictionaryService.GetDefaultDictionary();
         DictionaryService.SaveDictionary(dictionaryPath, errorDictionary);
      }

      Console.WriteLine($"Loaded corrections: {errorDictionary.Count}");

      // Select directory
      Console.Write("\nEnter directory path to process: ");
      string directoryPath = Console.ReadLine();

      if (!Directory.Exists(directoryPath)) {
        Console.WriteLine($"Directory does not exist:{directoryPath}");
        Console.ReadKey();
        return;
      }

      // Get all text files
      List<TextFile> textFiles = FileProcessor.GetTextFiles(directoryPath);
      Console.WriteLine($"Text files found: {textFiles.Count}");

      if (textFiles.Count == 0) {
        Console.WriteLine("No text files to process.");
        Console.ReadKey();
        return;
      }

      // Create services
      TextCorrectionService correctionService = new TextCorrectionService(errorDictionary);
      PhoneNumberFormatter phoneFormatter = new PhoneNumberFormatter();

      // Process each file
      Console.WriteLine("\nPROCESSING STARTED...\n");
      int processedCount = 0;
      int errorFixCount = 0;
      int phoneFixCount = 0;

      foreach (TextFile textFile in textFiles) {
        Console.WriteLine($"Processing: {textFile.FileName}");

        // Fix spelling errors
        string correctedContent = correctionService.CorrectText(textFile.Content);
        int fixes = correctionService.GetLastFixCount();
        errorFixCount += fixes;

        // Format phone numbers
        string formattedContent = phoneFormatter.FormatPhoneNumbers(correctedContent);
        int phoneFixes = phoneFormatter.GetLastReplaceCount();
        phoneFixCount += phoneFixes;

        textFile.Content = formattedContent;
        textFile.Save();

        Console.WriteLine($"Errors fixed: {fixes}, phones formatted: {phoneFixes}");
        ++processedCount;
      }

      Console.WriteLine($"📊 PROCESSING RESULTS\n" +
        $"Files processed: {processedCount}\n" +
        $"Spelling errors fixed: {errorFixCount}\n" +
        $"Phone numbers formatted: {phoneFixCount}\n" +
        $"\n✨ Done! Press any key to exit...");
      Console.ReadKey();
    }
  }
}
