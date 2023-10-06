namespace BuildingBlock.Domain.Constants.Identity;

public static class Permissions
{
    public const string Prefix = "Permissions";

    public static class Accounts
    {
        private const string Default = Prefix + ".Accounts";
        public const string Read = Default + ".Read";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Contacts
    {
        private const string Default = Prefix + ".Contacts";
        public const string Read = Default + ".Read";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Leads
    {
        private const string Default = Prefix + ".Leads";
        public const string Read = Default + ".Read";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Deals
    {
        private const string Default = Prefix + ".Deals";
        public const string Read = Default + ".Read";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Products
    {
        private const string Default = Prefix + ".Products";
        public const string Read = Default + ".Read";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class ProductDeals
    {
        private const string Default = Prefix + ".ProductDeals";
        public const string Read = Default + ".Read";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Users
    {
        private const string Default = Prefix + ".Users";
        public const string Read = Default + ".Read";
        public const string Create = Default + ".Create";
        public const string CreateAdmin = Default + ".CreateAdmin";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string EditRoles = Default + ".EditRoles";
    }
}