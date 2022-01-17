using GFCA.APT.Domain.Dto;
using System.Collections.Generic;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IEmployeeRepository : IRepositories<EmployeeDto>
    {
        EmployeeDto GetEmployee(string email, string password);
        IEnumerable<EmployeeRoleDto> GetRoles(int empId);
    }
}
