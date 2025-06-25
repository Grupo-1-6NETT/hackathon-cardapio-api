using MassTransit;

namespace Domain;

[MessageUrn("add-item-dto")]
[EntityName("add-item-dto")]
public class AddItemDto
{
    public Guid TransportId { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public bool Disponivel { get; set; }
    public string NomeCategoria { get; set; }    
}
