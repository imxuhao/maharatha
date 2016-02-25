using System.Data.Entity;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using System.Linq;
using System.Linq.Dynamic;
using Abp.Linq.Extensions;
using Abp.Authorization;

namespace CAPS.CORPACCOUNTING.Masters
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class CustomerPaymentTermUnitAppService : CORPACCOUNTINGServiceBase, ICustomerPaymentTermUnitAppService
    {
        private readonly CustomerPaymentTermUnitManager _customerPaymentTermUnitManager;
        private readonly IRepository<CustomerPaymentTermUnit> _customerPaymentTermUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CustomerPaymentTermUnitAppService(CustomerPaymentTermUnitManager customerPaymentTermUnitManager, IRepository<CustomerPaymentTermUnit> customerPaymentTermUnitRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _customerPaymentTermUnitManager = customerPaymentTermUnitManager;
            _customerPaymentTermUnitRepository = customerPaymentTermUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        [UnitOfWork]
        public async Task<CustomerPaymentTermUnitDto> CreateCustomerPaymentTermUnit(
            CreateCustomerPaymentTermUnitInput input)
        {
            var customerPaymentTermUnit = new CustomerPaymentTermUnit(description: input.Description,
                duedays: input.DueDays, paymentinstruction: input.PaymentInstruction,
                discountpercent: input.DiscountPercent, discountdays: input.DiscountDays,
                overnightinstructions: input.OvernightInstructions, wiringinstructions: input.WiringInstructions,
                footermessage: input.FooterMessage, logocaption: input.LogoCaption, isdefault: input.IsDefault,
                organizationid: input.OrganizationUnitId);
            await _customerPaymentTermUnitManager.CreateAsync(customerPaymentTermUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return customerPaymentTermUnit.MapTo<CustomerPaymentTermUnitDto>();
        }

        public async Task DeleteCustomerPaymentTermUnit(IdInput input)
        {
            await _customerPaymentTermUnitManager.DeleteAsync(input.Id);
        }

        public async Task<PagedResultOutput<CustomerPaymentTermUnitDto>> GetCustomerPaymentTermUnits(
            GetCustomerPaymentTermInput input)
        {
            var query =
                from cpt in _customerPaymentTermUnitRepository.GetAll()
                select new {CustomerPayTerms = cpt};

            query = query
                .WhereIf(input.OrganizationUnitId != null,
                    item => item.CustomerPayTerms.OrganizationUnitId == input.OrganizationUnitId);

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultOutput<CustomerPaymentTermUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.CustomerPayTerms.MapTo<CustomerPaymentTermUnitDto>();
                dto.CustomerPaymentTermId = item.CustomerPayTerms.Id;
                return dto;
            }).ToList());
        }

        public async Task<CustomerPaymentTermUnitDto> UpdateCustomerPaymentTermUnit(UpdateCustomerPaymentTermUnitInput input)
        {
            var customerPaymentTermUnit = await _customerPaymentTermUnitRepository.GetAsync(input.CustomerPaymentTermId);

            #region Setting the values to be updated

            customerPaymentTermUnit.Description = input.Description;
            customerPaymentTermUnit.DueDays = input.DueDays;
            customerPaymentTermUnit.DiscountDays = input.DiscountDays;
            customerPaymentTermUnit.OrganizationUnitId = input.OrganizationUnitId;
            customerPaymentTermUnit.IsDefault = input.IsDefault;
            customerPaymentTermUnit.FooterMessage = input.FooterMessage;
            customerPaymentTermUnit.LogoCaption = input.LogoCaption;
            customerPaymentTermUnit.DiscountPercent = input.DiscountPercent;
            customerPaymentTermUnit.OvernightInstructions = input.OvernightInstructions;
            customerPaymentTermUnit.PaymentInstruction = input.PaymentInstruction;
            customerPaymentTermUnit.WiringInstructions = input.WiringInstructions;
            #endregion

            await _customerPaymentTermUnitManager.UpdateAsync(customerPaymentTermUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {
                /*Do Something when the Chart of customerPaymentTerm is Added*/
            };

            return customerPaymentTermUnit.MapTo<CustomerPaymentTermUnitDto>();
        }

        /// <summary>
        /// Get the CustomerPaymentTerms Details By CustomerPaymentTermsId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<CustomerPaymentTermUnitDto> GetCustomerPayTermUnitsById(IdInput input)
        {
            CustomerPaymentTermUnit customerPaytermUnit = await _customerPaymentTermUnitRepository.GetAsync(input.Id);
            CustomerPaymentTermUnitDto result = customerPaytermUnit.MapTo<CustomerPaymentTermUnitDto>();
            result.CustomerPaymentTermId = customerPaytermUnit.Id;
            return result;
        }
    }
}
