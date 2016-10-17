using System.IO;
using DAL;
using RazorEngine;
using RazorEngine.Templating;

namespace Services.MailSending
{
    public class RazorView
    {
        private readonly string templateFolderPath;

        public RazorView(string templatesFolder, string layout)
        {
            templateFolderPath = Path.Combine(Config.BaseDirectory, templatesFolder);
            Engine.Razor.AddTemplate(layout, GetViewContent(layout));
        }

        public string RenderViewToString(string viewName, object model)
        {
            if (Engine.Razor.IsTemplateCached(viewName, null))
            {
                return Engine.Razor.Run(viewName, null, model);
            }

            string template = GetViewContent(viewName);

            return Engine.Razor.RunCompile(template, viewName, null, model);
        }

        private string GetViewContent(string viewName)
        {
            string fullPath = Path.Combine(templateFolderPath, $"{viewName}.cshtml");

            return File.ReadAllText(fullPath);
        }
    }
}
