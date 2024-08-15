using System.Collections.Generic;

namespace Service.Logic.Contract
{
    public interface ISociedadServiceLogic
    {
        IEnumerable<Data.Common.Sociedad> Get();
    }
}
