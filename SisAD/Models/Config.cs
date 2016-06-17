using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace SisAD.Models
{
    [Serializable]
    [XmlRoot("configuracao"), XmlType("email")]
    public class Config
    {
        public string origem { get; set; }
        public string destino { get; set; }
    }
}