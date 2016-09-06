using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using Abp.Domain.Repositories;
using System.Data.Entity;
using System.Linq;
using AutoMapper;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// TaxCreditUnit AppService
    /// </summary>
    public class TaxCreditUnitAppService : CORPACCOUNTINGServiceBase, ITaxCreditUnitAppService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly TaxCreditUnitManager _taxCreditUnitManager;
        private readonly IRepository<TaxCreditUnit> _taxCreditUnitRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="taxCreditUnitManager"></param>
        /// <param name="taxCreditUnitRepository"></param>
        public TaxCreditUnitAppService(IUnitOfWorkManager unitOfWorkManager, TaxCreditUnitManager taxCreditUnitManager, IRepository<TaxCreditUnit> taxCreditUnitRepository)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _taxCreditUnitManager = taxCreditUnitManager;
            _taxCreditUnitRepository = taxCreditUnitRepository;
        }

        /// <summary>
        /// Create the TaxCredit.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateTaxCreditUnit(CreateTaxCreditUnitInput input)
        {
            var taxCreditUnit = input.MapTo<TaxCreditUnit>();
            await _taxCreditUnitManager.CreateAsync(taxCreditUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Update the TaxCredit based on TaxCreditId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateTaxCreditUnit(UpdateTaxCreditUnitInput input)
        {
            var taxCreditUnit = await _taxCreditUnitRepository.GetAsync(input.TaxCreditId);
            Mapper.Map(input, taxCreditUnit);
            await _taxCreditUnitManager.UpdateAsync(taxCreditUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        ///  Get the list of all TaxCredit.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListResultOutput<TaxCreditUnitDto>> GetTaxCreditUnits(SearchInputDto input)
        {
            var query = _taxCreditUnitRepository.GetAll();
            var items = await query.OrderBy(p=>p.Description).ToListAsync();

            return new ListResultOutput<TaxCreditUnitDto>(
                items.Select(item =>
                {
                    var dto = item.MapTo<TaxCreditUnitDto>();
                    dto.TaxCreditId = item.Id;
                    return dto;
                }).ToList());
        }

        /// <summary>
        ///  Delete the TaxCredit based on TaxCreditId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteTaxCreditUnit(IdInput input)
        {
            await _taxCreditUnitManager.DeleteAsync(input.Id);
        }
    }
}
