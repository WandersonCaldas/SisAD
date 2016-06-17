using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SisAD.BO;
using SisAD.DAO;
using System.IO;
using SisAD.Models;

namespace SisAD.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            Sessao.ExisteSessao();

            return View();
        }

        public ActionResult Config()
        {
            Sessao.ExisteSessao();

            ConfigDAO readXML = new ConfigDAO();
            var data = readXML.ListConfig();
                
            string txt_origem = string.Empty;

            foreach (var item in data)
            {
                txt_origem = item.origem;
            }           

            return View(data.ToList());	
        }

        public ActionResult DiretorioOrigem()
        {
            Sessao.ExisteSessao();

            ConfigDAO readXML = new ConfigDAO();
            var data = readXML.ListConfig();

            string txt_origem = string.Empty;

            foreach (var item in data)
            {
                txt_origem = item.origem;
            }

            string[] diretorios = Directory.GetDirectories(txt_origem);            

            List<Diretorio> listaDiretorio = new List<Diretorio>();

            foreach (string dir in diretorios)
            {
                Diretorio diretorio = new Diretorio();
                diretorio.pasta = dir.ToString();
                diretorio.pasta_nome = dir.Replace(txt_origem, "").ToString();

                listaDiretorio.Add(diretorio);
            }

            DirectoryInfo Dir = new DirectoryInfo(txt_origem);

            FileInfo[] Files = Dir.GetFiles("*");
            foreach (FileInfo File in Files)
            {
                Diretorio diretorio2 = new Diretorio();
                diretorio2.arquivo = File.FullName;
                diretorio2.arquivo_nome = File.FullName.Replace(txt_origem, "").ToString();

                listaDiretorio.Add(diretorio2);
            }

            return View(listaDiretorio.ToList());
        }       

        public ActionResult SubDiretorio(string caminho)
        {
            Sessao.ExisteSessao();

            string[] diretorios = Directory.GetDirectories(caminho);

            List<Diretorio> listaDiretorio = new List<Diretorio>();

            foreach (string dir in diretorios)
            {
                Diretorio diretorio = new Diretorio();
                diretorio.pasta = dir.ToString();
                diretorio.pasta_nome = dir.Replace(caminho, "").ToString();

                listaDiretorio.Add(diretorio);
            }

            DirectoryInfo Dir = new DirectoryInfo(caminho);
            
            FileInfo[] Files = Dir.GetFiles("*");
            foreach (FileInfo File in Files)
            {
                Diretorio diretorio2 = new Diretorio();
                diretorio2.arquivo = File.FullName;
                diretorio2.arquivo_nome = File.FullName.Replace(caminho, "").ToString();

                listaDiretorio.Add(diretorio2);
            }

            return View(listaDiretorio.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Copiar()
        {
            Sessao.ExisteSessao();
            if (ModelState.IsValid)
            {
                string item = Request.Form["item"];
                string destino = string.Empty;

                string[] a_item = item.Split(',');

                ConfigDAO readXML = new ConfigDAO();
                var data = readXML.ListConfig();                

                foreach (var z in data)
                {

                    foreach(var j in a_item)
                    {
                        destino = j.Replace(z.origem, z.destino);

                        CopiarDAO fls = new CopiarDAO();
                        fls.Copiar(j, destino);      
                    }                    
                }                                          

                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}