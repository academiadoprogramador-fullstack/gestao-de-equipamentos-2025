using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Extensoes;
using GestaoDeEquipamentos.ConsoleApp.Models;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentos.ConsoleApp.Controllers;

[Route("equipamentos")]
public class ControladorEquipamento : Controller
{
    private ContextoDados contextoDados;
    private IRepositorioEquipamento repositorioEquipamento;
    private IRepositorioFabricante repositorioFabricante;

    public ControladorEquipamento()
    {
        contextoDados = new ContextoDados(true);
        repositorioEquipamento = new RepositorioEquipamentoEmArquivo(contextoDados);
        repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var fabricantes = repositorioFabricante.SelecionarRegistros();

        var cadastrarVM = new CadastrarEquipamentoViewModel(fabricantes);

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarEquipamentoViewModel cadastrarVM)
    {
        var fabricantes = repositorioFabricante.SelecionarRegistros();

        Equipamento equipamento = cadastrarVM.ParaEntidade(fabricantes);

        repositorioEquipamento.CadastrarRegistro(equipamento);

        var notificacaoVM = new NotificacaoViewModel(
            "Equipamento Cadastrado!",
            $"O registro \"{equipamento.Nome}\" foi cadastrado com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        var equipamentos = repositorioEquipamento.SelecionarRegistros();

        var visualizarVM = new VisualizarEquipamentosViewModel(equipamentos);

        return View(visualizarVM);
    }
}
