using System.Collections.Immutable;
using System.Security.Claims;

namespace BuildingBlock.Domain.Constants.Identity;

public static class Permissions
{
    public const string Prefix = "Permissions";

    public static readonly ImmutableList<Claim> PermissionsList = ImmutableList.Create(
        // USER
        new Claim(User.ViewPersonal, "Can view personal user profile"),
        new Claim(User.ViewAll, "Can view all user profiles"),
        new Claim(User.EditPersonal, "Can edit personal user profile"),
        new Claim(User.EditAll, "Can edit all user profiles"),
        new Claim(User.DeleteAll, "Can delete all user profiles"),
        new Claim(User.Create, "Can create user"),

        // ACCOUNT
        new Claim(Account.View, "Can view accounts"),
        new Claim(Account.Edit, "Can edit accounts"),
        new Claim(Account.Delete, "Can delete accounts"),
        new Claim(Account.Create, "Can create accounts"),

        // CONTACT
        new Claim(Contact.View, "Can view contacts"),
        new Claim(Contact.Edit, "Can edit contacts"),
        new Claim(Contact.Delete, "Can delete contacts"),
        new Claim(Contact.Create, "Can create contacts"),

        // LEAD
        new Claim(Lead.View, "Can view leads"),
        new Claim(Lead.Edit, "Can edit leads"),
        new Claim(Lead.Delete, "Can delete leads"),
        new Claim(Lead.Create, "Can create leads"),

        // DEAL
        new Claim(Deal.View, "Can view deals"),
        new Claim(Deal.Edit, "Can edit deals"),
        new Claim(Deal.Delete, "Can delete deals"),
        new Claim(Deal.Create, "Can create deals"),

        // PRODUCT
        new Claim(Product.View, "Can view products"),
        new Claim(Product.Edit, "Can edit products"),
        new Claim(Product.Delete, "Can delete products"),
        new Claim(Product.Create, "Can create products")
    );

    public static class Account
    {
        private const string Default = Prefix + ".Account";
        public const string View = Default + ".View";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Contact
    {
        private const string Default = Prefix + ".Contact";
        public const string View = Default + ".View";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Lead
    {
        private const string Default = Prefix + ".Lead";
        public const string View = Default + ".View";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Deal
    {
        private const string Default = Prefix + ".Deal";
        public const string View = Default + ".View";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Product
    {
        private const string Default = Prefix + ".Product";
        public const string View = Default + ".View";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class User
    {
        private const string Default = Prefix + ".User";
        public const string ViewPersonal = Default + ".ViewPersonal";
        public const string Create = Default + ".Create";
        public const string ViewAll = Default + ".ViewAll";
        public const string EditPersonal = Default + ".EditPersonal";
        public const string EditAll = Default + ".EditAll";
        public const string DeleteAll = Default + ".DeleteAll";
    }
}