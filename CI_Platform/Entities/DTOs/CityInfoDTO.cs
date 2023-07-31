using Common.Enums;

namespace Entities.DTOs;

public class CityInfoDTO
{
    public long Id { get; set; }

    public DropdownDTO Country { get; set; } = new DropdownDTO();

    public string Name { get; set; } = null!;

    public CityStatus? Status { get; set; }
}
