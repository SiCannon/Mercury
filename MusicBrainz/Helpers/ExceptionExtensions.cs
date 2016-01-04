using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MusicBrainz.Helpers
{
    public static class ExceptionExtensions
    {
        public static bool NotFound(this WebException exception)
        {
            return exception.Response is HttpWebResponse && ((HttpWebResponse)exception.Response).StatusCode == HttpStatusCode.NotFound;
        }
    }
}
