using Abp.AutoMapper;
using AutoMapper;
using CAPS.CORPACCOUNTING.GenericSearch;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CAPS.CORPACCOUNTING.Helper
{
    public static class Helper
    {
        /// <summary>
        /// To get formatted sorting string 
        /// </summary>
        /// <param name="sortList"></param>
        /// <returns></returns>
        public static string GetSortOrderAsString(List<Sort> sortList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var sortItem in sortList)
            {
                sb.Append((sortItem.Entity.Trim().Length > 0 ? (sortItem.Entity + ".") : " ") + sortItem.Property + " " + sortItem.Order + ",");
            }
            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Filters"></param>
        /// <returns></returns>
        public static SearchTypes MappingFilters(List<Filters> Filters)
        {
            SearchTypes search = new SearchTypes();
            List<TextSearch> textSearch = null;
            List<NumericSearch> numericSearch = null;
            List<DateSearch> dateSearch = null;
            List<BooleanSearch> booleanSearch = null;
            List<DecimalSearch> decimalSearch = null;
            foreach (var item in Filters)
            {
                switch (item.DataType)
                {
                    case DataTypes.Text:
                        if (ReferenceEquals(textSearch, null))
                            textSearch = new List<TextSearch>();
                        Mapper.CreateMap<Filters, TextSearch>()
                        .ForMember(u => u.Comparator, ap => ap.MapFrom(src => src.Comparator))
                        .ForMember(u => u.Property, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.Entity) ? (src.Entity + ".") : "") + src.Property));
                        textSearch.Add(item.MapTo(new TextSearch()));
                        break;
                    case DataTypes.Numaric:
                        if (ReferenceEquals(numericSearch, null))
                            numericSearch = new List<NumericSearch>();
                        Mapper.CreateMap<Filters, NumericSearch>()
                            .ForMember(u => u.Comparator, ap => ap.MapFrom(src => src.Comparator))
                            .ForMember(u => u.Property, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.Entity) ? (src.Entity + ".") : "") + src.Property));
                        numericSearch.Add(item.MapTo(new NumericSearch()));
                        break;
                    case DataTypes.Date:
                        if (ReferenceEquals(dateSearch, null))
                            dateSearch = new List<DateSearch>();
                        Mapper.CreateMap<Filters, DateSearch>()
                           .ForMember(u => u.Comparator, ap => ap.MapFrom(src => src.Comparator))
                           .ForMember(u => u.Property, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.Entity) ? (src.Entity + ".") : "") + src.Property))
                         .ForMember(u => u.SearchTerm, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.SearchTerm) ? src.SearchTerm : null)))
                           .ForMember(u => u.SearchTerm2, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.SearchTerm2) ? src.SearchTerm2 : null)));
                        dateSearch.Add(item.MapTo(new DateSearch()));
                        break;
                    case DataTypes.Bool:
                        if (ReferenceEquals(booleanSearch, null))
                            booleanSearch = new List<BooleanSearch>();
                        Mapper.CreateMap<Filters, BooleanSearch>()
                            .ForMember(u => u.Property, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.Entity) ? (src.Entity + ".") : "") + src.Property))
                         .ForMember(u => u.SearchTerm, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.SearchTerm) ? src.SearchTerm : null)));
                        booleanSearch.Add(item.MapTo(new BooleanSearch()));
                        break;
                    case DataTypes.Decimal:
                        if (ReferenceEquals(decimalSearch, null))
                            decimalSearch = new List<DecimalSearch>();
                        Mapper.CreateMap<Filters, DecimalSearch>()
                            .ForMember(u => u.Comparator, ap => ap.MapFrom(src => src.Comparator))
                            .ForMember(u => u.SearchTerm, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.SearchTerm) ? src.SearchTerm : null)))
                            .ForMember(u => u.SearchTerm2, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.SearchTerm2) ? src.SearchTerm2 : null)))
                            .ForMember(u => u.Property, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.Entity) ? (src.Entity + ".") : "") + src.Property));
                        decimalSearch.Add(item.MapTo(new DecimalSearch()));
                        break;
                    default:
                        break;
                }
            }

            if (!ReferenceEquals(textSearch, null))
                search.TextSearch = textSearch;
            if (!ReferenceEquals(numericSearch, null))
                search.NumericSearch = numericSearch;
            if (!ReferenceEquals(dateSearch, null))
                search.DateSearch = dateSearch;
            if (!ReferenceEquals(booleanSearch, null))
                search.BooleanSearch = booleanSearch;
            if (!ReferenceEquals(decimalSearch, null))
                search.DecimalSearch = decimalSearch;
            return search;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="searchTypes"></param>
        /// <returns></returns>
        public static IQueryable<T> CreateFilters<T>(this IQueryable<T> query, SearchTypes searchTypes)
        {
            //string Search
            if (!ReferenceEquals(searchTypes.TextSearch, null))
                query = query.ApplySearchCriterias(searchTypes.TextSearch);

            //Numaric Search
            if (!ReferenceEquals(searchTypes.NumericSearch, null))
                query = query.ApplySearchCriterias(searchTypes.NumericSearch);

            //date Search
            if (!ReferenceEquals(searchTypes.DateSearch, null))
                query = query.ApplySearchCriterias(searchTypes.DateSearch);
            //Boolean Search
            if (!ReferenceEquals(searchTypes.BooleanSearch, null))
                query = query.ApplySearchCriterias(searchTypes.BooleanSearch);
            if (!ReferenceEquals(searchTypes.DecimalSearch, null))
                query = query.ApplySearchCriterias(searchTypes.DecimalSearch);

            return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Sort
    {
        /// <summary>
        /// Gets or sets Column ParentTable Name
        /// </summary>
        public string Entity { get; set; }

        /// <summary>
        /// Gets or sets Column Name
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets Sorting Order
        /// </summary>
        public string Order { get; set; }
    }
    public class SearchTypes
    {
        public IEnumerable<TextSearch> TextSearch { get; set; }
        public IEnumerable<NumericSearch> NumericSearch { get; set; }
        public IEnumerable<EnumSearch> EnumSearch { get; set; }
        public IEnumerable<DateSearch> DateSearch { get; set; }
        public IEnumerable<BooleanSearch> BooleanSearch { get; set; }

        public IEnumerable<DecimalSearch> DecimalSearch { get; set; }
        

    }
    public enum DataTypes
    {
        Numaric = 0,
        Text = 1,
        Date = 2,
        Bool = 3,
        Enum = 4,
        Decimal=5
    }
    public class Filters
    {
        public string Entity { get; set; }
       
        public string Property { get; set; }
        public string SearchTerm { get; set; }
        public int Comparator { get; set; }
        public string SearchTerm2 { get; set; }
       
        public DataTypes DataType { get; set; }
    }

}
