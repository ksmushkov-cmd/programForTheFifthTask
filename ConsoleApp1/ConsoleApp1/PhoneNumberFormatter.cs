using System;
using System.Text;
using System.Text.RegularExpressions;

namespace TextCorrector.Services {
  /// Mobile phone number formatting service
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

    /// Get number of replacements from last operation
    public int GetLastReplaceCount() {
      return _lastReplaceCount;
    }

    /// Format phone numbers in text
    public string FormatPhoneNumbers(string input) {
      if (string.IsNullOrEmpty(input)) {
        _lastReplaceCount = 0;
        return input;
      }

      _lastReplaceCount = 0;

      // Replace all occurrences
      string result = _phoneRegex.Replace(input, match => {
        _lastReplaceCount++;
        string code = match.Groups[1].Value;      // 012
        string firstPart = match.Groups[2].Value;  // 345
        string secondPart = match.Groups[3].Value; // 67
        string thirdPart = match.Groups[4].Value;  // 89

        // Format: +380 12 345 67 89
         return $"+380 {code.Substring(1)} {firstPart} {secondPart} {thirdPart}";
      });

      return result;
    }

    /// Check if string is a phone number
    public bool IsPhoneNumber(string input) {
      return _phoneRegex.IsMatch(input);
    }

    /// Format a single phone number
    public string FormatSinglePhoneNumber(string phoneNumber) {
      Match match = _phoneRegex.Match(phoneNumber);
      if (match.Success) {
        string code = match.Groups[1].Value;
        string firstPart = match.Groups[2].Value;
        string secondPart = match.Groups[3].Value;
        string thirdPart = match.Groups[4].Value;
        return $"+380 {code.Substring(1)} {firstPart} {secondPart} {thirdPart}";
      }
      return phoneNumber;
     }
  }
}
