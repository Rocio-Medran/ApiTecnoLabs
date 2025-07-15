using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Servicios.Interfaces;
using Data.Interfaces;
using AppModels.Entities;
using Data.Data;
using AppModels.DTOs;

namespace Servicios.Servicios
{
    public class CarritoService: ICarritoService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IRepository<Carrito> _repository;
        private readonly ICarritoRepository _carritoRepository;

        public CarritoService(IMapper mapper, AppDbContext context, IRepository<Carrito> repository, ICarritoRepository carritoRepository)
        {
            _mapper = mapper;
            _context = context;
            _repository = repository;
            _carritoRepository = carritoRepository;
        }

        public async Task<IEnumerable<CarritoDTO>> GetCarritosAsync()
        {
            var carritos = await _carritoRepository.GetCarritosConProductosAsync();
            return _mapper.Map<IEnumerable<CarritoDTO>>(carritos);
        }

        public async Task<CarritoDTO?> GetCarritoByIdAsync(int id)
        {
            var carrito = await _carritoRepository.GetCarritoConProductosByIdAsync(id);
            return carrito == null ? null : _mapper.Map<CarritoDTO>(carrito);
        }

        public async Task<CarritoDTO> AddCarritoAsync(CreateCarritoDTO carritoDTO)
        {
            var carrito = _mapper.Map<Carrito>(carritoDTO);
            await _repository.AddAsync(carrito);
            await _context.SaveChangesAsync();
            return _mapper.Map<CarritoDTO>(carrito);
        }

        public async Task<bool> UpdateCarritoAsync(int id, UpCarritoDTO carritoDTO)
        {
            var carrito = await _repository.GetByIdAsync(id);
            if (carrito == null) return false;

            _mapper.Map(carritoDTO, carrito);
            _repository.Update(carrito);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCarritoAsync(int id)
        {
            var carrito = await _repository.GetByIdAsync(id);
            if (carrito == null) return false;

            _repository.Delete(carrito);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
