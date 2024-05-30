using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NC.Wpf.Application.Contracts
{
    /// <summary>
    /// Sample相关服务接口定义
    /// </summary>
    public interface ISampleAppService
    {
        Task<SampleDto> CreateAsync();

        Task<SampleDto> GetFirstOrDefaultAsync();

        Task<IEnumerable<SampleDto>> GetListAsync();

        Task<SampleDto> DeleteAsync();
    }
}
