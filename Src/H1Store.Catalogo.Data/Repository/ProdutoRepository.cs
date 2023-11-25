using AutoMapper;
using H1Store.Catalogo.Data.Providers.MongoDb.Collections;
using H1Store.Catalogo.Data.Providers.MongoDb.Interfaces;
using H1Store.Catalogo.Domain.Entities;
using H1Store.Catalogo.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1Store.Catalogo.Data.Repository
{
	public class ProdutoRepository : IProdutoRepository
	{
		private readonly IMongoRepository<ProdutoCollection> _produtoRepository;
		private readonly IMapper _mapper;

		#region - Construtores
		public ProdutoRepository(IMongoRepository<ProdutoCollection> produtoRepository, IMapper mapper)
		{
			_produtoRepository = produtoRepository;
			_mapper = mapper;
		}
		#endregion
		#region - Funções
		public async Task Adicionar(Produto produto)
		{		
			await	_produtoRepository.InsertOneAsync(_mapper.Map<ProdutoCollection>(produto));
		}

		public void Atualizar(Produto produto)
		{
			throw new NotImplementedException();
		}

		public async Task Desativar(Produto produto)
		{

			var buscaProduto = _produtoRepository.FilterBy(filter => filter.CodigoId == produto.CodigoId);

			if (buscaProduto == null) throw new ApplicationException("Não é possível desativar um produto que não existe");

			var produtoCollection = _mapper.Map<ProdutoCollection>(produto);

			produtoCollection.Id = buscaProduto.FirstOrDefault().Id;
			
			await _produtoRepository.ReplaceOneAsync(produtoCollection);
		}

		public Task<IEnumerable<Produto>> ObterPorCategoria(int codigo)
		{
			throw new NotImplementedException();
		}

		public async Task<Produto> ObterPorId(Guid id)
		{

			var buscaProduto = _produtoRepository.FilterBy(filter =>filter.CodigoId == id);

			var produto = _mapper.Map<Produto>(buscaProduto.FirstOrDefault());
			
			return produto;
		}

		public IEnumerable<Produto> ObterTodos()
		{
			var produtoList = _produtoRepository.FilterBy(filter => true);

			return _mapper.Map<IEnumerable<Produto>>(produtoList);

		}
		#endregion

	}
}
