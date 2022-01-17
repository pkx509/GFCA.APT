using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.Domain.Dto
{
    public class EmployeeRoleDto
    {
        public int EMP_ROLE_ID { get; set; }
        public int ROLE_ID { get; set; }
        public string ROLE_NAME { get; set; }
        public int EMP_ID { get; set; }
        public string EMAIL { get; set; }
        public string PERMISSION { get; set; }
    }
}
