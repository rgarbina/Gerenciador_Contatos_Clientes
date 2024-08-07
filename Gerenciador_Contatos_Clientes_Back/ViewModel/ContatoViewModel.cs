using Gerenciador_Contatos_Clientes_Back.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Gerenciador_Contatos_Clientes_Back.ViewModel
{
    public class ContatoViewModel
    {
        [Required(ErrorMessage = "Campo Nome é requerido.")]
        [StringLength(100, ErrorMessage = "o campo nome vai até 100 caracteres.")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "Campo Nome é requerido.")]
        [StringLength(100, ErrorMessage = "o campo email vai até 100 caracteres.")]
        [EmailAddress(ErrorMessage = "endereco de email formato invalido.")]
        public string Email { get; set; } = null!;

        [StringLength(20, ErrorMessage = "o campo telefone vai até 20 caracteres.")]
        public string? Telefone { get; set; }

        [StringLength(50, ErrorMessage = "o campo cargo vai até 50 caracteres.")]
        public string? Cargo { get; set; }

        public static ContatoViewModel ToViewModel(Contato contato)
        {
            return new ContatoViewModel
            {
                Nome = contato.Nome,
                Email = contato.Email,
                Telefone = contato.Telefone,
                Cargo = contato.Cargo
            };
        }

        public static Contato ToEntity(ContatoViewModel viewModel, Contato contato)
        {
            contato.Nome = viewModel.Nome;
            contato.Email = viewModel.Email;
            contato.Telefone = viewModel.Telefone;
            contato.Cargo = viewModel.Cargo;

            return contato;
        }
    }
}
