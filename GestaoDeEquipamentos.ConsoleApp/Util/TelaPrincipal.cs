﻿using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

namespace GestaoDeEquipamentos.ConsoleApp.Util;

public class TelaPrincipal
{
    private char opcaoPrincipal;
    private IRepositorioFabricante repositorioFabricante;
    private IRepositorioEquipamento repositorioEquipamento;
    private IRepositorioChamado repositorioChamado;

    public TelaPrincipal()
    {
        this.repositorioFabricante = new RepositorioFabricanteEmArquivo();
        this.repositorioEquipamento = new RepositorioEquipamentoEmMemoria();
        this.repositorioChamado = new RepositorioChamadoEmMemoria();
    }

    public void ApresentarMenuPrincipal()
    {
        Console.Clear();

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("|        Gestão de Equipamentos        |");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        Console.WriteLine("1 - Controle de Fabricantes");
        Console.WriteLine("2 - Controle de Equipamentos");
        Console.WriteLine("3 - Controle de Chamados");
        Console.WriteLine("S - Sair");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        opcaoPrincipal = Console.ReadLine()[0];
    }

    public ITelaCrud ObterTela()
    {
        if (opcaoPrincipal == '1')
            return new TelaFabricante(repositorioFabricante);

        else if (opcaoPrincipal == '2')
            return new TelaEquipamento(repositorioEquipamento, repositorioFabricante);

        else if (opcaoPrincipal == '3')
            return new TelaChamado(repositorioChamado, repositorioEquipamento);

        return null;
    }
}