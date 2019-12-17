using Sitecore.Data.Items;
using Sitecore.ExperienceForms.Mvc.Models.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px.SitecoreForms.Demo
{
    public class CustomSingleLineText : InputViewModel<string>
    {

        public string apiMappingField { get; set; }

        protected override void InitItemProperties(Item item)
        {
            // on load of the form
            base.InitItemProperties(item);
            apiMappingField = item.Fields["API Mapping Field"].Value;
           

        }

        protected override void UpdateItemFields(Item item)
        {
            // upon save
            base.UpdateItemFields(item);
            item.Fields["API Mapping Field"]?.SetValue(apiMappingField, true);           

        }
    }
}
