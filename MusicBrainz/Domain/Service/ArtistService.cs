using System.Linq;
using MusicBrainz.Domain.Infrastructure;
using MusicBrainz.Domain.Entity;
using System.Collections.Generic;

namespace MusicBrainz.Domain.Service
{
    public class ArtistService
    {
        public IQueryable<Artist> Artists
        {
            get
            {
                return context.Artists;
            }
        }

        public void Save(Artist artist)
        {
            if (!artist.ArtistId.HasValue)
            {
                context.Artists.Add(artist);
            }
            context.SaveChanges();
        }

        public void SaveMultipleNew(List<Artist> artists)
        {
            int counter = 0;
            foreach (var a in artists)
            {
                context.Artists.Add(a);
                counter++;
                if (counter % 20 == 0)
                {
                    System.Console.WriteLine("{0} records created", counter);
                    context.SaveChanges();
                }
            }
            context.SaveChanges();
        }

        public void SaveMultipleNewByName(List<string> list)
        {
            int counter = 0;
            foreach (var n in list)
            {
                context.Artists.Add(new Artist { Name = n });
                counter++;
                if (counter % 20 == 0)
                {
                    System.Console.WriteLine("{0} records created", counter);
                    context.SaveChanges();
                }
            }
            context.SaveChanges();
        }

        MbzContext context = new MbzContext();
    }
}
