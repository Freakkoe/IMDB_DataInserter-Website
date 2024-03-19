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
    }
}
