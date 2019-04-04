using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace Dominio
{
    public class Contexto
    {
        private SqlConnection MinhaConexao()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["StringConexao"].ConnectionString);
        }

        private SqlParameterCollection colecao = new SqlCommand().Parameters;

        public void AdicionarParametro(string parametro, object valor)
        {
            colecao.Add(new SqlParameter(parametro, valor));
        }

        public object Manipulacao(CommandType command, string procedure)
        {
            
            try
            {
                SqlConnection conexao = MinhaConexao();
                conexao.Open();

                SqlCommand cmd = conexao.CreateCommand();

                cmd.CommandText = procedure;
                cmd.CommandType = command;
                cmd.CommandTimeout = 90;

                foreach (SqlParameter parametro in colecao)
                {
                    cmd.Parameters.Add(new SqlParameter(parametro.ParameterName, parametro.Value));
                }

                return cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public SqlDataReader DataReader(CommandType command, string procedure)
        {

            try
            {
                SqlConnection conexao = MinhaConexao();
                conexao.Open();

                SqlCommand cmd = conexao.CreateCommand();

                cmd.CommandText = procedure;
                cmd.CommandType = command;
                cmd.CommandTimeout = 90;

                foreach (SqlParameter parametro in colecao)
                {
                    cmd.Parameters.Add(new SqlParameter(parametro.ParameterName, parametro.Value));
                }

                return cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
