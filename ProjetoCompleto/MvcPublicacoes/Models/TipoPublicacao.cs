using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;

namespace MvcPublicacoes.Models
{
    public class TipoPublicacao
    {
        [Key, ScaffoldColumn(false)]
        public int IdTipoPublicacao { get; set; }
        [DisplayName("Tipo"), StringLength(100)]
        public string Descricao { get; set; }
        [DisplayName("Publicações")]
        public virtual List<Publicacao> Publicacoes { get; set; }
    }
}