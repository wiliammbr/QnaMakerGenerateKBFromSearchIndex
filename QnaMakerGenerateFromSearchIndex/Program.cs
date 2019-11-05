using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QnaMakerGenerateFromSearchIndex
{
    class Program
    {
        static void Main(string[] args)
        {
            // List<QnaEntry> qnaEntries = new List<QnaEntry>();
            int initialId = 1;

            StringBuilder fileResult = new StringBuilder();
            fileResult.AppendLine("Question	Answer	Source	Metadata	SuggestedQuestions	IsContextOnly	Prompts	QnaId");

            using (StreamReader r = new StreamReader(@"..\..\..\data.json"))
            {

                string json = r.ReadToEnd();
                List<QnaIntent> items = JsonConvert.DeserializeObject<List<QnaIntent>>(json);

                int currentId = initialId;

                foreach (QnaIntent intent in items)
                {
                    foreach (string question in intent.questions)
                    {
                        QnaEntry qnaEntry = new QnaEntry();
                        qnaEntry.Answer = LiteralString(intent.answer);
                        qnaEntry.QnaId = currentId;
                        qnaEntry.Source = ClearString(intent.source);
                        qnaEntry.Question = ClearString(question);

                        fileResult.AppendLine(qnaEntry.ToString());
                        System.Console.WriteLine(qnaEntry.ToString());
                    }

                    currentId++;
                }
            }

            var logPath = "..\\..\\..\\data.tsv";
            using (var writer = File.CreateText(logPath))
            {
                writer.WriteLine(fileResult.ToString()); //or .Write(), if you wish
            }

            System.Console.ReadKey();
        }

        static string LiteralString(string input)
        {
            input = input.Replace("\a", @"\a");
            input = input.Replace("\b", @"\b");
            input = input.Replace("\f", @"\f");
            input = input.Replace("\n", @"\n");
            input = input.Replace("\r", @"\r");
            input = input.Replace("\t", @"\t");
            input = input.Replace("\v", @"\v");
            input = input.Replace("\\", @"\\");
            input = input.Replace("\0", @"\0");

            //The SO parser gets fooled by the verbatim version 
            //of the string to replace - @"\"""
            //so use the 'regular' version
            input = input.Replace("\"", "\\\"");

            return input;
        }
        static string ClearString(string input)
        {
            input = input.Replace("\a", "");
            input = input.Replace("\b", "");
            input = input.Replace("\f", "");
            input = input.Replace("\n", "");
            input = input.Replace("\r", "");
            input = input.Replace("\t", "");
            input = input.Replace("\v", "");
            input = input.Replace("\\", "");
            input = input.Replace("\0", "");

            //The SO parser gets fooled by the verbatim version 
            //of the string to replace - @"\"""
            //so use the 'regular' version
            input = input.Replace("\"", "");

            return input;
        }
    }
}
