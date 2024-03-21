using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_DataInserter.Models
{
    public class Title
    {
        public string tconst { get; set; }
        public string titleType { get; set; }
        public string primaryTitle { get; set; }
        public string originalTitle { get; set; }
        public bool isAdult { get; set; }
        public int? startYear { get; set; }
        public int? endYear { get; set; }
        public int? runtimeMinutes { get; set; }
        public string[]? genres { get; set; }

        public override string ToString()
        {
            return $"tconst: {tconst},isAdult: {isAdult}, start: {startYear}, end: {endYear}, runtime: {runtimeMinutes}, gen: {genres?.Length}";
        }

        //DONE
    }
}
