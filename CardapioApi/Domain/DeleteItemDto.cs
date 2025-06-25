using MassTransit;

namespace Domain;

[MessageUrn("delete-item-dto")]
[EntityName("delete-item-dto")]
public class DeleteItemDto
{
    public Guid Id { get; set; }
}
