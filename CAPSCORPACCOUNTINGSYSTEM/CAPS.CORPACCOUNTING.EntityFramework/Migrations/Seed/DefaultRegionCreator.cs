using System.Linq;
using CAPS.CORPACCOUNTING.EntityFramework;
using CAPS.CORPACCOUNTING.Masters;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Migrations.Seed
{

    public class DefaultRegionCreator
    {
        public static List<RegionUnit> InitialRegionList { get; private set; }

        private readonly CORPACCOUNTINGDbContext _context;

        static DefaultRegionCreator()
        {
            InitialRegionList = new List<RegionUnit>
            {
                        new RegionUnit(description:"Alaska",regionAbbreviation:"AK",stateCode:"null"),
                        new RegionUnit(description:"Alabama",regionAbbreviation:"AL",stateCode:"1"),
                        new RegionUnit(description:"Arkansas",regionAbbreviation:"AR",stateCode:"5"),
                        new RegionUnit(description:"Arizona",regionAbbreviation:"AZ",stateCode:"4"),
                        new RegionUnit(description:"California",regionAbbreviation:"CA",stateCode:"6"),
                        new RegionUnit(description:"Colorado",regionAbbreviation:"CO",stateCode:"null"),
                        new RegionUnit(description:"Connecticut",regionAbbreviation:"CT",stateCode:"8"),
                        new RegionUnit(description:"District Of Columbia",regionAbbreviation:"DC",stateCode:"null"),
                        new RegionUnit(description:"Delware",regionAbbreviation:"DE",stateCode:"10"),
                        new RegionUnit(description:"Florida",regionAbbreviation:"FL",stateCode:"null"),
                        new RegionUnit(description:"Georgia",regionAbbreviation:"GA",stateCode:"13"),
                        new RegionUnit(description:"Hawaii",regionAbbreviation:"HI",stateCode:"null"),
                        new RegionUnit(description:"Iowa",regionAbbreviation:"IA",stateCode:"null"),
                        new RegionUnit(description:"Idaho",regionAbbreviation:"ID",stateCode:"16"),
                        new RegionUnit(description:"Illinois",regionAbbreviation:"IL",stateCode:"null"),
                        new RegionUnit(description:"Indiana",regionAbbreviation:"IN",stateCode:"18"),
                        new RegionUnit(description:"Kansas",regionAbbreviation:"KS",stateCode:"20"),
                        new RegionUnit(description:"Kentucky",regionAbbreviation:"KY",stateCode:"null"),
                        new RegionUnit(description:"Louisiana",regionAbbreviation:"LA",stateCode:"22"),
                        new RegionUnit(description:"Massachusetts",regionAbbreviation:"MA",stateCode:"25"),
                        new RegionUnit(description:"Maryland",regionAbbreviation:"MD",stateCode:"24"),
                        new RegionUnit(description:"Maine",regionAbbreviation:"ME",stateCode:"23"),
                        new RegionUnit(description:"Michigan",regionAbbreviation:"MI",stateCode:"26"),
                        new RegionUnit(description:"Minnesota",regionAbbreviation:"MN",stateCode:"null"),
                        new RegionUnit(description:"Missouri",regionAbbreviation:"MO",stateCode:"29"),
                        new RegionUnit(description:"Mississippi",regionAbbreviation:"MS",stateCode:"28"),
                        new RegionUnit(description:"Montana",regionAbbreviation:"MT",stateCode:"30"),
                        new RegionUnit(description:"North Carolina",regionAbbreviation:"NC",stateCode:"37"),
                        new RegionUnit(description:"North Dakota",regionAbbreviation:"ND",stateCode:"null"),
                        new RegionUnit(description:"Nebraska",regionAbbreviation:"NE",stateCode:"31"),
                        new RegionUnit(description:"New Hampshire",regionAbbreviation:"NH",stateCode:"null"),
                        new RegionUnit(description:"New Jersey",regionAbbreviation:"NJ",stateCode:"34"),
                        new RegionUnit(description:"New Mexico",regionAbbreviation:"NM",stateCode:"35"),
                        new RegionUnit(description:"Nevada",regionAbbreviation:"NV",stateCode:"null"),
                        new RegionUnit(description:"New York",regionAbbreviation:"NY",stateCode:"null"),
                        new RegionUnit(description:"Ohio",regionAbbreviation:"OH",stateCode:"39"),
                        new RegionUnit(description:"Oklahoma",regionAbbreviation:"OK",stateCode:"null"),
                        new RegionUnit(description:"Oregon",regionAbbreviation:"OR",stateCode:"null"),
                        new RegionUnit(description:"Pennsylvania",regionAbbreviation:"PA",stateCode:"null"),
                        new RegionUnit(description:"Rhode Island",regionAbbreviation:"RI",stateCode:"null"),
                        new RegionUnit(description:"South Carolina",regionAbbreviation:"SC",stateCode:"45"),
                        new RegionUnit(description:"South Dakota",regionAbbreviation:"SD",stateCode:"null"),
                        new RegionUnit(description:"Tennessee",regionAbbreviation:"TN",stateCode:"null"),
                        new RegionUnit(description:"Texas",regionAbbreviation:"TX",stateCode:"null"),
                        new RegionUnit(description:"Utah",regionAbbreviation:"UT",stateCode:"null"),
                        new RegionUnit(description:"Virginia",regionAbbreviation:"VA",stateCode:"null"),
                        new RegionUnit(description:"Vermont",regionAbbreviation:"VT",stateCode:"50"),
                        new RegionUnit(description:"Washington",regionAbbreviation:"WA",stateCode:"null"),
                        new RegionUnit(description:"Wisconsin",regionAbbreviation:"WI",stateCode:"55"),
                        new RegionUnit(description:"West Virginia",regionAbbreviation:"WV",stateCode:"null"),
                        new RegionUnit(description:"Wyoming",regionAbbreviation:"WY",stateCode:"null"),
                        new RegionUnit(description:"American Samoa",regionAbbreviation:"AS",stateCode:"null"),
                        new RegionUnit(description:"Guam",regionAbbreviation:"GU",stateCode:"null"),
                        new RegionUnit(description:"Puerto Rico",regionAbbreviation:"PR",stateCode:"null"),
                        new RegionUnit(description:"Virgin Islands",regionAbbreviation:"VI",stateCode:"null"),
                        new RegionUnit(description:"Alberta, Canada",regionAbbreviation:"AB",stateCode:"null"),
                        new RegionUnit(description:"British Columbia, Canada",regionAbbreviation:"BC",stateCode:"null"),
                        new RegionUnit(description:"Saskatchewan, Canada",regionAbbreviation:"SK",stateCode:"null"),
                        new RegionUnit(description:"Manitoba, Canada",regionAbbreviation:"MB",stateCode:"null"),
                        new RegionUnit(description:"Ontario, Canada",regionAbbreviation:"ON",stateCode:"null"),
                        new RegionUnit(description:"Quebec, Canada",regionAbbreviation:"PQ",stateCode:"null"),
                        new RegionUnit(description:"New Brunswick, Canada",regionAbbreviation:"NB",stateCode:"null"),
                        new RegionUnit(description:"Nova Scotia, Canada",regionAbbreviation:"NS",stateCode:"null"),
                        new RegionUnit(description:"Prince Edward Is, Canada",regionAbbreviation:"PE",stateCode:"null"),
                        new RegionUnit(description:"Northwest Terr., Canada",regionAbbreviation:"NT",stateCode:"null"),
                        new RegionUnit(description:"Yukon Territory, Canada",regionAbbreviation:"YT",stateCode:"null"),
                        new RegionUnit(description:"New Foundland, Canada",regionAbbreviation:"NF",stateCode:"null"),
                        new RegionUnit(description:"Alaska",regionAbbreviation:"AK",stateCode:"null"),
                        new RegionUnit(description:"Alabama",regionAbbreviation:"AL",stateCode:"1"),
                        new RegionUnit(description:"Arkansas",regionAbbreviation:"AR",stateCode:"5"),
                        new RegionUnit(description:"Arizona",regionAbbreviation:"AZ",stateCode:"4"),
                        new RegionUnit(description:"California",regionAbbreviation:"CA",stateCode:"6"),
                        new RegionUnit(description:"Colorado",regionAbbreviation:"CO",stateCode:"null"),
                        new RegionUnit(description:"Connecticut",regionAbbreviation:"CT",stateCode:"8"),
                        new RegionUnit(description:"District Of Columbia",regionAbbreviation:"DC",stateCode:"null"),
                        new RegionUnit(description:"Delware",regionAbbreviation:"DE",stateCode:"10"),
                        new RegionUnit(description:"Florida",regionAbbreviation:"FL",stateCode:"null"),
                        new RegionUnit(description:"Georgia",regionAbbreviation:"GA",stateCode:"13"),
                        new RegionUnit(description:"Hawaii",regionAbbreviation:"HI",stateCode:"null"),
                        new RegionUnit(description:"Iowa",regionAbbreviation:"IA",stateCode:"null"),
                        new RegionUnit(description:"Idaho",regionAbbreviation:"ID",stateCode:"16"),
                        new RegionUnit(description:"Illinois",regionAbbreviation:"IL",stateCode:"null"),
                        new RegionUnit(description:"Indiana",regionAbbreviation:"IN",stateCode:"18"),
                        new RegionUnit(description:"Kansas",regionAbbreviation:"KS",stateCode:"20"),
                        new RegionUnit(description:"Kentucky",regionAbbreviation:"KY",stateCode:"null"),
                        new RegionUnit(description:"Louisiana",regionAbbreviation:"LA",stateCode:"22"),
                        new RegionUnit(description:"Massachusetts",regionAbbreviation:"MA",stateCode:"25"),
                        new RegionUnit(description:"Maryland",regionAbbreviation:"MD",stateCode:"24"),
                        new RegionUnit(description:"Maine",regionAbbreviation:"ME",stateCode:"23"),
                        new RegionUnit(description:"Michigan",regionAbbreviation:"MI",stateCode:"26"),
                        new RegionUnit(description:"Minnesota",regionAbbreviation:"MN",stateCode:"null"),
                        new RegionUnit(description:"Missouri",regionAbbreviation:"MO",stateCode:"29"),
                        new RegionUnit(description:"Mississippi",regionAbbreviation:"MS",stateCode:"28"),
                        new RegionUnit(description:"Montana",regionAbbreviation:"MT",stateCode:"30"),
                        new RegionUnit(description:"North Carolina",regionAbbreviation:"NC",stateCode:"37"),
                        new RegionUnit(description:"North Dakota",regionAbbreviation:"ND",stateCode:"null"),
                        new RegionUnit(description:"Nebraska",regionAbbreviation:"NE",stateCode:"31"),
                        new RegionUnit(description:"New Hampshire",regionAbbreviation:"NH",stateCode:"null"),
                        new RegionUnit(description:"New Jersey",regionAbbreviation:"NJ",stateCode:"34"),
                        new RegionUnit(description:"New Mexico",regionAbbreviation:"NM",stateCode:"35"),
                        new RegionUnit(description:"Nevada",regionAbbreviation:"NV",stateCode:"null"),
                        new RegionUnit(description:"New York",regionAbbreviation:"NY",stateCode:"null"),
                        new RegionUnit(description:"Ohio",regionAbbreviation:"OH",stateCode:"39"),
                        new RegionUnit(description:"Oklahoma",regionAbbreviation:"OK",stateCode:"null"),
                        new RegionUnit(description:"Oregon",regionAbbreviation:"OR",stateCode:"null"),
                        new RegionUnit(description:"Pennsylvania",regionAbbreviation:"PA",stateCode:"null"),
                        new RegionUnit(description:"Rhode Island",regionAbbreviation:"RI",stateCode:"null"),
                        new RegionUnit(description:"South Carolina",regionAbbreviation:"SC",stateCode:"45"),
                        new RegionUnit(description:"South Dakota",regionAbbreviation:"SD",stateCode:"null"),
                        new RegionUnit(description:"Tennessee",regionAbbreviation:"TN",stateCode:"null"),
                        new RegionUnit(description:"Texas",regionAbbreviation:"TX",stateCode:"null"),
                        new RegionUnit(description:"Utah",regionAbbreviation:"UT",stateCode:"null"),
                        new RegionUnit(description:"Virginia",regionAbbreviation:"VA",stateCode:"null"),
                        new RegionUnit(description:"Vermont",regionAbbreviation:"VT",stateCode:"50"),
                        new RegionUnit(description:"Washington",regionAbbreviation:"WA",stateCode:"null"),
                        new RegionUnit(description:"Wisconsin",regionAbbreviation:"WI",stateCode:"55"),
                        new RegionUnit(description:"West Virginia",regionAbbreviation:"WV",stateCode:"null"),
                        new RegionUnit(description:"Wyoming",regionAbbreviation:"WY",stateCode:"null"),
                        new RegionUnit(description:"American Samoa",regionAbbreviation:"AS",stateCode:"null"),
                        new RegionUnit(description:"Guam",regionAbbreviation:"GU",stateCode:"null"),
                        new RegionUnit(description:"Puerto Rico",regionAbbreviation:"PR",stateCode:"null"),
                        new RegionUnit(description:"Virgin Islands",regionAbbreviation:"VI",stateCode:"null"),
                        new RegionUnit(description:"Alberta, Canada",regionAbbreviation:"AB",stateCode:"null"),
                        new RegionUnit(description:"British Columbia, Canada",regionAbbreviation:"BC",stateCode:"null"),
                        new RegionUnit(description:"Saskatchewan, Canada",regionAbbreviation:"SK",stateCode:"null"),
                        new RegionUnit(description:"Manitoba, Canada",regionAbbreviation:"MB",stateCode:"null"),
                        new RegionUnit(description:"Ontario, Canada",regionAbbreviation:"ON",stateCode:"null"),
                        new RegionUnit(description:"Quebec, Canada",regionAbbreviation:"PQ",stateCode:"null"),
                        new RegionUnit(description:"New Brunswick, Canada",regionAbbreviation:"NB",stateCode:"null"),
                        new RegionUnit(description:"Nova Scotia, Canada",regionAbbreviation:"NS",stateCode:"null"),
                        new RegionUnit(description:"Prince Edward Is, Canada",regionAbbreviation:"PE",stateCode:"null"),
                        new RegionUnit(description:"Northwest Terr., Canada",regionAbbreviation:"NT",stateCode:"null"),
                        new RegionUnit(description:"Yukon Territory, Canada",regionAbbreviation:"YT",stateCode:"null"),
                        new RegionUnit(description:"New Foundland, Canada",regionAbbreviation:"NF",stateCode:"null")
            };
        }

        public DefaultRegionCreator(CORPACCOUNTINGDbContext context)
        {
            _context = context;
        }


        public void Create()
        {
            CreateRegionList();
        }

        private void CreateRegionList()
        {
            foreach (var region in InitialRegionList)
            {
                AddRegionListIfNotExists(region);
            }
        }

        private void AddRegionListIfNotExists(RegionUnit region)
        {
            if (_context.RegionUnit.Any(l => l.Description == region.Description))
            {
                return;
            }

            _context.RegionUnit.Add(region);

            _context.SaveChanges();
        }

    }
}
