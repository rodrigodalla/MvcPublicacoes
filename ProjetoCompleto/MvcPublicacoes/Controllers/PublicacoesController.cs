using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPublicacoes.Models;
using MvcPublicacoes.Persistencia;
using WebMatrix.WebData;
using System.Xml;
using System.Globalization;
using PagedList;

namespace MvcPublicacoes.Controllers
{
    public enum PubOrder { DtPublicacao, Titulo, Autor, Tipo, Instituicao, Situacao };
    public enum Order { asc, desc };

    public class PublicacoesController : Controller
    {
        private PubContext db = new PubContext();

        //
        // GET: /Publicacoes/

        public ActionResult Index(PubOrder pubOrder = PubOrder.DtPublicacao, Order order = Order.desc, int page = 1)
        {
            var publicacoes = db.Publicacoes.Include(p => p.Autor).Include(p => p.TipoPublicacao).Include(p => p.Instituicao).Include(p => p.Situacao);

                switch (pubOrder)
                {
                    case PubOrder.DtPublicacao:
                        if(order == Order.asc) {
                            publicacoes = publicacoes.OrderBy(p => p.DtPublicacao);
                        } else {
                            publicacoes = publicacoes.OrderByDescending(p => p.DtPublicacao);
                        } 
                    break;
                    case PubOrder.Titulo:
                        if (order == Order.asc) {
                            publicacoes = publicacoes.OrderBy(p => p.Titulo);
                        } else {
                            publicacoes = publicacoes.OrderByDescending(p => p.Titulo);
                        } 
                    break;
                    case PubOrder.Autor:
                        if (order == Order.asc) {
                            publicacoes = publicacoes.OrderBy(p => p.Autor.Nome);
                        } else {
                            publicacoes = publicacoes.OrderByDescending(p => p.Autor.Nome);
                        } 
                    break;
                    case PubOrder.Tipo:
                        if (order == Order.asc) {
                            publicacoes = publicacoes.OrderBy(p => p.TipoPublicacao.Descricao);
                        } else {
                            publicacoes = publicacoes.OrderByDescending(p => p.TipoPublicacao.Descricao);
                        } 
                    break;
                    case PubOrder.Instituicao:
                        if (order == Order.asc) {
                            publicacoes = publicacoes.OrderBy(p => p.Instituicao.Nome);
                        } else {
                            publicacoes = publicacoes.OrderByDescending(p => p.Instituicao.Nome);
                        }
                    break;
                    case PubOrder.Situacao:
                        if (order == Order.asc) {
                            publicacoes = publicacoes.OrderBy(p => p.Situacao.Descricao);
                        } else {
                            publicacoes = publicacoes.OrderByDescending(p => p.Situacao.Descricao);
                        }
                    break;
                }

            ViewBag.PubOrder = pubOrder;
            ViewBag.Order = order;

            int pageSize = 10;
            int pageNumber = page;
            return View(publicacoes.ToPagedList(pageNumber, pageSize));
            //return View(publicacoes.ToList());
        }

        //
        // GET: /Publicacoes/Details/5

        public ActionResult Details(int id = 0)
        {
            Publicacao publicacao = db.Publicacoes.Find(id);
            if (publicacao == null)
            {
                return HttpNotFound();
            }
            return View(publicacao);
        }

        //
        // GET: /Publicacoes/Create
        [Authorize(Roles = "Administrador,Pesquisador")]
        public ActionResult Create()
        {

            PubContext db = new PubContext();
            int userId = WebSecurity.GetUserId(User.Identity.Name);
            Publicacao publicacao = new Publicacao();
            publicacao.IdAutor = db.Autores.Single(a => a.UserId.Equals(userId)).IdAutor;
            publicacao.DtPublicacao = DateTime.Now;

            ViewBag.Autores = db.Autores;
            ViewBag.TiposPublicacoes = db.TiposPublicacoes;
            ViewBag.Instituicoes = db.Instituicoes;
            ViewBag.Situacoes = db.Situacoes;
            return View(publicacao);
        }

