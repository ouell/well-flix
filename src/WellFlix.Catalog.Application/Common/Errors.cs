using ErrorOr;

namespace WellFlix.Catalog.Application.Common;

public static partial class Errors
{
    public static class Category
    {
        public static Error InvalidCategory(string messages) => Error.Validation(code: "InvalidCategory",
                                                                                 description: messages);
        
        public static Error NotFound(string messages) => Error.NotFound(code: "CategoryNotFound",
                                                                        description: messages);
    }
}