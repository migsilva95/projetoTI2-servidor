using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoTI2_servidor.Models
{
    public class Perguntas
    {
        public Perguntas()
        {
            ListaDePlanetas = new HashSet<Planetas>();
            ListaDeRespostas = new HashSet<Respostas>();
        }

        [Key]
        public int ID { get; set; } // Chave Primária

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Pergunta { get; set; }

        // referente à tabela de ligação dos planetas às perguntas
        public virtual ICollection<Planetas> ListaDePlanetas { get; set; }

        // referente à tabela de ligação dos planetas às perguntas
        public virtual ICollection<Respostas> ListaDeRespostas { get; set; }

        // FK para a tabela das Perguntas
        [ForeignKey("Utilizador")]
        public int UtilizadoresFK { get; set; }
        public virtual Utilizadores Utilizador { get; set; }
    }
}