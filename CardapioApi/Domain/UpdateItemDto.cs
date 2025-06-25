using MassTransit;

namespace Domain;

[MessageUrn("update-item-dto")]
[EntityName("update-item-dto")]
public class UpdateItemDto
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal? Preco { get; set; }
    public bool? Disponivel { get; set; }
    public string? NomeCategoria { get; set; }    
}
