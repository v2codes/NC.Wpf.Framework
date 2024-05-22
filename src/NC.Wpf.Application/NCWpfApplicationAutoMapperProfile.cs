
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NC.Wpf.Application.Contracts;
using NC.Wpf.Domain;

namespace NC.Wpf.Application
{
    public class NCWpfApplicationAutoMapperProfile : Profile
    {
        public NCWpfApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<Sample, SampleDto>();
        }
    }
}
