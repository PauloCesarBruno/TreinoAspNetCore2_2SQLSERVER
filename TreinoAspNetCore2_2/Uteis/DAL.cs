using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//Adicionado:
using System.Data;
using System.Data.SqlClient;

namespace TreinoAspNetCore2_2.Uteis
{
    public class DAL
    {
        public static readonly String Server = "LAPTOP-PP6GUSQU";
        public static readonly String Database = "TreinoASPCore2_2";
        public static readonly String User = "sa";
        public static readonly String Password = "Paradoxo22";

        public static readonly String sqlString = $"Server ={Server}; Database = {Database}; Uid = {User}; Pwd = {Password}";

        public SqlConnection Conexao ()
        {
            return new SqlConnection(sqlString);
        }
        public void FecharConexao()
        {
            SqlConnection conn = Conexao();
            conn.Close();
        }
        private SqlParameterCollection Colecao = new SqlCommand().Parameters;

        public void LimparParametros()
        {
            Colecao.Clear();
        }
        public void AddParametros(String nome, Object valor)
        {
            Colecao.Add(new SqlParameter(nome, valor));
        }

        public Object ExecutaManipulacao(CommandType commandType, String Sp_Ou_Texto)
        {
            try
            {
                SqlConnection conn = Conexao();
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = commandType;
                cmd.CommandText = Sp_Ou_Texto;
                cmd.CommandTimeout = 3600;

                foreach(SqlParameter param in Colecao)
                {
                    cmd.Parameters.Add(new SqlParameter(param.ParameterName, param.Value));
                }
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ExecutaConsulta(CommandType commandType, String Sp_Ou_Texto)
        {
            try
            {
                SqlConnection conn = Conexao();
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = commandType;
                cmd.CommandText = Sp_Ou_Texto;
                cmd.CommandTimeout = 3600;

                foreach (SqlParameter param in Colecao)
                {
                    cmd.Parameters.Add(new SqlParameter(param.ParameterName, param.Value));
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //========================================================================//
        //Abaixo rotina para trabalhar com Login, como o polimorfismo por exemplo//

        private static SqlConnection conn;

        //Criação do Construtor 
        public DAL()
        {
            conn = new SqlConnection(sqlString);
            conn.Open();
        }

        // Espera um parâmetro do tipo string
        // contendo um conteudo MySQL do tipo SELECT
        public DataTable RetDatatable(String sql)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        // Polimorfismo para evitar problemas com Racker na Injeção de Dependência.
        public DataTable RetDatatable(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            cmd.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        // Método que permite executar comandos de CRUD
        public void ExecutarComandoSql(String sql)
        {
            SqlCommand Command = new SqlCommand(sql, conn);
            Command.ExecuteNonQuery();
        }
    }
}
