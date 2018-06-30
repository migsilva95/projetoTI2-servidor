namespace ProjetoTI2_servidor.Migrations
{
    using ProjetoTI2_servidor.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjetoTI2_servidor.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ProjetoTI2_servidor.Models.ApplicationDbContext context)
        {
            //*********************************************************************
            // adiciona UTILIZADORES
            var utilizadores = new List<Utilizadores>
            {
                new Utilizadores {Nome="José Alves",  Telemovel="919191919",  Username="josealves@sapo.pt"}, // professor
                new Utilizadores {Nome="Maria Silva", Telemovel="919191920", Username="mariasilva@sapo.pt"},  // aluno
                new Utilizadores {Nome="João Lopes",  Telemovel="919191921", Username="joaolopes@sapo.pt"}  // aluno
            };
            utilizadores.ForEach(uu => context.Utilizadores.AddOrUpdate(u => u.Nome, uu));
            context.SaveChanges();

            //*********************************************************************
            // adiciona SISTEMAS
            var sistemas = new List<Sistemas> {
                new Sistemas {Nome="Sistema Solar",UtilizadoresFK=1}
            };
            sistemas.ForEach(ss => context.Sistemas.AddOrUpdate(s => s.Nome, ss));
            context.SaveChanges();

            //*********************************************************************
            // adiciona PLANETAS
            var planetas = new List<Planetas> {
                new Planetas {Nome="Mercúrio",SistemasFK=1},
                new Planetas {Nome="Vénus",SistemasFK=1},
                new Planetas {Nome="Terra",SistemasFK=1},
                new Planetas {Nome="Marte",SistemasFK=1},
                new Planetas {Nome="Júpiter",SistemasFK=1},
                new Planetas {Nome="Saturno",SistemasFK=1},
                new Planetas {Nome="Úrano",SistemasFK=1},
                new Planetas {Nome="Neptuno",SistemasFK=1}
            };
            planetas.ForEach(pp => context.Planetas.AddOrUpdate(p => p.Nome, pp));
            context.SaveChanges();

            //*********************************************************************
            // adiciona PERGUNTAS
            var perguntas = new List<Perguntas> {
                new Perguntas {Pergunta="Quantas luas tem este planeta?",UtilizadoresFK=1}
         };
            perguntas.ForEach(pp => context.Perguntas.AddOrUpdate(p => p.Pergunta, pp));
            context.SaveChanges();

            //*********************************************************************
            // adiciona RESPOSTAS
            var respostas = new List<Respostas> {
                new Respostas {Resposta="1",PerguntasFK=1,RespostaCerta=false,UtilizadoresFK=1},
                new Respostas {Resposta="2",PerguntasFK=1,RespostaCerta=false,UtilizadoresFK=1},
                new Respostas {Resposta="3",PerguntasFK=1,RespostaCerta=true,UtilizadoresFK=1},
                new Respostas {Resposta="4",PerguntasFK=1,RespostaCerta=false,UtilizadoresFK=1}
            };
            respostas.ForEach(rr => context.Respostas.AddOrUpdate(r => r.ID, rr));
            context.SaveChanges();
        }
    }
}
