using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBrainz.QueryResultCache
{
    class Query
    {
        public int? QueryId { get; set; }
        public string QueryString { get; set; }
        public string Result { get; set; }
    }
}
