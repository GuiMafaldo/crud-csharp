using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModuloApi.Entities;
using ModuloApi.Context;

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
        // Method POST: Inserir usuarios no banco de dados

        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObeterPorId), new { id = contato.Id }, contato);
        }

        // Method GET: Retorna todos os usuarios pelo ID no banco de dados

        [HttpGet("{id}")]
        public IActionResult ObeterPorId(int id)
        {
            var contato = _context.Contatos.Find(id);
            if (contato == null)
                return NotFound();

            return Ok(contato);
        }

        // Method GET: Retorna todos os usuarios que contém o nome no banco de dados
        [HttpGet("ObterPorNome/{nome}")]
        public IActionResult ObterPorNome(string nome)
        {
            // dentro do metodo where e feita uma função lambda para verificvar si a variavel nome que esta sendo passado pelo metodo contem no banco de dados
            var contatos = _context.Contatos.Where(x => x.Name.Contains(nome));
            return Ok(contatos);
        }



        // Method PUT: Atualiza um usuario pelo ID no banco de dados
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

        // Method DELETE: Remove um usuario pelo ID no banco de dados
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
    }
}