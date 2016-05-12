using System;
using System.Linq;
using CAPS.CORPACCOUNTING.EntityFramework;
using CAPS.CORPACCOUNTING.Masters;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace CAPS.CORPACCOUNTING.Migrations.Seed
{
    public class DefaultSystemViewSettingsCreator
    {
        public static List<SystemViewSettingsUnit> InitialSystemViewSettingsList { get; set; }

        private readonly CORPACCOUNTINGDbContext _context;

        static DefaultSystemViewSettingsCreator()
        {
            InitialSystemViewSettingsList = new List<SystemViewSettingsUnit>
            {
                new SystemViewSettingsUnit(viewId: 16,viewSettingName:  "System Default 1",viewSettings:
                    "{'column':[{'hidden':false,'width':'10%','dataIndex':'jobNumber'},{'hidden':false,'width':'15%','dataIndex':'caption'},{'hidden':false,'width':'13%','dataIndex':'detailTransactions'},{'hidden':false,'width':'15%','dataIndex':'productName'},{'hidden':false,'width':'10%','dataIndex':'directorName'},{'hidden':false,'width':'10%','dataIndex':'agency'},{'hidden':false,'width':'15%','dataIndex':'poLogCount'},{'hidden':false,'width':'15%','dataIndex':'typeofProjectName'},{'hidden':false,'width':'20%','dataIndex':'shootingDate'},{'hidden':false,'width':'14%','dataIndex':'shootLocations'},{'hidden':false,'width':'15%','dataIndex':'isWrapUpInsurance'},{'hidden':false,'width':'10%','dataIndex':'jobStatusName'},{'hidden':true,'width':'10%','dataIndex':'totalCost'},{'hidden':true,'width':'12%','dataIndex':'bidContractTotal'},{'hidden':true,'width':'13%','dataIndex':'producersActual'},{'hidden':true,'width':'13%','dataIndex':'billedAmount'},{'hidden':true,'width':'13%','dataIndex':'recievedAmount'},{'hidden':true,'width':'11%','dataIndex':'variance'},{'hidden':true,'width':'13%','dataIndex':'agencyProducer'}],'groupInfo':{'isGrouped':false,'groupField':'','groupDir':'ASC'}}"),
                new SystemViewSettingsUnit(viewId: 16,viewSettingName:  "System Default 2",viewSettings:
                  "{'column':[{'hidden':false,'width':'10%','dataIndex':'agency'},{'hidden':false,'width':'10%','dataIndex':'jobNumber'},{'hidden':false,'width':'15%','dataIndex':'caption'},{'hidden':true,'width':'13%','dataIndex':'detailTransactions'},{'hidden':true,'width':'15%','dataIndex':'productName'},{'hidden':true,'width':'10%','dataIndex':'directorName'},{'hidden':true,'width':'15%','dataIndex':'poLogCount'},{'hidden':true,'width':'15%','dataIndex':'typeofProjectName'},{'hidden':false,'width':'10%','dataIndex':'totalCost'},{'hidden':false,'width':'12%','dataIndex':'bidContractTotal'},{'hidden':false,'width':'13%','dataIndex':'producersActual'},{'hidden':false,'width':'13%','dataIndex':'billedAmount'},{'hidden':false,'width':'13%','dataIndex':'recievedAmount'},{'hidden':false,'width':'11%','dataIndex':'variance'},{'hidden':false,'width':'10%','dataIndex':'jobStatusName'},{'hidden':false,'width':'20%','dataIndex':'shootingDate'},{'hidden':false,'width':'14%','dataIndex':'shootLocations'},{'hidden':true,'width':'15%','dataIndex':'isWrapUpInsurance'},{'hidden':false,'width':'13%','dataIndex':'agencyProducer'}],'groupInfo':{ 'isGrouped':false,'groupField':'','groupDir':'ASC'}}")
            };
        }

        public DefaultSystemViewSettingsCreator(CORPACCOUNTINGDbContext context)
        {
            _context = context;
        }


        public void Create()
        {
            CreateSystemViewSettingsList();
        }

        private void CreateSystemViewSettingsList()
        {
            foreach (var systemSetting in InitialSystemViewSettingsList)
            {
                _context.SystemViewSettingsUnit.Add(systemSetting);

                try
                {
                    // doing here my logic
                    _context.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.
              PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
                // AddSystemViewSettingsListIfNotExists(systemSetting);
            }
        }
        //private void AddSystemViewSettingsListIfNotExists(SystemViewSettingsUnit userSettingsList)
        //{
            
        //    if (_context.SystemViewSettingsUnit.Any(l => l.ViewId == userSettingsList.ViewId && l.ViewName == userSettingsList.ViewName))
        //    {
        //        return;
        //    }

        //    _context.SystemViewSettingsUnit.Add(userSettingsList);

        //    _context.SaveChanges();
        //}


    }
}
