using System.ComponentModel.DataAnnotations;

namespace Gerenciador_Contatos_Clientes_Front.Data
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Nome é requerido.")]
        [StringLength(100, ErrorMessage = "o campo nome vai até 100 caracteres.")]
        [MinLength(3, ErrorMessage = "Nome deve conter ao menos 3 caracteres.")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "Campo CNPJ é requerido.")]
        [CNPJ(ErrorMessage = "CNPJ formato invalido.")]
        public string Cnpj { get; set; } = null!;

        [Required(ErrorMessage = "Data Fundação é requerido.")]
        [DateNotLaterThanToday(ErrorMessage = "Data Fundação não pode ser maior que hoje.")]
        public DateTime? DataFundacao { get; set; }

        public bool Ativo { get; set; }

        public virtual ICollection<Contato> Contatos { get; set; } = new List<Contato>();

        public string CNPJFormatado()
        {
            if (this.Cnpj.Length == 14)
            {
                return Convert.ToUInt64(this.Cnpj).ToString(@"00\.000\.000\/0000\-00");
            }
            return this.Cnpj; // Return original string if it doesn't match expected length
        }
    }

    public class CNPJAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("CNPJ é requirido.");
            }

            var cnpj = value.ToString();

            if (!IsCNPJ(cnpj))
            {
                return new ValidationResult("CNPJ Invalido.");
            }

            return ValidationResult.Success;
        }

        private bool IsCNPJ(string cnpj)
        {
            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
            {
                return false;
            }

            if (new string(cnpj[0], cnpj.Length) == cnpj)
            {
                return false;
            }

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCnpj;
            string digito;
            int soma;
            int resto;

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            }

            resto = (soma % 11);

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            }

            resto = (soma % 11);

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
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
