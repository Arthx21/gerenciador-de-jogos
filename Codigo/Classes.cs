namespace Classes
{
    using System.Text.Json;
    class Pessoa
    {
        public int id;
        public string nome = string.Empty;
        public string senha = string.Empty;

        public Pessoa() { }
        public Pessoa(int id, string nome, string senha)
        {
            this.id = id;
            this.nome = nome;
            this.senha = senha;
        }

        public static Pessoa Logar()
        {
            string nome = "";
            while (string.IsNullOrWhiteSpace(nome))
            {
                try
                {
                    Console.Write("Nome: ");
                    nome = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(nome))
                        Console.WriteLine("O nome não pode ser vazio!");
                }
                catch
                {
                    Console.WriteLine("Erro ao ler o nome. Tente novamente!");
                }
            }

            string senha = "";
            while (string.IsNullOrWhiteSpace(senha))
            {
                try
                {
                    Console.Write("Senha: ");
                    senha = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(senha))
                        Console.WriteLine("A senha não pode ser vazia!");
                }
                catch
                {
                    Console.WriteLine("Erro ao ler a senha. Tente novamente!");
                }
            }

            // Verifica login do admin
            Admin admin = Database.Dados.admins
                .FirstOrDefault(a => a.nome == nome && a.senha == senha);

            if (admin != null)
                return admin;

            // Verifica login do usuário
            Usuario usuario = Database.Dados.usuarios
                .FirstOrDefault(u => u.nome == nome && u.senha == senha);

            if (usuario != null)
                return usuario;

            Console.WriteLine("Credenciais inválidas!");
            return null;
        }

        public static void CadastrarPessoa()
        {
            Console.Clear();
            Console.WriteLine("===== CADASTRAR NOVO USUÁRIO =====");

            string nome = "";
            while (string.IsNullOrWhiteSpace(nome))
            {
                try
                {
                    Console.Write("Nome: ");
                    nome = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(nome))
                        Console.WriteLine("O nome não pode ser vazio!");
                }
                catch
                {
                    Console.WriteLine("Erro ao ler o nome. Tente novamente!");
                }
            }

            string senha = "";
            while (string.IsNullOrWhiteSpace(senha))
            {
                try
                {
                    Console.Write("Senha: ");
                    senha = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(senha))
                        Console.WriteLine("A senha não pode ser vazia!");
                }
                catch
                {
                    Console.WriteLine("Erro ao ler a senha. Tente novamente!");
                }
            }

            // Verificar se já existe usuário com esse nome
            if (Database.Dados.usuarios.Any(u => u.nome.Equals(nome, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Já existe um usuário com esse nome!");
                Console.ReadKey();
                return;
            }

            // ID automático
            int novoId = (Database.Dados.usuarios.Count > 0)
                ? Database.Dados.usuarios.Max(u => u.id) + 1
                : 1;

            Usuario novo = new Usuario(novoId, nome, senha);

            Database.Dados.usuarios.Add(novo);
            Database.Salvar();

            Console.WriteLine("Usuário cadastrado com sucesso!");
            Console.ReadKey();
        }

    }


    class Admin : Pessoa
    {
        public Admin(int id, string nome, string senha)
            : base(id, nome, senha) { }

        public static void CadastrarJogo()
        {
            Console.WriteLine("=== Cadastro de Jogo ===");


            int id = (Database.Dados.jogos.Count == 0) ? 1 : Database.Dados.jogos.Max(j => j.id) + 1;

            Console.WriteLine($"ID gerado automaticamente: {id}");

            string nome = "";
            while (string.IsNullOrWhiteSpace(nome))
            {
                Console.Write("Nome do jogo: ");
                nome = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(nome))
                    Console.WriteLine("O nome não pode ser vazio!");
            }

            string genero = "";
            while (string.IsNullOrWhiteSpace(genero))
            {
                Console.Write("Gênero: ");
                genero = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(genero))
                    Console.WriteLine("O gênero não pode ser vazio!");
            }


            Jogo jogo = new Jogo(id, nome, genero, new List<Conquista>());


            Database.Dados.jogos.Add(jogo);
            Database.Salvar();

            Console.WriteLine("\nJogo cadastrado com sucesso!");

            // CADASTRO DE CONQUISTAS

            int n = 0;
            while (true)
            {
                Console.Write("\nQuantas conquistas esse jogo terá? ");
                try
                {
                    n = int.Parse(Console.ReadLine() ?? "");
                    if (n < 0)
                    {
                        Console.WriteLine("O número não pode ser negativo!");
                        continue;
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("Digite um número inteiro válido.");
                }
            }

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nCadastro da conquista {i + 1}/{n}:");
                CadastrarConquista(jogo);
            }

            Database.Salvar();
            Console.WriteLine("\nTodas as conquistas foram registradas.");
        }



        public static void CadastrarConquista(Jogo jogo)
        {
            Console.WriteLine("=== Cadastro de Conquista ===");

            int id = (jogo.conquistas.Count == 0) ? 1 : jogo.conquistas.Max(c => c.id) + 1;

            Console.WriteLine($"ID gerado automaticamente: {id}");

            string nome = "";
            while (string.IsNullOrWhiteSpace(nome))
            {
                Console.Write("Nome da conquista: ");
                nome = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(nome))
                    Console.WriteLine("O nome não pode ser vazio!");
            }

            int xp = 0;
            while (true)
            {
                Console.Write("XP da conquista: ");
                try
                {
                    xp = int.Parse(Console.ReadLine() ?? "");
                    if (xp < 0)
                    {
                        Console.WriteLine("XP não pode ser negativo!");
                        continue;
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("Digite um número inteiro válido.");
                }
            }

            string dificuldade = "";
            while (string.IsNullOrWhiteSpace(dificuldade))
            {
                Console.Write("Dificuldade (Fácil / Médio / Difícil / Lendária): ");
                dificuldade = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(dificuldade))
                    Console.WriteLine("A dificuldade não pode ser vazia!");
            }

            Conquista c = new Conquista(id, nome, xp, dificuldade);

            jogo.conquistas.Add(c);

            Console.WriteLine("Conquista cadastrada com sucesso!");
        }

    }

    class Usuario : Pessoa
    {
        public int Nivel { get;  set; }
        public int Experiencia { get;  set; }
        public BibliotecaUsuario biblioteca;


        public Usuario(int id, string nome, string senha)
            : base(id, nome, senha)
        {
            biblioteca = new BibliotecaUsuario();
            Nivel = 1;
            Experiencia = 0;
        }

        public void MostrarBiblioteca()
        {
            Console.Clear();
            Console.WriteLine("===== SUA BIBLIOTECA =====");

            if (biblioteca.jogos.Count == 0)
            {
                Console.WriteLine("Você não possui jogos ainda.");
                Console.ReadKey();
                return;
            }

            foreach (var jogo in biblioteca.jogos)
            {
                Console.WriteLine($"\n--- {jogo.nome} ---");

                if (jogo.conquistas.Count == 0)
                {
                    Console.WriteLine("  (Sem conquistas)");
                    continue;
                }

                foreach (var c in jogo.conquistas)
                {
                    Console.WriteLine($"  > {c.nome} | XP: {c.XP} | Dif: {c.dificuldade} | Status: {c.status}");
                }
            }

            Console.WriteLine("==========================");
            Console.WriteLine("Aperte qualquer tecla para continuar...");
            Console.ReadKey();
        }


        public void ExibirConquistas()
        {
            Console.Clear();
            Console.WriteLine("===== CONQUISTAS DOS JOGOS =====");

            if (biblioteca.jogos.Count == 0)
            {
                Console.WriteLine("Você não possui jogos ainda.");
                Console.ReadKey();
                return;
            }

            foreach (var jogo in biblioteca.jogos)
            {
                Console.WriteLine($"\n--- {jogo.nome} ---");

                if (jogo.conquistas.Count == 0)
                {
                    Console.WriteLine("  (Nenhuma conquista cadastrada)");
                    continue;
                }

                foreach (var c in jogo.conquistas)
                {
                    Console.WriteLine($"  > {c.nome}  | XP: {c.XP} | Dif: {c.dificuldade} | Status: {c.status}");
                }
            }

            Console.WriteLine("\n===============================");
            Console.ReadKey();
        }

        public void AdicionarJogo()
        {
            Console.Clear();
            Console.WriteLine("===== ADICIONAR JOGO =====");

            if (Database.Dados.jogos.Count == 0)
            {
                Console.WriteLine("Nenhum jogo cadastrado pelo Admin!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Jogos disponíveis:");
            foreach (var jogo in Database.Dados.jogos)
            {
                Console.WriteLine($"- {jogo.nome}");
            }

            Console.Write("\nDigite o nome do jogo que deseja adicionar: ");
            string nome = Console.ReadLine() ?? string.Empty;

            Jogo? jogoSelecionado = Database.Dados.jogos
                .FirstOrDefault(j => j.nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

            if (jogoSelecionado == null)
            {
                Console.WriteLine("Jogo não encontrado!");
                Console.ReadKey();
                return;
            }

            if (biblioteca.jogos.Any(j => j.nome == jogoSelecionado.nome))
            {
                Console.WriteLine("Você já possui esse jogo.");
                Console.ReadKey();
                return;
            }

            Jogo novoJogo = new Jogo()
            {
                id = jogoSelecionado.id,
                nome = jogoSelecionado.nome,
                genero = jogoSelecionado.genero,
                plataforma = jogoSelecionado.plataforma,
                conquistas = new List<Conquista>()
            };

            foreach (var c in jogoSelecionado.conquistas)
            {
                Conquista novaConquista = new Conquista(c.id, c.nome, c.XP, c.dificuldade);
                novaConquista.status = "Bloqueada";
                novoJogo.conquistas.Add(novaConquista);
            }

            biblioteca.jogos.Add(novoJogo);
            Database.Salvar();

            Console.WriteLine($"Jogo '{novoJogo.nome}' adicionado à sua biblioteca!");
            Console.Write("Aperte qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public void MarcarConquista()
        {
            Console.Clear();
            Console.WriteLine("===== JOGOS DISPONIVEIS =====");

            if (biblioteca.jogos.Count == 0)
            {
                Console.WriteLine("Você não possui jogos ainda.");
                Console.ReadKey();
                return;
            }

            foreach (var jogo in biblioteca.jogos)
            {
                Console.WriteLine($"\n--- {jogo.nome} ---");

                if (jogo.conquistas.Count == 0)
                {
                    Console.WriteLine("  (Sem conquistas)");
                    continue;
                }

                foreach (var c in jogo.conquistas)
                {
                    Console.WriteLine($"  > {c.nome} | XP: {c.XP} | Dif: {c.dificuldade} | Status: {c.status}");
                }
                Console.WriteLine("\n");
            }

            Console.WriteLine("===== MARCAR CONQUISTA =====");
            Console.Write("Nome do jogo: ");
            string jogoNome = Console.ReadLine() ?? string.Empty;

            Console.Write("Nome da conquista: ");
            string consNome = Console.ReadLine() ?? string.Empty;

            MarcarConquista(jogoNome, consNome);
            Database.Salvar();

            Console.Write("Aperte qualquer tecla para continuar...");
            Console.ReadKey();
        }


        public void MarcarConquista(string Nomejogo, string Nomeconquista) // Ambas pesquisas
        {
            Jogo? jogo = biblioteca.BuscarJogoPorNome(Nomejogo);

            if (jogo == null || !biblioteca.jogos.Contains(jogo))
            {
                Console.WriteLine("Você não possui esse jogo!");
                return;
            }

            int I = biblioteca.jogos
            .FindIndex(j => j.nome != null && j.nome.Equals(Nomejogo, StringComparison.OrdinalIgnoreCase));

            var conquista = biblioteca.BuscarConquistaPorNome(jogo, Nomeconquista);

            if (conquista == null || !biblioteca.jogos[I].conquistas.Contains(conquista))
            {
                Console.WriteLine("O jogo não possui está conquista!");
                return;
            }

            int J = biblioteca.jogos[I].conquistas
            .FindIndex(c => c.nome != null && c.nome.Equals(Nomeconquista, StringComparison.OrdinalIgnoreCase));

            biblioteca.jogos[I].conquistas[J].Desbloquear();
            Console.WriteLine($"Conquista marcada como desbloqueada!");

            Experiencia += conquista.XP;
            AtualizarNivel();

            Console.WriteLine($"Você ganhou {conquista.XP} XP! Total de XP: {Experiencia}, Nível: {Nivel}");
        }

        private void AtualizarNivel()
        {
            Nivel = 1 + (Experiencia / 1000);
        }

        public void CompararUsuario()
        {
            Console.Clear();
            Console.WriteLine("===== COMPARAR USUÁRIOS =====");

            Console.Write("Digite o nome do usuário para comparar: ");
            string nome = Console.ReadLine() ?? string.Empty;

            var outroUsuario = Database.Dados.usuarios
            .FirstOrDefault(u => u.nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

            if (outroUsuario == null)
            {
                Console.WriteLine("Usuário não encontrado!");
                Console.ReadKey();
                return;
            }

            int xpProximoNivel = Nivel * 1000;
            int xpOutroProximoNivel = outroUsuario.Nivel * 1000;

            double progressoAtual = (Experiencia % 1000) / 10.0;
            double progressoOutro = (outroUsuario.Experiencia % 1000) / 10.0;

            Console.WriteLine($"\nComparando {this.nome} com {outroUsuario.nome}:");
            Console.WriteLine($"- Nível: {Nivel} vs {outroUsuario.Nivel}");
            Console.WriteLine($"- Experiência: {Experiencia} vs {outroUsuario.Experiencia}");
            Console.WriteLine($"- Progresso para o proximo nível: {progressoAtual:F1}% vs {progressoOutro:F1}%");

            Console.WriteLine("\nAperte qualquer tecla para continuar...");
            Console.ReadKey();
        }

    }

    class Jogo
    {
        public int id;
        public string? nome;
        public string? genero;
        public string? plataforma;

        public List<Conquista> conquistas = new List<Conquista>();
        public Jogo() { }
        public Jogo(int id, string nome, string genero, List<Conquista> conquistas)
        {
            this.id = id;
            this.nome = nome;
            this.genero = genero;
            this.conquistas = conquistas;
        }

    }

    class Conquista
    {
        public int id;
        public string? nome;
        public int XP;
        public string? dificuldade;
        public string? status; // Desbloqueada / Bloqueada
        public void Desbloquear()
        {
            status = "Desbloqueada";
        }

        public Conquista(int id, string nome, int XP, string dificuldade)
        {
            this.id = id;
            this.nome = nome;
            this.XP = XP;
            this.dificuldade = dificuldade;
            status = "Bloqueada";
        }
    }

    class BibliotecaUsuario
    {
        public List<Jogo> jogos = new List<Jogo>();

        public BibliotecaUsuario()
        {
            jogos = new List<Jogo>();
        }


        public Jogo? BuscarJogoPorNome(string nome)
        {
            return jogos
                .FirstOrDefault(j => j.nome != null && j.nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }

        public Conquista? BuscarConquistaPorNome(Jogo jogo, string nome)
        {
            return jogo.conquistas
                .FirstOrDefault(c => c.nome != null && c.nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }

    }

    class BancoDeDados
    {
        public List<Admin> admins { get; set; } = new();
        public List<Usuario> usuarios { get; set; } = new();
        public List<Jogo> jogos { get; set; } = new();
    }

    static class Database
    {
        public static BancoDeDados Dados { get; private set; } = new BancoDeDados();
        private static string caminho = "dados.json";

        public static JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true
        };

        public static void Carregar()
        {
            if (File.Exists(caminho))
            {
                // Console.WriteLine("Passei aq");
                string json = File.ReadAllText(caminho);
                Dados = JsonSerializer.Deserialize<BancoDeDados>(json, options);
            }
            else
            {
                Salvar(); // CRIA ARQUIVO NOVO
            }
        }

        public static void Salvar()
        {
            string json = JsonSerializer.Serialize(Dados, options);
            File.WriteAllText(caminho, json);
        }
    }



}