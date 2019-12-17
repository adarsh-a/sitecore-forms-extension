using Sitecore.Data.Items;
using Sitecore.ExperienceForms.Mvc.Models.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px.SitecoreForms.Demo
{
    [Serializable]
    public class ComplexObject : InputViewModel<string>
    {
        public string numberOfFields { get; set; }

        public string fieldPlaceholders { get; set; }


        protected override void InitItemProperties(Item item)
        {
            // on load of the form
            base.InitItemProperties(item);

            numberOfFields = item.Fields["Number of Text Fields"].Value;
            fieldPlaceholders = item.Fields["Field Placeholders"].Value;          

        }

        protected override void UpdateItemFields(Item item)
        {
            // upon save
            base.UpdateItemFields(item);

            item.Fields["Number of Text Fields"]?.SetValue(numberOfFields, true);
            item.Fields["Field Placeholders"]?.SetValue(fieldPlaceholders, true);           

        }
    }
}
