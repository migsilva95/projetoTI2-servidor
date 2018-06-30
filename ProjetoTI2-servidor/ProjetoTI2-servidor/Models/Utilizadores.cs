using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoTI2_servidor.Models
{
    public class Utilizadores
    {
        public Utilizadores()
        {
            ListaDePerguntas = new HashSet<Perguntas>();
            ListaDeRespostas = new HashSet<Respostas>();
            ListaDeSistemas = new HashSet<Sistemas>();
        }

        [Key]
        public int ID { get; set; } // Chave Primária

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Telemovel { get; set; }
        
        public string Fotografia { get; set; }

        [Required]
        public string Username { get; set; }

        // referente à tabela de ligação dos planetas às perguntas
        public virtual ICollection<Respostas> ListaDeRespostas { get; set; }

        // referente à tabela de ligação dos planetas às perguntas
        public virtual ICollection<Perguntas> ListaDePerguntas { get; set; }

        // referente à tabela de ligação dos planetas às perguntas
        public virtual ICollection<Sistemas> ListaDeSistemas { get; set; }
    }
}