using AutoMapper;
using FormPackage6.Application.AutoMapperConfiguration.Base;
using FormPackage6.Core.DomainModel.Form;
using FormPackage6.Core.Services.FormServices;
using Lecoati.LeBlender.Extension.Models;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Application.AutoMapperConfiguration
{
    public class FormFieldMapper : IMapperBase
    {
        public void Configure(IContainer container)
        {
            IFieldService service = container.GetInstance<IFieldService>();
            Mapper.CreateMap<LeBlenderValue, Field>()
              .ForMember(dest => dest.Name, opt => { opt.MapFrom(source => service.GetLeBlenderValueFieldValue(source, "name")); })
              .ForMember(dest => dest.Mandatory, opt => { opt.MapFrom(source => service.GetValueAsBoolean(service.GetLeBlenderValueFieldValue(source, "mandatory"))); })
              .ForMember(dest => dest.Options, opt => { opt.MapFrom(source => service.GetArchetypePrevalues(source, "prevalues")); })
              .ForMember(dest => dest.Placeholder, opt => { opt.MapFrom(source => service.GetLeBlenderValueFieldValue(source, "placeholder")); })
              .ForMember(dest => dest.SelectFileText, opt => { opt.MapFrom(source => service.GetLeBlenderValueFieldValue(source, "selectFileText")); });
        }
    }
}
