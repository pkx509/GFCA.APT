using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models; 
using System.Collections.Generic; 

namespace GFCA.APT.BAL.Interfaces
{  
    public interface IGLAccountService
    {
        IEnumerable<GLAccountDto> GetAll();
        GLAccountDto GetByCode(string code);
        BusinessResponse Create(GLAccountDto model);
        BusinessResponse Edit(GLAccountDto model);
        BusinessResponse Remove(GLAccountDto model);
    }
}
