 ###   Caminhos para criação do CRUD em C# ###



 ### 1 - Pasta Context contem o construtor "inicializador do banco de dados" ###
 
 
 ### 2 - Pasta Entities contem as TABELAS que serão criadas no banco de dados ###

 
 ### 3 - Pasta Controllers contem as rotas do projeto ###

 
 ### 4 - para configurar a conexao com o banco de dados, abra o arquivo "appsettings.json"- PARA PRODUÇÃO, e altere o valor do "ConexaoPadrao" para o nome do seu banco de dados ###


 ### 5 - para configurar a conexao com o banco de dados, abra o arquivo "appsettings.Development.json"- PARA DESENVOLVIMENTO, e altere o valor do "ConexaoPadrao" para o nome do seu banco de dados ###


 ### 6 - Para buildar e fazer a migração do banco de dados, abra o arquivo "Program.cs" e crie "builder.Services.AddDbContext<AgendaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao"))); ### para nao ficar hard-coded.


 ### 7 - criar a conecxao com banco de dados "ConnectionStrings": { "ConexaoPadrao": "Server=localhost\\sqlexpress; Initial Catalog=Agenda; Integrated Security=true;TrustServerCertificate=True;" DENTRO DO ARQUIVO "appsettings.Development.json" ###}


 ### 9 - COMANDOS UTILIZADOS ### 1-  dotnet webapi para criar uma api server / 2 - dotnet-ef migrations add inicializador para criar a migração / 3- dotnet-ef database update para executar a migração e criar de fato a tabela no banco de dados. / 4- para iniciar o servidor dotnet watch run para assistir as modificações e aplicar OU dotnet run somente para executar o servidor ###

### 10 - corpo de endpoint para o metodo POST, e nao esquecer de fazer as importações necessárias ###
    namespace ModuloApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ContatoController : ControllerBase
    {   private readonly AgendaContext _context;
        public ContatoController(AgendaContext context)
        {
            _context = context;
        }        
        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();
            return Ok(contato);
        }
    }
}
###


### 11 - corpo de endpoint para o metodo GET, e nao esquecer de fazer as importações necessárias 
    [HttpGet("{id}")]
        public IActionResult ObeterPorId(int id)
        {
            var contato = _context.Contatos.Find(id);
            if (contato == null)
                return NotFound();

            return Ok(contato);
        }
###


### 12 - corpo de endpoint para o metodo PUT, e nao esquecer de fazer as importações necessárias 
    [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(id);

            if (contatoBanco == null)
                return NotFound();

            // contatoBanco.Nome: sao os dados que ja estao no banco de dados

            // contato.Nome: sao os dados que serao inseridos no banco de dados no lugar dos dados ja existentes
            
            contatoBanco.Name = contato.Name;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Active = contato.Active;
            
            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();
            
            return Ok(contatoBanco);
        }
###

### 13 - corpo de endpoint para o metodo DELETE, e nao esquecer de fazer as importações necessárias 
    [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contatoBanco = _context.Contatos.Find(id);

            if (contatoBanco == null)
                return NotFound();

            _context.Contatos.Remove(contatoBanco);
            _context.SaveChanges();
            return NoContent();
        }
###


### 14 - corpo de endpoint para o metodo GET, filtrando por nome.
   // Method GET: Retorna todos os usuarios que contém o nome no banco de dados
       
        [HttpGet("ObterPorNome/{nome}")]
        public IActionResult ObterPorNome(string nome)
        {
            // dentro do metodo where e feita uma função lambda para verificar si a variavel nome que esta sendo passado pelo metodo contem no banco de dados e retorna o resultado.

            var contatos = _context.Contatos.Where(x => x.Name.Contains(nome));
            return Ok(contatos);
        }

###