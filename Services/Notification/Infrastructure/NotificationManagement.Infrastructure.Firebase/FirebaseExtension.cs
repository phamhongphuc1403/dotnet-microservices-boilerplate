using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.DependencyInjection;

namespace NotificationManagement.Infrastructure.Firebase;

public static class FirebaseExtension
{
    public static IServiceCollection AddFirebase(this IServiceCollection services)
    {
        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile(Path.Combine(Directory.GetCurrentDirectory(),
                "test-ee97e-firebase-adminsdk-i5kez-8818b268e5.json")),
        });

        return services;
    }
}