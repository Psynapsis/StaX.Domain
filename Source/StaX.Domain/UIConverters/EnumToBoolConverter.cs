using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using System.Globalization;

namespace StaX.Domain.UIConverters;

/// <summary>
/// Two-way conversion from flags to bool and back using parameter as mask
/// Warning: The trick is in storing value locally between calls to Convert and ConvertBack
/// You must have a single instance of this converter per flags property per object
/// Do not share this converter between different objects or properties
/// N.B.! works only with int32 enum values!
/// </summary>
public class EnumToBoolConverter : AvaloniaObject, IValueConverter
{
    private object? _target;
    private Type? _enumType;

    public static readonly StyledProperty<bool> RespectEnumFlagsProperty =
      AvaloniaProperty.Register<EnumToBoolConverter, bool>(
          nameof(RespectEnumFlags),
          true);

    public bool RespectEnumFlags
    {
        get => GetValue(RespectEnumFlagsProperty);
        set => SetValue(RespectEnumFlagsProperty, value);
    }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Enum enumValue && parameter is Enum enumToCompare)
        {
            _target = enumValue;
            _enumType = _target.GetType();

            return enumValue.Equals(enumToCompare) || (RespectEnumFlags && enumValue.HasFlag(enumToCompare));
        }
        else
            return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (_target is null || _enumType is null)
            return BindingOperations.DoNothing;

        if (value is bool boolValue
            && parameter is Enum enumParameter)
        {
            var parameterIntValue = System.Convert.ToInt32(enumParameter);
            var currentEnumIntValue = System.Convert.ToInt32(_target);

            if (RespectEnumFlags)
            {
                if (boolValue)
                    currentEnumIntValue |= parameterIntValue;
                else
                    currentEnumIntValue &= ~parameterIntValue;
            }
            else
                currentEnumIntValue = parameterIntValue;

            _target = Enum.Parse(_enumType, currentEnumIntValue.ToString());

            return _target;
        }
        else
            return BindingOperations.DoNothing;
    }
}