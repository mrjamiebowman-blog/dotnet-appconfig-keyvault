using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace MrJB.Api.AppConfig;

public static class Builder
{
    public static TBuilder ConfigureAzureAppConfig<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        // azure app config
        builder.Configuration.AddAzureAppConfiguration(options =>
        {
            // app config
            var appConfigConnStr = builder.Configuration["AZ_APPCONFIG_CONNECTION_STRING"];
            var azLabelFilter = builder.Configuration["AZ_APPCONFIG_LABEL_FILTER"] ?? "MRJB-APPCONFIG-DEV";

            // client id & secret
            var tenantId = builder.Configuration["AZ_TENANT_ID"];
            var clientId = builder.Configuration["AAD_CLIENT_ID"];
            var secret = builder.Configuration["AAD_CLIENT_SECRET"];

            // validation
            var missing = new List<string>();

            if (string.IsNullOrWhiteSpace(appConfigConnStr))
                missing.Add("AZ_APPCONFIG_CONNECTION_STRING");

            if (string.IsNullOrWhiteSpace(tenantId))
                missing.Add("AZ_TENANT_ID");

            if (string.IsNullOrWhiteSpace(clientId))
                missing.Add("AAD_CLIENT_ID");

            if (string.IsNullOrWhiteSpace(secret))
                missing.Add("AAD_CLIENT_SECRET");

            if (missing.Count > 0) {
                throw new InvalidOperationException("Missing required Azure App Config configuration values: " + string.Join(", ", missing));
            }

            // label
            options.Select(KeyFilter.Any, azLabelFilter);

            options.Connect(appConfigConnStr).ConfigureKeyVault(kv => {
                kv.SetCredential(new ClientSecretCredential(tenantId, clientId, secret));
            });
        });

        return builder;
    }
}
