using IMDB_DataInserter;
using System.Data.SqlClient;

Console.WriteLine("Connecting to database...");

using SqlConnection sqlConn = new("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IMDBv1;Integrated Security=True;Connect Timeout=30;Encrypt=False");

sqlConn.Open();
Console.WriteLine("Connection successful");

Console.WriteLine("Reading titles...");
var titles = Reader.GetTitles();
Console.WriteLine("Data is in memory!");
Console.WriteLine("Bulk inserting titles...");
Inserter.InsertTitles(sqlConn, titles);
Console.WriteLine("Insert completed");
GC.Collect();

Console.WriteLine("Reading names...");
var names = Reader.GetNames();
Console.WriteLine("Data is in memory!");
Console.WriteLine("Bulk inserting names...");
Inserter.InsertNames(sqlConn, names);
Console.WriteLine("Insert completed");
GC.Collect();

Console.WriteLine("Reading crews...");
var crews = Reader.GetCrews();
Console.WriteLine("Data is in memory!");
Console.WriteLine("Bulk inserting crews...");
Inserter.InsertCrew(sqlConn, crews);
Console.WriteLine("Insert completed");
Console.WriteLine("Done!");