using IMDB_DataInserter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_DataInserter
{
    public static class Reader
    {
        private static readonly string path = @"..\..\..\..\Data\"; // New Test
        public static List<Title> GetTitles()
        {
            var list = new List<Title>();

            foreach (string line in File.ReadLines(path + "title.basics.tsv")
                                        .Skip(1).Take(20))
            {
                string[] values = line.Split('\t');

                if (values.Length == 9)
                {
                    list.Add(new Title()
                    {
                        tconst = values[0],
                        titleType = values[1],
                        primaryTitle = values[2],
                        originalTitle = values[3],
                        isAdult = values[4] != "0",
                        startYear = CheckInt(values[5]),
                        endYear = CheckInt(values[6]),      
                        runtimeMinutes = CheckInt(values[7]),
                        genres = values[8] == @"\N" ? null : values[8].Split(",")
                    });
                }
            }

            return list;
        }

        // TRYING AGAIN
        private static int? CheckInt(string value)
        {
            bool canParse = int.TryParse(value, out int parsed);

            if (canParse) return parsed;

            return null;
        }
    }
}
