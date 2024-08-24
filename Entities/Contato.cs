using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModuloApi.Entities
{
    public class Contato
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Telefone { get; set; }
        public bool Active { get; set; }
    }
}