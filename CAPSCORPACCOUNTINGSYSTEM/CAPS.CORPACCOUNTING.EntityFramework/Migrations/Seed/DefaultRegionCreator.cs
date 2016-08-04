using System.Linq;
using CAPS.CORPACCOUNTING.EntityFramework;
using CAPS.CORPACCOUNTING.Masters;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Migrations.Seed
{

    public class DefaultRegionCreator
    {
        public static List<RegionUnit> InitialRegionList { get; private set; }


        public static List<RegionUnit> InitialUsaList { get; private set; }

        private readonly CORPACCOUNTINGDbContext _context;

        public DefaultRegionCreator(CORPACCOUNTINGDbContext context)
        {
            _context = context;

            InitialUsaList = new List<RegionUnit>
            {
                  new RegionUnit(description:"California",regionAbbreviation:"CA",stateCode:"6"),
                  new RegionUnit(description:"New York",regionAbbreviation:"NY",stateCode:"null"),
            };
            //RegionList(tenantId);

        }
        //public DefaultRegionCreator(CORPACCOUNTINGDbContext context, int tenantId)
        //{
        //    _context = context;

        //    InitialUsaList = new List<RegionUnit>
        //    {
        //          new RegionUnit(description:"California",regionAbbreviation:"CA",stateCode:"6"),
        //          new RegionUnit(description:"New York",regionAbbreviation:"NY",stateCode:"null"),
        //    };
        //    //RegionList(tenantId);

        //}
        public void Create()
        {
            CreateRegionList();
        }

        private void CreateRegionList()
        {
            var country = _context.CountryUnit.Where(u => u.TwoLetterAbbreviation == "US").FirstOrDefault();
            foreach (var region in InitialUsaList)
            {
                AddRegionListIfNotExists(region, country.Id);
            }
        }

        private void AddRegionListIfNotExists(RegionUnit region,int countryId)
        {
            if (_context.RegionUnit.Any(l => l.CountryID == countryId && l.Description == region.Description))
            {
                return;
            }
            region.CountryID = countryId;
            _context.RegionUnit.Add(region);

            _context.SaveChanges();
        }

        private void RegionList(int TenantId)
        {
            InitialRegionList = new List<RegionUnit>
            {
                        //new RegionUnit(description:"Alaska",regionAbbreviation:"AK",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Alabama",regionAbbreviation:"AL",stateCode:"1",tenantId:TenantId),
                        //new RegionUnit(description:"Arkansas",regionAbbreviation:"AR",stateCode:"5",tenantId:TenantId),
                        //new RegionUnit(description:"Arizona",regionAbbreviation:"AZ",stateCode:"4",tenantId:TenantId),
                        new RegionUnit(description:"California",regionAbbreviation:"CA",stateCode:"6"),
                        //new RegionUnit(description:"Colorado",regionAbbreviation:"CO",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Connecticut",regionAbbreviation:"CT",stateCode:"8",tenantId:TenantId),
                        //new RegionUnit(description:"District Of Columbia",regionAbbreviation:"DC",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Delware",regionAbbreviation:"DE",stateCode:"10",tenantId:TenantId),
                        //new RegionUnit(description:"Florida",regionAbbreviation:"FL",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Georgia",regionAbbreviation:"GA",stateCode:"13",tenantId:TenantId),
                        //new RegionUnit(description:"Hawaii",regionAbbreviation:"HI",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Iowa",regionAbbreviation:"IA",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Idaho",regionAbbreviation:"ID",stateCode:"16",tenantId:TenantId),
                        //new RegionUnit(description:"Illinois",regionAbbreviation:"IL",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Indiana",regionAbbreviation:"IN",stateCode:"18",tenantId:TenantId),
                        //new RegionUnit(description:"Kansas",regionAbbreviation:"KS",stateCode:"20",tenantId:TenantId),
                        //new RegionUnit(description:"Kentucky",regionAbbreviation:"KY",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Louisiana",regionAbbreviation:"LA",stateCode:"22",tenantId:TenantId),
                        //new RegionUnit(description:"Massachusetts",regionAbbreviation:"MA",stateCode:"25",tenantId:TenantId),
                        //new RegionUnit(description:"Maryland",regionAbbreviation:"MD",stateCode:"24",tenantId:TenantId),
                        //new RegionUnit(description:"Maine",regionAbbreviation:"ME",stateCode:"23",tenantId:TenantId),
                        //new RegionUnit(description:"Michigan",regionAbbreviation:"MI",stateCode:"26",tenantId:TenantId),
                        //new RegionUnit(description:"Minnesota",regionAbbreviation:"MN",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Missouri",regionAbbreviation:"MO",stateCode:"29",tenantId:TenantId),
                        //new RegionUnit(description:"Mississippi",regionAbbreviation:"MS",stateCode:"28",tenantId:TenantId),
                        //new RegionUnit(description:"Montana",regionAbbreviation:"MT",stateCode:"30",tenantId:TenantId),
                        //new RegionUnit(description:"North Carolina",regionAbbreviation:"NC",stateCode:"37",tenantId:TenantId),
                        //new RegionUnit(description:"North Dakota",regionAbbreviation:"ND",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Nebraska",regionAbbreviation:"NE",stateCode:"31",tenantId:TenantId),
                        //new RegionUnit(description:"New Hampshire",regionAbbreviation:"NH",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"New Jersey",regionAbbreviation:"NJ",stateCode:"34",tenantId:TenantId),
                        //new RegionUnit(description:"New Mexico",regionAbbreviation:"NM",stateCode:"35",tenantId:TenantId),
                        //new RegionUnit(description:"Nevada",regionAbbreviation:"NV",stateCode:"null",tenantId:TenantId),
                        new RegionUnit(description:"New York",regionAbbreviation:"NY",stateCode:"null"),
                        //new RegionUnit(description:"Ohio",regionAbbreviation:"OH",stateCode:"39",tenantId:TenantId),
                        //new RegionUnit(description:"Oklahoma",regionAbbreviation:"OK",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Oregon",regionAbbreviation:"OR",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Pennsylvania",regionAbbreviation:"PA",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Rhode Island",regionAbbreviation:"RI",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"South Carolina",regionAbbreviation:"SC",stateCode:"45",tenantId:TenantId),
                        //new RegionUnit(description:"South Dakota",regionAbbreviation:"SD",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Tennessee",regionAbbreviation:"TN",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Texas",regionAbbreviation:"TX",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Utah",regionAbbreviation:"UT",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Virginia",regionAbbreviation:"VA",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Vermont",regionAbbreviation:"VT",stateCode:"50",tenantId:TenantId),
                        //new RegionUnit(description:"Washington",regionAbbreviation:"WA",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Wisconsin",regionAbbreviation:"WI",stateCode:"55",tenantId:TenantId),
                        //new RegionUnit(description:"West Virginia",regionAbbreviation:"WV",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Wyoming",regionAbbreviation:"WY",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"American Samoa",regionAbbreviation:"AS",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Guam",regionAbbreviation:"GU",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Puerto Rico",regionAbbreviation:"PR",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Virgin Islands",regionAbbreviation:"VI",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Alberta, Canada",regionAbbreviation:"AB",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"British Columbia, Canada",regionAbbreviation:"BC",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Saskatchewan, Canada",regionAbbreviation:"SK",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Manitoba, Canada",regionAbbreviation:"MB",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Ontario, Canada",regionAbbreviation:"ON",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Quebec, Canada",regionAbbreviation:"PQ",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"New Brunswick, Canada",regionAbbreviation:"NB",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Nova Scotia, Canada",regionAbbreviation:"NS",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Prince Edward Is, Canada",regionAbbreviation:"PE",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Northwest Terr., Canada",regionAbbreviation:"NT",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Yukon Territory, Canada",regionAbbreviation:"YT",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"New Foundland, Canada",regionAbbreviation:"NF",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Alaska",regionAbbreviation:"AK",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Alabama",regionAbbreviation:"AL",stateCode:"1",tenantId:TenantId),
                        //new RegionUnit(description:"Arkansas",regionAbbreviation:"AR",stateCode:"5",tenantId:TenantId),
                        //new RegionUnit(description:"Arizona",regionAbbreviation:"AZ",stateCode:"4",tenantId:TenantId),
                        //new RegionUnit(description:"California",regionAbbreviation:"CA",stateCode:"6",tenantId:TenantId),
                        //new RegionUnit(description:"Colorado",regionAbbreviation:"CO",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Connecticut",regionAbbreviation:"CT",stateCode:"8",tenantId:TenantId),
                        //new RegionUnit(description:"District Of Columbia",regionAbbreviation:"DC",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Delware",regionAbbreviation:"DE",stateCode:"10",tenantId:TenantId),
                        //new RegionUnit(description:"Florida",regionAbbreviation:"FL",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Georgia",regionAbbreviation:"GA",stateCode:"13",tenantId:TenantId),
                        //new RegionUnit(description:"Hawaii",regionAbbreviation:"HI",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Iowa",regionAbbreviation:"IA",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Idaho",regionAbbreviation:"ID",stateCode:"16",tenantId:TenantId),
                        //new RegionUnit(description:"Illinois",regionAbbreviation:"IL",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Indiana",regionAbbreviation:"IN",stateCode:"18",tenantId:TenantId),
                        //new RegionUnit(description:"Kansas",regionAbbreviation:"KS",stateCode:"20",tenantId:TenantId),
                        //new RegionUnit(description:"Kentucky",regionAbbreviation:"KY",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Louisiana",regionAbbreviation:"LA",stateCode:"22",tenantId:TenantId),
                        //new RegionUnit(description:"Massachusetts",regionAbbreviation:"MA",stateCode:"25",tenantId:TenantId),
                        //new RegionUnit(description:"Maryland",regionAbbreviation:"MD",stateCode:"24",tenantId:TenantId),
                        //new RegionUnit(description:"Maine",regionAbbreviation:"ME",stateCode:"23",tenantId:TenantId),
                        //new RegionUnit(description:"Michigan",regionAbbreviation:"MI",stateCode:"26",tenantId:TenantId),
                        //new RegionUnit(description:"Minnesota",regionAbbreviation:"MN",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Missouri",regionAbbreviation:"MO",stateCode:"29",tenantId:TenantId),
                        //new RegionUnit(description:"Mississippi",regionAbbreviation:"MS",stateCode:"28",tenantId:TenantId),
                        //new RegionUnit(description:"Montana",regionAbbreviation:"MT",stateCode:"30",tenantId:TenantId),
                        //new RegionUnit(description:"North Carolina",regionAbbreviation:"NC",stateCode:"37",tenantId:TenantId),
                        //new RegionUnit(description:"North Dakota",regionAbbreviation:"ND",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Nebraska",regionAbbreviation:"NE",stateCode:"31",tenantId:TenantId),
                        //new RegionUnit(description:"New Hampshire",regionAbbreviation:"NH",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"New Jersey",regionAbbreviation:"NJ",stateCode:"34",tenantId:TenantId),
                        //new RegionUnit(description:"New Mexico",regionAbbreviation:"NM",stateCode:"35",tenantId:TenantId),
                        //new RegionUnit(description:"Nevada",regionAbbreviation:"NV",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"New York",regionAbbreviation:"NY",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Ohio",regionAbbreviation:"OH",stateCode:"39",tenantId:TenantId),
                        //new RegionUnit(description:"Oklahoma",regionAbbreviation:"OK",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Oregon",regionAbbreviation:"OR",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Pennsylvania",regionAbbreviation:"PA",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Rhode Island",regionAbbreviation:"RI",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"South Carolina",regionAbbreviation:"SC",stateCode:"45",tenantId:TenantId),
                        //new RegionUnit(description:"South Dakota",regionAbbreviation:"SD",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Tennessee",regionAbbreviation:"TN",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Texas",regionAbbreviation:"TX",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Utah",regionAbbreviation:"UT",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Virginia",regionAbbreviation:"VA",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Vermont",regionAbbreviation:"VT",stateCode:"50",tenantId:TenantId),
                        //new RegionUnit(description:"Washington",regionAbbreviation:"WA",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Wisconsin",regionAbbreviation:"WI",stateCode:"55",tenantId:TenantId),
                        //new RegionUnit(description:"West Virginia",regionAbbreviation:"WV",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Wyoming",regionAbbreviation:"WY",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"American Samoa",regionAbbreviation:"AS",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Guam",regionAbbreviation:"GU",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Puerto Rico",regionAbbreviation:"PR",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Virgin Islands",regionAbbreviation:"VI",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Alberta, Canada",regionAbbreviation:"AB",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"British Columbia, Canada",regionAbbreviation:"BC",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Saskatchewan, Canada",regionAbbreviation:"SK",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Manitoba, Canada",regionAbbreviation:"MB",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Ontario, Canada",regionAbbreviation:"ON",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Quebec, Canada",regionAbbreviation:"PQ",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"New Brunswick, Canada",regionAbbreviation:"NB",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Nova Scotia, Canada",regionAbbreviation:"NS",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Prince Edward Is, Canada",regionAbbreviation:"PE",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Northwest Terr., Canada",regionAbbreviation:"NT",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"Yukon Territory, Canada",regionAbbreviation:"YT",stateCode:"null",tenantId:TenantId),
                        //new RegionUnit(description:"New Foundland, Canada",regionAbbreviation:"NF",stateCode:"null",tenantId:TenantId)
            };

          
        }

            
    }
}
