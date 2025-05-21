# Noo.Api 3.0

Backend API for Noo project.

## Requirements

- .NET 8.0
- MySQL 8.4 or higher

## Installation

1. Clone or pull the repository
2. Open the solution in Visual Studio or VS Code
3. Restore the NuGet packages
4. Create a new MySQL database
5. Update the connection string and all the other settings in `appsettings.json` file in each project
6. Run the migrations to create the database schema:
   ```bash
   dotnet ef database update
   ```
7. Run or Debug the application
8. Open the Swagger UI in your browser (usually at `http://localhost:5001/api-docs`)

## Configuration

The configuration is done in the `appsettings.json` file. An example (all possible fields) of the configuration file is provided below. You can copy it to your own `appsettings.json` file and modify it as needed:

```json
{
	"App": {
		"Mode": "Development",
		"Location": "https://localhost:5001",
		"AppVersion": "1.0.0",
		"AuthenticationType": "Bearer",
		"BaseUrl": "https://noo-school.ru",
		"AllowedOrigins": [
			"https://localhost:5001",
			"http://localhost:5000"
		]
	},
	"Http": {
		"Port": 5001,
		"MaximumRequestSize": 10485760,
		"CacheSizeLimit": 104857600,
		"MaximumCacheBodySize": 1048576,
		"MinRequestBodyDataRate": 100,
		"MinRequestBodyDataRateGracePeriod": 10,
		"MinResponseDataRate": 100,
		"MinResponseDataRateGracePeriod": 10
	},
	"Logs": {
		"Mode": "Console", // Console, Telegram
		"LogLevel": "Information",
		"TelegramLogToken": "...",
		"TelegramLogChatIds": [
			"1234567890",
			"1234567810",
			...
		]
	},
	"Db": {
		"User": "...",
		"Password": "...",
		"Host": "localhost",
		"Port": "3306",
		"Database": "...",
		"CommandTimeout": 60,
		"DefaultCharset": "utf8mb4",
		"DefaultCollation": "utf8mb4_general_ci"
	},
	"Cache": {
		"ConnectionString": "localhost:6379",
		"DefaultCacheTime": 60
	},
	"Jwt": {
		"Secret": "...",
		"Issuer": "https://localhost:5001",
		"Audience": "https://localhost:5001",
		"ExpireDays": 60
	},
	"Telegram": {
		"Token": "...",
		"WaitAfterBatch": 100,
		"WaitForBatch": 1000
	},
	"Swagger": {
		"Title": "API v1",
		"Description": "Noo.Api module",
		"Endpoint": "/swagger/v1/swagger.json",
		"RoutePrefix": "api-docs",
		"EnableUI": true,
		"Version": "v1"
	},
	"Email": {
		"SmtpHost": "localhost",
		"SmtpPort": 1025,
		"SmtpUsername": "...",
		"SmtpPassword": "...",
		"SmtpTimeout": 15000,
		"FromEmail": "...",
		"FromName": "..."
	},
	"AllowedHosts": "*"
}
```

## Optional

Those are optional requirements that are needed for some features to work. All of them can be disabled.

- Docker
- Redis [Not implemented yet]
- RabbitMQ (or other message broker, not decided yet) [Not implemented yet]
- SMTP Server [Not implemented yet]
- Telegram Bot for notifications [Not implemented yet]
- Telegram Bot for logs

## Current TODO

- Change `ProducesResponseTypeAttribute` to Custom attribute to automatically include NooException schema, as well as properly type the `ApiResponseDTO` and handle `Ulid` so that swagger doesn't think it's an object
- [50%] Add configurable startup to be able to start without optional components
- [50%] Migrate all the modules
- [50%] Add unit, integration tests
- [ ] Consider database tests like there are no orphaned records etc
- [50%] Add support for SMTP
- [50%] Add versioning for API
- [ ] Add support for planned tasks
- [ ] Test resource usage and performance
- [ ] Add support for caching (Redis)
- [ ] Consider SignalR for autosave (and solve the problem with multiple server instances)
- [ ] Add notification buses (Telegram, Browser, Apps)

## Ideas

- [ ] Use ETags for caching and concurrency control
- [ ] Use feature flags
