using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using AutoMapper;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Banking.Dto;
using CAPS.CORPACCOUNTING.GenericSearch;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.Interceptors;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using CAPS.CORPACCOUNTING.Journals.Dto;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.Payables;

namespace CAPS.CORPACCOUNTING
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(typeof(CORPACCOUNTINGCoreModule))]
    public class CORPACCOUNTINGApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();
            //Adding an interceptor to measure the time taken for each method to execute in Applition
            MeasureDurationInterceptorRegistrar.Initialize(IocManager.IocContainer.Kernel);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg => {
                //Add mappings. Example:
                //configuration.CreateMap<MySourceClass, MyDestClass>();
                cfg.CreateMap<Filters, TextSearch>()
                        .ForMember(u => u.Comparator, ap => ap.MapFrom(src => src.Comparator))
                        .ForMember(u => u.Property, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.Entity) ? (src.Entity + ".") : "") + src.Property));

                cfg.CreateMap<InvoiceEntryDocumentDetailUnit, InvoiceEntryDocumentDetailUnit>();

                cfg.CreateMap<UpdateAddressUnitInput, CreateAddressUnitInput>();

                cfg.CreateMap<UpdateUserViewSettingsUnitInput, UserViewSettingsUnit>().ForMember(u => u.Id, ap => ap.MapFrom(src => src.UserViewId));

                cfg.CreateMap<JournalCreditEntryDetailUnitDto, JournalCreditEntryDetailUnitDto>();

                cfg.CreateMap<JobUnit, JobCommercialUnitDto>().ForMember(u => u.JobId, ap => ap.MapFrom(src => src.Id));

                cfg.CreateMap<UpdateJobPORangeAllocationInput, CreateJobPORangeAllocationInput>();

                cfg.CreateMap<UpdateARBillingTypeUnitInput, ARBillingTypeUnit>();

                cfg.CreateMap<Filters, TextSearch>().ForMember(u => u.Comparator, ap => ap.MapFrom(src => src.Comparator))
                                .ForMember(u => u.Property, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.Entity) ? (src.Entity + ".") : "") + src.Property));

                cfg.CreateMap<Filters, NumericSearch>().ForMember(u => u.Comparator, ap => ap.MapFrom(src => src.Comparator))
                            .ForMember(u => u.Property, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.Entity) ? (src.Entity + ".") : "") + src.Property))
                            .ForMember(u => u.SearchTerms, ap => ap.MapFrom(src => ((src.Comparator == 6) ? src.SearchTerm : "")))
                            .ForMember(u => u.SearchTerm, ap => ap.MapFrom(src => ((src.Comparator == 6) ? null : src.SearchTerm)));

                cfg.CreateMap<Filters, DateSearch>()
                            .ForMember(u => u.Comparator, ap => ap.MapFrom(src => src.Comparator))
                           .ForMember(u => u.Property, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.Entity) ? (src.Entity + ".") : "") + src.Property))
                           .ForMember(u => u.SearchTerm, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.SearchTerm) ? src.SearchTerm : null)))
                           .ForMember(u => u.SearchTerm2, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.SearchTerm2) ? src.SearchTerm2 : null)));

                cfg.CreateMap<Filters, BooleanSearch>()
                           .ForMember(u => u.Property, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.Entity) ? (src.Entity + ".") : "") + src.Property))
                        .ForMember(u => u.SearchTerm, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.SearchTerm) ? src.SearchTerm : null)));

                cfg.CreateMap<Filters, DecimalSearch>()
                           .ForMember(u => u.Comparator, ap => ap.MapFrom(src => src.Comparator))
                           .ForMember(u => u.SearchTerm, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.SearchTerm) ? src.SearchTerm : null)))
                           .ForMember(u => u.SearchTerm2, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.SearchTerm2) ? src.SearchTerm2 : null)))
                           .ForMember(u => u.Property, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.Entity) ? (src.Entity + ".") : "") + src.Property))
                           .ForMember(u => u.SearchTerms, ap => ap.MapFrom(src => ((src.Comparator == 6) ? src.SearchTerm : "")))
                           .ForMember(u => u.SearchTerm, ap => ap.MapFrom(src => ((src.Comparator == 6) ? null : src.SearchTerm)));

                cfg.CreateMap<Filters, EnumSearch>()
                            .ForMember(u => u.Property, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.Entity) ? (src.Entity + ".") : "") + src.Property))
                           .ForMember(u => u.SearchTerm, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.SearchTerm) ? src.SearchTerm : null)));

                cfg.CreateMap<UpdateBatchUnitInput, BatchUnit>();
            });
        }
   
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //Custom DTO auto-mappings
            CustomDtoMapper.CreateMappings();
        }
    }
}
