using System.Collections.Immutable;

namespace TinyCRM.Domain.Constants
{
    public static class Permission
    {
        public const string Prefix = "Permission";

        public static class User
        {
            public const string ViewPersonal = $"{Prefix}.{nameof(User)}.View.Personal";
            public const string ViewAll = $"{Prefix}.{nameof(User)}.View.All";
            public const string UpdatePersonal = $"{Prefix}.{nameof(User)}.Update.Personal";
            public const string UpdateAll = $"{Prefix}.{nameof(User)}.Update.All";
            public const string DeleteAll = $"{Prefix}.{nameof(User)}.Delete.All";
            public const string Create = $"{Prefix}.{nameof(User)}.Create";
        }

        public static class Account
        {
            public const string View = $"{Prefix}.{nameof(Account)}.View";
            public const string Update = $"{Prefix}.{nameof(Account)}.Update";
            public const string Delete = $"{Prefix}.{nameof(Account)}.Delete";
            public const string Create = $"{Prefix}.{nameof(Account)}.Create";
        }

        public static class Contact
        {
            public const string View = $"{Prefix}.{nameof(Contact)}.View";
            public const string Update = $"{Prefix}.{nameof(Contact)}.Update";
            public const string Delete = $"{Prefix}.{nameof(Contact)}.Delete";
            public const string Create = $"{Prefix}.{nameof(Contact)}.Create";
        }

        public static class Lead
        {
            public const string View = $"{Prefix}.{nameof(Lead)}.View";
            public const string Update = $"{Prefix}.{nameof(Lead)}.Update";
            public const string Delete = $"{Prefix}.{nameof(Lead)}.Delete";
            public const string Create = $"{Prefix}.{nameof(Lead)}.Create";
        }

        public static class Deal
        {
            public const string View = $"{Prefix}.{nameof(Deal)}.View";
            public const string Update = $"{Prefix}.{nameof(Deal)}.Update";
            public const string Delete = $"{Prefix}.{nameof(Deal)}.Delete";
            public const string Create = $"{Prefix}.{nameof(Deal)}.Create";
        }

        public static class Product
        {
            public const string View = $"{Prefix}.{nameof(Product)}.View";
            public const string Update = $"{Prefix}.{nameof(Product)}.Update";
            public const string Delete = $"{Prefix}.{nameof(Product)}.Delete";
            public const string Create = $"{Prefix}.{nameof(Product)}.Create";
        }

        public static class Role
        {
            public const string View = $"{Prefix}.{nameof(Role)}.View";
            public const string Update = $"{Prefix}.{nameof(Role)}.Update";
        }
        
        public static readonly ImmutableList<PermissionContent> PermissionsList = ImmutableList.Create<PermissionContent>(
            // USER
            new PermissionContent(User.ViewPersonal, "Can view personal user profile"),
            new PermissionContent(User.ViewAll, "Can view all user profiles"),
            new PermissionContent(User.UpdatePersonal, "Can update personal user profile"),
            new PermissionContent(User.UpdateAll, "Can update all user profiles"),
            new PermissionContent(User.DeleteAll, "Can delete all user profiles"),
            new PermissionContent(User.Create, "Can create user"),

            // ACCOUNT
            new PermissionContent(Account.View, "Can view accounts"),
            new PermissionContent(Account.Update, "Can update accounts"),
            new PermissionContent(Account.Delete, "Can delete accounts"),
            new PermissionContent(Account.Create, "Can create accounts"),

            // CONTACT
            new PermissionContent(Contact.View, "Can view contacts"),
            new PermissionContent(Contact.Update, "Can update contacts"),
            new PermissionContent(Contact.Delete, "Can delete contacts"),
            new PermissionContent(Contact.Create, "Can create contacts"),

            // LEAD
            new PermissionContent(Lead.View, "Can view leads"),
            new PermissionContent(Lead.Update, "Can update leads"),
            new PermissionContent(Lead.Delete, "Can delete leads"),
            new PermissionContent(Lead.Create, "Can create leads"),

            // DEAL
            new PermissionContent(Deal.View, "Can view deals"),
            new PermissionContent(Deal.Update, "Can update deals"),
            new PermissionContent(Deal.Delete, "Can delete deals"),
            new PermissionContent(Deal.Create, "Can create deals"),

            // PRODUCT
            new PermissionContent(Product.View, "Can view products"),
            new PermissionContent(Product.Update, "Can update products"),
            new PermissionContent(Product.Delete, "Can delete products"),
            new PermissionContent(Product.Create, "Can create products")
        );
    }

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
}
