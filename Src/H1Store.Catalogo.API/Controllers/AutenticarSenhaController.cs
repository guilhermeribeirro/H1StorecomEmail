using H1Store.Catalogo.Data.Repository;
using H1Store.Catalogo.Infra.Autenticacao.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AutenticarController : ControllerBase
{
    private readonly AutenticarRepository autenticarrepository;

    public AutenticarController()
    {
        autenticarrepository = new AutenticarRepository();
    }

    [HttpPost("criar")]
    public IActionResult CriarUsuario([FromBody] SenhaAutenticar model)
    {
        autenticarrepository.CriarUsuario(model.Nome, model.Senha);
        return Ok();
    }

    [HttpPost("autenticar")]
    public IActionResult AutenticarUsuario([FromBody] SenhaAutenticar model)
    {
        bool autenticado = autenticarrepository.AutenticarUsuario(model.Nome, model.Senha);

        if (autenticado)
            return Ok("Usuário autenticado com sucesso!");
        else
            return Unauthorized("Falha na autenticação. Verifique seu nome de usuário e senha.");
    }
}
