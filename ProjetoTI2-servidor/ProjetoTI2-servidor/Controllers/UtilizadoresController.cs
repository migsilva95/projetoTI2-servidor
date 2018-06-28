using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetoTI2_servidor.Models;

namespace ProjetoTI2_servidor.Controllers
{
    public class UtilizadoresController : Controller
    {

        // cria uma variável que representa a Base de Dados
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Utilizadores
        /// <summary>
        /// lista todos os utilizadores
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // db.Utilizadores.ToList() -> em sql: SELECT * FROM Utilizadores;
            // enviar para a View uma lista com todos os Utilizadores, da BD

            // obter a lista de todos os utilizadores
            // em SQL:  SELECT * FROM Utilizadores ORDER BY Nome;
            var listaDeUtilizadores = db.Utilizadores.ToList().OrderBy(a => a.Nome);

            return View(listaDeUtilizadores);
            //  return View();
        }

        // GET: Utilizadores/Details/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            // se se escrever 'int?' é possível
            // não fornecer o valor para o ID e não há erro

            // proteção para o caso de não ter sido fornecido um ID válido
            if (id == null)
            {
                // instrução original
                // devolve um erro qd não há ID
                // logo, não é possível pesquisar por um Utilizador
                // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                // redirecionar para uma página que nós controlamos
                return RedirectToAction("Index");
            }

            // procura na BD, o Utilizador cujo ID foi fornecido
            Utilizadores utilizador = db.Utilizadores.Find(id);

            // proteção para o caso de não ter sido encontrado qq Utilizador
            // que tenha o ID fornecido
            if (utilizador == null)
            {
                // o utilizador não foi encontrado
                // logo, gera-se uma msg de erro
                // return HttpNotFound();

                // redirecionar para uma página que nós controlamos
                return RedirectToAction("Index");
            }

