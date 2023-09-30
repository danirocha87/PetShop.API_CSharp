//1A - importação 
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace PetShp_Api.Controllers

    
{
    //2 - aqui informo que a classe é api controller
    //criei tambem uma documentação XML
    /// <summary>
    /// Controller utilizado para operações de CRUD para colaboradores
    /// </summary>
    [ApiController]
    //6-aqui eu coloco a rota que esta minha API, o que eu quero acessar
    [Route("api/colaboradores")]


    //1 -- para criar a APIWEB eu herdo : da controllerBase
    //e importo o using 
    public class ColaboradoresController : ControllerBase

    {
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
    }
}
