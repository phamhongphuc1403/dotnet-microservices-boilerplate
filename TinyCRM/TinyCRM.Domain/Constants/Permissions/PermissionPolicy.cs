namespace TinyCRM.Domain.Constants.Permissions
{
    public class PermissionContent
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public PermissionContent(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }

    public static class Permissions
    {
        public static class UserPermissions
        {
            public static PermissionContent ViewPersonal { get; set; } = 
                new("User.View.Personal", "Can view personal user profile");
            public static PermissionContent ViewAll { get; set; } = 
                new("User.View.All", "Can view all user profiles");
            public static PermissionContent UpdatePersonal { get; set; } = 
                new("User.Update.Personal", "Can update personal user profile");
            public static PermissionContent UpdateAll { get; set; } = 
                new("User.Update.All", "Can update all user profiles");

            public static PermissionContent DeleteAll { get; set; } =
                new("User.Delete.All", "Can delete all user profiles");
            public static PermissionContent Create { get; set; } = 
                new("User.Create", "Can create user");
        }

        public static class AccountPermissions
        {
            public static PermissionContent View { get; set; } =
                new("Account.View", "Can view accounts");
            public static PermissionContent Update { get; set; } =
                new("Account.Update", "Can update accounts");
            public static PermissionContent Delete { get; set; } =
                new("Account.Delete", "Can delete accounts");
            public static PermissionContent Create { get; set; } =
                new("Account.Create", "Can create accounts");
        }

        public static class ContactPermissions
        {
            public static PermissionContent View { get; set; } =
                new("Contact.View", "Can view contacts");
            public static PermissionContent Update { get; set; } =
                new("Contact.Update", "Can update contacts");
            public static PermissionContent Delete { get; set; } =
                new("Contact.Delete", "Can delete contacts");
            public static PermissionContent Create { get; set; } =
                new("Contact.Create", "Can create contacts");
        }

        public static class LeadPermissions
        {
            public static PermissionContent View { get; set; } =
                new("Lead.View", "Can view leads");
            public static PermissionContent Update { get; set; } =
                new("Lead.Update", "Can update leads");
            public static PermissionContent Delete { get; set; } =
                new("Lead.Delete", "Can delete leads");
            public static PermissionContent Create { get; set; } =
                new("Lead.Create", "Can create leads");
        }
        public static class DealPermissions
        {
            public static PermissionContent View { get; set; } =
                new("Deal.View", "Can view deals");
            public static PermissionContent Update { get; set; } =
                new("Deal.Update", "Can update deals");
            public static PermissionContent Delete { get; set; } =
                new("Deal.Delete", "Can delete deals");
            public static PermissionContent Create { get; set; } =
                new("Deal.Create", "Can create deals");
        }
        public static class ProductPermissions
        {
            public static PermissionContent View { get; set; } =
                new("Product.View", "Can view products");
            public static PermissionContent Update { get; set; } =
                new("Product.Update", "Can update products");
            public static PermissionContent Delete { get; set; } =
                new("Product.Delete", "Can delete products");
            public static PermissionContent Create { get; set; } =
                new("Product.Create", "Can create products");
        }
    }
}
