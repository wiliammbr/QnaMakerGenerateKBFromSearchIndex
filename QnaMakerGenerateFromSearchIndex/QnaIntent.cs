using System;
using System.Collections.Generic;
using System.Text;

namespace QnaMakerGenerateFromSearchIndex
{
    public class QnaIntent
    {
        public string kbId { get; set; }
        public string key { get; set; }
        public string id { get; set; }
        public IList<string> questions { get; set; }
        public string answer { get; set; }
        public string source { get; set; }
        public IList<object> keywords { get; set; }
        public string changeStatus { get; set; }
        public object alternateQuestions { get; set; }
        public string isContextOnly { get; set; }
        public IList<object> parentIds { get; set; }
        public string prompts { get; set; }
        public string metadata_editorial { get; set; }
    }
}
