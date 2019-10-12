using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// 
using System.Data;
using TreinoAspNetCore2_2.Uteis;
using System.ComponentModel.DataAnnotations;

namespace TreinoAspNetCore2_2.Models
{
    public class LoginModel
    {
        public String Id { get; set; }
        public String Nome { get; set; }


        [Required(ErrorMessage = "E-Mail obrigatório!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "E-Mail não é valido!")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Senha obrigatória!")]
        public String Senha { get; set; }

        // Polimorfismo para evitar Ataque SQL-Injection esta feita na classe DAL.
        public bool ValidarLogin()
        {
            String sql = $"Select Id, Nome From Login Where Email = '{Email}' And Senha = '{Senha}'";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDatatable(sql);

            if(dt.Rows.Count ==1)
            {
                Id = dt.Rows[0]["Id"].ToString();
                Nome = dt.Rows[0]["Nome"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

/*
 *  public bool ValidarLogin()
        {
            string sql = $"Select Id, Nome From Login Where Email ='{Email}' And Senha='{Senha}'";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDatatable(sql);
            if (dt.Rows.Count == 1)
            {
                Id = dt.Rows[0]["Id"].ToString();
                Nome = dt.Rows[0]["Nome"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
 */
