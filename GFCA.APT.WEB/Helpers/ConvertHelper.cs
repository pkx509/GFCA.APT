using GFCA.APT.Domain.Models;
using System.IO;
using System.Web;

namespace GFCA.APT.WEB.Helpers
{
    public static class ConvertHelper
    {

        public static FileModel ToFileModel(this HttpPostedFileBase item)
        {
            FileModel result = new FileModel();
            if (item != null)
            {
                result.FileName = item.FileName;

                MemoryStream target = new MemoryStream();
                item.InputStream.CopyTo(target);

                result.Data = target.ToArray();
            }

            return result;
        }

    }
}