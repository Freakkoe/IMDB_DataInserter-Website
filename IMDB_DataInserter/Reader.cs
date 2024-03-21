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
        // In this case ive put them inside my Source Repos folder where all my solutions are - in a folder called "Data"
        // This way it is easier for me just to use the path string to get all files
        private static readonly string path = @"C:\Users\madsg\source\repos\Data\";

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
        // Method to read names from the file and parse them into a list of Name objects
        public static List<Name> GetNames()
        {
            var list = new List<Name>();

            // Reading lines from the name basics file, skipping the header, and taking only 20 lines for testing
            foreach (string line in File.ReadLines(path + "name.basics.tsv")
                                        .Skip(1).Take(20))
            {
                string[] values = line.Split('\t');

                // Checking if the line has the expected number of fields
                if (values.Length == 6)
                {
                    list.Add(new Name()
                    {
                        // Extracting data from each field and using it to create a new Name object
                        nconst = values[0],
                        primaryName = values[1],
                        birthYear = CheckInt(values[2]),
                        deathYear = CheckInt(values[3]),
                        primaryProfessions = values[4] == @"\N" ? null : values[4].Split(","),
                        knownForTitles = values[5] == @"\N" ? null : values[5].Split(",")
                    });
                }
            }

            return list;
        }

        // Method to read crew information from the file and parse them into a list of Crew objects
        public static List<Crew> GetCrews()
        {
            var list = new List<Crew>();

            // Reading lines from the title crew file, skipping the header, and taking only 20 lines for testing
            foreach (string line in File.ReadLines(path + "title.crew.tsv")
                                        .Skip(1).Take(20))
            {
                string[] values = line.Split('\t');

                // Checking if the line has the expected number of fields
                if (values.Length == 3)
                {
                    list.Add(new Crew()
                    {
                        // Extracting data from each field and using it to create a new Crew object
                        tconst = values[0],
                        dconst = values[1] == @"\N" ? null : values[1].Split(","),
                        wconst = values[2] == @"\N" ? null : values[2].Split(",")
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
