using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using GestaoDeEquipamentos.ConsoleApp.Util;
using System.Text;

namespace GestaoDeEquipamentos.ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        // criar um servidor web
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        WebApplication app = builder.Build();

        // mapeamento de rotas

        app.MapGet("/fabricantes/editar/{id:int}", ExibirFormularioEdicaoFabricante);
        app.MapPost("/fabricantes/editar/{id:int}", EditarFabricante);

        app.MapGet("/fabricantes/excluir/{id:int}", ExibirFormularioExclusaoFabricante);
        app.MapPost("/fabricantes/excluir/{id:int}", ExcluirFabricante);

        app.MapControllers();

        app.Run();
    }

    // Controlador de Fabricantes


    static Task ExibirFormularioEdicaoFabricante(HttpContext context)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);

        int id = Convert.ToInt32(context.GetRouteValue("id"));

        Fabricante fabricanteSelecionado = repositorioFabricante.SelecionarRegistroPorId(id);

        string conteudo = File.ReadAllText("ModuloFabricante/Html/Editar.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#id#", id.ToString());
        sb.Replace("#nome#", fabricanteSelecionado.Nome);
        sb.Replace("#email#", fabricanteSelecionado.Email);
        sb.Replace("#telefone#", fabricanteSelecionado.Telefone);

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    static Task EditarFabricante(HttpContext context)
    {
        int id = Convert.ToInt32(context.GetRouteValue("id"));

        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);

        string nome = context.Request.Form["nome"].ToString();
        string email = context.Request.Form["email"].ToString();
        string telefone = context.Request.Form["telefone"].ToString();

        Fabricante fabricanteAtualizado = new Fabricante(nome, email, telefone);

        repositorioFabricante.EditarRegistro(id, fabricanteAtualizado);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#mensagem#", $"O registro \"{fabricanteAtualizado.Nome}\" foi editado com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    static Task ExibirFormularioExclusaoFabricante(HttpContext context)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);

        int id = Convert.ToInt32(context.GetRouteValue("id"));

        Fabricante fabricanteSelecionado = repositorioFabricante.SelecionarRegistroPorId(id);

        string conteudo = File.ReadAllText("ModuloFabricante/Html/Excluir.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#id#", id.ToString());
        sb.Replace("#fabricante#", fabricanteSelecionado.Nome);

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    static Task ExcluirFabricante(HttpContext context)
    {
        int id = Convert.ToInt32(context.GetRouteValue("id"));

        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);

        repositorioFabricante.ExcluirRegistro(id);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#mensagem#", $"O registro foi excluído com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }
}
