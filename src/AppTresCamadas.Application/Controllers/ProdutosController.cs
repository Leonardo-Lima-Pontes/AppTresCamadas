using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppTresCamadas.Application.Data;
using AppTresCamadas.Application.ViewModels;
using AppTresCamadas.Business.Interfaces;
using AutoMapper;
using AppTresCamadas.Business.Models;

namespace AppTresCamadas.Application.Controllers
{
    public class ProdutosController : BaseController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public ProdutosController(
            IProdutoRepository produtoRepository,
            IMapper mapper,
            IFornecedorRepository fornecedorRepository)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task<IActionResult> Index()
        {
            var produtosViewModel = _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos());
            return View(produtosViewModel);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var produtoViewModel = ObterProdutoViewModel(id);

            if (produtoViewModel is null) return NotFound();

            return View(produtoViewModel);
        }

        public async Task<IActionResult> Create() => View(await ObterNovoProdutoComFornecedores());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid is false) return View(produtoViewModel);

            var produtoModel = _mapper.Map<ProdutoModel>(produtoViewModel);
            await _produtoRepository.Adicionar(produtoModel);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var produtoViewModel = ObterProdutoViewModel(id);

            if (produtoViewModel is null) return NotFound();

            return View(produtoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id) return NotFound();

            if (ModelState.IsValid is false) return View(produtoViewModel);

            var produtoModel = _mapper.Map<ProdutoModel>(produtoViewModel);
            await _produtoRepository.Adicionar(produtoModel);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var produtoViewModel = await _produtoRepository.ObterPorId(id);

            if (produtoViewModel is null) return NotFound();

            return View(produtoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produtoViewModel = await _produtoRepository.ObterPorId(id);

            if (produtoViewModel is null) NotFound();

            await _produtoRepository.Remover(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<ProdutoViewModel> ObterProdutoViewModel(Guid id)
        {
            var produtoViewModel = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));
            produtoViewModel.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produtoViewModel;
        }

        private async Task<ProdutoViewModel> ObterNovoProdutoComFornecedores()
        {
            var produtoViewModel = new ProdutoViewModel();
            produtoViewModel.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produtoViewModel;
        }
    }
}
