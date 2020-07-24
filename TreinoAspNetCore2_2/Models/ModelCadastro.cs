using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TreinoAspNetCore2_2.Uteis;

namespace TreinoAspNetCore2_2.Models
{
    public class ModelCadastro
    {
        public String Id { get; set; }

        [Required(ErrorMessage ="Campo Obrigatório !")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório !")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="O Email não tem um formato Valido !")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório !")]
        [DataType(DataType.Password)]
        [Display(Name ="Senha")]
        public String Senha { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório !")]
        [DataType(DataType.Password)]
        [Display(Name ="Confirme a Senha !")]
        [Compare("Senha", ErrorMessage ="A Senha não confere !")]
        public String ConfSenha { get; set; }

        public void Inserir()
        {
            try
            {
                if (Id == null)
                {
                    DAL objDAL = new DAL();
                    objDAL.LimparParametros();
                    objDAL.AddParametros("@Nome", Nome);
                    objDAL.AddParametros("@Email", Email);
                    objDAL.AddParametros("@Senha", Senha);
                    String IdLogado = objDAL.ExecutaManipulacao(CommandType.Text, "Insert Into Login(Nome, Email, Senha) Values (@Nome, @Email, @Senha)").ToString();
                    objDAL.FecharConexao();
                }
            }
            catch
            {
                //
            }
        }
    }
}
