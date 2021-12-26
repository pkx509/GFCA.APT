using System;
using System.Web.Mvc;

namespace GFCA.APT.WEB
{
    public class RowTypeModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value != null)
            {
                var rawValues = value.RawValue as string[];
                if (rawValues != null)
                {
                    Domain.Enums.ROW_TYPE result;
                    if (Enum.TryParse<Domain.Enums.ROW_TYPE>(string.Join(",", rawValues), out result))
                    {
                        return result;
                    }
                }
            }
            return base.BindModel(controllerContext, bindingContext);
        }
    }
}