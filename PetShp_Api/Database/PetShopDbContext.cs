using Microsoft.EntityFrameworkCore;

namespace PetShop_Api.DataBase
{
    public class PetShopDbContext : DbContext
    {
        //toda vez que cria o DbContext tem que criar um construtor que vai receber um options
        //e vai passar para a classe base
        //esse option vai receber uma string de conexão, por isso precisa passar para a classe base

        public PetShopDbContext(DbContextOptions<PetShopDbContext> options) : base(options) 
        { }
    }
}
