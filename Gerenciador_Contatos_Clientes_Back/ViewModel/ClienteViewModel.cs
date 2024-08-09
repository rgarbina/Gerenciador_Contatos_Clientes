using Gerenciador_Contatos_Clientes_Back.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Gerenciador_Contatos_Clientes_Back.ViewModel
{
    public class ClienteViewModel
    {
        private string _cnpj = null!;  // Backing store

        [Required(ErrorMessage = "Campo Nome é requerido.")]
        [StringLength(100, ErrorMessage = "o campo nome vai até 100 caracteres.")]
        [MinLength(3, ErrorMessage = "Nome deve conter ao menos 3 caracteres.")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "Campo CNPJ é requerido.")]
        [CNPJ(ErrorMessage = "Invalid CNPJ.")]
        public string Cnpj
        {
            get => _cnpj;
            set
            {
                _cnpj = Regex.Replace(value, @"[^\d]", string.Empty);
            }
        }

        [Required(ErrorMessage = "Data Fundação é requerido.")]
        [DateNotLaterThanToday(ErrorMessage = "Data Fundação não pode ser maior que hoje.")]
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
    public class DateNotLaterThanTodayAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                var today = DateTime.Today;
                if (dateValue <= today)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"Data não pode ser posterior à {today.ToShortDateString()}.");
                }
            }
            return new ValidationResult("data invalida.");
        }
    }
}
