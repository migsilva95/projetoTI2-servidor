using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoTI2_servidor.Models
{
    public class Respostas
    {

        [Key]
        public int ID { get; set; } // Chave Primária

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Resposta { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public Boolean RespostaCerta { get; set; }

        // FK para a tabela das Perguntas
        [ForeignKey("Pergunta")]
        public int PerguntasFK { get; set; }
        public virtual Perguntas Pergunta { get; set; }

        // FK para a tabela das Perguntas
        [ForeignKey("Utilizador")]
        public int UtilizadoresFK { get; set; }
        public virtual Utilizadores Utilizador { get; set; }
    }
}