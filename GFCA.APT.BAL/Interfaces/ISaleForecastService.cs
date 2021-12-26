using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Interfaces
{
    public interface ISaleForecastService : IDocumentService
    {
        IEnumerable<SaleForecastHeaderDto> GetHeaderAll();
        SaleForecastHeaderDto GetHeaderById(int DOC_SFCH_ID);
        SaleForecastHeaderDto GetHeaderByCode(string code, int ver = -1, int rev = -1);
        BusinessResponse CreateHeader(SaleForecastHeaderDto model);
        BusinessResponse EditHeader(SaleForecastHeaderDto model);
        BusinessResponse RemoveHeader(SaleForecastHeaderDto model);

        //IEnumerable<SaleForecastDetailDto> GetDetailItems(string code, int ver = -1, int rev = -1);
        IEnumerable<SaleForecastDetailDto> GetDetailItems(int DOC_SFCH_ID);
        SaleForecastDetailDto GetDetailItem(int DOC_SFCD_ID);
        BusinessResponse CreateDetail(SaleForecastDetailDto model);
        BusinessResponse CreateDetailList(List<SaleForecastDetailDto> model);
        BusinessResponse EditDetail(SaleForecastDetailDto model);
        BusinessResponse RemoveDetail(SaleForecastDetailDto model);
        IEnumerable<SaleForecastDetailDto> GetDetailItemToExport(int DOC_SFCD_ID);
    }
}
