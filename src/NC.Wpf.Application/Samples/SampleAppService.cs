using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;
using NC.Wpf.Application.Contracts;
using NC.Wpf.Domain;

namespace NC.Wpf.Application
{
    public class SampleAppService : NCWpfAppService, ISampleAppService
    {
        private readonly IRepository<Sample> _sampleRepository;
        public SampleAppService(IRepository<Sample> sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        //private readonly INCDbContext _dbContext;
        //public SampleAppService(INCDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        public async Task<SampleDto> GetAsync()
        {
            try
            {
                var sample = await _sampleRepository.FirstOrDefaultAsync();
                // var sample = await _dbContext.Samples.FirstOrDefaultAsync();
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
            var sampleList = await _sampleRepository.ToListAsync();
            //var sampleList = await _dbContext.Samples.ToListAsync();
            var result = ObjectMapper.Map<List<Sample>, List<SampleDto>>(sampleList);
            return result;
        }

        public Task<SampleDto> GetByIdAsync()
        {
            throw new NotImplementedException();
        }

    }
}
