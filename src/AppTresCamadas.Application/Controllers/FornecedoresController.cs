using Microsoft.AspNetCore.Mvc;
using AppTresCamadas.Application.ViewModels;
using AppTresCamadas.Business.Interfaces;
using AutoMapper;
using AppTresCamadas.Business.Models;

namespace AppTresCamadas.Application.Controllers
{
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public FornecedoresController(
            IFornecedorRepository fornecedorRepository,
            IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var fornecedoresViewModel = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return View(fornecedoresViewModel);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorViewModel(id);

            if (fornecedorViewModel == null) return NotFound();

            return View(fornecedorViewModel);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (ModelState.IsValid is false) View(fornecedorViewModel);

            var fornecedorModel = _mapper.Map<FornecedorModel>(fornecedorViewModel);
            await _fornecedorRepository.Adicionar(fornecedorModel);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = ObterFornecedorViewModel(id);

            if (fornecedorViewModel is null)
                return NotFound();

            return View(fornecedorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return NotFound();

            if (ModelState.IsValid is false) return View(fornecedorViewModel);

            var fornecedorModel = _mapper.Map<FornecedorModel>(fornecedorViewModel);
            await _fornecedorRepository.Atualizar(fornecedorModel);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = ObterFornecedorViewModel(id);

            if (fornecedorViewModel is null) return NotFound();

            return View(fornecedorViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedorViewModel = ObterFornecedorViewModel(id);

            if (fornecedorViewModel is null) return NotFound();

            await _fornecedorRepository.Remover(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<FornecedorViewModel> ObterFornecedorViewModel(Guid id) =>
            _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
    }
}
