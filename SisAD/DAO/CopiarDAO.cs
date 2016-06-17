using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace SisAD.DAO
{
    public class CopiarDAO
    {
        public void Copiar(string txt_origem, string txt_destino)
        {            

            if (string.IsNullOrWhiteSpace(txt_origem) || string.IsNullOrWhiteSpace(txt_destino))
            {
                ConfigDAO readXML = new ConfigDAO();
                var data = readXML.ListConfig();

                foreach (var item in data)
                {
                    if (string.IsNullOrWhiteSpace(txt_origem))
                    {
                        txt_origem = item.origem;
                    }

                    if (string.IsNullOrWhiteSpace(txt_destino))
                    {
                        txt_destino = item.destino;
                    }
                }
            }

            //ARQUIVO
            bool direc = true;
            if (File.Exists(txt_destino))
            {
                File.Copy(txt_origem, txt_destino, true);
                direc = false;
            }
            else
            {
                string path = Path.GetDirectoryName(txt_destino);                

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                try
                {
                    File.Copy(txt_origem, txt_destino, true);
                    direc = false;
                }
                catch(Exception)
                {
                    direc = true;
                }                
            }

            //DIRETÓRIO
            if(direc)
            {
                DirectoryInfo dir = new DirectoryInfo(txt_origem);
                DirectoryInfo dir_dest = new DirectoryInfo(txt_destino);

                if (!Directory.Exists(txt_destino))
                {
                    Directory.CreateDirectory(txt_destino);
                }

                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(txt_destino, file.Name);
                    file.CopyTo(temppath, true);
                }

                foreach (DirectoryInfo subdir in dir.GetDirectories())
                {
                    Copiar(subdir.FullName, Path.Combine(dir_dest.FullName, subdir.Name));
                }            
            }            
        }
    }
}