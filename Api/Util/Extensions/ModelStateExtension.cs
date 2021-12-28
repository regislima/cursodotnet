using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace api.Util.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(m => m.Value.Errors)
                .Select(m => m.ErrorMessage)
                .ToList();
        }

        public static bool IsNull(this object obj)
        {
            if (obj is null)
                return true;

            return false;
        }
    }
}