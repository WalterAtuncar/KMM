using System.Collections.Generic;

namespace Service.Contracts
{
    public interface ISociedadService
    {
        IEnumerable<Data.Common.Sociedad> Get();
    }
}
