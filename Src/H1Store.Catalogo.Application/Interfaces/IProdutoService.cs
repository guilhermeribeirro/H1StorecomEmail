using H1Store.Catalogo.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace H1Store.Catalogo.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoViewModel>> ObterTodos();
        Task<ProdutoViewModel> ObterPorId(Guid id);
        Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo);
        Task Adicionar(NovoProdutoViewModel produto);
        Task Atualizar(ProdutoViewModel produto);
        Task Desativar(Guid id);
        Task Reativar(Guid id);
        Task AtualizarEstoque(Guid id, int quantidade);
        Task AlterarPreco(Guid id, decimal novoPreco);
        Task<IEnumerable<ProdutoViewModel>> ObterPorNome(string palavraBuscada);
    }
}