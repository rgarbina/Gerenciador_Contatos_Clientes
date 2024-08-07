using System;
using System.Collections.Generic;

namespace Gerenciador_Contatos_Clientes_Back.Data.Entity;

public class Cliente
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Cnpj { get; set; } = null!;

    public DateOnly DataFundacao { get; set; }

    public bool Ativo { get; set; }

    public virtual ICollection<Contato> Contatos { get; set; } = new List<Contato>();
}
