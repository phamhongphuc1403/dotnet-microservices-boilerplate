using System.Collections.Immutable;
using System.Security.Claims;

namespace BuildingBlock.Core.Domain.Shared.Constants.Identity;

public static class Permissions
{
    public const string Prefix = "Permissions";

    public static readonly ImmutableList<Claim> AdminPermissions = ImmutableList.Create(
        // USER
        new Claim(User.ViewAll, "Can view all user profiles"),
        new Claim(User.EditAll, "Can edit all user profiles"),
        new Claim(User.DeleteAll, "Can delete all user profiles"),
        new Claim(User.Create, "Can create user"),
        new Claim(User.ViewPersonal, "Can view personal user profile"),
        new Claim(User.EditPersonal, "Can edit personal user profile"),

        // ROLE
        new Claim(Role.View, "Can view roles"),
        new Claim(Role.Edit, "Can edit roles"),
        new Claim(Role.Delete, "Can delete roles"),
        new Claim(Role.Create, "Can create roles")
    );

    public static readonly List<Claim> UserPermissions = new()
    {
        new Claim(User.ViewPersonal, "Can view personal user profile"),
        new Claim(User.EditPersonal, "Can edit personal user profile"),

        // PRODUCT
        new Claim(Product.View, "Can view products"),
        new Claim(Product.Edit, "Can edit products"),
        new Claim(Product.Delete, "Can delete products"),
        new Claim(Product.Create, "Can create products")
    };

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

    public static class Role
    {
        private const string Default = Prefix + ".Role";
        public const string View = Default + ".View";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}