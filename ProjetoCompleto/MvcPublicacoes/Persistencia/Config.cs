using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using MvcPublicacoes.Models;
using System.Data.Entity;

namespace MvcPublicacoes.Persistencia
{
    //DropCreateDatabaseIfModelChanges<PubContext>
    public class Config : DbMigrationsConfiguration<PubContext> 
    {
        public Config()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PubContext context)
        {
            if(context.Situacoes.Count(s => s.Descricao.Equals("Submetida"))==0) {
                context.Situacoes.Add(new Situacao { Descricao = "Submetida" }); 
            }  
            if(context.Situacoes.Count(s => s.Descricao.Equals("Em avaliação"))==0) {
                context.Situacoes.Add(new Situacao { Descricao = "Em avaliação" }); 
            }  
            if(context.Situacoes.Count(s => s.Descricao.Equals("Aceita"))==0) {
                context.Situacoes.Add(new Situacao { Descricao = "Aceita" }); 
            }  
            if(context.Situacoes.Count(s => s.Descricao.Equals("Publicada"))==0) {
                context.Situacoes.Add(new Situacao { Descricao = "Publicada" }); 
            }  

            if (context.Instituicoes.Count(s => s.Nome.Equals("PUCRS")) == 0) {
                context.Instituicoes.Add(new Instituicao { Nome = "PUCRS", Cidade = "Porto Alegre" });
            }
            if (context.Instituicoes.Count(s => s.Nome.Equals("UFRGS")) == 0) {
                context.Instituicoes.Add(new Instituicao { Nome = "UFRGS", Cidade = "Porto Alegre" });
            }
            if (context.Instituicoes.Count(s => s.Nome.Equals("Unisinos")) == 0) {
                context.Instituicoes.Add(new Instituicao { Nome = "Unisinos", Cidade = "São Leopoldo" });
            }

            if (context.TiposPublicacoes.Count(s => s.Descricao.Equals("Periódico")) == 0) {
                context.TiposPublicacoes.Add(new TipoPublicacao { Descricao = "Periódico" });
            }
            if (context.TiposPublicacoes.Count(s => s.Descricao.Equals("Conferência")) == 0) {
                context.TiposPublicacoes.Add(new TipoPublicacao { Descricao = "Conferência" });
            }
            if (context.TiposPublicacoes.Count(s => s.Descricao.Equals("Monografia")) == 0) {
                context.TiposPublicacoes.Add(new TipoPublicacao { Descricao = "Monografia" });
            }

        }

    }
}