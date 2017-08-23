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
    public class FormPickerMapper : IMapperBase
    {
        public void Configure(IContainer container)
        {
            IFormService formService = container.GetInstance<IFormService>();
            IFieldService fieldService = container.GetInstance<IFieldService>();

            Mapper.CreateMap<LeBlenderValue, FormPicker>()
                 .ForMember(dest => dest.Form, opt => { opt.MapFrom(source => formService.GetByNodeId(fieldService.GetLeBlenderValueFieldValue(source, "form") != null ? Convert.ToInt32(fieldService.GetLeBlenderValueFieldValue(source, "form")) : 0)); })
                 .ForMember(dest => dest.FormHeader, opt => { opt.MapFrom(source => fieldService.GetLeBlenderValueFieldValue(source, "formContent")); })
            ;
        }
    }
}
