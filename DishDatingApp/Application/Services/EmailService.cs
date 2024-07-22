using DishDatingApp.Domain.Services;

namespace DishDatingApp.Application.Services;

public class EmailService :IEmailService
{
    public async Task SendEmailConfirmation(string email, string confirmationToken, CancellationToken cancellationToken)
    {
        //Some Implementation of sending email to specified email
        //Will spend to much time writing this up
        await Task.Delay(3000, cancellationToken);
    }
}