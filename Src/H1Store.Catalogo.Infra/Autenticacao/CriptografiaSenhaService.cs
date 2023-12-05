using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace H1Store.Catalogo.Infra.Autenticacao
{
    public class CriptografiaSenhaService
    {

        public string CriptografarSenha(string senha)
        {
            // Gera um salt aleatório
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Cria o hash da senha com o salt
            string senhaCifrada = BCrypt.Net.BCrypt.HashPassword(senha, salt);

            return senhaCifrada;
        }

        public bool VerificarSenha(string senhaPlana, string senhaCifrada)
        {
            // Verifica se a senha plana corresponde à senha cifrada
            return BCrypt.Net.BCrypt.Verify(senhaPlana, senhaCifrada);
        }


    }
}
