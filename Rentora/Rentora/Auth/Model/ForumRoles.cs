namespace Rentora.Auth.Model;

public class ForumRoles
{
    public const string Admin = nameof(Admin);
    public const string ForumUser = nameof(ForumUser);
    public const string Rentee  = nameof(Rentee);
    public const string Renter  = nameof(Renter);

    public static readonly IReadOnlyCollection<string> All = new[] 
    {
        Admin, 
        ForumUser,
        Rentee,
        Renter,
        
    };
}