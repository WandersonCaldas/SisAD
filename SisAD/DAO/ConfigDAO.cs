using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SisAD.Models;

namespace SisAD.DAO
{
    public class ConfigDAO   
    {
        public List<Config> ListConfig()
        {
            var xmlData = HttpContext.Current.Server.MapPath("~/App_Data/Configuracao.xml");
            DataSet ds = new DataSet();
            ds.ReadXml(xmlData);
            var configuracao = new List<Config>();
            configuracao = (from rows in ds.Tables[0].AsEnumerable()
                            select new Config
                            {
                                origem = rows[0].ToString(),
                                destino = rows[1].ToString()                                
                            }).ToList();
            return configuracao;
        }
    }
}