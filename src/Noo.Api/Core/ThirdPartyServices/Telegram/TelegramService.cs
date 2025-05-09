using Microsoft.Extensions.Diagnostics.HealthChecks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Noo.Api.Core.ThirdPartyServices.Telegram;

public abstract class TelegramService : IHealthCheckable
{
    protected readonly ITelegramBotClient botClient;

    protected TelegramService(string token)
    {
        botClient = new TelegramBotClient(token);
    }

    protected async Task SendMessageAsync(long chatId, string message)
    {
        await botClient.SendMessage(chatId, message);
    }

    public async Task<HealthCheckResult> HealthCheckAsync()
    {
        try
        {
            var info = await GetBotInfoAsync();

            return info != null
                ? HealthCheckResult.Healthy()
                : HealthCheckResult.Unhealthy("Bot info is null");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(ex.Message, ex);
        }
    }

    protected Task<User> GetBotInfoAsync()
    {
        return botClient.GetMe();
    }
}
