namespace schoolOfDevs.Entities
{
    // o dois pontos significa que estou herdando as propriedades de baseEntity
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
