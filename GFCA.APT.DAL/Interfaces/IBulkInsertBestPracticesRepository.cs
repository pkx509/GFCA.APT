using GFCA.APT.DAL.Implements;
using GFCA.APT.Domain.Dto;
using System.Collections.Generic;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IBulkInsertBestPracticesRepository
    {
        IEnumerable<BulkMessageDto> ValidationTableStaging();
        bool InsertTableStaging(IEnumerable<TableStagingDto> bulkData);
        bool InsertTableReal();
    }
}
