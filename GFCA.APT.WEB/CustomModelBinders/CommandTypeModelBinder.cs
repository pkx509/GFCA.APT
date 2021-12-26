using GFCA.APT.Domain.Enums;
using System;
using System.Web.Mvc;

namespace GFCA.APT.WEB
{
    public class CommandTypeModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value != null)
            {
                var rawValues = value.RawValue as string[];
                if (rawValues != null)
                {
                    COMMAND_TYPE result;
                    if (Enum.TryParse<COMMAND_TYPE>(string.Join(",", rawValues), out result))
                    {
                        return result;
                    }
                }
            }
            return base.BindModel(controllerContext, bindingContext);
        }
    }
}