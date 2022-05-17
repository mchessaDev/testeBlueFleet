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
            return View(await _veiculoContexto.Veiculos.Find(a => true).ToListAsync());
        }

        [HttpGet]
        public IActionResult NovoVeiculo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NovoVeiculo(Veiculo veiculo)
        {
            veiculo.VeiculoId = Guid.NewGuid();
            await _veiculoContexto.Veiculos.InsertOneAsync(veiculo);
            return RedirectToAction(nameof(Index));
        }
        ///******************************************************************////
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
        
        [HttpPost]
        public async Task<IActionResult> ExcluirVeiculo(Guid veiculoId)
        {
            await _veiculoContexto.Veiculos.DeleteOneAsync(a => a.VeiculoId == veiculoId);
            return RedirectToAction(nameof(Index));
        }
    }
}
