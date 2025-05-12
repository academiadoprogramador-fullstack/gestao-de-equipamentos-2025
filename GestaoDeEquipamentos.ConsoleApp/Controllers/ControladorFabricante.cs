using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace GestaoDeEquipamentos.ConsoleApp.Controllers;

[Route("fabricantes")]
public class ControladorFabricante : Controller
{
    [HttpGet("cadastrar")]
    public Task ExibirFormularioCadastroFabricante()
    {
        string conteudo = System.IO.File.ReadAllText("ModuloFabricante/Html/Cadastrar.html");

        return HttpContext.Response.WriteAsync(conteudo);
    }

    [HttpPost("cadastrar")]
    public Task CadastrarFabricante()
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);

        string nome = HttpContext.Request.Form["nome"].ToString();
        string email = HttpContext.Request.Form["email"].ToString();
        string telefone = HttpContext.Request.Form["telefone"].ToString();

        Fabricante novoFabricante = new Fabricante(nome, email, telefone);

        repositorioFabricante.CadastrarRegistro(novoFabricante);

        string conteudo = System.IO.File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#mensagem#", $"O registro \"{novoFabricante.Nome}\" foi cadastrado com sucesso!");

        string conteudoString = sb.ToString();

        return HttpContext.Response.WriteAsync(conteudoString);
    }

    [HttpGet("visualizar")]
    public IActionResult VisualizarFabricantes()
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);

        string conteudo = System.IO.File.ReadAllText("ModuloFabricante/Html/Visualizar.html");

        StringBuilder stringBuilder = new StringBuilder(conteudo);

        List<Fabricante> fabricantes = repositorioFabricante.SelecionarRegistros();

        foreach (Fabricante f in fabricantes)
        {
            string itemLista = $"<li>{f.ToString()} / <a href=\"/fabricantes/editar/{f.Id}\">Editar</a> / <a href=\"/fabricantes/excluir/{f.Id}\">Excluir</a> </li> #fabricante#";

            stringBuilder.Replace("#fabricante#", itemLista);
        }

        stringBuilder.Replace("#fabricante#", "");

        string conteudoString = stringBuilder.ToString();

        return Content(conteudoString, "text/html");
    }
}
