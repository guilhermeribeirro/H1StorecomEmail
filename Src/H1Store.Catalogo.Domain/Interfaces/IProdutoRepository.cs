using H1Store.Catalogo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1Store.Catalogo.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        IEnumerable<Produto> ObterTodos();
        Task<Produto> ObterPorId(Guid id);
        Task<IEnumerable<Produto>> ObterPorCategoria(int codigo);

        Task Adicionar(Produto produto);
        Task Desativar(Produto produto);
        Task Atualizar(Produto produto);

        Task Reativar(Guid id);

        Task AlterarPreco(Guid id, decimal novoPreco);

        Task AtualizarEstoque(Guid id, int quantidade);

        IEnumerable<Produto> ObterPorNome(string palavraBuscada);







    }
}