namespace MasterCatCore.Dto
{
    public enum CompanyType
    {
        Distributor = 1,
        Outlet = 2,
        Operator = 4,
        Manufacturer = 8,
        TradingNetwork = 16,
        Store = 32,
        InetStore = 64
    }
    public enum EnumObjectsState
    {
        Draft = 0,
        Active = 1,
        Deleted = -100
    }
}
