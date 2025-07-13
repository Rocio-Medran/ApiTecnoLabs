using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppModels;
using AppModels.Entities;
using AppModels.DTOs;

namespace AppModels.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Producto, CreateProductoDTO>().ReverseMap();
			CreateMap<Producto, UpProductoDTO>().ReverseMap();
			CreateMap<Producto, ProductoDTO>()
				.ForMember(dest => dest.NombreCategoria, opt => opt.MapFrom(src => src.Categoria.Nombre));
			CreateMap<ProductoDTO, Producto>();


			CreateMap<Categoria, CategoriaDTO>().ReverseMap();
			CreateMap<Categoria, CreateCategoriaDTO>().ReverseMap();

			CreateMap<Usuario, UsuarioRegistroDTO>().ReverseMap();
			CreateMap<Usuario, UsuarioLoginDTO>().ReverseMap();
			CreateMap<Usuario, UsuarioDTO>().ReverseMap();
		}
	}
}
