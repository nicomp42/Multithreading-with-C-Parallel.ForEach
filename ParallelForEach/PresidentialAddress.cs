using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParallelForEach {
    class PresidentialAddress {
        public int ID;
        public String president;
        public String textOfSpeech;

        public PresidentialAddress(String csvLine) {
//          String[] data = csvLine.Split(','); // Does not work because there are commas inside the quoted strings
            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            String[] data = CSVParser.Split(csvLine);
            String tmp;
            tmp = StripQuotes(data[0]);
            this.ID = Convert.ToInt32(tmp);
            this.president = StripQuotes(data[1]);
            this.textOfSpeech = StripQuotes(data[4]);
        }
        public int CountWords() {
            int words = textOfSpeech.Split(' ').Length;
            return words;
        }
        private String StripQuotes(String s) {
            String tmp = s.Trim(' ').Trim('\"').Trim(' '); ;
            return tmp;
        }
    }
}
