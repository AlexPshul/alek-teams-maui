# Alek Teams .NET MAUI
This is a demo repo to create your own, teams-like chat app.
Current features:
* Group chat for everyone who connects to the app
* `X is typing` indicator
* **Feel free to open PRs to add more features**
  
  
## Steps to make this repo work
### 1. Create Azure Resources
1. Go to https://portal.azure.com
2. Create a [SignalR](https://docs.microsoft.com/en-us/azure/azure-signalr/signalr-overview) service.
3. Create an [Azure Function App](https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview).

### 2. Update cloud resources and code
1. [Get the connection string](https://docs.microsoft.com/en-us/azure/azure-signalr/concept-connection-string#how-to-get-my-connection-strings) of your newly created SignalR service.
2. [Add a new application setting](https://docs.microsoft.com/en-us/azure/azure-functions/functions-how-to-use-azure-function-app-settings?tabs=portal) to your Azure function with the connection string from SignalR. The key should be `AzureSignalRConnectionString`.
3. [Publish the backend code](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-your-first-function-visual-studio?tabs=in-process#publish-the-project-to-azure) to your created Azure function app.
4. Get your function app base URL (without the `/api` part and without any other suffixes). It should look something like this: `https://my-very-cool-app.azurewebsites.net`.
5. Open the frontend (.NET MAUI) solution and go to the `MessagesService.cs` file. Update the base address of the `HTtpClient` in the Connect method to your Azure function app base URL.

### 3. Run the code
1. Open the .NET MAUI solution
2. Choose a platform to run on
3. Run without a debug
4. Choose *another* platform (or a different virtual/physical device) to run on
5. Run with or without debug
6. Type between two deployments
