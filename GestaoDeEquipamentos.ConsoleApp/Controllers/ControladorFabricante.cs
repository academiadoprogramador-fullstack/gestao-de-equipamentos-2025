using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace GestaoDeEquipamentos.ConsoleApp.Controllers;

[Route("fabricantes")]
public class ControladorFabricante : Controller
{
    [HttpGet("cadastrar")]
    public IActionResult ExibirFormularioCadastroFabricante()
    {
        return View("Cadastrar");
    }

    [HttpPost("cadastrar")]
    public IActionResult CadastrarFabricante(
        [FromForm] string nome,
        [FromForm] string email,
        [FromForm] string telefone
    )
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);
    
        Fabricante novoFabricante = new Fabricante(nome, email, telefone);

        repositorioFabricante.CadastrarRegistro(novoFabricante);

        ViewBag.Mensagem = $"O registro \"{novoFabricante.Nome}\" foi cadastrado com sucesso!";

        return View("Notificacao");
    }

    [HttpGet("editar/{id:int}")]
    public IActionResult ExibirFormularioEdicaoFabricante([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);

        Fabricante fabricanteSelecionado = repositorioFabricante.SelecionarRegistroPorId(id);

        ViewBag.Fabricante = fabricanteSelecionado;

        return View("Editar");
    }

    [HttpPost("editar/{id:int}")]
    public IActionResult EditarFabricante(
        [FromRoute] int id,
        [FromForm] string nome,
        [FromForm] string email,
        [FromForm] string telefone
    )
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);

        Fabricante fabricanteAtualizado = new Fabricante(nome, email, telefone);

        repositorioFabricante.EditarRegistro(id, fabricanteAtualizado);

        ViewBag.Mensagem = $"O registro \"{fabricanteAtualizado.Nome}\" foi editado com sucesso!";

        return View("Notificacao");
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult ExibirFormularioExclusaoFabricante([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);

        Fabricante fabricanteSelecionado = repositorioFabricante.SelecionarRegistroPorId(id);

        ViewBag.Fabricante = fabricanteSelecionado;

        return View("Excluir");
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult ExcluirFabricante([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);

        repositorioFabricante.ExcluirRegistro(id);

        ViewBag.Mensagem = $"O registro foi excluído com sucesso!";

        return View("Notificacao");
    }

    [HttpGet("visualizar")]
    public IActionResult VisualizarFabricantes()
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);

        List<Fabricante> fabricantes = repositorioFabricante.SelecionarRegistros();

        ViewBag.Fabricantes = fabricantes;

        return View("Visualizar");
    }
}