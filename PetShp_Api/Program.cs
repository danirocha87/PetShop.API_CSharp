using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PetShop_Api.DataBase;
using System.Reflection;

namespace PetShp_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //aqui uso porque deixei minha senha secreta
            //aqui eu digo p/o aspnet vai la e busca o userSecret desse cara
            builder.Configuration.AddUserSecrets<Program>();
            builder.Services.AddControllers();

            
            //essa instru��o que fala, registra os controllers na inje��o de dependencia.
            //eu sei que � um registro de dependencia porque esta no services
            //tudo que eu coloco services, significa que eu estou criando uma inje��o de dependecia
            builder.Services.AddControllers();

            //Aqui uso para conectar o banco de dados

           

            //aqui eu conecto a string e no final coloco o mesmo nome que escolhi no appsetings
            // neste caso foi o Default, mas poderia ser qualquer outro .
            //chamamos ela de configuration
            var connectionString = builder.Configuration.GetConnectionString("Default");

            //aqui estou usando porque deixei a senha secreta
            var dbPassword = builder.Configuration["DatabasePassword"];

            //fa�o isso porque deixei minha senha secreta.
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            connectionStringBuilder.Password= dbPassword;

            builder.Services.AddDbContext<PetShopDbContext>(options =>
            {
                options.UseSqlServer(connectionStringBuilder.ConnectionString);
            });

            //esse aqui fala a mesma coisa que o AddControllers s� que no Swagger,registro de dependencia
            //eu sei que � um registro de dependencia porque esta no services
            //tudo que eu coloco services, significa que eu estou criando uma inje��o de dependecia
            //agora vamos colocar informa��es no Swagger atraves desse AddSwaggerGen
            builder.Services.AddSwaggerGen(options => 
            {
                //aqui eu coloco todas as informa��es que quero ver no Swagger
                var openApiInfo = new OpenApiInfo();
                openApiInfo.Title = "Titulo da Documenta��o";
                openApiInfo.Description = "Descri��o da Documenta��o";
                openApiInfo.License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri(@"http://www.mit.com/license"),
                };
                openApiInfo.Contact = new OpenApiContact()
                {
                    Name = "Turma 1038 - BTG",
                    Email = "contato@turma1038btg.com.br",
                };
                options.SwaggerDoc("v1", openApiInfo); //aqui parando o mouse mostra o que preciso add no caso o nome e 

                //AQUI EU COLOCO O CAMINHO QUE ESTA MINHA DOCUMENTA��O XML PARA VINCULAR AO SWAGGER
                //primeiro eu pego o nome 
                //aqui usei a interpola��o $ para pegar o nome no caso PetShop_Api + xml
                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                //depois eu pego o caminho 
                var path = Path.Combine(AppContext.BaseDirectory, fileName);

                //aqui estou incluindo o swagger
                //quando coloco true estou add as informa��es do controller e dos actions 
                //se coloco s� false add apenas os actions 
                options.IncludeXmlComments(path, true);
            });

                

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            //aqui os apps vem do builder app que cria o Use
            //o mid euer s�o partes(gominhos)da minha aplica��o 
            //s�o camadas, classes, que sao executadas em sequencia a partir do momento que a 
            //minha requisi��o chega no servidor at� o momento que ela retorna como uma resposta
            //ex: o Mideuer recebe a requisi��o passa por toda as camadas at� chegar ao fim esta
            //pronto e volta pelas mesmas camadas at� chegar onde foi o seu inicio que � a resposta
            //do usuario
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}