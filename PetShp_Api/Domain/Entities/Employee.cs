namespace PetShop_Api.Domain.Entities
{
    public class Employee
    {
        //aqui eu coloco o que vai ter no meu banco de dados
        public int id { get; set; }
        public string? Name { get; set; }
        
        public string? Email { get; set; }
    }
}
