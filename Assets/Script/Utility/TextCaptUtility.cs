using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;

namespace Utiilty
{
    public class TextCaptUtility
    {
        public static string Capture_Bracket_Char(string input)
        {
            string pattern = @"\[(.*?)\]";

            Regex regex = new Regex(pattern);
            Match matches = regex.Match(input);

            if (matches.Success) return matches.Value;

            return null;
        }
    }
}
