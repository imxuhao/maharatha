using Abp.Application.Features;
using Abp.Localization;
using Abp.Runtime.Validation;
using Abp.UI.Inputs;

namespace CAPS.CORPACCOUNTING.Features
{
    /* This feature provider is just for an example.
     * You can freely delete all features and add your own.
     */
    public class AppFeatureProvider : FeatureProvider
    {
        public override void SetFeatures(IFeatureDefinitionContext context)
        {
            var chatFeature = context.Create(
                  AppFeatures.ChatFeature,
                  defaultValue: "false",
                  displayName: L("ChatFeature"),
                  inputType: new CheckboxInputType()
                  );

            chatFeature.CreateChildFeature(
                AppFeatures.TenantToTenantChatFeature,
                defaultValue: "false",
                displayName: L("TenantToTenantChatFeature"),
                inputType: new CheckboxInputType()
                );

            chatFeature.CreateChildFeature(
                AppFeatures.TenantToHostChatFeature,
                defaultValue: "false",
                displayName: L("TenantToHostChatFeature"),
                inputType: new CheckboxInputType()
                );

            var clientType = context.Create(
                 AppFeatures.TenantType,
                 defaultValue: "",
                 displayName: L("TenantType"),
                 inputType: null
                 );

            clientType.CreateChildFeature(
                AppFeatures.TenantTypeCommercial,
                defaultValue: "false",
                displayName: L("Commercial"),
                inputType: new CheckboxInputType()
                );

            clientType.CreateChildFeature(
                AppFeatures.TenantTypeFeatures,
                defaultValue: "false",
                displayName: L("Feature"),
                inputType: new CheckboxInputType()
                );
           
            clientType.CreateChildFeature(
                AppFeatures.TenantTypeTelevisionEpisodic,
                defaultValue: "false",
                displayName: L("Episodic"),
                inputType: new CheckboxInputType()
                );
           clientType.CreateChildFeature(
                AppFeatures.TenantTypeTelevisionNonEpisodic,
                defaultValue: "false",
                displayName: L("NonEpisodic"),
                inputType: new CheckboxInputType()
                );

            clientType.CreateChildFeature(
                AppFeatures.TenantTypeMusicTour,
                defaultValue: "false",
                displayName: L("MusicTour"),
                inputType: new CheckboxInputType()
                );

            clientType.CreateChildFeature(
                AppFeatures.TenantTypeFestivalLiveEvent,
                defaultValue: "false",
                displayName: L("FestivalLiveEvent"),
                inputType: new CheckboxInputType()
                );

            clientType.CreateChildFeature(
                AppFeatures.TenantTypeVenues,
                defaultValue: "false",
                displayName: L("Venue"),
                inputType: new CheckboxInputType()
                );

            // You can set maximum project count here
            context.Create(
                  AppFeatures.ProjectLimitation,
                  defaultValue: "0",
                  displayName: L("MaxProjectCount"),
                  inputType: new SingleLineStringInputType()
                  );



        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CORPACCOUNTINGConsts.LocalizationSourceName);
        }
    }
}
