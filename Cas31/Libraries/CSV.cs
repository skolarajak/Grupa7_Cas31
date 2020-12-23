using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;

namespace Cas31.Libraries
{
    class CSV
    {
        List<string[]> lines = new List<string[]>();

        public int RowCount {
            get {
                return this.lines.Count;
            }
        }

        public CSV(string fileName, bool flagSkipFirstRow = true)
        {
            using (TextFieldParser parser = new TextFieldParser(fileName))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                if (flagSkipFirstRow)
                {
                    parser.ReadFields();
                }

                while(!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    this.lines.Add(fields);
                }
            }
        }

        public string[] GetLine(int line)
        {
            if (line > this.lines.Count)
            {
                return null;
            }
            return this.lines.ElementAt(line);
        }
    }
}
