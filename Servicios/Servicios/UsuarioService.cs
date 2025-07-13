using AppModels.DTOs;
using AppModels.Entities;
using AutoMapper;
using Data.Data;
using Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
	public class UsuarioService: IUsuarioService
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		private readonly IRepository<Usuario> _repository;
		private readonly IConfiguration _config;

		public UsuarioService(IMapper mapper, IRepository<Usuario> repository, AppDbContext context, IConfiguration config)
		{
			_context = context;
			_mapper = mapper;
			_repository = repository;
			_config = config;
		}

		public async Task<bool> RegistrarAsync(UsuarioRegistroDTO usuarioDto)
		{
			var existente = await _repository.FindAsync(u => u.Email == usuarioDto.Email);
			if (existente.Any()) return false;

			var usuario = _mapper.Map<Usuario>(usuarioDto);
			usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Password);

			await _repository.AddAsync(usuario);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<string?> LoginAsync(UsuarioLoginDTO usuarioDto)
		{
			var usuarios = await _repository.FindAsync(u => u.Email == usuarioDto.Email);
			var usuario = usuarios.FirstOrDefault();

			if (usuario == null || !BCrypt.Net.BCrypt.Verify(usuarioDto.Password, usuario.PasswordHash))
				return null;

			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
				new Claim(ClaimTypes.Email, usuario.Email),
				new Claim(ClaimTypes.Role, usuario.Rol)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Audience"],
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public async Task<UsuarioDTO?> GetByIdAsync(int id)
		{
			var usuario = await _repository.GetByIdAsync(id);
			return usuario == null ? null : _mapper.Map<UsuarioDTO>(usuario);
		}
	}
}
