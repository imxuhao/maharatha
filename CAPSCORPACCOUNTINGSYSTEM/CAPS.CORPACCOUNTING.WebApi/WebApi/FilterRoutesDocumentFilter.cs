using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swashbuckle.Swagger;
using System.Web.Http.Description;

namespace CAPS.CORPACCOUNTING.WebApi
{
    /// <summary>
    /// Thsi will hide the methods you don't want to expose outside for documentation.
    /// Note : This will only help in hiding the documentation
    /// </summary>
    public class FilterRoutesDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            ///Example of Hiding the Methods you want. 
            
            //swaggerDoc.paths["/api/services/app/coaUnit/CreateCoaUnit"].post = null;
        }
    }
}
