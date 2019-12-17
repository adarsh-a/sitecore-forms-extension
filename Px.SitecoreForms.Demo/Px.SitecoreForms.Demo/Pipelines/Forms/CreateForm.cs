
using Newtonsoft.Json;
using Sitecore.Diagnostics;
using Sitecore.ExperienceForms.Client.Models.Builder;
using Sitecore.ExperienceForms.Client.Pipelines.SaveForm;
using Sitecore.ExperienceForms.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Mvc.Models;
using Sitecore.Mvc.Pipelines;
using Sitecore.Reflection;
using System;
using System.Collections.ObjectModel;

namespace Px.SitecoreForms.Demo.Pipelines.Forms
{
    public class CreateForm : MvcPipelineProcessor<SaveFormEventArgs>
    {
        private readonly ILogger _logger;

        public CreateForm(ILogger logger)
        {
            Assert.ArgumentNotNull((object)logger, nameof(logger));
            this._logger = logger;
        }

        public override void Process(SaveFormEventArgs args)
        {
            Assert.ArgumentNotNull((object)args, nameof(args));
            if (args.ModelWrappers == null)
                return;
            args.ViewModelWrappers = new Collection<ViewModelWrapper>();
            foreach (ModelWrapper<string> modelWrapper in args.ModelWrappers)
            {
                Type type = string.IsNullOrEmpty(modelWrapper.RenderingSettings.ModelType) ? typeof(FormViewModel) : ReflectionUtil.GetTypeInfo(modelWrapper.RenderingSettings.ModelType);
                if (type != (Type)null)
                {
                    IViewModel viewModel = (IViewModel)null;
                    try
                    {
                        viewModel = JsonConvert.DeserializeObject(modelWrapper.Model, type) as IViewModel;
                    }
                    catch (Exception ex)
                    {
                        this._logger.LogError(ex.Message, ex, (object)this);
                        args.AddMessage(ex.Message);
                    }
                    if (viewModel != null)
                    {
                        ViewModelWrapper viewModelWrapper1 = new ViewModelWrapper();
                        viewModelWrapper1.Model = viewModel;
                        viewModelWrapper1.RenderingSettings = modelWrapper.RenderingSettings;
                        viewModelWrapper1.ParentId = modelWrapper.ParentId;
                        viewModelWrapper1.SortOrder = modelWrapper.SortOrder;
                        ViewModelWrapper viewModelWrapper2 = viewModelWrapper1;
                        args.ViewModelWrappers.Add(viewModelWrapper2);
                    }
                }
            }
        }
    }
}
