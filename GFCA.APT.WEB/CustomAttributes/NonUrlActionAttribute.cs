using System;

namespace GFCA.APT.WEB.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class NonUrlActionAttribute : Attribute
    {
        private bool nonUrl;
        public bool NonUrl
        {
            get { return this.nonUrl; }
            set { this.NonUrl = value; }
        }
        public NonUrlActionAttribute()
        {

        }
    }
}