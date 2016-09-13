using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Auditing.Dto;
using CAPS.CORPACCOUNTING.Dto;

namespace CAPS.CORPACCOUNTING.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);
    }
}
