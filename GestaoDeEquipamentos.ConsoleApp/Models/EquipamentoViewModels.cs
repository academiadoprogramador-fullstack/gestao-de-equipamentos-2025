using GestaoDeEquipamentos.ConsoleApp.Extensoes;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

namespace GestaoDeEquipamentos.ConsoleApp.Models;

public class VisualizarEquipamentosViewModel
{
    public List<DetalhesEquipamentoViewModel> Registros { get; set; }

    public VisualizarEquipamentosViewModel(List<Equipamento> equipamentos)
    {
        Registros = new List<DetalhesEquipamentoViewModel>();

        foreach (Equipamento e in equipamentos)
        {
            DetalhesEquipamentoViewModel detalhesVM = e.ParaDetalhesVM();

            Registros.Add(detalhesVM);
        }
    }
}

public class DetalhesEquipamentoViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal PrecoAquisicao { get; set; }
    public DateTime DataFabricacao { get; set; }
    public string NomeFabricante { get; set; }

    public DetalhesEquipamentoViewModel(
        int id,
        string nome,
        decimal precoAquisicao,
        DateTime dataFabricacao,
        string nomeFabricante
    )
    {
        Id = id;
        Nome = nome;
        PrecoAquisicao = precoAquisicao;
        DataFabricacao = dataFabricacao;
        NomeFabricante = nomeFabricante;
    }

    public override string ToString()
    {
        return $"Id: {Id} - Nome: {Nome} - Fabricante: {NomeFabricante} - Preço de Aquisição: {PrecoAquisicao:C2} - Data de Fabricação: {DataFabricacao:d}";
    }
}