using IMDB_DataInserter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace IMDB_DataInserter
{
    public class Inserter
    {
        private const int timeout = 0; // Setting the timeout for SQL bulk copy operations to 0 (Wait forever to be finished)

        private const int batchSize = 100000; // Setting the batch size for SQL bulk copy operations to 100,000 records
        // Doing this increases performances (Copies 100.000 datarows before it moves on to the next 100.000)


        public static void InsertTitles(SqlConnection connection, List<Title> titles) // Method to insert titles and associated genres into the database

        {
            DataTable titleTable = new("Titles");       // Creating DataTable for Titles
            DataTable genreTable = new("Genres");       // Creating DataTable for Genres

            // Defining columns for the Title DataTable
            titleTable.Columns.Add("tconst", typeof(string));
            titleTable.Columns.Add("titleType", typeof(string));
            titleTable.Columns.Add("primaryTitle", typeof(string));
            titleTable.Columns.Add("originalTitle", typeof(string));
            titleTable.Columns.Add("isAdult", typeof(bool));
            titleTable.Columns.Add("startYear", typeof(int));
            titleTable.Columns.Add("endYear", typeof(int));
            titleTable.Columns.Add("runtimeMinutes", typeof(int));

            // Defining columns for the Genre DataTable
            genreTable.Columns.Add("id", typeof(int)).DefaultValue = DBNull.Value;
            genreTable.Columns.Add("genre", typeof(string));
            genreTable.Columns.Add("tconst", typeof(string));

            // Cycles through each Title object and populating the DataTables
            foreach (Title title in titles)
            {
                DataRow titleRow = titleTable.NewRow();
                // Populating Title DataTable
                titleRow["tconst"] = title.tconst;
                titleRow["titleType"] = title.titleType;
                titleRow["primaryTitle"] = title.primaryTitle;
                titleRow["originalTitle"] = title.originalTitle;
                titleRow["isAdult"] = title.isAdult;
                titleRow["startYear"] = title.startYear == null ? DBNull.Value : title.startYear;
                titleRow["endYear"] = title.endYear == null ? DBNull.Value : title.endYear;
                titleRow["runtimeMinutes"] = title.runtimeMinutes == null ? DBNull.Value : title.runtimeMinutes;
                titleTable.Rows.Add(titleRow);

                // Populating Genre DataTable
                if (title.genres != null)
                {
                    foreach (var genre in title.genres)
                    {
                        DataRow row = genreTable.NewRow();
                        //row["id"] = DBNull.Value;
                        row["genre"] = genre;
                        row["tconst"] = title.tconst;

                        genreTable.Rows.Add(row);
                    }
                }
            }
            // Bulk copying data into the database for Titles and Genres
            SqlBulkCopy bulkTitles = new(connection, SqlBulkCopyOptions.KeepNulls, null)
            {
                DestinationTableName = "Titles",
                BatchSize = batchSize,
                BulkCopyTimeout = timeout
            };
            bulkTitles.WriteToServer(titleTable);

            SqlBulkCopy bulkGenres = new(connection, SqlBulkCopyOptions.KeepNulls, null)
            {
                DestinationTableName = "Genres",
                BatchSize = batchSize,
                BulkCopyTimeout = timeout
            };
            bulkGenres.WriteToServer(genreTable);

            return;
        }

        // Method to insert names, associated known-for titles, and professions into the database
        public static void InsertNames(SqlConnection connection, List<Name> names)
        {
            DataTable knownForTable = new("KnownForTitles");    // Creating DataTable for KnownForTitles
            DataTable nameTable = new("Names");                 // Creating DataTable for Names
            DataTable professionTable = new("Professions");     // Creating DataTable for Professions

            // Defining columns for the Name DataTable
            nameTable.Columns.Add("nconst", typeof(string));
            nameTable.Columns.Add("primaryName", typeof(string));
            nameTable.Columns.Add("birthYear", typeof(int));
            nameTable.Columns.Add("deathYear", typeof(int));

            // Defining columns for the KnownForTitles DataTable
            knownForTable.Columns.Add("id", typeof(int));
            knownForTable.Columns.Add("nconst", typeof(string));
            knownForTable.Columns.Add("tconst", typeof(string));

            // Defining columns for the Professions DataTable
            professionTable.Columns.Add("id", typeof(int));
            professionTable.Columns.Add("profession", typeof(string));
            professionTable.Columns.Add("nconst", typeof(string));

            // Cycles through each Name object and populating the DataTables
            foreach (Name name in names)
            {
                DataRow nameRow = nameTable.NewRow();
                // Populating Name DataTable
                nameRow["nconst"] = name.nconst;
                nameRow["primaryName"] = name.primaryName;
                nameRow["birthYear"] = name.birthYear == null ? DBNull.Value : name.birthYear;
                nameRow["deathYear"] = name.deathYear == null ? DBNull.Value : name.deathYear;

                nameTable.Rows.Add(nameRow);
                // Populating KnownForTitles DataTable
                if (name.knownForTitles != null)
                {
                    foreach (var tconst in name.knownForTitles)
                    {
                        DataRow row = knownForTable.NewRow();
                        row["id"] = DBNull.Value;
                        row["nconst"] = name.nconst;
                        row["tconst"] = tconst;

                        knownForTable.Rows.Add(row);
                    }
                }

                // Populating Professions DataTable
                if (name.primaryProfessions != null)
                {
                    foreach (var profession in name.primaryProfessions)
                    {
                        DataRow row = professionTable.NewRow();
                        row["id"] = DBNull.Value;
                        row["profession"] = profession;
                        row["nconst"] = name.nconst;


                        professionTable.Rows.Add(row);
                    }
                }
            }

            // Bulk copying data into the database for Names, KnownForTitles, and Professions
            SqlBulkCopy bulkNames = new(connection, SqlBulkCopyOptions.KeepNulls, null)
            {
                DestinationTableName = "Names",
                BatchSize = batchSize,
                BulkCopyTimeout = timeout
            };
            bulkNames.WriteToServer(nameTable);

            SqlBulkCopy bulkKnownFor = new(connection, SqlBulkCopyOptions.KeepNulls, null)
            {
                DestinationTableName = "KnownForTitles",
                BatchSize = batchSize,
                BulkCopyTimeout = timeout
            };
            bulkKnownFor.WriteToServer(knownForTable);

            SqlBulkCopy bulkProfessions = new(connection, SqlBulkCopyOptions.KeepNulls, null)
            {
                DestinationTableName = "Professions",
                BatchSize = batchSize,
                BulkCopyTimeout = timeout
            };
            bulkProfessions.WriteToServer(professionTable);

            return;
        }

        // Method to insert crew members, such as writers and directors, into the database
        public static void InsertCrew(SqlConnection connection, List<Crew> crews)
        {
            DataTable writerTable = new("Writers");         // Creating DataTable for Writers
            DataTable directorTable = new("Directors");     // Creating DataTable for Directors

            // Defining columns for the Writer DataTable
            writerTable.Columns.Add("id", typeof(int));
            writerTable.Columns.Add("tconst", typeof(string));
            writerTable.Columns.Add("nconst", typeof(string));

            // Defining columns for the Director DataTable
            directorTable.Columns.Add("id", typeof(int));
            directorTable.Columns.Add("tconst", typeof(string));
            directorTable.Columns.Add("nconst", typeof(string));

            // Cycles through each Crew object and populating the DataTables
            foreach (Crew c in crews)
            {
                // Populating Writer DataTable
                if (c.wconst != null)
                {
                    foreach (var wconst in c.wconst)
                    {
                        DataRow row = writerTable.NewRow();
                        row["id"] = DBNull.Value;
                        row["tconst"] = c.tconst;
                        row["nconst"] = wconst;

                        writerTable.Rows.Add(row);
                    }
                }

                // Populating Director DataTable
                if (c.dconst != null)
                {
                    foreach (var dconst in c.dconst)
                    {
                        DataRow row = directorTable.NewRow();
                        row["id"] = DBNull.Value;
                        row["tconst"] = c.tconst;
                        row["nconst"] = dconst;

                        directorTable.Rows.Add(row);
                    }
                }
            }

            // Bulk copying data into the database for Writers
            SqlBulkCopy bulkWriters = new(connection, SqlBulkCopyOptions.KeepNulls, null)
            {
                DestinationTableName = "Writers",
                BatchSize = batchSize,
                BulkCopyTimeout = timeout
            };
            bulkWriters.WriteToServer(writerTable);

            // Bulk copying data into the database for Directors
            SqlBulkCopy bulkDirectors = new(connection, SqlBulkCopyOptions.KeepNulls, null)
            {
                DestinationTableName = "Directors",
                BatchSize = batchSize,
                BulkCopyTimeout = timeout
            };
            bulkDirectors.WriteToServer(directorTable);

            return;
        }
    }
}
