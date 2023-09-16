using System.Collections.Immutable;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Constants;

public static class Permission
{
    public const string Prefix = "Permission";

    public static readonly ImmutableList<PermissionEntity> PermissionsList = ImmutableList.Create(
        // USER
        new PermissionEntity(User.ViewPersonal, "Can view personal user profile"),
        new PermissionEntity(User.ViewAll, "Can view all user profiles"),
        new PermissionEntity(User.UpdatePersonal, "Can update personal user profile"),
        new PermissionEntity(User.UpdateAll, "Can update all user profiles"),
        new PermissionEntity(User.DeleteAll, "Can delete all user profiles"),
        new PermissionEntity(User.Create, "Can create user"),

        // ACCOUNT
        new PermissionEntity(Account.View, "Can view accounts"),
        new PermissionEntity(Account.Update, "Can update accounts"),
        new PermissionEntity(Account.Delete, "Can delete accounts"),
        new PermissionEntity(Account.Create, "Can create accounts"),

        // CONTACT
        new PermissionEntity(Contact.View, "Can view contacts"),
        new PermissionEntity(Contact.Update, "Can update contacts"),
        new PermissionEntity(Contact.Delete, "Can delete contacts"),
        new PermissionEntity(Contact.Create, "Can create contacts"),

        // LEAD
        new PermissionEntity(Lead.View, "Can view leads"),
        new PermissionEntity(Lead.Update, "Can update leads"),
        new PermissionEntity(Lead.Delete, "Can delete leads"),
        new PermissionEntity(Lead.Create, "Can create leads"),

        // DEAL
        new PermissionEntity(Deal.View, "Can view deals"),
        new PermissionEntity(Deal.Update, "Can update deals"),
        new PermissionEntity(Deal.Delete, "Can delete deals"),
        new PermissionEntity(Deal.Create, "Can create deals"),

        // PRODUCT
        new PermissionEntity(Product.View, "Can view products"),
        new PermissionEntity(Product.Update, "Can update products"),
        new PermissionEntity(Product.Delete, "Can delete products"),
        new PermissionEntity(Product.Create, "Can create products"),

        // ROLE
        new PermissionEntity(Role.View, "Can view roles"),
        new PermissionEntity(Role.Update, "Can update roles")
    );

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
}