            // entrega à View os dados do Utilizador encontrado
            return View(utilizador);
        }



        // GET: Utilizadores/Create
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            // apresenta a View para se inserir um novo Utilizador
            return View();
        }




        // POST: Utilizadores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="utilizador"></param>
        /// <param name="uploadFotografia"></param>
        /// <returns></returns>
        // anotador para HTTP Post 
        [HttpPost]
        // anotador para proteção por roubo de identidade
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Telemovel,Email")] Utilizadores utilizador, HttpPostedFileBase uploadFotografia)
        {
            // escrever os dados de um novo Utilizador na BD 

            // especificar o ID do novo Utilizador
            // testar se há registos na tabela dos Utilizadores
            // if (db.Utilizadores.Count()!=0){ }

            // ou então, usar a instrução TRY/CATCH
            int idNovoUtilizador = 0;
            try
            {
                idNovoUtilizador = db.Utilizadores.Max(a => a.ID) + 1;
            }
            catch (Exception)
            {
                idNovoUtilizador = 1;
            }

            // guardar o ID do novo Agente
            utilizador.ID = idNovoUtilizador;

            // especificar (escolher) o nome do ficheiro
            string nomeImagem = "Utilizador_" + idNovoUtilizador + ".jpg";

            // var. auxiliar
            string path = "";

            // validar se a imagem foi fornecida
            if (uploadFotografia != null)
            {
                // o ficheiro foi fornecido
                // validar se o q foi fornecido é uma imagem ----> fazer em casa
                // formatar o tamanho da imagem

                // criar o caminho completo até ao sítio onde o ficheiro
                // será guardado
                path = Path.Combine(Server.MapPath("~/imagens/"), nomeImagem);

                // guardar o nome do ficheiro na BD
                utilizador.Fotografia = nomeImagem;
            }
            else
            {
                // não foi fornecido qq ficheiro
                // gerar uma mensagem de erro
                ModelState.AddModelError("", "Não foi fornecida uma imagem...");
                // devolver o controlo à View
                return View(utilizador);
            }

            // ModelState.IsValid -> confronta os dados fornecidos da View
            //                       com as exigências do Modelo
            if (ModelState.IsValid)
            {
                try
                {
                    // adiciona o novo Utilizador à BD
                    db.Utilizadores.Add(utilizador);
                    // faz 'Commit' às alterações
                    db.SaveChanges();
                    // escrever o ficheiro com a fotografia no disco rígido, na pasta 'imagens'
                    uploadFotografia.SaveAs(path);

                    // se tudo correr bem, redireciona para a página de Index
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Houve um erro com a criação do novo Utilizador..."+ex);

                    /// se existir uma classe chamada 'Erro.cs'
                    /// iremos nela registar os dados do erro
                    /// - criar um objeto desta classe
                    /// - atribuir a esse objeto os dados do erro
                    ///   - nome do controller
                    ///   - nome do método
                    ///   - data + hora do erro
                    ///   - mensagem do erro (ex)
                    ///   - dados que se tentavam inserir
                    ///   - outros dados considerados relevante
                    /// - guardar o objeto na BD
                    /// 
                    /// - notificar um GESTOR do sistema, por email,
                    ///   ou por outro meio, da ocorrência do erro 
                    ///   e dos seus dados              

                }
            }

            // se houver um erro, 
            // reapresenta os dados do Utilizador na View
            return View(utilizador);
        }





        // GET: Utilizadores/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            // se se escrever 'int?' é possível
            // não fornecer o valor para o ID e não há erro

            // proteção para o caso de não ter sido fornecido um ID válido
            if (id == null)
            {
                // instrução original
                // devolve um erro qd não há ID
                // logo, não é possível pesquisar por um Utilizador
                // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                // redirecionar para uma página que nós controlamos
                return RedirectToAction("Index");
            }

            // procura na BD, o Utilizador cujo ID foi fornecido
            Utilizadores utilizador = db.Utilizadores.Find(id);

            // proteção para o caso de não ter sido encontrado qq Utilizador
            // que tenha o ID fornecido
            if (utilizador == null)
            {
                // o utilizador não foi encontrado
                // logo, gera-se uma msg de erro
                // return HttpNotFound();

                // redirecionar para uma página que nós controlamos
                return RedirectToAction("Index");
            }

            // entrega à View os dados do Utilizador encontrado
            return View(utilizador);
        }

        // POST: Utilizadores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="utilizadores"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Telemovel,Email,Fotografia")] Utilizadores utilizadores)
        {
            if (ModelState.IsValid)
            {
                // neste caso já existe um Utilizador
                // apenas quero EDITAR os seus dados
                db.Entry(utilizadores).State = EntityState.Modified;
                // efetuar 'Commit'
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(utilizadores);
        }

        // GET: Utilizadores/Delete/5
        /// <summary>
        /// apresenta na View os dados de um utilizador,
        /// com vista à sua, eventual, eliminação
        /// </summary>
        /// <param name="id">identificador do Utilizador a apagar</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {

            // verificar se foi fornecido um ID válido
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            // pesquisar pelo Utilizador, cujo ID foi fornecido
            Utilizadores utilizador = db.Utilizadores.Find(id);

            // verificar se o Utilizador foi encontrado
            if (utilizador == null)
            {
                // o Utilizador não existe
                // redirecionar para a página inicial
                return RedirectToAction("Index");
            }

            // apresentar os dados na View
            return View(utilizador);
        }

        // POST: Utilizadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utilizadores utilizador = db.Utilizadores.Find(id);
            try
            {
                // remove o Utilizador da BD
                db.Utilizadores.Remove(utilizador);
                // Commit
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", string.Format("Não é possível apagar o Utilizador nº {0} - {1}, porque há Sistemas ou perguntas associadas a ele..."+ex,
                                             id, utilizador.Nome)
                );
            }
            // se cheguei aqui é pq houve um problema
            // devolvo os dados do Utilizador à View
            return View(utilizador);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
