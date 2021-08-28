using System;

namespace GFCA.APT.BAL
{
    internal class DataDuplicateException : Exception
    {
        public DataDuplicateException(string message) : base($"Data is duplicated {message}")
        {

        }
    }
}
