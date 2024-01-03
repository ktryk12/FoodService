namespace FoodService.BusinessLogic.ServiceInterface
{
    public interface IJwtTokenService
    {
        Task<string> GenerateJwtToken(string username);
    }
}
