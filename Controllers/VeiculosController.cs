using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using testeBlueFleet.Config;
using testeBlueFleet.Models;


namespace testeBlueFleet.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly VeiculoContexto _veiculoContexto;

        public VeiculosController(IOptions<ConfigDB> opcoes)
        {
            _veiculoContexto = new VeiculoContexto(opcoes);
        }

        public async Task<IActionResult> Index()
        {
            /* Retornando os dados e transformando uma Lista */
            return View(await _veiculoContexto.Veiculos.Find(a => true).ToListAsync());
        }

        // Incluido um novo Aviao
        [HttpGet]
        public IActionResult NovoVeiculo()
        {
            return View();
        }
        //
        [HttpPost]
        public async Task<IActionResult> NovoVeiculo(Veiculo veiculo)
        {
            // Gerando um valor no nosso Guid porque ele não atribiu o valor do Id automaticamente
            veiculo.VeiculoId = Guid.NewGuid();
            await _veiculoContexto.Veiculos.InsertOneAsync(veiculo);
            return RedirectToAction(nameof(Index));
        }
        ///**************************ATUALIZAR***************************////
        [HttpGet]
        public async Task<IActionResult> AtualizarVeiculo(Guid veiculoId)
        {
            Veiculo veiculo = await _veiculoContexto.Veiculos.Find(a => a.VeiculoId == veiculoId).FirstOrDefaultAsync();
            return View(veiculo);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarVeiculo(Veiculo veiculo)
        {
            await _veiculoContexto.Veiculos.ReplaceOneAsync(a => a.VeiculoId == veiculo.VeiculoId, veiculo);
            return RedirectToAction(nameof(Index));
        }
        ///**************************EXCLUIR***************************////
        [HttpPost]
        public async Task<IActionResult> ExcluirVeiculo(Guid veiculoId)
        {
            await _veiculoContexto.Veiculos.DeleteOneAsync(a => a.VeiculoId == veiculoId);
            return RedirectToAction(nameof(Index));
        }
    }
}
