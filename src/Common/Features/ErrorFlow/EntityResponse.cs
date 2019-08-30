using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ErrorFlow
{
    public class EntityResponse
    {
        public List<ValidationErrorItem> ValidationErrors { get; } =  new List<ValidationErrorItem>();

        public string Message { get; set; }
    }
}
