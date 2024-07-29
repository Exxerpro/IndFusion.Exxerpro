namespace IndFusion.Exxerpro.Domain.Models;

public static class ProductMachineItemExtensions
{
    public static IEnumerable<ProductMachineItem> RemoveDuplicates(this IEnumerable<ProductMachineItem> source)
    {
        return source
            .GroupBy(item => new { item.Name, item.MachineId, item.Status })
            .Select(group => group.First());
    }

    public static void InsertIfNotExist(this IList<ProductMachineItem> source, int index, ProductMachineItem item)
    {
        bool exists = source.Any(existingItem =>
            existingItem.Name == item.Name &&
            existingItem.MachineId == item.MachineId &&
            existingItem.Status == item.Status);

        if (!exists)
        {
            source.Insert(index, item);
        }
    }
    public static void AddIfNotExist(this IList<ProductMachineItem> source, ProductMachineItem item)
    {
        bool exists = source.Any(existingItem =>
            existingItem.Name == item.Name &&
            existingItem.MachineId == item.MachineId &&
            existingItem.Status == item.Status);

        if (!exists)
        {
            source.Add(item);
        }
    }
}