using System.Collections.Generic;
using Rema.Domain.Entity;
using Rema.Domain.Service.Abstract;

namespace Rema.Domain.Service.Concrete
{
    public class LabelService : ILabelService
    {
        public List<Label> ListAll()
        {
            return Helpers.Database.GetList("select LABEL_CODE, LABEL_DESC from LABEL", x => new Label(x));
        }
    }
}
