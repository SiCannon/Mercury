using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Hub.Domain.Entity;

namespace Hub.Domain.Infrastructure.Seed
{
    class ArtistSeeder
    {
        public static void Seed(HubContext context, string artistFilename)
        {
            List<Artist> artists;

            XmlSerializer reader = new XmlSerializer(typeof(List<Artist>));
            using (StreamReader file = new StreamReader(artistFilename))
            {
                artists = (List<Artist>)reader.Deserialize(file);
            }

            context.Database.ExecuteSqlCommand(@"set identity_insert Artist on");

            int counter = 0;
            //artists.Reverse();
            foreach (var a in artists)
            {
                context.Artists.Add(a);
                counter++;
                if (counter % 100 == 0)
                {
                    Console.WriteLine("processed {0} artists", counter);
                    context.SaveChanges();
                }
            }

            context.Database.ExecuteSqlCommand(@"set identity_insert Artist off");
        }
    }
}
