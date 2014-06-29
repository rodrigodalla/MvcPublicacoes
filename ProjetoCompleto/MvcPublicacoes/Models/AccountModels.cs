using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace MvcPublicacoes.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required(ErrorMessage = "{0} deve ser informado.")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }
        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required(ErrorMessage = "{0} deve ser informado.")]
        [Display(Name = "Nome")]
        [StringLength(200, ErrorMessage = "{0} deve ter pelo menos {2} caracteres.", MinimumLength = 7)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} deve ser informado.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "{0} deve ser informado.")]
        [StringLength(100, ErrorMessage = "{0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação da Nova Senha")]
        [Compare("NewPassword", ErrorMessage = "Nova Senha e {0} devem ser iguais.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "{0} deve ser informado.")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} deve ser informado.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar meus dados?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "{0} deve ser informado.")]
        [Display(Name = "Nome")]
        [StringLength(200, ErrorMessage = "{0} deve ter pelo menos {2} caracteres.", MinimumLength = 7)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} deve ser informado.")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} deve ser informado.")]
        [StringLength(100, ErrorMessage = "{0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação da Senha")]
        [Compare("Password", ErrorMessage = "Senha e {0} devem ser iguais.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
