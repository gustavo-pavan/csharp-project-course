using Dominio;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao
{
    public class UsuarioApp
    {
        #region Método Salvar

        public string Salvar(Usuario usuario)
        {
            try
            {
                Contexto contexto = new Contexto();
                contexto.AdicionarParametro("@email", usuario.email);
                contexto.AdicionarParametro("@senha", usuario.senha);
                contexto.AdicionarParametro("@nome", usuario.nome);

                object retorno = contexto.Manipulacao(CommandType.StoredProcedure, "usp_salvarUsuario");

                if (retorno != null)
                {
                    return "Salvo";
                }
                else
                {
                    return "Erro ao salvar";
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region Método Update

        public string Update(Usuario usuario)
        {
            try
            {
                Contexto contexto = new Contexto();

                contexto.AdicionarParametro("@idUsuario", usuario.idUsuario);
                contexto.AdicionarParametro("@email", usuario.email);
                contexto.AdicionarParametro("@senha", usuario.senha);
                contexto.AdicionarParametro("@nome", usuario.nome);

                object retorno = contexto.Manipulacao(CommandType.StoredProcedure, "usp_updateUsuario");

                if (retorno != null)
                {
                    return "Alterado com sucesso";
                }
                else
                {
                    return "Erro ao alterar";
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region Método Excluir

        public string Excluir(Usuario usuario)
        {
            try
            {
                Contexto contexto = new Contexto();

                contexto.AdicionarParametro("@idUsuario", usuario.idUsuario);

                contexto.Manipulacao(CommandType.StoredProcedure, "usp_excluirUsuario");

                object user = contexto.DataReader(CommandType.StoredProcedure, "usp_listarUsuario");

                if (user == null)
                {
                    return "Dados deletados com sucesso";
                }
                else
                {
                    return "Erro ao apagar dados";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region TransforLista
        public List<Usuario> TransformaLista(SqlDataReader data)
        {
            try
            {
                var usuario = new List<Usuario>();

                while (data.Read())
                {
                    var objeto = new Usuario()
                    {
                        idUsuario = Convert.ToInt32(data["idUsuario"]),
                        nome = data["nome"].ToString(),
                        email = data["email"].ToString(),
                        senha = data["senha"].ToString()
                    };
                    usuario.Add(objeto);
                }
                return usuario;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Listar Usuario

        public Usuario ListarUsuario(Usuario usuario)
        {
            try
            {
                Contexto contexto = new Contexto();

                contexto.AdicionarParametro("@idUsuario", usuario.idUsuario);
                var user = contexto.DataReader(CommandType.StoredProcedure, "usp_listarUsuario");
                return TransformaLista(user).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
    }
}
