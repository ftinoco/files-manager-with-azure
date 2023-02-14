# FilesManagerWithAzure
This is a solution developed with ASP.NET MVC/.NET6 integrated with Azure AD B2C, Azure Blob Storage, Azure Cosmos DB and is deployed in Azure App Service. I tried to structure the projects based on responsability:
- Core: The logic and database connection are here. 
- API: Web application that show the functionality to user.

## Getting started

- Cloning code
```
git clone https://github.com/ftinoco/blog-engine-net.git
```

- Build the whole solution in order to install all packages.
- Run the **FilesManagerWithAzure.APP** project and it should display a screen like the following:
![](./docs/images/azure-ad-b2c-login.png)
- A user must be created to log in

## Availability

The application is available through [FilesManagerWithAzureApp](https://file-manager-app.azurewebsites.net/).