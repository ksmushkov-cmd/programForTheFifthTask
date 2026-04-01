using System;
using System.Collections.Generic;
using System.Text;

namespace TextCorrector.Services {
  /// Service for correcting text using error dictionary
  public class TextCorrectionService {
    private Dictionary<string, string> _errorDictionary;
    private int _lastFixCount;

    public TextCorrectionService(Dictionary<string, string> errorDictionary) {
      _errorDictionary = errorDictionary;
      _lastFixCount = 0;
    }

    /// Get number of fixes from last operation
    public int GetLastFixCount() {
      return _lastFixCount;
    }

    /// Correct text using error dictionary
    public string CorrectText(string input) {
      if (string.IsNullOrEmpty(input)) {
        _lastFixCount = 0;
        return input;
      }

      _lastFixCount = 0;
      string result = input;

      // Split text into words (accounting for punctuation)
      string[] words = result.Split(new char[] { ' ', '\n', '\r', '\t', '.', ',', '!', '?', ';', ':', '(', ')', '[', ']', '{', '}', '"', '\'' },
        StringSplitOptions.RemoveEmptyEntries);

      // Create copy for replacement
      StringBuilder correctedText = new StringBuilder(result);

      foreach (string word in words) {
        string lowerWord = word.ToLower();

        if (_errorDictionary.ContainsKey(lowerWord)) {
          string correctWord = _errorDictionary[lowerWord];

          // Find word position in text preserving case
          int index = 0;
          while ((index = correctedText.ToString().IndexOf(word, index, StringComparison.Ordinal)) != -1) {
            // Preserve original first character case
            string replacement;
            if (char.IsUpper(word[0])) {
              replacement = char.ToUpper(correctWord[0]) + correctWord.Substring(1);
            } else {
              replacement = correctWord;
            }

            correctedText.Replace(word, replacement, index, word.Length);
            index += replacement.Length;
            ++_lastFixCount;
          }
        }
      }

      return correctedText.ToString();
    }
  }
}
