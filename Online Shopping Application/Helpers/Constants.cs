namespace Online_Shopping_Application.Helpers
{
    public static class Constants
    {
        public static class APIEndpoints
        {
            public const string Login = "Authenticate/Login";
            public const string Register = "Authenticate/Register";
            
        }
       
        public static class HttpNamedClients
        {
            public const string API = "API";
        }
        public static class Roles
        {
            public const string Admin = "Admin";
            public const string User = " User";
        }
        public static class Category
        {
            public const string GetAllCategories = "categories/getallcategories";
            public const string GetCategoryById = "categories/id";
            public const string Create = "categories/create";
            public const string Edit = "categories/Edit";
            public const string Delete = "categories/Delete";

        }
        public static class Products
        {
            public const string Get = " Products/id";
            public const string Create = "Products/Create";
            

        }
    }
}
