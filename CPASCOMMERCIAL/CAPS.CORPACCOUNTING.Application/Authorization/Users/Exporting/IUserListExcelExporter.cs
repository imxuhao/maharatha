using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Authorization.Users.Dto;
using CAPS.CORPACCOUNTING.Dto;

namespace CAPS.CORPACCOUNTING.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}