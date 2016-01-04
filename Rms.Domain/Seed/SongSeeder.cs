using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Rms.Domain.Infrastructure;

namespace Rms.Domain.Seed
{
    using RemaSong = Rema.Domain.Entity.Song;
    using RmsSong = Rms.Domain.Entity.Song;

    class SongSeeder
    {
        public static void Seed(RmsContext context, string songFile)
        {
            List<RemaSong> songs;

            XmlSerializer reader = new XmlSerializer(typeof(List<RemaSong>));
            using (StreamReader file = new StreamReader(songFile))
            {
                songs = (List<RemaSong>)reader.Deserialize(file);
            }

            foreach (RemaSong s in songs)
            {
                context.Songs.Add(new RmsSong(s));
            }
        }
    }
}
