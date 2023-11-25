using H1Store.Catalogo.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1Store.Catalogo.Application.Interfaces
{
	public interface IProdutoService
	{
		IEnumerable<ProdutoViewModel> ObterTodos();
		Task<ProdutoViewModel> ObterPorId(Guid id);
		Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo);

		Task Adicionar(NovoProdutoViewModel produto);
		void Atualizar(ProdutoViewModel produto);
		Task Desativar(Guid id);
	}
}
