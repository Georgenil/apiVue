namespace apiNux.Domain
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaterialId { get; set; }
        public Material? Material { get; set; }
    }
}
