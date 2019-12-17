using Sitecore.Diagnostics;
using Sitecore.ExperienceForms.Mvc;
using Sitecore.ExperienceForms.Mvc.Pipelines.RenderForm;
using Sitecore.Mvc.Pipelines;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;


namespace Px.SitecoreForms.Demo.Pipelines.Forms
{
    public class BuildForm : MvcPipelineProcessor<RenderFormEventArgs>
    {
        private readonly IFormRenderingContext _formRenderingContext;

        public BuildForm(IFormRenderingContext formRenderingContext)
        {
            Assert.ArgumentNotNull((object)formRenderingContext, nameof(_formRenderingContext));
            this._formRenderingContext = formRenderingContext;
        }

        public override void Process(RenderFormEventArgs args)
        {
            Assert.ArgumentNotNull((object)args, nameof(args));
            args.Form = FormExtensions.BeginRouteForm(args.HtmlHelper, "CustomForm", args.QueryString, FormMethod.Post, (IDictionary<string, object>)args.Attributes);
            args.HtmlHelper.ViewContext.Writer.Write((object)args.HtmlHelper.Hidden("FormSessionId", (object)this._formRenderingContext.SessionId));
        }
    }
}
