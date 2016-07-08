using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Xunit;
using Shouldly;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;

namespace CAPS.CORPACCOUNTING.Tests.ChartOfAccount
{
    public class CoaUnitAppService_Tests : AppTestBase
    {

        private readonly ICoaUnitAppService _coaUnitoaUnitAppService;

        public CoaUnitAppService_Tests()
        {
            _coaUnitoaUnitAppService = Resolve<ICoaUnitAppService>();
        }


        [Fact]
        public async Task Test_GetCoaUnits()
        {

            SearchInputDto input = new SearchInputDto();
            //Act
            var output = await _coaUnitoaUnitAppService.GetCoaUnits(input);

            //Assert
            output.Items.Count.ShouldBeGreaterThan(-1);
        }

       
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task Test_GetCoaUnitById(int id)
        {
            var input = new IdInput {Id= id };
            var coaUnit =await _coaUnitoaUnitAppService.GetCoaUnitById(input);
            coaUnit.ShouldNotBeNull("Coa Name: " + coaUnit.Caption);
        }

        [Fact]
        public async Task Test_CreateCoaUnit()
        {
            CreateCoaUnitInput coaUnitInput = new CreateCoaUnitInput();
            coaUnitInput.Caption = "coa1";
            coaUnitInput.Description = "CoaDesc";
            coaUnitInput.IsCorporate = true;
            coaUnitInput.IsActive = true;

            //Act
            var output = await _coaUnitoaUnitAppService.CreateCoaUnit(coaUnitInput);


            //Assert
            UsingDbContext(context =>
            {
                var newCoa = context.CoaUnits.FirstOrDefault(ou => ou.Caption == coaUnitInput.Caption);
                newCoa.ShouldNotBeNull();
            });

        }

        [Fact]
        public async Task Test_UpdateCoaUnit()
        {

            var coaUnit = GetCoa("coa1");
            //Act
            await _coaUnitoaUnitAppService.UpdateCoaUnit(new UpdateCoaUnitInput
            {
                CoaId = coaUnit.Id,
                Caption = "coa2",
                Description = "CoaDesc",
                IsCorporate = true,
                IsActive = true
            });

            //Assert
            UsingDbContext(context =>
            {
                var newCoa = context.CoaUnits.FirstOrDefault(ou => ou.Caption == "coa2");
                newCoa.ShouldNotBeNull();
            });

        }
        
        [Fact]
        public async Task Test_DeleteCoaUnit()
        {
            //Arrange
            var coaUnit = GetCoa("coa1");

            UsingDbContext(context =>
            {
                context.CoaUnits.FirstOrDefault(u => u.Id == coaUnit.Id && u.TenantId == AbpSession.TenantId.Value).ShouldNotBeNull();
            });

            //Act
            await _coaUnitoaUnitAppService.DeleteCoaUnit(new IdInput {Id= coaUnit.Id });

            //Assert
            GetCoa(coaUnit.Id).IsDeleted.ShouldBeTrue();
        }


        private CoaUnit GetCoa(string caption)
        {
            var coaUnit = UsingDbContext(context => context.CoaUnits.FirstOrDefault(ou => ou.Caption == caption));
            coaUnit.ShouldNotBeNull();
            return coaUnit;
        }

        private CoaUnit GetCoa(long id)
        {
            var coaUnit = UsingDbContext(context => context.CoaUnits.FirstOrDefault(ou => ou.Id == id));
            coaUnit.ShouldNotBeNull();

            return coaUnit;
        }
    }

}
