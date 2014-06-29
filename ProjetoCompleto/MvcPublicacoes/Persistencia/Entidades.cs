using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPublicacoes.Models; 
using System.Data.Entity;

namespace MvcPublicacoes.Persistencia
{
    public class PubContext : DbContext
    {
        public PubContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Situacao> Situacoes { get; set; }
        public DbSet<TipoPublicacao> TiposPublicacoes { get; set; }
        public DbSet<Publicacao> Publicacoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PubContext, Config>());
            //Database.SetInitializer<PubContext>(new MvcPublicacoes.Persistencia.Config);
        }
    }

}