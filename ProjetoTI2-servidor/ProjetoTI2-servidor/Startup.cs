using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ProjetoTI2_servidor.Models;

[assembly: OwinStartupAttribute(typeof(ProjetoTI2_servidor.Startup))]
namespace ProjetoTI2_servidor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // invocar o método para iniciar a configuração
            // dos ROLES +USERS
            iniciaAplicacao();
        }

        /// <summary>
        /// cria, caso não existam, as Roles de suporte à aplicação: Agentes, Condutores
        /// cria, nesse caso, também, um utilizador...
        /// </summary>
        private void iniciaAplicacao()
        {

            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // criar a Role 'Professor'
            if (!roleManager.RoleExists("Professor"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Professor";
                roleManager.Create(role);
            }

            // criar a Role 'Aluno'
            if (!roleManager.RoleExists("Aluno"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Aluno";
                roleManager.Create(role);
            }

            try
            {
                //new Utilizadores { Nome = "José Alves", Telemovel = "919191919", Email = "josealves@sapo.pt", Username = "josealves@sapo.pt" }, // professor
                //new Utilizadores { Nome = "Maria Silva", Telemovel = "919191920", Email = "mariasilva@sapo.pt", Username = "mariasilva@sapo.pt" },  // aluno
                //new Utilizadores { Nome = "João Lopes", Telemovel = "919191921", Email = "joaolopes@sapo.pt", Username = "joaolopes@sapo.pt" }  // aluno

                // criar um utilizador 'Professor'
                var user = new ApplicationUser();
                user.UserName = "josealves@sapo.pt";
                user.Email = "josealves@sapo.pt";

                string userPWD = "123_Asd";
                var chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Professor-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Professor");
                }

                // criar um utilizador 'Aluno'
                user = new ApplicationUser();
                user.UserName = "mariasilva@sapo.pt";
                user.Email = "mariasilva@sapo.pt";

                userPWD = "123_Asd";
                chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Aluno-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Aluno");
                }

                // criar um utilizador 'Aluno'
                user = new ApplicationUser();
                user.UserName = "joaolopes@sapo.pt";
                user.Email = "joaolopes@sapo.pt";

                userPWD = "123_Asd";
                chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Aluno-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Aluno");
                }
            }
            catch (System.Exception)
            { }
        }
        // https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97

    }
}
