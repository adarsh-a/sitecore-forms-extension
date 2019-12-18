using System.Web.Mvc;

namespace Px.SitecoreForms.Demo.Areas.CustomForm
{
    public class CustomFormAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CustomForm";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
             "CustomForm",
             "customform",
             new { controller = "CustomForm", action = "Index", id = UrlParameter.Optional },
             new[] { "Px.SitecoreForms.Demo.Controller" }
         );

            context.MapRoute(
                "CustomForm_default",
                "CustomForm/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}