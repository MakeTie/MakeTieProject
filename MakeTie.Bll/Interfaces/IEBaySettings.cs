namespace MakeTie.Bll.Interfaces
{
    public interface IEBaySettings
    {
        string ApiTemplate { get; }

        string ApiToken { get; }

        string StoreName { get; }

        string StoreImageUrl { get; }
    }
}