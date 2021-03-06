﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZOR
{
    public class TagMode
    {
        private Boolean isAutomatic;
        private Boolean isManual;
        private static readonly string automaticString = "Automatico";
        private static readonly string manualString = "Manual";
        /// <summary>
        /// For Json default constructor.
        /// </summary>
        public TagMode()
        {

        }
        public TagMode(string text)
        {
            SetModeWithString(text);
        }
        public TagMode(bool isAutomatic, bool isManual)
        {
            this.IsAutomatic = isAutomatic;
            this.IsManual = isManual;
        }

        public bool IsAutomatic { get => isAutomatic; set => isAutomatic = value; }
        public bool IsManual { get => isManual; set => isManual = value; }
        public static string AutomaticText()
        {
            return automaticString;
        }
        public static string ManualText()
        {
            return manualString;
        }
        /// <summary>
        /// Set the mode with string.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>True if text is correct, false otherwise.</returns>
        public Boolean SetModeWithString(String text)
        {
            if (text.Equals(automaticString))
            {
                isAutomatic = true;
                return true;
            }else if (text.Equals(manualString))
            {
                isManual = true;
                return true;
            }
            return false;
        }
    }
}
