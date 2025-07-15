using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppModels;
using AppModels.Entities;
using AppModels.DTOs;
using System.Runtime.ConstrainedExecution;

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

			CreateMap<Carrito, CarritoDTO>()
			.ForMember(dest => dest.Productos, opt => opt.MapFrom(src =>
				src.CarritoProductos.Select(cp => new CarritoProductoDTO
				{
					Id = cp.Id,
					ProductoId = cp.ProductoId,
					NombreProducto = cp.Producto != null ? cp.Producto.Nombre : "",
					Cantidad = cp.Cantidad,
					PrecioUnitario = cp.PrecioUnitario
				}).ToList()
			));
			CreateMap<CreateCarritoDTO, Carrito>()
			.ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(_ => DateTime.Now))
			.ForMember(dest => dest.Finalizado, opt => opt.MapFrom(_ => false));
			CreateMap<Carrito, CreateCarritoDTO>();
			CreateMap<Carrito, UpCarritoDTO>().ReverseMap();

			CreateMap<CarritoProducto, CarritoProductoDTO>()
				.ForMember(dest => dest.PrecioUnitario, opt => opt.MapFrom(src => src.Producto.Precio))
				.ForMember(dest => dest.NombreProducto, opt => opt.MapFrom(src => src.Producto.Nombre)).ReverseMap();
			CreateMap<CarritoProductoDTO, CarritoProducto>();
			CreateMap<CarritoProducto, CreateCarritoProductoDTO>().ReverseMap();
			CreateMap<CarritoProducto, UpCarritoProductoDTO>().ReverseMap();

            CreateMap<Compra, CompraDTO>()
				.ForMember(dest => dest.Productos, opt => opt.MapFrom(src => src.Carrito.CarritoProductos))
				.ReverseMap();

        }
    }
}
