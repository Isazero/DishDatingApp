namespace DishDatingApp.Domain.Services;

public interface IEmailService
{
    Task SendEmailConfirmation(string email, string confirmationToken, CancellationToken cancellationToken);
}