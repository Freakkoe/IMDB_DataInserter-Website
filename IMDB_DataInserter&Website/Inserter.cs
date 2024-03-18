using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_DataInserter_Website
{
    public class Inserter
    {
        private const int timeout = 0; // Setting the timeout for SQL bulk copy operations to 0 (Wait forever to be finished)

        private const int batchSize = 100000; // Setting the batch size for SQL bulk copy operations to 100,000 records
        // Doing this increases performances (Copies 100.000 datarows before it moves on to the next 100.000)

    }
}
