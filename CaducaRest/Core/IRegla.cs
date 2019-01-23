using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.Core
{
    public interface IRegla
    {   
        CustomError customError { get; set; }

        bool ValidarRegla();
    }
}
