using H1Store.Catalogo.Data.Providers.MongoDb.Collections;
using H1Store.Catalogo.Data.Providers.MongoDb.Interfaces;
using H1Store.Catalogo.Domain.Entities;
using H1Store.Catalogo.Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;

namespace H1Store.Catalogo.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IMongoRepository<ProdutoCollection> _produtoRepository;
        private readonly IMapper _mapper;
        private Guid id;

        public ProdutoRepository(IMongoRepository<ProdutoCollection> produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task Adicionar(Produto produto)
        {
            await _produtoRepository.InsertOneAsync(_mapper.Map<ProdutoCollection>(produto));
        }

        public async Task Atualizar(Produto produto)
        {
            //var filter = Builders<ProdutoCollection>.Filter.Eq(p => p.CodigoId, produto.CodigoId);
            var mapped = _mapper.Map<ProdutoCollection>(produto);
            await _produtoRepository.ReplaceOneAsync(mapped);
        }

        public async Task Desativar(Produto produto)
        {

            var produtoDesativar = await _produtoRepository.FindOneAsync(p => p.CodigoId == produto.CodigoId);
            if (produtoDesativar != null)
            {
                produtoDesativar.Ativo = false;
                await _produtoRepository.ReplaceOneAsync(produtoDesativar);
            }
        }

        public Task<IEnumerable<Produto>> ObterPorCategoria(int codigo)
        {
            throw new NotImplementedException();
        }

        public async Task<Produto> ObterPorId(Guid id)
        {

            var produtoCollection = await _produtoRepository.FindOneAsync(p => p.CodigoId == id);
            return _mapper.Map<Produto>(produtoCollection);
        }

        public IEnumerable<Produto> ObterTodos()
        {

            var produtoList = _produtoRepository.FilterBy(p => true);
            return _mapper.Map<IEnumerable<Produto>>(produtoList);
        }

        public async Task Reativar(Guid id)
        {
            var atualizacao = Builders<ProdutoCollection>.Update.Set(p => p.Ativo, true);
            await _produtoRepository.UpdateOneAsync(p => p.CodigoId == id, atualizacao);
        }

        public async Task AlterarPreco(Guid id, decimal novoPreco)
        {
            var atualizacao = Builders<ProdutoCollection>.Update.Set(p => p.Valor, novoPreco);
            await _produtoRepository.UpdateOneAsync(p => p.CodigoId == id, atualizacao);
        }



        public async Task AtualizarEstoque(Guid id, int quantidade)
        {
            var produtoCollection = await _produtoRepository.FindOneAsync(p => p.CodigoId == id);
            if (produtoCollection != null)
            {
                if (produtoCollection.QuantidadeEstoque + quantidade >= 0)
                {
                    produtoCollection.QuantidadeEstoque += quantidade;
                    await _produtoRepository.ReplaceOneAsync(produtoCollection);
                }
            }
        }

        public IEnumerable<Produto> ObterPorNome(string palavraBuscada)
        {

            var produtos = _produtoRepository.FilterBy(p => p.Nome.Contains(palavraBuscada));
            return _mapper.Map<IEnumerable<Produto>>(produtos);
        }
    }
}