        //
        // POST: /Publicacoes/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Pesquisador")]
        public ActionResult Create(Publicacao publicacao)
        {

            //Pega os dados do local e transforma em coordenadas
            XmlDocument doc = new XmlDocument();
            doc.Load("http://maps.googleapis.com/maps/api/geocode/xml?address=" + publicacao.Localizacao + "&sensor=false");
            try {
                publicacao.Latitude = double.Parse(doc.SelectSingleNode("GeocodeResponse/result/geometry/location/lat").InnerText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                publicacao.Longitude = double.Parse(doc.SelectSingleNode("GeocodeResponse/result/geometry/location/lng").InnerText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            } catch (Exception) {
                ModelState.AddModelError("Localizacao", "Localização inválida.");
            }

            if (ModelState.IsValid)
            {
                db.Publicacoes.Add(publicacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Autores = db.Autores;
            ViewBag.TiposPublicacoes = db.TiposPublicacoes;
            ViewBag.Instituicoes = db.Instituicoes;
            ViewBag.Situacoes = db.Situacoes;
            return View(publicacao);
        }

        //
        // GET: /Publicacoes/Edit/5

        [Authorize(Roles = "Administrador,Pesquisador")]
        public ActionResult Edit(int id = 0)
        {
            Publicacao publicacao = db.Publicacoes.Find(id);
            if (publicacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.TiposPublicacoes = db.TiposPublicacoes;
            ViewBag.Instituicoes = db.Instituicoes;
            ViewBag.Situacoes= db.Situacoes;
            return View(publicacao);
        }

        //
        // POST: /Publicacoes/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Pesquisador")]
        public ActionResult Edit(Publicacao publicacao)
        {
            //Pega os dados do local e transforma em coordenadas
            XmlDocument doc = new XmlDocument();
            doc.Load("http://maps.googleapis.com/maps/api/geocode/xml?address=" + publicacao.Localizacao + "&sensor=false");
            try {
                publicacao.Latitude = double.Parse(doc.SelectSingleNode("GeocodeResponse/result/geometry/location/lat").InnerText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                publicacao.Longitude = double.Parse(doc.SelectSingleNode("GeocodeResponse/result/geometry/location/lng").InnerText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            } catch (Exception) {
                ModelState.AddModelError("Localizacao", "Localização inválida.");                
            }

            if (ModelState.IsValid)
            {
                db.Entry(publicacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TiposPublicacoes = db.TiposPublicacoes;
            ViewBag.Instituicoes = db.Instituicoes;
            ViewBag.Situacoes = db.Situacoes;
            return View(publicacao);
        }

        //
        // GET: /Publicacoes/Delete/5

        [Authorize(Roles = "Administrador,Pesquisador")]
        public ActionResult Delete(int id = 0)
        {
            Publicacao publicacao = db.Publicacoes.Find(id);
            if (publicacao == null)
            {
                return HttpNotFound();
            }
            return View(publicacao);
        }

        //
        // POST: /Publicacoes/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Pesquisador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Publicacao publicacao = db.Publicacoes.Find(id);
            db.Publicacoes.Remove(publicacao);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Publicacoes/Mapa

        public ActionResult Mapa()
        {
            var query = (from t in db.Publicacoes 
                         group t by new { t.Latitude, t.Longitude }
                             into grp
                             let localiz = grp.Max(x => x.Localizacao)
                             select new
                             {
                                 grp.Key.Latitude,
                                 grp.Key.Longitude,
                                 qtd = grp.Count(),
                                 localizacao = localiz
                             }).ToList();

            List<QtdPublicacoesLocal> model = new List<QtdPublicacoesLocal>();
            foreach (var item in query)
            {
                QtdPublicacoesLocal q = new QtdPublicacoesLocal();
                q.Latitude = item.Latitude;
                q.Longitude = item.Longitude;
                q.quantidade = item.qtd;
                q.Localizacao = item.localizacao; 
                model.Add(q); 
            }

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}