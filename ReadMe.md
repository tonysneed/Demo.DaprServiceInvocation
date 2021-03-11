# Dapr Service Invocation Demo

Demonstrates how to invoke a Web API using Dapr.

### Prerequisites
- Install [Docker Desktop](https://www.docker.com/products/docker-desktop) running Linux containers.
- Install [Dapr CLI](https://docs.dapr.io/getting-started/install-dapr-cli/).
- Run `dapr init`, `dapr --version`, `docker ps`
- [Dapr Visual Studio Extension](https://github.com/microsoft/vscode-dapr) (for debugging).

### Daprized Web API

- Follow the [Dapr ASP.NET Core Controller example](https://github.com/dapr/dotnet-sdk/blob/master/examples/AspNetCore/ControllerSample/README.md).

1. Create an ASPNET Web API project.
   - Disable HTTPS, enable Open API.
   - Run the Web API: `dotnet run`, browse to http://localhost:5000/.
   - Execute `GET` for `Weatherforecast`.

2. Run the Web API via Dapr from the project root.
   - Refresh the Swagger page and execute `GET` for `Weatherforecast`.

```
dapr run --app-id my-web-api --app-port 5000 -- dotnet run
```

### Dapr Console Client

- Follow the [Dapr .NETService Invocation example](https://github.com/dapr/dotnet-sdk/blob/master/examples/Client/ServiceInvocation/README.md).

1. Create a Console app.
   - Change `Main` to `async` and return `Task`.
2. Add `Dapr.Client` package.
3. Create an `HttpClient` using `DaprClient`.
   - Specify `my-web-api` for the app id.

> Note: A `DaprClient` may be used to submit POST requests.

```csharp
var client = DaprClient.CreateInvokeHttpClient(appId: "my-web-api");
```

4. Invoke the `Get` method of the Web API.
   - Specify `weatherforecast` for the request Uri.
   - Read `Content` as Json.

```csharp
var response = await client.GetAsync("weatherforecast");
response.EnsureSuccessStatusCode();
var result = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();

foreach (var item in result)
{
    Console.WriteLine($"Date: {item.Date} TempC: {item.TemperatureC} TempF: {item.TemperatureF} Summary: {item.Summary}");
}
```

5. From the prohect root run the client from Dapr.

```
dapr run --app-id my-client -- dotnet run
```

### Debugging the Web API

1. Open the `DaprWebApi` project folder using VS Code.
2. Generate build and debug assets.
3. Make sure you can run and debug the service (press F5).
4. Press Ctrl+P, enter `Dapr` and select `Scaffold Dapr Tasks`.
   - Select .NET Core Launch (web)
   - Enter `my-web-api` for the app id
   - Accept port 5000
5. On the Debug tab of VS Code select the option "with Dapr".
6. Press F5 to launch the app with the debugger using Dapr.
   - Set a breakpoint and browse to http://localhost:5000/weatherforecast.
7. Invoke the Web API via the Dapr tab in VS Code.
   - Right click the `my-web-api` Dapr app
   - Select Invoke GET
   - Enter `weatherforecast`

> Note: You can also use the Dapr tab from another running instance of VS Code.

### Debugging the Console Client

1. Open the `DaprConsoleClient` project folder using VS Code.
2. Generate build and debug assets.
3. Make sure you can run and debug the service (press F5).
4. Press Ctrl+P, enter `Dapr` and select `Scaffold Dapr Tasks`.
   - Select .NET Core Launch (web)
   - Enter `my-client` for the app id
   - Accept port 5000
   - Then open tasks.json and delete `"appPort": 5000`
5. On the Debug tab of VS Code select the option "with Dapr".
6. Set a breakpoint and press F5 to launch the app with the debugger using Dapr.
