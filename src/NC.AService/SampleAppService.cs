using NC.AService.Contracts;
using Volo.Abp.DependencyInjection;

namespace NC.AService
{
    public class SampleAppService : ISampleAppService, ITransientDependency
    {
        public Task<string> Hello()
        {
            return Task.FromResult("Hello Sample Service Module！");
        }

        public Task<string> Read()
        {
            return Task.FromResult("Read Sample Service Module！");
        }

        public Task<string> Write()
        {
            return Task.FromResult("Write Sample Service Module！");
        }
    }
}
