﻿
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

namespace GestaoDeEquipamentos.ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        TelaEquipamento telaEquipamento = new TelaEquipamento();

        RepositorioEquipamento repositorioEquipamento = telaEquipamento.repositorioEquipamento;

        TelaChamado telaChamado = new TelaChamado(repositorioEquipamento);

        TelaPrincipal telaPrincipal = new TelaPrincipal();

        while (true)
        {
            char opcaoPrincipal = telaPrincipal.ApresentarMenuPrincipal();

            if (opcaoPrincipal == '1')
            {
                char opcaoEscolhida = telaEquipamento.ApresentarMenu();

                switch (opcaoEscolhida)
                {
                    case '1': telaEquipamento.CadastrarEquipamento(); break;

                    case '2': telaEquipamento.EditarEquipamento(); break;

                    case '3': telaEquipamento.ExcluirEquipamento(); break;

                    case '4': telaEquipamento.VisualizarEquipamentos(true); break;

                    default: break;
                }
            }

            else if (opcaoPrincipal == '2')
            {
                char opcaoEscolhida = telaChamado.ApresentarMenu();

                switch (opcaoEscolhida)
                {
                    case '1': telaChamado.CadastrarChamado(); break;

                    case '2': telaChamado.EditarChamado(); break;

                    case '3': telaChamado.ExcluirChamado(); break;

                    case '4': telaChamado.VisualizarChamados(true); break;

                    default: break;
                }
            }
        }
    }
}
