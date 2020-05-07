using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ERMA.Code
{
    public class Parsing
    {
        private List<string> _Notes = new List<string>();

        public List<string> Notes { get => _Notes; set => _Notes = value; }

        public void Parse(string NoteToParse, Int32 MaxCharPerLine)
        {
            string val = NoteToParse.Trim() + " ";
            InternalParse(val, MaxCharPerLine);
        }

        private void InternalParse(string NoteToParse, Int32 MaxCharPerLine)
        {

            List<string> LineValues = new List<string>();

            try
            {
                string[] breaks = NoteToParse.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                if (breaks.Length > 1)
                {
                    foreach (string brk in breaks)
                    {
                        if (!(brk.Length.Equals(0)))
                        {
                            InternalParse(brk.Trim() + " ", MaxCharPerLine);
                        }
                        else
                        {
                            Notes.Add(Environment.NewLine);
                        }
                    }
                }
                else
                {
                    string[] words = NoteToParse.Split(' ');

                    string Remainder = string.Empty;
                    string line = string.Empty;
                    Int32 CurrentChars = 0;
                    Int32 WordCount = 1;

                    foreach (var word in words)
                    {
                        CurrentChars += word.Length + 1;
                        if ((CurrentChars <= MaxCharPerLine) || (WordCount == 1))
                        {
                            if (!(word.Equals(" ")))
                            {
                                LineValues.Add(word.Trim() + " ");
                            }
                        }
                        WordCount++;
                    }

                    foreach (string item in LineValues)
                    {
                        if (!(item.Equals(" ")))
                        {
                            line += item;
                        }
                    }

                    if (line.Length > 0)
                    {
                        try
                        {
                            Notes.Add(line);
                            var regex = new Regex(Regex.Escape(line));
                            Remainder = regex.Replace(NoteToParse, "", 1);
                        }
                        catch(Exception ex) {
                            var msg = ex.Message;
                        }

                    }
                    else
                    {
                        if (NoteToParse.Length > 0)
                        {
                            Remainder = NoteToParse;
                        }
                        else
                        {
                            Remainder = string.Empty;
                        }
                    }

                    if (Remainder.Length > 0)
                    {
                        InternalParse(Remainder, MaxCharPerLine);
                    }
                }

            }
            catch (StackOverflowException exrange)
            {
                Notes.Add("No line breaks: " + exrange.Message);
            }
            catch (Exception ex)
            {
                Notes.Add("Error: " + ex.Message);
            }

        }

    }
}