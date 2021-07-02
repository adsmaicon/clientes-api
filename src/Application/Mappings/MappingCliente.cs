
using AutoMapper;
using Clientes.Application.Models;
using Clientes.Domain.Entities;

namespace Clientes.Application.Mappings
{
    public class MappingsCliente : Profile
    {
        public MappingsCliente()
        {
            CreateMap<ClienteRequest, Cliente>()
                .ForMember(d => d.CPF, map =>
                    map.MapFrom(s => s.CPF.Replace(".", "").Replace("-", ""))
            );
            
            CreateMap<Cliente, ClienteResponse>()
                .ForMember(d => d.CPF, map =>
                    map.MapFrom(s => s.CPF.Replace(".", "").Replace("-", ""))
            );
        }

    }
}