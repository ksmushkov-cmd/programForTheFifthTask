using System;
using System.Text.RegularExpressions;

namespace TextCorrector.Services {

  public class PhoneNumberFormatter {
    private int _lastReplaceCount;

    // Regular expression to find phone numbers in format (012) 345-67-89
    private readonly Regex _phoneRegex = new Regex(
      @"\((\d{3})\)\s*(\d{3})-(\d{2})-(\d{2})",
    RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    public PhoneNumberFormatter() {
      _lastReplaceCount = 0;
    }

    public int GetLastReplaceCount() {
      return _lastReplaceCount;
    }

    public string FormatPhoneNumbers(string input) {
      if (string.IsNullOrEmpty(input)) {
        _lastReplaceCount = 0;
        return input;
      }

      _lastReplaceCount = 0;

      // Replace all occurrences
      string result = _phoneRegex.Replace(input, match => {
        ++_lastReplaceCount;
        string operatorCode;      // Operator code (first three digits in brackets)
        string firstNumberPart;   // The first part of the number after the code
        string secondNumberPart;  // The second part of the number is for example
        string thirdNumberPart;   // Part three of the issue

        operatorCode = match.Groups[1].Value;      // (012) → "012"
        firstNumberPart = match.Groups[2].Value;   // 345-67-89 → "345"
        secondNumberPart = match.Groups[3].Value;  // 345-67-89 → "67"
        thirdNumberPart = match.Groups[4].Value;   // 345-67-89 → "89"
        
        return $"+380 {operatorCode.Substring(1)} {firstNumberPart} {secondNumberPart} {thirdNumberPart}";
      });

      return result;
    }
  }
}
