using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL
{
    public class DataNoSelectionException : Exception
    {
        public DataNoSelectionException(string message = "No data selection") : base($"{message}")
        {

        }
    }
}
