
namespace NC.AService.Contracts
{
    public interface ISampleAppService
    {
        Task<string> Hello();
        Task<string> Read();
        Task<string> Write();
    }
}
