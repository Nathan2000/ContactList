using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Shared.Auth
{
    public class UserInfoDto
    {
        public bool IsAuthenticated { get; set; }
        public string? Name { get; set; }
        public List<ClaimInfoDto> Claims { get; set; } = [];
    }

    public class ClaimInfoDto
    {
        public string Type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
