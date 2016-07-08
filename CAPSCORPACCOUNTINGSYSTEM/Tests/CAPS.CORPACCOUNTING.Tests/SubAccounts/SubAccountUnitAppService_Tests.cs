using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Accounting.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CAPS.CORPACCOUNTING.Tests
{
    public class SubAccountUnitAppService_Tests : AppTestBase
    {
        private readonly ISubAccountUnitAppService _subAccountUnitAppService;

        public SubAccountUnitAppService_Tests()
        {
            _subAccountUnitAppService = Resolve<ISubAccountUnitAppService>();
        }

        [Fact]
        public async Task Test_GetSubAccountUnits()
        {

            SearchInputDto input = new SearchInputDto();
            //Act
            var output = await _subAccountUnitAppService.GetSubAccountUnits(input);

            //Assert
            output.Items.Count.ShouldBeGreaterThan(-1);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task Test_GetSubAccountUnitsById(int id)
        {
            var input = new IdInput { Id = id };
            var subAccountUnit = await _subAccountUnitAppService.GetSubAccountUnitsById(input);
            subAccountUnit.ShouldNotBeNull("SubAccount Name: " + subAccountUnit.Caption);
        }

        [Fact]
        public async Task Test_CreateSubAccountUnit()
        {
            CreateSubAccountUnitInput subAccountUnitInput = new CreateSubAccountUnitInput();
            subAccountUnitInput.Caption = "subaccount";
            subAccountUnitInput.Description = "subaccountDesc";
            subAccountUnitInput.OrganizationUnitId = 1;
            subAccountUnitInput.SubAccountNumber = "subacc001";
            subAccountUnitInput.IsActive = true;

            //Act
            var output = await _subAccountUnitAppService.CreateSubAccountUnit(subAccountUnitInput);

            //Assert
            UsingDbContext(context =>
            {
                var newCoa = context.SubAccountUnits.FirstOrDefault(ou => ou.SubAccountNumber == subAccountUnitInput.SubAccountNumber);
                newCoa.ShouldNotBeNull();
            });

        }


        [Fact]
        public async Task Test_UpdateSubAccountUnit()
        {

            var subAccountUnit = GetSubAccount("subaccount");
            //Act
            await _subAccountUnitAppService.UpdateSubAccountUnit(new UpdateSubAccountUnitInput
            {
                SubAccountId = subAccountUnit.Id,
                Caption = "subaccount2",
                Description = "CoaDescsubaccountDesc",
                OrganizationUnitId = 1,
                SubAccountNumber = "subacc001",
                IsActive = true
            });

            //Assert
            UsingDbContext(context =>
            {
                var newCoa = context.CoaUnits.FirstOrDefault(ou => ou.Caption == "subaccount2");
                newCoa.ShouldNotBeNull();
            });

        }

        [Fact]
        public async Task Test_DeleteSubAccountUnit()
        {
            //Arrange
            var subAccountUnit = GetSubAccount("subaccount2");

            UsingDbContext(context =>
            {
                context.SubAccountUnits.FirstOrDefault(u => u.Id == subAccountUnit.Id && u.TenantId == AbpSession.TenantId.Value).ShouldNotBeNull();
            });

            //Act
            await _subAccountUnitAppService.DeleteSubAccountUnit(new IdInput<long> { Id = subAccountUnit.Id });

            //Assert
            GetSubAccount(subAccountUnit.Id).IsDeleted.ShouldBeTrue();
        }

        private SubAccountUnit GetSubAccount(string caption)
        {
            var subAccountUnit = UsingDbContext(context => context.SubAccountUnits.FirstOrDefault(ou => ou.Caption == caption));
            subAccountUnit.ShouldNotBeNull();
            return subAccountUnit;
        }

        private SubAccountUnit GetSubAccount(long id)
        {
            var subAccountUnit = UsingDbContext(context => context.SubAccountUnits.FirstOrDefault(ou => ou.Id == id));
            subAccountUnit.ShouldNotBeNull();
            return subAccountUnit;
        }
    }
}
