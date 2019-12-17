using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Mvc;
using Sitecore.ExperienceForms.Mvc.Controllers;
using Sitecore.ExperienceForms.Mvc.Extensions;
using Sitecore.ExperienceForms.Mvc.Filters;
using Sitecore.ExperienceForms.Mvc.Pipelines.GetModel;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExperienceForms.Tracking;
using Sitecore.Globalization;
using Sitecore.Mvc.Filters;
using Sitecore.Mvc.Pipelines;
using Sitecore.Web;
using System;
using System.Web.Mvc;

namespace Px.SitecoreForms.Demo.Controller
{
    public class CustomFormController : BaseFormBuilderController
    {
        public CustomFormController(
          IFormRenderingContext formRenderingContext,
          IFormSubmitHandler formSubmitHandler)
          : base(formRenderingContext, formSubmitHandler)
        {
        }

        [HttpGet]
        [SetFormMode(Editing = false)]
        public ActionResult Index()
        {
            this.FormRenderingContext.SessionId = ID.NewID.ToClientIdString();
            return this.RenderForm(this.FormRenderingContext.RenderingFormId);
        }

        [HttpGet]
        [SitecoreAuthorize(Roles = "sitecore\\Forms Editor")]
        [SetFormMode(Editing = true)]
        public ActionResult Load(string id)
        {
            this.FormRenderingContext.SessionId = ID.NewID.ToClientIdString();
            return this.RenderForm(id);
        }

        [HttpPost]
        [ValidateFormRequest]
        public ActionResult Index(FormDataModel data)
        {
            if (data == null)
                return this.Index();
            ProcessFormDataResult processFormDataResult = this.ProcessFormData(data);
            Assert.IsNotNull((object)processFormDataResult, "processFormDataResult");
            if (!processFormDataResult.Success)
                return this.RenderForm(data.FormItemId);
            return this.GetSubmitActionResult(processFormDataResult.FormSubmitContext);
        }

        protected ActionResult GetSubmitActionResult(FormSubmitContext submitContext)
        {
            Assert.ArgumentNotNull((object)submitContext, nameof(submitContext));
            this.FormRenderingContext.RegisterFormEvent(new FormEventData()
            {
                FormId = submitContext.FormId,
                EventId = FormPageEventIds.FormSubmitSuccessEventId
            });
            this.FormRenderingContext.ResetFormSessionData();
            if (!submitContext.RedirectOnSuccess || string.IsNullOrEmpty(submitContext.RedirectUrl) || WebUtil.IsOnPage(submitContext.RedirectUrl))
                return this.Index();
            if (Request.IsAjaxRequest())
                return (ActionResult)this.Redirect(submitContext.RedirectUrl);
            return (ActionResult)new JavaScriptResult()
            {
                Script = ("window.location='" + submitContext.RedirectUrl + "';")
            };
        }

        protected virtual ActionResult RenderForm(string id)
        {
            using (GetModelEventArgs args = new GetModelEventArgs())
            {
                args.ItemId = id;
                args.TemplateId = Sitecore.ExperienceForms.Mvc.Constants.TemplateIds.FormTemplateId;
                GetModelEventArgs getModelEventArgs = PipelineService.Get().RunPipeline<GetModelEventArgs, GetModelEventArgs>("forms.getModel", args, (Func<GetModelEventArgs, GetModelEventArgs>)(a => a));
                if (getModelEventArgs.ViewModel == null)
                    return (ActionResult)this.HttpNotFound(Translate.Text("Item not found"));
                return (ActionResult)this.PartialView(getModelEventArgs.RenderingSettings.ViewPath, (object)getModelEventArgs.ViewModel);
            }
        }
    }
}