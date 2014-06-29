using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;

namespace MvcPublicacoes.Models
{
    public class Autor
    {
        [Key, ScaffoldColumn(false)]
        public int IdAutor { get; set; }
        [DisplayName("Autor"), StringLength(200)]
        public string Nome { get; set; }
        [DisplayName("Formato Citação"), StringLength(200)]
        public string FormatoCitacao { get; set; }
        [DisplayName("Publicações")]
        public virtual List<Publicacao> Publicacoes { get; set; }

        [DisplayName("Usuário")]
        [Required(ErrorMessage = "{0} deve ser informado.")]
        public int UserId { get; set; }
    }
}