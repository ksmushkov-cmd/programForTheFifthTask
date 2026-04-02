using System;
using System.Collections.Generic;
using System.IO;
using TextCorrector.Models;

namespace TextCorrector.Utils {

  public class FileProcessor {

    public List<TextFile> GetTextFiles(string directoryPath, bool searchSubdirectories = true) {
      List<TextFile> textFiles = new List<TextFile>();

      if (!Directory.Exists(directoryPath)) {
        return textFiles;
      }

      SearchOption searchOption;
      if (searchSubdirectories) {
        searchOption = SearchOption.AllDirectories;
      } else {
        searchOption = SearchOption.TopDirectoryOnly;
      }

      string[] filePaths = Directory.GetFiles(directoryPath, "*.txt", searchOption);

      foreach (string filePath in filePaths) {
        try {
          textFiles.Add(new TextFile(filePath));
        } catch (Exception ex) {
          Console.WriteLine($"Error reading file {filePath}:{ex.Message}");
        }
      }

      return textFiles;
    }
  }
}
