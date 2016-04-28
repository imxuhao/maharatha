using System.Linq;
using CAPS.CORPACCOUNTING.EntityFramework;
using CAPS.CORPACCOUNTING.Masters;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Common;

namespace CAPS.CORPACCOUNTING.Migrations.Seed
{

    public class DefaultTypeOfCurrencyRateCreator
    {
        public static List<TypeOfCurrencyRateUnit> InitialTypeOfCurrencyRateList { get; private set; }

        private readonly CORPACCOUNTINGDbContext _context;

        static DefaultTypeOfCurrencyRateCreator()
        {
            InitialTypeOfCurrencyRateList = new List<TypeOfCurrencyRateUnit>
            {
                new TypeOfCurrencyRateUnit(description:"Daily Rate (Manual Spot Rate)",
                                          caption:"",
                                          notes:"Source: Oanda website (www.oanda.com)   A) Vendor payments & customer receipts (recording transactions flowing through foreign currency accounts in US dollars)",
                                          typeOfUploadFileId:null),
                 new TypeOfCurrencyRateUnit(description:"Daily Rate (Automated Spot Rate - Web service)",
                                         caption:"",
                                         notes:"Source: Oanda (web service)   A) Vendor payments & customer receipts (recording transactions flowing through foreign currency accounts in US dollars)",
                                         typeOfUploadFileId:22222),
                 new TypeOfCurrencyRateUnit(description:"Month End Rate",
                                         caption:"",
                                         notes:@"Source: Fremantle Media monthly exchange rates schedule
                                                        A) Monthly financial reporting (Balance Sheet currency conversion of foreign entities)
                                                      B) Intercompany reporting (Intercompany Asset & Liablility accounts)
                                                      C) Intercompany Balance Sheet consolidating accounts
                                                      D) Currency conversion of Radical USA Euro and GBP bank accounts into US dollars
                                                         ",
                                         typeOfUploadFileId:null),
                 new TypeOfCurrencyRateUnit(description:"Weighted Average Rate",
                                         caption:"",
                                         notes:@"Source: Fremantle Media monthly exchange rates schedule
                                                        A) Monthly financial reporting (Income Statement currency conversion of foreign entities)
                                                      B) Currency conversion of foreign projects for database entry in US dollars
                                                      C) Intercompany Income Statement consolidating accounts
                                                     ",
                                         typeOfUploadFileId:null),
                 new TypeOfCurrencyRateUnit(description:"Historical Rates",
                                             caption:"",
                                             notes:@"Source: Monthly Trial Balance conversions
                                                    A) Monthly financial reporting (Fixed Assets, Retained Earnings & Capital Investment accounts)",
                                             typeOfUploadFileId:null),
            };
        }

        public DefaultTypeOfCurrencyRateCreator(CORPACCOUNTINGDbContext context)
        {
            _context = context;
        }


        public void Create()
        {
            CreateTypeOfCurrencyRateList();
        }

        private void CreateTypeOfCurrencyRateList()
        {
            foreach (var currencyRateList in InitialTypeOfCurrencyRateList)
            {
                AddTypeOfCurrencyRateIfNotExists(currencyRateList);
            }
        }

        private void AddTypeOfCurrencyRateIfNotExists(TypeOfCurrencyRateUnit currencyRate)
        {
            if (_context.TypeOfCurrencyRateUnit.Any(l => l.Description == currencyRate.Description))
            {
                return;
            }

            _context.TypeOfCurrencyRateUnit.Add(currencyRate);

            _context.SaveChanges();
        }

    }

}
