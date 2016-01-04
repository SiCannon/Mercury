using System.Linq;
using Rms.Domain.Entity;

namespace Rms.Domain.Service.Abstract
{
    public interface ISongService
    {
        IQueryable<Song> Songs { get; }
    }
}
