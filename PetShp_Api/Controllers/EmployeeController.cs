//1A - importação 
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using PetShop_Api.DataBase;
using PetShop_Api.Domain.Entities;
using PetShop_Api.Domain.Requests;
using System.Net.Mime;
using System.Security.Cryptography;

namespace PetShp_Api.Controllers

    
{
    //2 - aqui informo que a classe é api controller
    //criei tambem uma documentação XML
    /// <summary>
    /// Controller utilizado para operações de CRUD para colaboradores
    /// </summary>
    [ApiController]
    //6-aqui eu coloco a rota que esta minha API, o que eu quero acessar
    [Route("api/employees")]


    //1 -- para criar a APIWEB eu herdo : da controllerBase
    //e importo o using 
    public class EmployeeController : ControllerBase

    {
        
        //aqui eu adiciono um filder, uma variavel private
        private readonly PetShopDbContext petshopDbContext;

        //aqui uso injeção de depencia 
        public EmployeeController(PetShopDbContext petShopDbContext)
        {
            this.petshopDbContext = petShopDbContext;
        }



        //7- aqui uso o httpGet porque quero pegar a minha informação.
        [HttpGet]
        //7- aqui uso o  [HttpGet] porque quero pegar a minha informação.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]



        //3 crio as IAction
        //as action tem que retorna alguma coisa
        //um objectResult retorna uma IActionResult, consigo consultar isso vendo de onde vem o codigo f12
        public IActionResult Get() 
        
        {
            // 5- aqui estou criando uma lista( array) dos funcionarios
            var colaboradores = new[] { "Daniel", "Daniela", "Antonia", "Rocha"};

            //4-o metodo ok cria um objetoResult e retorna o que quero
            // return Ok("Daniel");OK é um método
            //pode ser um string,um int etc
            //outra forma de fazer
            //ela resulta do codigo 200 ok status
            return new OkObjectResult(colaboradores);
        
        }
        // caso eu queria ter mais um get preciso colcoar o nome diferente para não da erro 
        // [HttpGet("2")]
        //public IActionResult Get2()
        //{
        //  var colaboradores = new[] { "Daniel1", "Daniela2", "Antonia3", "Rocha4" };
        //return new OkObjectResult(colaboradores);
        //}

        //aqui vou fazer um GET para pegar pelo ID do colaborador inves de ser a lista inteira

        /// <summary>
        /// Retorno das informações sobre o colaborador com <paramref name="id"/>
        /// </summary>
        /// <param name="id">Id do colaborador</param>
        /// <response code ="200">Retorna os dados do colaborador quando encontrado</response>
        /// <response code ="404">Colaborador não encontrado</response>
        /// 
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]

        public IActionResult Get(int id) 
        {
           var colaboradores = new[] { "Daniel", "Daniela", "Antonia", "Rocha" };
            if (id >= colaboradores.Length || id < 0)
                return NotFound();
            return new OkObjectResult(colaboradores[id]);
        }
        //a partir de agora começo a fazer o POST
        //1-aqui uso o[HttpGet] porque quero pegar a minha informação.
        [HttpPost]
        
        //2- aqui eu dou o nome que quiser no caso vou chamar de CreateEmployee
        //por parametro preciso receber as informações do employee
        //entãi eu crio uma tabela de colaboradores no caso em ingles
        // createEmployee a classe que eu criei para receber meus parametros
        //não esquecer de importa o using
        public IActionResult CreateEmployee(CreateEmployeeRequest request)
        {
            //aqui eu criei uma entidade do tipo employee 
            var entity = new Employee()
            {
                Name = request.EmployeeName,
                Email = request.EmployeeEmail
            };
            
 

            //aqui eu vou no petshodbcontext e adiciono um employee
            petshopDbContext.Employees.Add(entity);
            //aqui eu quero salvar no banco o que mandie antes
            petshopDbContext.SaveChanges();


            return Ok();   
        }
    }
}
