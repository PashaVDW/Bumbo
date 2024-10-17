namespace DataLayer.Models.DTOs
{
    public sealed record NormOverviewDTO(
        int NormId,
        int Year,
        int Week,
        int ColiInSeconds,
        int ShelveInSeconds,
        int CashierInSeconds,
        int FreshInSeconds,
        int FrontInSeconds);
}
