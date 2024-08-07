﻿using System;
using System.Collections.Generic;

namespace Gerenciador_Contatos_Clientes_Back.Data.Entity;

public class Contato
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefone { get; set; }

    public string? Cargo { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;
}
