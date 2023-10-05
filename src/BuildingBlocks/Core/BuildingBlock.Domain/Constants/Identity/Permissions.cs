namespace BuildingBlock.Domain.Constants;

public static class Permissions
{
    private const string GroupName = "Permissions";

    public static class Accounts
    {
        private const string Default = GroupName + ".Accounts";
        public const string Read = Default + ".Read";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Contacts
    {
        private const string Default = GroupName + ".Contacts";
        public const string Read = Default + ".Read";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Leads
    {
        private const string Default = GroupName + ".Leads";
        public const string Read = Default + ".Read";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Deals
    {
        private const string Default = GroupName + ".Deals";
        public const string Read = Default + ".Read";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Products
    {
        private const string Default = GroupName + ".Products";
        public const string Read = Default + ".Read";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class ProductDeals
    {
        private const string Default = GroupName + ".ProductDeals";
        public const string Read = Default + ".Read";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Users
    {
        private const string Default = GroupName + ".Users";
        public const string Read = Default + ".Read";
        public const string Create = Default + ".Create";
        public const string CreateAdmin = Default + ".CreateAdmin";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string EditRoles = Default + ".EditRoles";
    }
}