using Common.Constants;
using Common.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;
public class BannerDTO
{
    public long Id { get; set; }

    [Required(ErrorMessage = ModelStateConstant.ERROR_BANNER_IMAGE)]
    public IFormFile Image { get; set; } = null!;

    [Required(ErrorMessage = ModelStateConstant.ERROR_BANNER_DESCRIPTION)]
    [MinLength(50)]
    [MaxLength(4000)]
    public string Description { get; set; } = null!;

    [EnumDataType(typeof(BannerStatus), ErrorMessage = ModelStateConstant.STATUS_INVALID)]
    public BannerStatus? Status { get; set; }
}