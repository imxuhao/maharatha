using Abp.AutoMapper;
using AutoMapper;
using CAPS.CORPACCOUNTING.GenericSearch;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Helpers
{
    public static class Helper
    {

        public enum RecordType
        {
            [Display(Name = "Created")]
            Created = 1,
            [Display(Name = "Deleted")]
            Deleted = 2,
            [Display(Name = "Updated")]
            Updated = 3
        }


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
        /// setting defaultsort string if sort is Empty
        /// </summary>
        /// <param name="defaultsort"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static string GetSort(string defaultsort, string sort)
        {
            return string.IsNullOrEmpty(sort) ? defaultsort : sort;
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
            List<EnumSearch> enumSearch = null;
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
                    case DataTypes.Numeric:
                        if (ReferenceEquals(numericSearch, null))
                            numericSearch = new List<NumericSearch>();
                        Mapper.CreateMap<Filters, NumericSearch>()
                            .ForMember(u => u.Comparator, ap => ap.MapFrom(src => src.Comparator))
                            .ForMember(u => u.Property, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.Entity) ? (src.Entity + ".") : "") + src.Property))
                            .ForMember(u => u.SearchTerms, ap => ap.MapFrom(src => ((src.Comparator == 6) ? src.SearchTerm : "")))
                            .ForMember(u => u.SearchTerm, ap => ap.MapFrom(src => ((src.Comparator == 6) ? null : src.SearchTerm)));
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
                            .ForMember(u => u.Property, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.Entity) ? (src.Entity + ".") : "") + src.Property))
                            .ForMember(u => u.SearchTerms, ap => ap.MapFrom(src => ((src.Comparator == 6) ? src.SearchTerm : "")))
                            .ForMember(u => u.SearchTerm, ap => ap.MapFrom(src => ((src.Comparator == 6) ? null : src.SearchTerm)));
                        decimalSearch.Add(item.MapTo(new DecimalSearch()));
                        break;
                    case DataTypes.Enum:
                        if (ReferenceEquals(enumSearch, null))
                            enumSearch = new List<EnumSearch>();
                        Mapper.CreateMap<Filters, EnumSearch>()
                            .ForMember(u => u.Property, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.Entity) ? (src.Entity + ".") : "") + src.Property))
                            .ForMember(u => u.SearchTerm, ap => ap.MapFrom(src => (!string.IsNullOrEmpty(src.SearchTerm) ? src.SearchTerm : null)));
                        enumSearch.Add(item.MapTo(new EnumSearch()));
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
            if (!ReferenceEquals(enumSearch, null))
                search.EnumSearch = enumSearch;
            return search;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="searchTypes"></param>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        public static IQueryable<T> CreateFilters<T>(this IQueryable<T> query, SearchTypes searchTypes, SearchPattern searchPattern = SearchPattern.And)
        {
            //string Search
            if (!ReferenceEquals(searchTypes.TextSearch, null))
                query = query.ApplySearchCriterias(searchTypes.TextSearch, searchPattern);

            //Numeric Search
            if (!ReferenceEquals(searchTypes.NumericSearch, null))
                query = query.ApplySearchCriterias(searchTypes.NumericSearch, searchPattern);

            //date Search
            if (!ReferenceEquals(searchTypes.DateSearch, null))
                query = query.ApplySearchCriterias(searchTypes.DateSearch, searchPattern);

            //Boolean Search
            if (!ReferenceEquals(searchTypes.BooleanSearch, null))
                query = query.ApplySearchCriterias(searchTypes.BooleanSearch, searchPattern);

            //Decimal Search
            if (!ReferenceEquals(searchTypes.DecimalSearch, null))
                query = query.ApplySearchCriterias(searchTypes.DecimalSearch, searchPattern);

            //Enum Search Search
            if (!ReferenceEquals(searchTypes.EnumSearch, null))
                query = query.ApplySearchCriterias(searchTypes.EnumSearch, searchPattern);

            return query;
        }

        public static DateTime GetClientDate(string date, object ClientTimeZoneoffset)
        {
            if (ClientTimeZoneoffset != null)
            {
                string Temp = ClientTimeZoneoffset.ToString().Trim();
                if (!Temp.Contains("+") && !Temp.Contains("-"))
                {
                    Temp = Temp.Insert(0, "+");
                }
                //Retrieve all system time zones available into a collection
                ReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();
                DateTime startTime = DateTime.Parse(date);
                DateTime _now = DateTime.Parse(date);
                foreach (TimeZoneInfo timeZoneInfo in timeZones)
                {
                    if (timeZoneInfo.ToString().Contains(Temp))
                    {
                        TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById(timeZoneInfo.Id);
                        _now = TimeZoneInfo.ConvertTime(startTime, TimeZoneInfo.Utc, tst);
                        break;
                    }
                }
                return _now;
            }
            else
                return DateTime.Parse(date);
        }
        /// <summary>
        /// Extension Method to return Emptystring when we pass as Null
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string EmptyIfNull(this string self)
        {
            return self ?? string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strCronNeeded"></param>
        /// <returns></returns>
        public static async Task<string> GetCron(string strCronNeeded)
        {

            //use the required URL :http://www.cronmaker.com/help/rest-api-help.html
            var client = new HttpClient();
            string strCronReturned = string.Empty;
            // Create the HttpContent for the form to be posted.
            var requestContent = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("SUMIT", "This is a block of text"), });

            // Get the response.
            HttpResponseMessage response = await client.PostAsync(" http://www.cronmaker.com/rest/hourly/every/1", requestContent);

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            //using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            //{
            //    // Write the output.
            //    Console.WriteLine(await reader.ReadToEndAsync());
            //}

            return response.Content.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static List<Filters> GetMultiRangeFilters(List<Filters> filters)
        {
            for (int i = 0; i < filters.Count; i++)
            {
                var splittedFilters = filters[i].SearchTerm.Split(';');
                var newFilters = new List<Filters>();
                if (splittedFilters.Count() > 0)
                {
                    var filterRanges = splittedFilters;
                    for (int j = 0; j < filterRanges.Count(); j++)
                    {
                        var newFiltersRange = filterRanges[j].Split(new string[] { ".." }, StringSplitOptions.None);
                        var newFilter = new Filters();
                        newFilter.Comparator = filters[i].Comparator;
                        newFilter.DataType = filters[i].DataType;
                        newFilter.Property = filters[i].Property;
                        newFilter.SearchTerm = newFiltersRange[0];
                        newFilter.SearchTerm2 = newFiltersRange.Count() > 1 ? newFiltersRange[1] : newFiltersRange[0];
                        newFilters.Add(newFilter);
                    }
                }
                return newFilters;
            }
            return filters;

        }

        /// <summary>
        /// Converting string to bool
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static bool ConvertToBoolean(string flag)
        {
            return flag.ToUpper() == "TRUE";

        }
    }
}
