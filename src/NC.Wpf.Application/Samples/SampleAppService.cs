using NC.Wpf.Application.Contracts;
using NC.Wpf.Domain;
using NC.Wpf.SqlSugar.Repositories;
using SqlSugar;

namespace NC.Wpf.Application
{
    public class SampleAppService : NCWpfAppService, ISampleAppService
    {
        private readonly IRepository<Sample> _sampleRepository;

        public SampleAppService(IRepository<Sample> sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        public async Task<SampleDto> CreateAsync()
        {
            var count = await _sampleRepository.CountAsync(p => 1 == 1);
            var sample = new Sample()
            {
                Id = LongIdGenerator.NextId(),
                Name = $"Sample_{DateTime.Now.ToString("yyyyMMddHHssmm")}",
                Type = Domain.Shared.EnumSampleType.Information,
                Sequence = count + 1,
            };
            await _sampleRepository.InsertAsync(sample);
            var result = ObjectMapper.Map<Sample, SampleDto>(sample);
            return result;
        }

        public async Task<SampleDto> GetFirstOrDefaultAsync()
        {
            try
            {
                var sample = await _sampleRepository.GetFirstAsync(p => 1 == 1);
                var result = ObjectMapper.Map<Sample, SampleDto>(sample);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public async Task<IEnumerable<SampleDto>> GetListAsync()
        {
            var sampleList = await _sampleRepository.GetListAsync();
            var result = ObjectMapper.Map<List<Sample>, List<SampleDto>>(sampleList);
            return result;
        }

        public async Task<SampleDto> DeleteAsync()
        {
            var lastRecord = await _sampleRepository.AsQueryable().OrderByDescending(p => p.Sequence).FirstAsync();
            
            // 物理删除
            //await _sampleRepository.DeleteAsync(lastRecord);

            // 逻辑删除
            await _sampleRepository.SoftDeletedAsync(lastRecord);
            var result = ObjectMapper.Map<Sample, SampleDto>(lastRecord);
            return result;
        }

    }
}
