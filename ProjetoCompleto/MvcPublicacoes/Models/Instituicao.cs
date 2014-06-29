using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;

namespace MvcPublicacoes.Models
{
    public class Instituicao
    {
        [Key, ScaffoldColumn(false)]
        public int IdInstituicao { get; set; }
        [DisplayName("Instituição"), StringLength(200)]
        public string Nome { get; set; }
        [DisplayName("Cidade"), StringLength(200)]
        public string Cidade { get; set; }
        [DisplayName("Publicações")]
        public virtual List<Publicacao> Publicacoes { get; set; }
    }
}