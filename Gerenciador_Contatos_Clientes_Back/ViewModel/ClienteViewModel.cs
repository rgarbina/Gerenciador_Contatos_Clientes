using Gerenciador_Contatos_Clientes_Back.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gerenciador_Contatos_Clientes_Back.ViewModel
{
    public class ClienteViewModel
    {
        [Required(ErrorMessage = "Campo Nome é requerido.")]
        [StringLength(100, ErrorMessage = "o campo nome vai até 100 caracteres.")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "Campo Nome é requerido.")]
        [CNPJ(ErrorMessage = "Invalid CNPJ.")]
        public string Cnpj { get; set; } = null!;

        public DateTime DataFundacao { get; set; }

        public bool Ativo { get; set; }

        public static ClienteViewModel ToViewModel(Cliente cliente)
        {
            return new ClienteViewModel
            {
                Nome = cliente.Nome,
                Cnpj = cliente.Cnpj,
                DataFundacao = cliente.DataFundacao.ToDateTime(TimeOnly.MinValue),
                Ativo = cliente.Ativo
            };
        }

        public static Cliente ToEntity(ClienteViewModel viewModel, Cliente cliente)
        {
            cliente.Nome = viewModel.Nome;
            cliente.Cnpj = viewModel.Cnpj;
            cliente.DataFundacao = DateOnly.FromDateTime(viewModel.DataFundacao);
            cliente.Ativo = viewModel.Ativo;

            return cliente;
        }

    }
}
