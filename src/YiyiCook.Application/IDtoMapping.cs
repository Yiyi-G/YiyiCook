using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Application
{
    public interface IDtoMapping
    {
        void CreateMapping(IMapperConfigurationExpression mapperConfig);
    }
}
