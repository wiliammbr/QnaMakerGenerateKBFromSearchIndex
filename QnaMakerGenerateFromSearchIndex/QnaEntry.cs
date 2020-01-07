namespace QnaMakerGenerateFromSearchIndex
{
    public class QnaEntry
    {
        public string Question { get; set; }
        public string Answer { get; set; }

        public string Source { get; set; }

        public string Metadata { get; set; }

        public const string Suggested = "[]";

        public bool IsContext { get; set; }

        public const string Prompts = "[]";

        public int QnaId { get; set; }

        public override string ToString()
        {
            return string.Concat(Question, "\t", Answer, "\t",
                Source, "\t", Metadata, "\t", Suggested, "\t", IsContext,
                "\t", Prompts, "\t", QnaId).Replace("\\\\\\\\n", "\\n").Replace(@"\\\"", "");
        }
    }
}
