using AutoMapper;
using H1Store.Catalogo.Application.Interfaces;
using H1Store.Catalogo.Application.ViewModels;
using H1Store.Catalogo.Domain.Entities;
using H1Store.Catalogo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace H1Store.Catalogo.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private IMapper _mapper;

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task Adicionar(NovoProdutoViewModel novoProdutoViewModel)
        {
            var novoProduto = _mapper.Map<Produto>(novoProdutoViewModel);
            await _produtoRepository.Adicionar(novoProduto);
        }

        public async Task AlterarPreco(Guid id, decimal novoPreco)
        {
            var produto = await _produtoRepository.ObterPorId(id);

            if (produto == null)
            {
                throw new ApplicationException("Produto não encontrado.");
            }

            produto.AlterarPreco(novoPreco);

            await _produtoRepository.Atualizar(produto);
        }

        public async Task Atualizar(ProdutoViewModel produtoViewModel)
        {
            var produto = _mapper.Map<Produto>(produtoViewModel);
            await _produtoRepository.Atualizar(produto);
        }

        public async Task AtualizarEstoque(Guid id, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(id);

            if (produto == null)
            {
                throw new ApplicationException("Produto não encontrado.");
            }

            produto.AtualizarEstoque(quantidade);

            await _produtoRepository.Atualizar(produto);
        }

        public async Task Desativar(Guid id)
        {
            var buscaProduto = await _produtoRepository.ObterPorId(id);

            if (buscaProduto == null)
            {
                throw new ApplicationException("Não é possível desativar um produto que não existe!");
            }

            buscaProduto.Desativar();
            await _produtoRepository.Desativar(buscaProduto);
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo)
        {
            var produtos = await _produtoRepository.ObterPorCategoria(codigo);
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos);
        }

        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            return _mapper.Map<ProdutoViewModel>(produto);
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterPorNome(string palavraBuscada)
        {
            var produtos = _produtoRepository.ObterPorNome(palavraBuscada);
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos);
        }

        public async Task Reativar(Guid id)
        {
            var produto = await _produtoRepository.ObterPorId(id);

            if (produto == null)
            {
                throw new ApplicationException("Produto não encontrado.");
            }

            produto.Reativar();
            await _produtoRepository.Atualizar(produto);
        }

        //public IEnumerable<ProdutoViewModel> ObterTodos()
        //{
        //    var produtos = _produtoRepository.ObterTodos();
        //    return _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos);
        //}

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            var produtos = _produtoRepository.ObterTodos();
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos);
        }
    }
}