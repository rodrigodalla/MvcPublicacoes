using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;

namespace MvcPublicacoes.Models
{
    public class Publicacao
    {
        [Key, ScaffoldColumn(false)]
        public int IdPublicacao { get; set; }

        [DisplayName("Autor")]
        [Required(ErrorMessage = "{0} deve ser informado.")]
        public int IdAutor { get; set; }

        [DisplayName("Tipo")]
        [Required(ErrorMessage = "{0} deve ser informado.")]
        public int IdTipoPublicacao { get; set; }

        [DisplayName("Instituição")]
        [Required(ErrorMessage = "{0} deve ser informado.")]
        public int IdInstituicao { get; set; }

        [DisplayName("Situação")]
        [Required(ErrorMessage = "{0} deve ser informado.")]
        public int IdSituacao { get; set; }

        [DisplayName("Título")]
        [Required(ErrorMessage = "{0} deve ser informado.")]
        [StringLength(250, ErrorMessage = "{0} deve ter pelo menos {2} caracteres.", MinimumLength = 10)]
        public string Titulo { get; set; }

        [DisplayName("Data de Publicação")]
        [Required(ErrorMessage = "{0} deve ser informado.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DtPublicacao { get; set; }

        [DisplayName("Localização")]
        [Required(ErrorMessage = "{0} deve ser informado.")]
        [StringLength(300, ErrorMessage = "{0} deve ter pelo menos {2} caracteres.", MinimumLength = 10)]
        public string Localizacao { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Resumo")]
        [StringLength(250, ErrorMessage = "{0} deve ter no máximo 250.")]
        public string Resumo { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Referência")]
        [StringLength(250, ErrorMessage = "{0} deve ter no máximo 250.")]
        public string Referencia { get; set; }

        [DisplayName("Conceito")]
        [StringLength(250, ErrorMessage = "{0} deve ter no máximo 250.")]
        public string Conceito { get; set; }

        public virtual Autor Autor { get; set; }
        public virtual TipoPublicacao TipoPublicacao { get; set; }
        public virtual Instituicao Instituicao { get; set; }
        public virtual Situacao Situacao { get; set; }
    }
}