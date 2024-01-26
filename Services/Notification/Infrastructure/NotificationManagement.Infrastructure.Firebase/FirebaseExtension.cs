using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.DependencyInjection;

namespace NotificationManagement.Infrastructure.Firebase;

public static class FirebaseExtension
{
    public static IServiceCollection AddFirebase(this IServiceCollection services, string privateKeyName)
    {
        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile(Path.Combine(Directory.GetCurrentDirectory(), privateKeyName))
        });

        return services;
    }
}