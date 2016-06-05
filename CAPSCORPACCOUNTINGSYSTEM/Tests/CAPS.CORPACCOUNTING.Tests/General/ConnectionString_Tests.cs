using System.Data.SqlClient;
using Shouldly;
using Xunit;

namespace CAPS.CORPACCOUNTING.Tests.General
{
    public class ConnectionString_Tests
    {
        [Fact]
        public void SqlConnectionStringBuilder_Test()
        {
            var csb = new SqlConnectionStringBuilder("Server=tcp:sumitdevdbserver.database.windows.net,1433; Database=sumitdevdbserver.database.windows.net; Initial Catalog=sumithost;Persist Security Info=False;User ID=sumitdev;Password=W!nter21;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            csb["Database"].ShouldBe("CORPACCOUNTING");
        }
    }
}
