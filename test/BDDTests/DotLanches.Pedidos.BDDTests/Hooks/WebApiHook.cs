using DotLanches.Pedidos.BDDTests.Setup;
using Reqnroll;

namespace DotLanches.Pedidos.BDDTests.Hooks;

[Binding]
public class WebApiHook
{
    [BeforeFeature]
    public static void BeforeFeature(FeatureContext featureContext) 
    {
        var webApi = new WebApiFactory();
        featureContext.FeatureContainer.RegisterInstanceAs(webApi, dispose: true);
    }
}