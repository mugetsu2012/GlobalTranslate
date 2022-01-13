using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GlobalTranslateProcesos.Helpers
{
    public class FileUploadOperation: IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.OperationId.ToLower() == "apiextractorpost" || operation.OperationId.ToLower() == "apiextractortraductorpost")
            {
                operation.Parameters.Clear();
                operation.Parameters.Add(new NonBodyParameter()
                {
                    Name = "archivo",
                    In = "formData",
                    Description = "Upload File",
                    Required = true,
                    Type = "file"
                });

                operation.Consumes.Add("multipart/form-data");
            }
        }
    }
}
