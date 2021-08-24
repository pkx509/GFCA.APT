using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models; 
using System.Collections.Generic; 

namespace GFCA.APT.BAL.Interfaces
{  
    public interface IGLAccountService
    {
        IEnumerable<GLAccountDto> GetAll();
        GLAccountDto GetById(int Id);
        BusinessResponse Create(GLAccountDto model);
        BusinessResponse Edit(GLAccountDto model);
        BusinessResponse Remove(GLAccountDto model);
    }
}
