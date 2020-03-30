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


       public List<ClienteModel> ListarTodosClientes()
        {
            try
            {
                List<ClienteModel> lista = new List<ClienteModel>();
                ClienteModel item;
                DAL objDAL = new DAL();
                objDAL.LimparParametros();
                // Usando Texto como permite a Classe DAL e uma View (VClientes) Criada no BD.
                DataTable dt = objDAL.ExecutaConsulta(CommandType.Text, "Select * From VClientes");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    item = new ClienteModel()
                    {
                        Id = dt.Rows[i]["Id"].ToString(),
                        Nome = dt.Rows[i]["Nome"].ToString(),
                        CPF = dt.Rows[i]["CPF"].ToString(),
                        DataNascimento = dt.Rows[i]["DataNascimento"].ToString(),
                        LimiteDeCredito = Convert.ToDecimal (dt.Rows[i]["LimiteDeCredito"])
                    };
                    lista.Add(item);
                    objDAL.FecharConexao();
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       // Método para carregar as informações para Edição
       public ClienteModel RetornarCliente(int? Id)
        {
            try
            {
                DAL objDAL = new DAL();
                ClienteModel item;
                objDAL.LimparParametros();
                objDAL.AddParametros("Id", Id);
                DataTable dt = objDAL.ExecutaConsulta(CommandType.StoredProcedure, "CarregarPorId");

                item = new ClienteModel()
                {
                    Id = dt.Rows[0]["Id"].ToString(),
                    Nome = dt.Rows[0]["Nome"].ToString(),
                    CPF = dt.Rows[0]["CPF"].ToString(),
                    DataNascimento = dt.Rows[0]["DataNascimento"].ToString(),
                    LimiteDeCredito = Convert.ToDecimal (dt.Rows[0]["LimiteDeCredito"])
                };
                objDAL.FecharConexao();
                return item;
            }
            catch  (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       // Método para Inserir ou Alterar (INSERT OU UPDATE)
       public void Gravar()
        {
            try
            {
                DAL objDAL = new DAL();

                if(Id != null)
                {
                    objDAL.LimparParametros();
                    objDAL.AddParametros("Id", Id);
                    objDAL.AddParametros("Nome", Nome);
                    objDAL.AddParametros("CPF", CPF);
                    objDAL.AddParametros("DataNascimento", DataNascimento);
                    objDAL.AddParametros("LimiteDeCredito", LimiteDeCredito);
                    String IdCliente = objDAL.ExecutaManipulacao(CommandType.StoredProcedure, "Alterar").ToString();
                    objDAL.FecharConexao();
                }
                else
                {
                    objDAL.LimparParametros();
                    //Não há necessidade do Id, pois o mesmo é chave primária.
                    objDAL.AddParametros("Nome", Nome);
                    objDAL.AddParametros("CPF", CPF);
                    objDAL.AddParametros("DataNascimento", DataNascimento);
                    objDAL.AddParametros("LimiteDeCredito", LimiteDeCredito);
                    String IdCliente = objDAL.ExecutaManipulacao(CommandType.Text, "Insert Into Clientes (Nome, CPF, DataNascimento, LimiteDeCredito) Values (@Nome, @CPF, @DataNascimento, @LimiteDeCredito)").ToString();
                    objDAL.FecharConexao();
                }
            }
            catch (Exception) // estou usando não só S.P., mas também Texto, se não seria-> catch (Exception ex).
            {
                //throw new Exception(ex.Message);
            }
        }

       // Método para Excluir Registro
       public void Excluir(int? Id)
        {
            try
            {
                DAL objDAL = new DAL();
                objDAL.LimparParametros();
                objDAL.AddParametros("Id", Id);
                String IdCliente = objDAL.ExecutaManipulacao(CommandType.StoredProcedure, "Excluir").ToString();
                objDAL.FecharConexao();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

//Não há necessidade do Id, pois o mesmo é chave primária.

// Usando Texo ao invés de S.Procedure, conforme permitido na classe DAL.

// estou usando não só S.P., mas também Texto, se não seria-> catch (Exception ex).

// Usando Texto como permite a Classe DAL e uma View (VClientes) Criada no BD.