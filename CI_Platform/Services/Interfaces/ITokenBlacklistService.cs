namespace Services.Interfaces;

public interface ITokenBlacklistService
{
    void AddTokenToBlacklist(string token, DateTime expiration);
    bool IsTokenBlacklisted(string token);
}