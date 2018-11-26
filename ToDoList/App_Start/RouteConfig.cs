using System.Web.Mvc;
using System.Web.Routing;

namespace ToDoList
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Tasks",
                url: "",
                defaults: new { controller = "Tasks", action = "Tasks" }
            );

            routes.MapRoute(
                name: "Home",
                url: "home",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Signup",
                url: "signup",
                defaults: new { controller = "Home", action = "SignUp" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Home", action = "LogIn" }
            );

            routes.MapRoute(
                name: "Logout",
                url: "logout",
                defaults: new { controller = "Home", action = "LogOut" }
            );

            routes.MapRoute(
                name: "GetAccountCategories",
                url: "get-account-categories",
                defaults: new { controller = "Tasks", action = "GetAccountCategories" }
            );

            routes.MapRoute(
                name: "CreateCategory",
                url: "post-category",
                defaults: new { controller = "Tasks", action = "CreateCategory" }
            );

            routes.MapRoute(
                name: "CreateTask",
                url: "post-task",
                defaults: new { controller = "Tasks", action = "CreateTask" }
            );

            routes.MapRoute(
                name: "DeleteTask",
                url: "delete-task",
                defaults: new { controller = "Tasks", action = "DeleteTask" }
            );

            routes.MapRoute(
                name: "UpdateTask",
                url: "update-task",
                defaults: new { controller = "Tasks", action = "UpdateTask" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
