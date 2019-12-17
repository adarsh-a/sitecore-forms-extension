using Sitecore.Data.Items;
using Sitecore.ExperienceForms.Mvc.Models.Fields;
using System;

namespace Px.SitecoreForms.Demo
{
    [Serializable]
    public class CustomButton : ButtonViewModel
    {
        public string containerClasstoDelete { get; set; }

        public string classSelectorToAdd { get; set; }

        public string hideContainer { get; set; }

        public string showContainer { get; set; }



        protected override void InitItemProperties(Item item)
        {
            // on load of the form
            base.InitItemProperties(item);

            containerClasstoDelete = item.Fields["Class Selector for container deletion"].Value;
            classSelectorToAdd = item.Fields["Class Selector to duplicate"].Value;
            hideContainer = item.Fields["Hide Container"].Value;
            showContainer = item.Fields["Show Container"].Value;
        }


        protected override void UpdateItemFields(Item item)
        {
            // upon save
            base.UpdateItemFields(item);

            item.Fields["Class Selector for container deletion"]?.SetValue(containerClasstoDelete, true);
            item.Fields["Class Selector to duplicate"]?.SetValue(classSelectorToAdd, true);
            item.Fields["Hide Container"].SetValue(hideContainer, true);
            item.Fields["Show Container"].SetValue(showContainer, true); ;

        }
    }
}
