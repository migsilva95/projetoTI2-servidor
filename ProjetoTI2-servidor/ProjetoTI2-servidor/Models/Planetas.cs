using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoTI2_servidor.Models
{
    public class Planetas
    {
        public Planetas()
        {
            ListaDePerguntas = new HashSet<Perguntas>();
        }

        [Key]
        public int ID { get; set; } // Chave Primária

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Nome { get; set; }

        public string Fotografia { get; set; }

        // referente à tabela de ligação dos planetas às perguntas
        public virtual ICollection<Perguntas> ListaDePerguntas { get; set; }

        // FK para a tabela dos Sistemas
        [ForeignKey("Sistema")]
        public int SistemasFK { get; set; }
        public virtual Sistemas Sistema { get; set; }

    }
}