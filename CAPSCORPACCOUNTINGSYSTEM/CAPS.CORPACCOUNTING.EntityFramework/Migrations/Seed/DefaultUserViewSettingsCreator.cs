using System.Linq;
using CAPS.CORPACCOUNTING.EntityFramework;
using CAPS.CORPACCOUNTING.Masters;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Migrations.Seed
{
    public class DefaultUserViewSettingsCreator
    {
        public static List<UserViewSettingsUnit> InitialUserViewSettingsList { get; private set; }

        private readonly CORPACCOUNTINGDbContext _context;

        static DefaultUserViewSettingsCreator()
        {
            InitialUserViewSettingsList = new List<UserViewSettingsUnit>
            {
                new UserViewSettingsUnit(16, null, "System Default 1",
                    "{'column':[{'hidden':false,'width':'10%','dataIndex':'jobNumber'},{'hidden':false,'width':'15%','dataIndex':'caption'},{'hidden':false,'width':'13%','dataIndex':'detailTransactions'},{'hidden':false,'width':'15%','dataIndex':'productName'},{'hidden':false,'width':'10%','dataIndex':'directorName'},{'hidden':false,'width':'10%','dataIndex':'agency'},{'hidden':false,'width':'15%','dataIndex':'poLogCount'},{'hidden':false,'width':'15%','dataIndex':'typeofProjectName'},{'hidden':false,'width':'20%','dataIndex':'shootingDate'},{'hidden':false,'width':'14%','dataIndex':'shootLocations'},{'hidden':false,'width':'15%','dataIndex':'isWrapUpInsurance'},{'hidden':false,'width':'10%','dataIndex':'jobStatusName'},{'hidden':true,'width':'10%','dataIndex':'totalCost'},{'hidden':true,'width':'12%','dataIndex':'bidContractTotal'},{'hidden':true,'width':'13%','dataIndex':'producersActual'},{'hidden':true,'width':'13%','dataIndex':'billedAmount'},{'hidden':true,'width':'13%','dataIndex':'recievedAmount'},{'hidden':true,'width':'11%','dataIndex':'variance'},{'hidden':true,'width':'13%','dataIndex':'agencyProducer'}],'groupInfo':{'isGrouped':false,'groupField':'','groupDir':'ASC'}}"),
                new UserViewSettingsUnit(16, null, "System Default 2",
                  "{'column':[{'hidden':false,'width':'10%','dataIndex':'agency'},{'hidden':false,'width':'10%','dataIndex':'jobNumber'},{'hidden':false,'width':'15%','dataIndex':'caption'},{'hidden':true,'width':'13%','dataIndex':'detailTransactions'},{'hidden':true,'width':'15%','dataIndex':'productName'},{'hidden':true,'width':'10%','dataIndex':'directorName'},{'hidden':true,'width':'15%','dataIndex':'poLogCount'},{'hidden':true,'width':'15%','dataIndex':'typeofProjectName'},{'hidden':false,'width':'10%','dataIndex':'totalCost'},{'hidden':false,'width':'12%','dataIndex':'bidContractTotal'},{'hidden':false,'width':'13%','dataIndex':'producersActual'},{'hidden':false,'width':'13%','dataIndex':'billedAmount'},{'hidden':false,'width':'13%','dataIndex':'recievedAmount'},{'hidden':false,'width':'11%','dataIndex':'variance'},{'hidden':false,'width':'10%','dataIndex':'jobStatusName'},{'hidden':false,'width':'20%','dataIndex':'shootingDate'},{'hidden':false,'width':'14%','dataIndex':'shootLocations'},{'hidden':true,'width':'15%','dataIndex':'isWrapUpInsurance'},{'hidden':false,'width':'13%','dataIndex':'agencyProducer'}],'groupInfo':{ 'isGrouped':false,'groupField':'','groupDir':'ASC'}}")
            };
        }

        public DefaultUserViewSettingsCreator(CORPACCOUNTINGDbContext context)
        {
            _context = context;
        }


        public void Create()
        {
            CreateUserViewSettingsList();
        }

        private void CreateUserViewSettingsList()
        {
            foreach (var userSetting in InitialUserViewSettingsList)
            {
                AddUserViewSettingsListIfNotExists(userSetting);
            }
        }
        private void AddUserViewSettingsListIfNotExists(UserViewSettingsUnit userSettingsList)
        {
            if (_context.UserViewSettingsUnit.Any(l => l.TenantId == userSettingsList.TenantId && l.ViewSettingName == userSettingsList.ViewSettingName))
            {
                return;
            }

            _context.UserViewSettingsUnit.Add(userSettingsList);

            _context.SaveChanges();
        }


    }
}
