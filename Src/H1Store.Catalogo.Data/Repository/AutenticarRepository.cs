using H1Store.Catalogo.Infra.Autenticacao;
using H1Store.Catalogo.Infra.Autenticacao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace H1Store.Catalogo.Data.Repository
{
    public class AutenticarRepository
    {
        private CriptografiaSenhaService criptografiaService;
        private List<SenhaAutenticar> usuarios;

        private const string CaminhoArquivoJson = "autenticacao.json";

        public AutenticarRepository()
        {
            criptografiaService = new CriptografiaSenhaService();
            usuarios = LerUsuariosDoArquivo();
        }

        private List<SenhaAutenticar> LerUsuariosDoArquivo()
        {
            if (File.Exists(CaminhoArquivoJson))
            {
                string json = File.ReadAllText(CaminhoArquivoJson);
                return JsonConvert.DeserializeObject<List<SenhaAutenticar>>(json);
            }

            return new List<SenhaAutenticar>();
        }

        private void SalvarUsuariosNoArquivo()
        {
            string json = JsonConvert.SerializeObject(usuarios);
            File.WriteAllText(CaminhoArquivoJson, json);
        }

        public void CriarUsuario(string nome, string senha)
        {
            string senhaCifrada = criptografiaService.CriptografarSenha(senha);
            usuarios.Add(new SenhaAutenticar { Nome = nome, Senha = senhaCifrada });
            SalvarUsuariosNoArquivo();
        }

        public bool AutenticarUsuario(string nome, string senha)
        {
            var usuario = usuarios.Find(u => u.Nome == nome);

            if (usuario != null)
            {
                return criptografiaService.VerificarSenha(senha, usuario.Senha);
            }

            return false;
        }


    }
}
