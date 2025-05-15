using GestaoDeEquipamentos.ConsoleApp.Models;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

namespace GestaoDeEquipamentos.ConsoleApp.Extensoes;

public static class EquipamentoExtensions
{
    public static DetalhesEquipamentoViewModel ParaDetalhesVM(this Equipamento equipamento)
    {
        return new DetalhesEquipamentoViewModel(
            equipamento.Id,
            equipamento.Nome,
            equipamento.PrecoAquisicao,
            equipamento.DataFabricacao,
            equipamento.Fabricante.Nome
        );
    }
}
