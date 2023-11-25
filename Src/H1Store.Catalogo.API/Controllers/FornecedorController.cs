using H1Store.Catalogo.Application.Interfaces;
using H1Store.Catalogo.Application.ViewModels;
using H1Store.Catalogo.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace H1Store.Catalogo.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class FornecedorController : ControllerBase
	{
		private readonly IFornecedorService _fornecedorService;

		public FornecedorController(IFornecedorService fornecedorService)
		{
			_fornecedorService = fornecedorService;
		}

		[HttpPost]
		[Route("Adicionar")]
		public async Task<IActionResult> Post(NovoFornecedorViewModel novoFornecedorViewModel)
		{
		
			await _fornecedorService.Adicionar(novoFornecedorViewModel);

			return Ok("Fornecedor cadastrado com sucesso");
		}

		[HttpGet]
		[Route("ObterTodos")]
		public async Task<IActionResult> Get()
		{
			return Ok(await _fornecedorService.ObterTodos());
		}

		[HttpGet]
		[Route("ObterPorCnpj/{cnpj}")]
		public async Task<IActionResult> Get(string cnpj)
		{
			var buscaCnpj = await _fornecedorService.ObterPorCnpj(cnpj);
			if(buscaCnpj == null) return NotFound("Cnpj Não encontrado");
			return Ok(buscaCnpj);
		}

	}
}
