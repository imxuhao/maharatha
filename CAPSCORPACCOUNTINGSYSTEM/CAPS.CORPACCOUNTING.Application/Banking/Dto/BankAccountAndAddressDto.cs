using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters;
using System;


namespace CAPS.CORPACCOUNTING.Banking.Dto
{
   public class BankAccountAndAddressDto : IOutputDto
    {
            /// <summary>Gets or sets the BankAccount.</summary>
            public BankAccountUnit BankAccount { get; set; }

            /// <summary>Gets or sets the AddressUnit.</summary>
            public AddressUnit Address { get; set; }

            /// <summary>Gets or sets the TypeOfBankAccount.</summary>
            public string TypeOfBankAccount { get; set; }

            /// <summary>Gets or sets the Account.</summary>
            public string LedgerAccount { get; set; }

            /// <summary>Gets or sets the Job.</summary>
            public string Job { get; set; }

            /// <summary>Gets or sets the TypeofCheckStock.</summary>
            public string TypeofCheckStock { get; set; }

            /// <summary>Gets or sets the ClearingAccount.</summary>
            public string ClearingAccount { get; set; }

            /// <summary>Gets or sets the ClearingJob.</summary>
            public String ClearingJob { get; set; }

            /// <summary>Gets or sets the TypeOfUploadFile.</summary>
            public string TypeOfUploadFile { get; set; }

            /// <summary>Gets or sets the Vendor.</summary>
            public string Vendor { get; set; }

            /// <summary>Gets or sets the ControllingBankAccounts.</summary>
            public string ControllingBankAccounts { get; set; }

            /// <summary>Gets or sets the TypeOfInactiveStatus.</summary>
            public string TypeOfInactiveStatus { get; set; }

            /// <summary>Gets or sets the PositivePayTypeOfUploadFile.</summary>
            public string PositivePayTypeOfUploadFile { get; set; }

            /// <summary>Gets or sets the PettyCashAccount.</summary>
            public string PettyCashAccount { get; set; }

            /// <summary>Gets or sets the Batch.</summary>
            public string Batch { get; set; }
          

    }
}
