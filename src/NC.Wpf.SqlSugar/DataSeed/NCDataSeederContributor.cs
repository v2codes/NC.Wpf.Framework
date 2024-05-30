using NC.Wpf.Domain;
using NC.Wpf.SqlSugar.Repositories;

namespace NC.Wpf.SqlSugar.DataSeed
{
    public class NCDataSeederContributor : IDataSeeder
    {
        private readonly IRepository<Sample> _sampleRepository;

        public NCDataSeederContributor(IRepository<Sample> sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        public async Task SeedAsync()
        {
            var exists = await _sampleRepository.CountAsync(p => p.Id >= 1 && p.Id <= 15);
            if (exists > 0)
            {
                return;
            }
            var sampleList = new List<Sample>();
            for (int i = 1; i < 16; i++)
            {
                sampleList.Add(new Sample
                {
                    Id = i,
                    Name = $"Sample_{i}",
                    Sequence = i,
                    Type = GetRandomEnumValue<Domain.Shared.EnumSampleType>(),
                });
            }

            await _sampleRepository.InsertRangeAsync(sampleList);
        }

        private T GetRandomEnumValue<T>()
        {
            var random = new Random();
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(random.Next(values.Length));
        }
    }
}
