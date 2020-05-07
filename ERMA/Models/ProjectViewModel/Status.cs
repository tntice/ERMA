using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERMA.Models.ProjectViewModel
{
    public class Status
    {
        private string _value = string.Empty;
        private string _text = string.Empty;

        public string Value { get => _value; set => _value = value; }
        public string Text { get => _text; set => _text = value; }

        public Status()
        {

        }

        public Status(string value, string text)
        {
            _value = value;
            _text = text;
        }
    }
}