using System;
using System.IO;

namespace TextCorrector.Models {

  public class TextFile {
    public string FileName { get; set; }
    public string Content { get; set; }
    public string FilePath { get; set; }
    public DateTime LastModified { get; set; }

    public TextFile() { }

    public TextFile(string path) {
      FilePath = path;
      FileName = Path.GetFileName(path);

      if (File.Exists(path)) {
        Content = File.ReadAllText(path);
        LastModified = File.GetLastWriteTime(path);
      } else {
        Content = string.Empty;
        LastModified = DateTime.Now;
      }
    }

    public void Save() {
      File.WriteAllText(FilePath, Content);
      LastModified = DateTime.Now;
    }
  }
}
