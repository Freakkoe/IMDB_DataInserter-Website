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
        // Path to the data directory (where the IMDB files is located (tsv FILES))
        // In this case ive put them inside the solution folder - in a folder called "Data"
        private static readonly string path = @"..\..\..\..\Data\";

        // Method to read titles from the file and parse them into a list of Title objects
        public static List<Title> GetTitles()
        {
            var list = new List<Title>();

            // Reading lines from the title basics file, skipping the header, and taking only 20 lines for testing
            foreach (string line in File.ReadLines(path + "title.basics.tsv")
                                        .Skip(1).Take(20))
            {
                // Splitting the line into an array of strings using the tab character as the separator
                string[] values = line.Split('\t');

                // Checking if the line has the expected number of fields
                if (values.Length == 9)
                {
                    list.Add(new Title()
                    {
                        // Extracting data from each field and using it to create a new Title object
                        tconst = values[0],
                        titleType = values[1],
                        primaryTitle = values[2],
                        originalTitle = values[3],
                        isAdult = values[4] != "0",
                        startYear = CheckInt(values[5]),
                        endYear = CheckInt(values[6]),      // Converting startYear, endYear, and runtimeMinutes to nullable integers using the CheckInt helper method,
                        runtimeMinutes = CheckInt(values[7]),
                        genres = values[8] == @"\N" ? null : values[8].Split(",")       // and extracting genres into an array of strings by splitting the value using comma as the separator
                    });
                }
            }

            return list;
        }

        // Helper method to convert strings to nullable integers
        private static int? CheckInt(string value)
        {
            bool canParse = int.TryParse(value, out int parsed);

            if (canParse) return parsed;

            return null;
        }
    }
}
