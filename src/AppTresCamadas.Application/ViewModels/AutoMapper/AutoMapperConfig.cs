using AppTresCamadas.Business.Models;
using AutoMapper;

namespace AppTresCamadas.Application.ViewModels.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<FornecedorModel, FornecedorViewModel>().ReverseMap();
            CreateMap<EnderecoModel, EnderecoViewModel>().ReverseMap();
            CreateMap<ProdutoModel, ProdutoViewModel>().ReverseMap();
        }
    }
}
