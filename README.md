# dotnet-appconfig-keyvault
.NET: App Config + Key Vault

## Steps

1. Import `appconfig-import.json` into Azure App Config with the label `MRJB-APPCONFIG-DEV`   

2. Setup Azure Identity Credentials in either User Secrets or appSettings.json. (AZ_APPCONFIG_CONNECTION_STRING, AZ_TENANT_ID, AAD_CLIENT_ID, AAD_CLIENT_SECRET)   

3. Hit API Endpoint to test
