using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace QnaMakerGenerateFromSearchIndex
{
    class Program
    {
        static void Main(string[] args)
        {
            // List<QnaEntry> qnaEntries = new List<QnaEntry>();
            int currentId = 1;

            StringBuilder fileResult = new StringBuilder();
            fileResult.AppendLine("Question	Answer	Source	Metadata	SuggestedQuestions	IsContextOnly	Prompts	QnaId");

            using (StreamReader reader = new StreamReader(@"..\..\..\data.csv"))
            {
                List<QnaEntry> qnaEntries = new List<QnaEntry>();
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var data = line.Split(";");

                    QnaEntry qnaEntry = new QnaEntry();
                    qnaEntry.Answer = LiteralString(data[1]);
                    qnaEntry.QnaId = 0;
                    qnaEntry.Source = ClearString(data[2]);
                    qnaEntry.Question = ClearString(data[0]);

                    qnaEntries.Add(qnaEntry);
                }

                var questions = qnaEntries.GroupBy(g => g.Question).Select(g => new { g.Key, Data = g.ToList() });
                qnaEntries = new List<QnaEntry>();
                foreach (var question in questions)
                {
                    foreach (var data in question.Data)
                    {
                        QnaEntry qnaEntry = new QnaEntry();
                        qnaEntry.Answer = LiteralString(data.Answer);
                        qnaEntry.QnaId = currentId;
                        qnaEntry.Source = ClearString(data.Source);
                        qnaEntry.Question = ClearString(data.Question);

                        fileResult.AppendLine(qnaEntry.ToString());
                        System.Console.WriteLine(qnaEntry.ToString());

                    }
                    currentId++;
                }
            }

            // using (StreamReader r = new StreamReader(@"..\..\..\data.json"))
            // {
            // 
            //     string json = r.ReadToEnd();
            //     List<QnaIntent> items = JsonConvert.DeserializeObject<List<QnaIntent>>(json);
            // 
            //     int currentId = initialId;
            // 
            //     foreach (QnaIntent intent in items)
            //     {
            //         foreach (string question in intent.questions)
            //         {
            //             QnaEntry qnaEntry = new QnaEntry();
            //             qnaEntry.Answer = LiteralString(intent.answer);
            //             qnaEntry.QnaId = currentId;
            //             qnaEntry.Source = ClearString(intent.source);
            //             qnaEntry.Question = ClearString(question);
            // 
            //             fileResult.AppendLine(qnaEntry.ToString());
            //             System.Console.WriteLine(qnaEntry.ToString());
            //         }
            // 
            //         currentId++;
            //     }
            // }

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
