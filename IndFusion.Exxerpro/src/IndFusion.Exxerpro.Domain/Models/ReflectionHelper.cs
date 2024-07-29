using System.Reflection;
using MudBlazor;

namespace IndFusion.Exxerpro.Domain.Models;

public static class ReflectionHelper
{

    //MudDropContainer<ProductMachineItem>

    public static Dictionary<string, MudDropZone<T>> GetDropZones<T>(this MudDropContainer<ProductMachineItem> container)
    {
        var fieldName = "_mudDropZones";
        // Get the type of the container object
        Type containerType = container.GetType();

        // Get the FieldInfo for the private field
        FieldInfo fieldInfo = containerType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

        // Check if the field was found
        if (fieldInfo == null)
        {
            throw new ArgumentException($"Field '{fieldName}' not found in type '{containerType.FullName}'.");
        }

        // Get the value of the field
        var fieldValue = fieldInfo.GetValue(container);

        // Cast the field value to the expected type
        if (fieldValue is Dictionary<string, MudDropZone<T>> dictionary)
        {
            return dictionary;
        }
        else
        {
            throw new InvalidCastException($"Field '{fieldName}' is not of the expected type 'Dictionary<string, MudDropZone<{typeof(T).Name}>>'.");
        }
    }



    public static Dictionary<T, int> GetIndices<T>(this MudDropZone<T> dropZone)
    {
        // Get the type of the MudDropZone instance
        Type dropZoneType = dropZone.GetType();

        // Get the FieldInfo for the private _indices field
        FieldInfo fieldInfo = dropZoneType.GetField("_indices", BindingFlags.NonPublic | BindingFlags.Instance);

        // Check if the field was found
        if (fieldInfo == null)
        {
            throw new ArgumentException($"Field '_indices' not found in type '{dropZoneType.FullName}'.");
        }

        // Get the value of the field
        var fieldValue = fieldInfo.GetValue(dropZone);

        // Cast the field value to the expected type
        if (fieldValue is Dictionary<T, int> dictionary)
        {
            return dictionary;
        }
        else
        {
            throw new InvalidCastException($"Field '_indices' is not of the expected type 'Dictionary<{typeof(T).Name}, int>'.");
        }
    }

    


}
//class