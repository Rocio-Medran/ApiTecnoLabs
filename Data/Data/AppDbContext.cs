using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppModels.Entities;
using AppModels.DTOs;

namespace Data.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
		{
		}

		public DbSet<Producto> Productos { get; set; }
		public DbSet<Categoria> Categorias {  get; set; }
		public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<CarritoProducto> CarritoProductos { get; set; }
        public DbSet<Compra> Compras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Producto>()
				.HasOne(p => p.Categoria)
				.WithMany(c => c.Productos)
				.HasForeignKey(p => p.IdCategoria)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<CarritoProducto>()
				.HasOne(cp => cp.Carrito)
				.WithMany(c => c.CarritoProductos)
				.HasForeignKey(cp => cp.CarritoId);

            modelBuilder.Entity<CarritoProducto>()
                .HasOne(cp => cp.Producto)
				.WithMany()
                .HasForeignKey(cp => cp.ProductoId);

			modelBuilder.Entity<Compra>()
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId);

			modelBuilder.Entity<Compra>()
                .HasOne(c => c.Carrito)
                .WithMany()
                .HasForeignKey(c => c.CarritoId);
        }

		public void SeedAdmin()
		{
			if (!Usuarios.Any(u => u.Email == "admin@miapp.com"))
			{
				var admin = new Usuario
				{
					Nombre = "admin",
					Email = "admin@miapp.com",
					PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
					Rol = "Admin"
				};

				Usuarios.Add(admin);
				SaveChanges();
			}
		}

	}
}
