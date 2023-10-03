using Microsoft.EntityFrameworkCore;
using PetShop_Api.Domain.Entities;

namespace PetShop_Api.DataBase
{
    public class PetShopDbContext : DbContext
    {
        //toda vez que cria o DbContext tem que criar um construtor que vai receber um options
        //e vai passar para a classe base
        //esse option vai receber uma string de conexão, por isso precisa passar para a classe base

        public PetShopDbContext(DbContextOptions<PetShopDbContext> options) : base(options) 
        {

        }
        //AQUI EU ESTOU DIZENDO PARA MEU BANCO DE DADOS QUE VOU TER UMA TABELA DE EMPLOYEE
        //Dbset é uma tabela no banco de dados 
       public DbSet<Employee> Employees { get; set; }
    }
}
