using System;
using System.Linq;
using Rms.Domain.Entity;
using Rms.Domain.Infrastructure;
using Rms.Domain.Service.Abstract;

namespace Rms.Domain.Service.EF
{
    public class SongService : ISongService
    {
        public IQueryable<Song> Songs
        {
            get { return context.Songs; }
        }

        private RmsContext context = new RmsContext();
    }
}
