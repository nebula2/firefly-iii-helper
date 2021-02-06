# What is this?
This is a little helper tool I use to make my taxes.
To manage my finances I use [firefly-iii](https://github.com/firefly-iii/firefly-iii).
I do my taxes with the help of [Taxman](https://www.lexware.de/taxman/) which is why some parts in the code are named like the application but that's about it.

I wanted to have a report which tells me the values I need to insert into taxman without having to calculate this all on my own.

# What you need to do apart from the usual
As I am too lazy to handle configuration properly you need to create an  `appsettings.json` file (Inside `FireConsoleFlyApp` and/or `JfFireflyWebApp.Server`). 

I added this file to the .gitignore so you simply need to create one yourself and make sure that it is copied to the destination folder with your build.
`appsettings.json` may look like this:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "AppSettings": {
    "FireflyConfig": {
      "BaseUrl": "<YOUR FIREFLY URL HERE>",
      "ApiToken": "<YOUR FIREFLY API TOKEN HERE>",
      "FireflyTagShowUrlPart": "tags/show/{0}/all"
    },
    "TaxmanMappings": [
      {
        "TaxmanValue": "Vorsteuer",
        "FireflyTags": [ "Vorsteuer" ]
      },
      {
        "TaxmanValue": "Einnahmen Kleingewerbe",
        "FireflyTags": [ "Apple IAP", "Google Ads Revenue", "Google IAP" ]
      },
      {
        "TaxmanValue": "Ausgaben Kleingewerbe",
        "FireflyTags": [ "Kosten Kleingewerbe", "Dienstleistungskosten Kleingewerbe" ]
      },
      {
        "TaxmanValue": "Aussergewoehnliche Belastungen",
        "FireflyTags": [ "Arztkosten" ]
      },
      {
        "TaxmanValue": "Kontofuehrungsgebuehren",
        "FireflyTags": [ "ENTGELTABSCHLUSS" ]
      },
      {
        "TaxmanValue": "Sonstige Vorsorgeaufwendungen",
        "FireflyTags": [ "Zahnzusatz", "KFZ Versicherung" ]
      },
      {
        "TaxmanValue": "Steuersoftware",
        "FireflyTags": [ "Steuersoftware" ]
      }
    ]
  }
}

```

# Solution overview

## FireFlyClient
Contains Rest Api Client generated using [NSwagStudio](https://github.com/RicoSuter/NSwag/wiki/NSwagStudio).

## nswagConfig
Contains NSwagStudio configuration. If you want to update it you need to generate the file and place it in FireFlyClient.

It is worth to mention that i Replaced `Newtonsoft.Json.Required.DisallowNull` with `Newtonsoft.Json.Required.Default`.

## FireConsoleFlyApp
Simple .NET Core Console App as a PoC for firefly connection and data fetching.

## JfFireflyWebApp.Shared
Shared project with stuff that console and blazor wasm use.

## JfFireflyWebApp.Server and JfFireflyWebApp.Client
Blazor wasm