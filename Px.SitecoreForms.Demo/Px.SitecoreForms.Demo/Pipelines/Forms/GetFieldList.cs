using Microsoft.Extensions.DependencyInjection;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using Sitecore.ExperienceForms.Client.Pipelines.GetFormFields;
using Sitecore.ExperienceForms.Diagnostics;
using Sitecore.ExperienceForms.Extensions;
using Sitecore.ExperienceForms.Mvc.Constants;
using Sitecore.Globalization;
using Sitecore.Mvc.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Px.SitecoreForms.Demo.Pipelines.Forms
{
    public class GetFieldList : MvcPipelineProcessor<GetFieldListArgs>
    {
        protected virtual ILogger Logger
        {
            get
            {
                return ServiceLocator.ServiceProvider.GetService<ILogger>();
            }
        }

        public override void Process(GetFieldListArgs args)
        {
            Assert.ArgumentNotNull((object)args, nameof(args));
            Assert.ArgumentNotNull((object)args.FormId, "FormId");
            Database database = Factory.GetDatabase("master");
            Assert.ArgumentNotNull((object)database, "master");
            Item obj = database.GetItem(args.FormId, args.FormBuilderContext.Language);
            if (obj != null)
            {
                args.FormFields = ((IEnumerable<Item>)obj.Axes.GetDescendants()).Where<Item>((Func<Item, bool>)(item => item.Template.IsBasedOnTemplate(TemplateIds.FieldTemplateId))).ToList<Item>();
            }
            else
            {
                this.Logger.Info(string.Format("Form Id: {0} not found.", (object)args.FormId));
                args.AddMessage(Translate.Text("Item not found."));
                args.AbortPipeline();
            }
        }
    }
}
