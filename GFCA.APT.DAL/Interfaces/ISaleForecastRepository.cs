using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
using System.Collections.Generic;

namespace GFCA.APT.DAL.Interfaces
{
    public interface ISaleForecastRepository
    {

        IEnumerable<SaleForecastHeaderDto> GetSaleForecastAll();
        SaleForecastHeaderDto GetSaleForecastByItemID(int DOC_SFCD_ID);
        void InsertSaleForecastHeader(SaleForecastHeaderDto entity);
        void UpdateSaleForecastHeader(SaleForecastHeaderDto entity);
        void DeleteSaleForecastHeader(int DOC_SFCH_ID);

        SaleForecastDetailDto GetDetailItem(int DOC_SFCD_ID);
        IEnumerable<SaleForecastDetailDto> GetDetailItems(int DOC_SFCH_ID);
        void InsertSaleForecastDetail(SaleForecastDetailDto entity);
        void UpdateSaleForecastDetail(SaleForecastDetailDto entity);
        void DeleteSaleForecastDetail(int DOC_SFCD_ID);
        IEnumerable<SaleForecastDetailDto> GetDetailItemToExport(int DOC_SFCH_ID);
    }
}
