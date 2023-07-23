# pole-emploi-scrapper

This application was written as an exercice using hexagonal architecture to gather job offers from the [pôle-emploi API](https://pole-emploi.io) in an incremental way.

## Prerequisites

- .NET 7 runtime

## How to run it

- Download the latest release

- Open the [appsettings.json](src/PoleEmploiScrapper.Api/appsettings.json) configuration file

- Create your account of pole-emploi api by following this guide <https://pole-emploi.io/data/documentation/gestion-compte-applications>

- Make sure your account is configured to use the <https://pole-emploi.io/data/api/offres-emploi> API

- Configure your `clientId` and your `clientSecret`

```json
  "PoleEmploiApiOptions": {
    "companyApiUrl": "https://entreprise.pole-emploi.fr",
    "jobOffersApiUrl": "https://api.pole-emploi.io/partenaire/offresdemploi/v2/offres/",
    "clientId": "",
    "clientSecret": ""
  }
```

- Launch `PoleEmploiScrapper.Api.exe`

- Go to <http://localhost:5000/swagger/index.html> to see the swagger or directly to <http://localhost:5000/Reporting> to see the statistics about gathered job offers

## Improvement ideas

- Add unit/integration tests
- Use Polly to add resilience and transient-fault-handling
- Improve logging using NLog or another library
- Improve data by avoiding duplicates
