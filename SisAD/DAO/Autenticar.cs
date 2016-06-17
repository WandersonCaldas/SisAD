using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SisAD.Models;

namespace SisAD.DAO
{
    public class Autenticar
    {
        public int AutenticarUsuario(Usuario objUsuario) 
        {
            int retorno = 0;

            sislogin.Autentica WsLogin = new sislogin.Autentica();
            var obj = WsLogin.AutenticarUsuario(objUsuario.txt_email, objUsuario.txt_senha);

            if (obj.status == "SUCESSO")
            {
                for (int i = 0; i < obj.Usuarios.Length; i++)
                {
                    retorno = obj.Usuarios[i].id;
                }
            }            

            return retorno;
        }        

        public string NomeUsuario(Usuario objUsuario)
        {
            string retorno = "";

            sislogin.Autentica WsLogin = new sislogin.Autentica();
            var obj = WsLogin.ListarUsuario(objUsuario.id.ToString(), "", "");

            if (obj.status == "SUCESSO")
            {
                for (int i = 0; i < obj.Usuarios.Length; i++)
                {
                    retorno = obj.Usuarios[i].txt_nome;
                }
            }

            return retorno;
        }
    }
}