using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public class IsbnAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null) return true;

        string isbn = value.ToString().Replace("-", "").Replace(" ", "");

        // ISBN-10
        if (isbn.Length == 10 && IsValidIsbn10(isbn))
            return true;

        // ISBN-13
        if (isbn.Length == 13 && IsValidIsbn13(isbn))
            return true;

        return false;
    }

    private bool IsValidIsbn10(string isbn)
    {
        if (!System.Text.RegularExpressions.Regex.IsMatch(isbn, @"^\d{9}[\dX]$"))
            return false;

        int sum = 0;
        for (int i = 0; i < 9; i++)
        {
            sum += (isbn[i] - '0') * (10 - i);
        }

        char lastChar = isbn[9];
        sum += (lastChar == 'X') ? 10 : (lastChar - '0');

        return sum % 11 == 0;
    }

    private bool IsValidIsbn13(string isbn)
    {
        if (!System.Text.RegularExpressions.Regex.IsMatch(isbn, @"^\d{13}$"))
            return false;

        int sum = 0;
        for (int i = 0; i < 12; i++)
        {
            int digit = isbn[i] - '0';
            sum += (i % 2 == 0) ? digit : digit * 3;
        }

        int checkDigit = (10 - (sum % 10)) % 10;

        return checkDigit == (isbn[12] - '0');
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{name} must be a valid ISBN-10 or ISBN-13.";
    }
}
