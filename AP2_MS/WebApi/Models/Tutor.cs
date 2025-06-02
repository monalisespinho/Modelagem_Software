public class Tutor
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
}