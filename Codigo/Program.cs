using Classes;  
using Dados;

class Programa
{
    public static void Main()
    {
        int opt;
        Database.Carregar();

        while (true)
        {
            Console.Clear();


            Console.WriteLine("=======MENU PRINCIPAL=======");
            Console.WriteLine("1 - Logar");
            Console.WriteLine("2 - Cadastrar");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("============================");
            Console.Write("Opção: ");

            if (!int.TryParse(Console.ReadLine(), out opt))
                continue;

            switch (opt)
            {
                case 1:
                    Pessoa pessoa = Pessoa.Logar();

                    if (pessoa is Admin)
                    {
                        Console.WriteLine("Entrou como Admin!\nPressione Enter.. para continuar");
                        Console.ReadKey();
                        TelaPrincipalAdmin();
                    }
                    else if (pessoa is Usuario usuario)
                    {
                        Console.WriteLine("Entrou como Usuário!\nPressione Enter.. para continuar");
                        
                        Console.ReadKey();
                        TelaPrincipalUsuario(usuario);
                    }
                    else
                    {
                        Console.WriteLine("Falha no login! Pressione Enter.. para continuar");
                        Console.ReadKey();
                    }
                    break;
                case 2:
                    Pessoa.CadastrarPessoa();
                    break;
                case 0:

                    return;   // SAINDO DO PROGRAMA

                default:
                    Console.WriteLine("Opção inválida!");
                    Console.WriteLine("Aperte qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }

    }

    // MENUS

    public static void TelaPrincipalUsuario(Usuario usuario)
    {
        int opt;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("====== MENU DO USUÁRIO ======");
            Console.WriteLine($"Usuário: {usuario.nome}");
            Console.WriteLine($"Nível: {usuario.Nivel}  |  XP: {usuario.Experiencia}");
            Console.WriteLine("==============================");
            Console.WriteLine("1 - Ver Biblioteca");
            Console.WriteLine("2 - Adicionar Jogo");
            Console.WriteLine("3 - Marcar Conquista");
            Console.WriteLine("4 - Comparar com outro Usuário");
            Console.WriteLine("5 - Comparar jogo com outro Usuario");
            Console.WriteLine("0 - Voltar");
            Console.WriteLine("==============================");
            Console.Write("Opção: ");

            if (!int.TryParse(Console.ReadLine(), out opt))
                continue;

            switch (opt)
            {
                case 1:
                    usuario.MostrarBiblioteca();
                    break;

                case 2:
                    usuario.AdicionarJogo();
                    break;

                case 3:
                    usuario.MarcarConquista();
                    break;

                case 4:
                    usuario.CompararUsuario();
                    break; 

                case 5:
                    usuario.CompararJogoComUsuario();
                    break;

                case 0:
                    return; //VOLTA AO MENU PRINCIPAL

                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    public static void TelaPrincipalAdmin()
    {
        int opt;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("====== MENU ADMIN ======");
            Console.WriteLine("1 - Cadastrar Jogo e suas Conquistas");
            Console.WriteLine("0 - Voltar");
            Console.WriteLine("========================");
            Console.Write("Opção: ");

            if (!int.TryParse(Console.ReadLine(), out opt))
                continue;

            switch (opt)
            {
                case 1:
                    Admin.CadastrarJogo();
                    Console.ReadKey();
                    break;

                case 0:
                    return;  //VOLTA AO MENU PRINCIPAL

                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }
    }



}



































