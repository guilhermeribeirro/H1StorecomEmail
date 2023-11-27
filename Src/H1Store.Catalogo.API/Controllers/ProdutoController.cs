using H1Store.Catalogo.Application.Interfaces;
using H1Store.Catalogo.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace H1Store.Catalogo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpPost]
        [Route("Adicionar")]
        public async Task<IActionResult> AdicionarProduto([FromBody] NovoProdutoViewModel novoProdutoViewModel)
        {
            await _produtoService.Adicionar(novoProdutoViewModel);
            return Ok();
        }

        [HttpPut]
        [Route("Atualizar/{id}")]
        public async Task<IActionResult> AtualizarProduto([FromBody] ProdutoViewModel produtoViewModel)
        {
            await _produtoService.Atualizar(produtoViewModel);
            return Ok();
        }

        [HttpPut]
        [Route("Desativar/{id}")]
        public async Task<IActionResult> DesativarProduto(Guid id)
        {
            await _produtoService.Desativar(id);
            return Ok("Produto desativado com sucesso!!");
        }

        [HttpGet]
        [Route("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var produtos = _produtoService.ObterTodos();
            return Ok(produtos);
        }

        [HttpGet]
        [Route("ObterPorId/{id}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var produto = await _produtoService.ObterPorId(id);
            if (produto != null)
            {
                return Ok(produto);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("ObterPorNome")]
        public IActionResult ObterPorNome([FromQuery] string palavraBuscada)
        {
            var produtos = _produtoService.ObterPorNome(palavraBuscada);
            return Ok(produtos);
        }

        [HttpPut]
        [Route("Reativar/{id}")]
        public async Task<IActionResult> ReativarProduto(Guid id)
        {
            await _produtoService.Reativar(id);
            return Ok("Produto reativado com sucesso!!");
        }

        [HttpPut]
        [Route("AtualizarEstoque/{id}")]
        public async Task<IActionResult> AtualizarEstoqueProduto(Guid id, [FromQuery] int quantidade)
        {
            await _produtoService.AtualizarEstoque(id, quantidade);
            return Ok("Estoque atualizado com sucesso!!");
        }

        [HttpPut]
        [Route("AlterarPreco/{id}")]
        public async Task<IActionResult> AlterarPrecoProduto(Guid id, [FromQuery] decimal novoPreco)
        {
            await _produtoService.AlterarPreco(id, novoPreco);
            return Ok("Preço alterado com sucesso!!");
        }
    }
}