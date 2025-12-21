namespace Csis.Admission.Application.Extensions;

/// <inheritdoc/>
public static class ObjectExtensions
{
    /// <inheritdoc/>
    public static TDestination MapTo<TDestination>(this object source) where TDestination : new() {
        if ( source == null )
            throw new ArgumentNullException(nameof(source));

        var destination = new TDestination();
        var sourceType = source.GetType();
        var destinationType = typeof(TDestination);

        foreach ( var sourceProperty in sourceType.GetProperties() ) {
            var destinationProperty = destinationType.GetProperty(sourceProperty.Name);
            if ( destinationProperty != null && destinationProperty.CanWrite && destinationProperty.PropertyType == sourceProperty.PropertyType ) {
                var value = sourceProperty.GetValue(source);
                destinationProperty.SetValue(destination, value);
            }
        }

        foreach ( var sourceProperty in sourceType.GetProperties()
            .Where(x => x.Name.ToLower().Contains("date") && x.PropertyType.Name.ToLower().Contains("string")) ) {
            var destinationProperty = destinationType.GetProperty(sourceProperty.Name);

            if ( destinationProperty.PropertyType.Name.ToLower().Contains("int") ) {
                var value = sourceProperty.GetValue(source);
                destinationProperty.SetValue(destination, Common.Utilities.StringDateToInt(value));
            }
        }

        return destination;
    }
}
