using System.ComponentModel.DataAnnotations;

namespace Gerenciador_Contatos_Clientes_Front.Data
{
    public class Cliente
    {
        [Required(ErrorMessage = "Campo Nome é requerido.")]
        [StringLength(100, ErrorMessage = "o campo nome vai até 100 caracteres.")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "Campo Nome é requerido.")]
        public string Cnpj { get; set; } = null!;

        public DateTime DataFundacao { get; set; }

        public bool Ativo { get; set; }
    }
}
