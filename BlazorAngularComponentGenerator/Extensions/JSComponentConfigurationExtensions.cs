using BlazorAngularComponentGenerator.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorAngularComponentGenerator.Extensions
{
    public static class JSComponentConfigurationExtensions
    {
        public static void RegisterForAngular<TComponent>(this IJSComponentConfiguration configuration) where TComponent : IComponent
        {
            var typeNameKebabCase = CasingUtilities.ToKebabCase(typeof(TComponent).Name);
            configuration.RegisterForJavaScript<TComponent>($"{typeNameKebabCase}-angular");
        }
    }
}
