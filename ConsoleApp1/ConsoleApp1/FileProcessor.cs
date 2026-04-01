using System;
using System.Collections.Generic;
using System.IO;
using TextCorrector.Models;

namespace TextCorrector.Utils {
  /// Class for processing files in directory
  public static class FileProcessor {
    /// Get all text files from directory
    public static List<TextFile> GetTextFiles(string directoryPath, bool searchSubdirectories = true) {
      List<TextFile> textFiles = new List<TextFile>();

      if (!Directory.Exists(directoryPath)) {
        return textFiles;
      }

      SearchOption searchOption = searchSubdirectories
        ? SearchOption.AllDirectories
        : SearchOption.TopDirectoryOnly;

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

    /// Get all text files with specific extension
    public static List<TextFile> GetTextFilesByExtension(string directoryPath, string extension, bool searchSubdirectories = true) {
      List<TextFile> textFiles = new List<TextFile>();

      if (!Directory.Exists(directoryPath)) {
        return textFiles;
      }

      string searchPattern = $"*{extension}";
      SearchOption searchOption = searchSubdirectories
        ? SearchOption.AllDirectories
        : SearchOption.TopDirectoryOnly;

      string[] filePaths = Directory.GetFiles(directoryPath, searchPattern, searchOption);

      foreach (string filePath in filePaths) {
        try {
          textFiles.Add(new TextFile(filePath));
        } catch (Exception ex) {
                    Console.WriteLine($"Error reading file {filePath}:{ex.Message}");
        }
      }

      return textFiles;
    }

    /// Create backup of file
    public static void BackupFile(string filePath) {
      if (File.Exists(filePath)) {
        string backupPath = filePath + ".backup";
        File.Copy(filePath, backupPath, true);
      }
    }

    /// Restore file from backup
    public static void RestoreFromBackup(string filePath) {
      string backupPath = filePath + ".backup";
      if (File.Exists(backupPath)) {
        File.Copy(backupPath, filePath, true);
      }
    }
  }
}
