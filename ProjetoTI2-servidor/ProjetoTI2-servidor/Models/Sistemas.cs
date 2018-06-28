using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoTI2_servidor.Models
{
    public class Sistemas
    {
        public Sistemas()
        {
            ListaDePlanetas = new HashSet<Planetas>();
        }
        [Key]
        public int ID { get; set; } // Chave Primária

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Nome { get; set; }

        // referente aos planetas que um sistema tem
        public virtual ICollection<Planetas> ListaDePlanetas { get; set; }

        // FK para a tabela das Perguntas
        [ForeignKey("Utilizador")]
        public int UtilizadoresFK { get; set; }
        public virtual Utilizadores Utilizador { get; set; }
    }
}