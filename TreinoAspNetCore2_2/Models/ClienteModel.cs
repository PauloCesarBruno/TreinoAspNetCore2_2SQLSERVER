using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Importado
using System.Data;
using System.ComponentModel.DataAnnotations;
using TreinoAspNetCore2_2.Uteis;

namespace TreinoAspNetCore2_2.Models
{
    public class ClienteModel
    {

        public string Id { get; set; }

        [Required(ErrorMessage = "Informe o Nome do Cliente")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o CPF ou CNPJ do Cliente")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Informe a Data de Nascimento do Cliente")]      
        public string DataNascimento { get; set; }

        [Required(ErrorMessage = "Informe o Limite de Crédito do Cliente")]     
        [Range(1, 99999.99)] // Valor Mínimo ao Máximo
        [DataType(DataType.Currency)]
        [Display(Name = "Limite De Crédito")]
        public Decimal? LimiteDeCredito { get; set; }



        

        // Método para carregar 
        public List<ClienteModel> ListarTodosClientes()
        {
            List<ClienteModel> lista = new List<ClienteModel>();
            ClienteModel item;
            DAL objDal = new DAL();
            objDal.LimparParametros();
            DataTable dt = objDal.ExecutaConsulta(CommandType.StoredProcedure, "Carregar");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ClienteModel
                {
                    Id = dt.Rows[i]["Id"].ToString(),
                    Nome = dt.Rows[i]["Nome"].ToString(),
                    CPF = dt.Rows[i]["CPF"].ToString(),
                    DataNascimento = dt.Rows[i]["DataNascimento"].ToString(),
                    LimiteDeCredito = Convert.ToDecimal( dt.Rows[i]["LimiteDeCredito"].ToString())
                };
                objDal.FecharConexao();
                lista.Add(item);
            }

            return lista;
        }

        // Método para carregar as informações para Edição
        public ClienteModel RetornarCliente(int? Id)
        {
            ClienteModel item;
            DAL objDal = new DAL();
            objDal.LimparParametros();
            objDal.AddParametros("@Id", Id);
            DataTable dt = objDal.ExecutaConsulta(CommandType.StoredProcedure, "CarregarPorId");

            item = new ClienteModel
            {
                Id = dt.Rows[0]["Id"].ToString(),
                Nome = dt.Rows[0]["Nome"].ToString(),
                CPF = dt.Rows[0]["CPF"].ToString(),
                DataNascimento = dt.Rows[0]["DataNascimento"].ToString(),
                LimiteDeCredito = Convert.ToDecimal ( dt.Rows[0]["LimiteDeCredito"].ToString())
            };
            objDal.FecharConexao();
            return item;
        }

        // Método para Inserir ou Alterar (INSERT OU UPDATE)
        public void Gravar()
        {
            try
            {
                DAL objDAL = new DAL();
                string sql = string.Empty;

                if (Id != null)
                {
                    objDAL.LimparParametros();
                    objDAL.AddParametros("@Id", Id);
                    objDAL.AddParametros("@Nome", Nome);
                    objDAL.AddParametros("@CPF", CPF);
                    objDAL.AddParametros("@DataNascimento", DataNascimento);
                    objDAL.AddParametros("@LimiteDeCredito", LimiteDeCredito);
                    String IdCliente = objDAL.ExecutaManipulacao(CommandType.StoredProcedure, "Alterar").ToString();
                    objDAL.FecharConexao();
                }
                else
                {
                    objDAL.LimparParametros();
                    // Não Necessita Id, pois é Chave Primária
                    objDAL.AddParametros("@Nome", Nome);
                    objDAL.AddParametros("@CPF", CPF);
                    objDAL.AddParametros("@DataNascimento", DataNascimento);
                    objDAL.AddParametros("@LimiteDeCredito", LimiteDeCredito);
                    String IdCliente = objDAL.ExecutaManipulacao(CommandType.StoredProcedure, "Inserir").ToString();
                    objDAL.FecharConexao();
                }              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para Excluir Registro
        public void Excluir(int Id)
        {
            DAL objDAL = new DAL();
            objDAL.LimparParametros();
            objDAL.AddParametros("@Id", Id);
            String IdCliente = objDAL.ExecutaManipulacao(CommandType.StoredProcedure, "Excluir").ToString();
            objDAL.FecharConexao();
        }
    }
}
