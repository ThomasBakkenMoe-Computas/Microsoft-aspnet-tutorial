
internal class FruitDb
{
    public required List<Fruit> Fruits { get; set; }


    internal async Task SaveChangesAsync()
    {
        return;
    }
}