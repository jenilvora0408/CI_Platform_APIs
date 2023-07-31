using Common.Constants;
using System.ComponentModel.DataAnnotations;

public class MinuteValidationAttribute : ValidationAttribute
{
    private readonly string _dependentProperty;

    public MinuteValidationAttribute(string dependentProperty)
    {
        _dependentProperty = dependentProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var hourProperty = validationContext.ObjectType.GetProperty(_dependentProperty);

        if (hourProperty == null)
        {
            throw new ArgumentException("Invalid property name: " + _dependentProperty);
        }

        var hourValue = (short)hourProperty.GetValue(validationContext.ObjectInstance);
        var minuteValue = (short)value;

        if (hourValue == 0 && minuteValue <= 0 || minuteValue >= 60)
        {
            return new ValidationResult(ModelStateConstant.TIMESHEET_MINUTE);
        }

        return ValidationResult.Success;
    }
}
