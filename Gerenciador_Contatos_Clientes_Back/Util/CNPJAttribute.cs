using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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
