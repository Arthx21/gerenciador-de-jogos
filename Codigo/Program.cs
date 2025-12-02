using System;
using System.Text.Json;
using Microsoft.VisualBasic;


class Pessoa
{
    public int id;
    public string nome;
    public string senha;

    public Pessoa() { }
    public Pessoa(int id, string nome, string senha)
    {
        this.id = id;
        this.nome = nome;
        this.senha = senha;
    }

    public static Pessoa Logar()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        Console.Write("Senha: ");
        string senha = Console.ReadLine();

        var admin = Database.Dados.admins
            .FirstOrDefault(a => a.nome == nome && a.senha == senha);

        if (admin != null)
        {
            //Console.WriteLine("Login de ADMIN bem-sucedido!");
            return admin;
        }

        var usuario = Database.Dados.usuarios
            .FirstOrDefault(u => u.nome == nome && u.senha == senha);

        if (usuario != null)
        {
            //Console.WriteLine("Login de USUÁRIO bem-sucedido!");
            return usuario;
        }

        Console.WriteLine("Credenciais inválidas!");
        return null;
    }

    public static void CadastrarPessoa()
    {

    }

}


class Admin : Pessoa
{
    public Admin(int id, string nome, string senha)
        : base(id, nome, senha) { }

    public void CadastrarJogo()
    {
        // lógica
    }

    public void CadastrarConquista()
    {
        // lógica
    }

    public void CompararUsuario()
    {

    }

}

class Usuario : Pessoa
{
    public int Nivel { get; private set; }
    public int Experiencia { get; private set; }
    public BibliotecaUsuario biblioteca;


    public Usuario(int id, string nome, string senha)
        : base(id, nome, senha)
    {
        biblioteca = new BibliotecaUsuario();
        Nivel = 1;
        Experiencia = 0;
    }

    public void AdicionarJogo()
    {

        // lógica

    }

    public void MarcarConquista(string Nomejogo, string Nomeconquista) // Ambas pesquisas
    {
        Jogo jogo = biblioteca.BuscarJogoPorNome(Nomejogo);

        if (!biblioteca.jogos.Contains(jogo))
        {
            Console.WriteLine("Você não possui esse jogo!");
            return;
        }

        int I = biblioteca.jogos
        .FindIndex(j => j.nome.Equals(Nomejogo, StringComparison.OrdinalIgnoreCase));

        var conquista = biblioteca.BuscarConquistaPorNome(jogo, Nomeconquista);

        if (!biblioteca.jogos[I].conquistas.Contains(conquista))
        {
            Console.WriteLine("O jogo não possui está conquista!");
            return;
        }

        int J = biblioteca.jogos[I].conquistas
        .FindIndex(c => c.nome.Equals(Nomeconquista, StringComparison.OrdinalIgnoreCase));

        biblioteca.jogos[I].conquistas[J].Desbloquear();

        Experiencia += conquista.XP;
        AtualizarNivel();
    }

    private void AtualizarNivel()
    {
        Nivel = 1 + (Experiencia / 1000);
    }


}

class Jogo
{
    public int id;
    public string nome;
    public string genero;
    public string plataforma;

    public List<Conquista> conquistas = new List<Conquista>();

}

class Conquista
{
    public int id;
    public string nome;
    public int XP;
    public string dificuldade;
    public string status; // Desbloqueada / Bloqueada

    public void Desbloquear()
    {
        status = "Desbloqueada";
    }

}

class BibliotecaUsuario
{

    public List<Jogo> jogos = new List<Jogo>();

    public void ExibirConquistas()
    {

    }

    public Jogo BuscarJogoPorNome(string nome)
    {
        return jogos
            .FirstOrDefault(j => j.nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
    }

    public Conquista BuscarConquistaPorNome(Jogo jogo, string nomeConquista)
    {
        return jogo.conquistas
            .FirstOrDefault(c => c.nome.Equals(nomeConquista, StringComparison.OrdinalIgnoreCase));
    }

}

class BancoDeDados
{
    public List<Admin> admins { get; set; } = new();
    public List<Usuario> usuarios { get; set; } = new();
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
            Console.WriteLine("Passei aq");
            string json = File.ReadAllText(caminho);
            Dados = JsonSerializer.Deserialize<BancoDeDados>(json, options);
        }
        else
        {
            Salvar(); // cria arquivo novo
        }
    }

    public static void Salvar()
    {
        string json = JsonSerializer.Serialize(Dados, options);
        File.WriteAllText(caminho, json);
    }
}



class Programa
{
    public static void Main()
    {
        int opt;
        Database.Carregar();

        while (true)
        {
            Console.Clear();


            Console.WriteLine("======================================");
            Console.WriteLine("\t1 - Logar");
            Console.WriteLine("\t2 - Cadastrar");
            Console.WriteLine("\t3 - Sair");
            Console.WriteLine("======================================");
            opt = int.Parse(Console.ReadLine());

            switch (opt)
            {
                case 1:
                    Pessoa pessoa = Pessoa.Logar();

                    if (pessoa is Admin)
                    {
                        Console.WriteLine("Entrou como Admin!");
                        Console.ReadKey();
                        TelaPrincipalAdmin();
                    }
                    else if (pessoa is Usuario)
                    {
                        Console.WriteLine("Entrou como Usuário!");
                        Console.ReadKey();
                        TelaPrincipalUsuario();
                    }

                    Console.WriteLine("Enn");
                    break;

                case 2:

                    break;
                case 3:

                    break;
                default:

                    break;
            }
        }

    }
    // MENUS

    public static void TelaPrincipalUsuario()
    {
            // A fazer
    }

    public static void TelaPrincipalAdmin()
    {
            // A fazer
    }



}



































;/* 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace GerenciadorJogos
{
    public enum TipoPessoa
    {
        Pessoa,
        Admin
    }

    class Conquista
    {
        public int id;
        public string nome;
        public int XP;
        public string dificuldade;
        public string status; // "Desbloqueada" / "Bloqueada"

        public Conquista() { }

        public Conquista(int id, string nome, int xp, string dificuldade)
        {
            this.id = id;
            this.nome = nome;
            this.XP = xp;
            this.dificuldade = dificuldade;
            this.status = "Bloqueada";
        }

        public void Desbloquear()
        {
            status = "Desbloqueada";
        }

        public Conquista ClonarParaUsuario()
        {
            return new Conquista(id, nome, XP, dificuldade) { status = "Bloqueada" };
        }
    }

    class Jogo
    {
        public int id;
        public string nome;
        public string genero;
        public string plataforma;
        public List<Conquista> conquistas = new List<Conquista>();

        public Jogo() { }

        public Jogo(int id, string nome, string genero = "", string plataforma = "")
        {
            this.id = id;
            this.nome = nome;
            this.genero = genero;
            this.plataforma = plataforma;
            this.conquistas = new List<Conquista>();
        }

        public void AddConquista(Conquista c)
        {
            if (!conquistas.Any(x => x.nome.Equals(c.nome, StringComparison.OrdinalIgnoreCase)))
                conquistas.Add(c);
        }
    }

    class BibliotecaUsuario
    {
        public List<Jogo> jogos = new List<Jogo>();

        public BibliotecaUsuario() { }

        public void AdicionarJogoComoCopia(Jogo jogoTemplate)
        {
            if (jogoTemplate == null) return;
            if (jogos.Any(j => j.id == jogoTemplate.id)) return;

            var copia = new Jogo(jogoTemplate.id, jogoTemplate.nome, jogoTemplate.genero, jogoTemplate.plataforma);
            foreach (var c in jogoTemplate.conquistas)
            {
                copia.conquistas.Add(c.ClonarParaUsuario());
            }
            jogos.Add(copia);
        }

        public void ExibirConquistas(Jogo jogo)
        {
            if (jogo == null) return;
            Console.WriteLine($"Conquistas de {jogo.nome}:");
            foreach (var c in jogo.conquistas)
            {
                Console.WriteLine($"- {c.nome} | XP: {c.XP} | Dificuldade: {c.dificuldade} | Status: {c.status}");
            }
        }

        public Jogo BuscarJogoPorNome(string nome)
        {
            return jogos
                .FirstOrDefault(j => j.nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }

        public Conquista BuscarConquistaPorNome(Jogo jogo, string nomeConquista)
        {
            if (jogo == null) return null;
            return jogo.conquistas
                .FirstOrDefault(c => c.nome.Equals(nomeConquista, StringComparison.OrdinalIgnoreCase));
        }

        public double ProgressoDoJogo(Jogo jogo)
        {
            if (jogo == null) return 0;
            if (jogo.conquistas.Count == 0) return 0;
            int desbloqueadas = jogo.conquistas.Count(c => c.status == "Desbloqueada");
            return (double)desbloqueadas / jogo.conquistas.Count * 100;
        }
    }

    class Pessoa
    {
        public int id;
        public string nome;
        public string senha;
        public TipoPessoa tipo;

        public Pessoa() { }

        public Pessoa(int id, string nome, string senha, TipoPessoa tipo = TipoPessoa.Pessoa)
        {
            this.id = id;
            this.nome = nome;
            this.senha = senha;
            this.tipo = tipo;
        }

        public static Pessoa Logar()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine()?.Trim();

            Console.Write("Senha: ");
            string senha = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(senha))
            {
                Console.WriteLine("Nome ou senha inválidos.");
                return null;
            }

            var admin = Database.Dados.admins
                .FirstOrDefault(a => a.nome.Equals(nome, StringComparison.OrdinalIgnoreCase) && a.senha == senha);

            if (admin != null)
            {
                Console.WriteLine("Login de ADMIN bem-sucedido!");
                return admin;
            }

            var usuario = Database.Dados.usuarios
                .FirstOrDefault(u => u.nome.Equals(nome, StringComparison.OrdinalIgnoreCase) && u.senha == senha);

            if (usuario != null)
            {
                Console.WriteLine("Login de USUÁRIO bem-sucedido!");
                return usuario;
            }

            Console.WriteLine("Credenciais inválidas!");
            return null;
        }

        public static void CadastrarPessoa()
        {
            Console.WriteLine("Cadastrar pessoa");
            Console.Write("Nome: ");
            string nome = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(nome))
            {
                Console.WriteLine("Nome inválido.");
                return;
            }

            Console.Write("Senha: ");
            string senha = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(senha))
            {
                Console.WriteLine("Senha inválida.");
                return;
            }

            Console.Write("Tipo (1 - Admin, 2 - Usuário): ");
            string t = Console.ReadLine()?.Trim();
            TipoPessoa tipo = TipoPessoa.Pessoa;
            if (t == "1") tipo = TipoPessoa.Admin;
            else tipo = TipoPessoa.Pessoa;

            int novoId = Database.GetNextPessoaId();

            if (tipo == TipoPessoa.Admin)
            {
                var admin = new Admin(novoId, nome, senha);
                Database.Dados.admins.Add(admin);
                Database.Salvar();
                Console.WriteLine($"Admin '{nome}' cadastrado com ID {novoId}.");
            }
            else
            {
                var usuario = new Usuario(novoId, nome, senha);
                Database.Dados.usuarios.Add(usuario);
                Database.Salvar();
                Console.WriteLine($"Usuário '{nome}' cadastrado com ID {novoId}.");
            }
        }
    }

    class Admin : Pessoa
    {
        public Admin() { }

        public Admin(int id, string nome, string senha)
            : base(id, nome, senha, TipoPessoa.Admin) { }

        public void CadastrarJogo()
        {
            Console.WriteLine("--- Cadastrar Jogo ---");
            Console.Write("Nome do jogo: ");
            string nome = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(nome))
            {
                Console.WriteLine("Nome inválido.");
                return;
            }

            if (Database.Dados.jogos.Any(j => j.nome.Equals(nome, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Já existe um jogo com esse nome.");
                return;
            }

            Console.Write("Gênero: ");
            string genero = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Plataforma: ");
            string plataforma = Console.ReadLine()?.Trim() ?? "";

            int novoId = Database.GetNextJogoId();
            var jogo = new Jogo(novoId, nome, genero, plataforma);

            Console.WriteLine("Agora cadastre pelo menos UMA conquista para este jogo.");
            while (true)
            {
                Console.Write("Nome da conquista (ou vazio para finalizar): ");
                string nomeC = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(nomeC))
                {
                    if (jogo.conquistas.Count == 0)
                    {
                        Console.WriteLine("O jogo precisa de ao menos 1 conquista. Cadastre uma.");
                        continue;
                    }
                    break;
                }

                Console.Write("XP da conquista: ");
                if (!int.TryParse(Console.ReadLine(), out int xp)) xp = 0;

                Console.Write("Dificuldade: ");
                string diff = Console.ReadLine()?.Trim() ?? "";

                int idC = Database.GetNextConquistaId();
                var c = new Conquista(idC, nomeC, xp, diff);
                jogo.AddConquista(c);
                Console.WriteLine($"Conquista '{nomeC}' adicionada.");
            }

            Database.Dados.jogos.Add(jogo);
            Database.Salvar();
            Console.WriteLine($"Jogo '{jogo.nome}' cadastrado com ID {jogo.id}.");
        }

        public void CadastrarConquista()
        {
            Console.WriteLine("--- Cadastrar Conquista em Jogo Existente ---");
            if (Database.Dados.jogos.Count == 0)
            {
                Console.WriteLine("Não há jogos cadastrados.");
                return;
            }

            Console.WriteLine("Jogos disponíveis:");
            foreach (var j in Database.Dados.jogos)
                Console.WriteLine($"{j.id} - {j.nome}");

            Console.Write("ID do jogo: ");
            if (!int.TryParse(Console.ReadLine(), out int idJ))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var jogo = Database.Dados.jogos.FirstOrDefault(j => j.id == idJ);
            if (jogo == null)
            {
                Console.WriteLine("Jogo não encontrado.");
                return;
            }

            Console.Write("Nome da conquista: ");
            string nomeC = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(nomeC))
            {
                Console.WriteLine("Nome inválido.");
                return;
            }

            Console.Write("XP: ");
            if (!int.TryParse(Console.ReadLine(), out int xp)) xp = 0;

            Console.Write("Dificuldade: ");
            string diff = Console.ReadLine()?.Trim() ?? "";

            int idC = Database.GetNextConquistaId();
            var c = new Conquista(idC, nomeC, xp, diff);
            jogo.AddConquista(c);

            Database.Salvar();
            Console.WriteLine($"Conquista '{nomeC}' adicionada ao jogo '{jogo.nome}'.");
        }

        public void CompararUsuario()
        {
            Console.WriteLine("--- Comparar progresso entre dois usuários ---");
            if (Database.Dados.usuarios.Count < 2)
            {
                Console.WriteLine("Não há usuários suficientes para comparar.");
                return;
            }

            Console.WriteLine("Usuários:");
            foreach (var u in Database.Dados.usuarios)
                Console.WriteLine($"{u.id} - {u.nome}");

            Console.Write("ID do primeiro usuário: ");
            if (!int.TryParse(Console.ReadLine(), out int id1)) return;
            Console.Write("ID do segundo usuário: ");
            if (!int.TryParse(Console.ReadLine(), out int id2)) return;

            var user1 = Database.Dados.usuarios.FirstOrDefault(u => u.id == id1);
            var user2 = Database.Dados.usuarios.FirstOrDefault(u => u.id == id2);

            if (user1 == null || user2 == null)
            {
                Console.WriteLine("Usuário(s) não encontrados.");
                return;
            }

            Console.WriteLine($"Progresso de {user1.nome}:");
            foreach (var j in user1.biblioteca.jogos)
            {
                Console.WriteLine($"- {j.nome}: {user1.biblioteca.ProgressoDoJogo(j):0.##}%");
            }

            Console.WriteLine($"Progresso de {user2.nome}:");
            foreach (var j in user2.biblioteca.jogos)
            {
                Console.WriteLine($"- {j.nome}: {user2.biblioteca.ProgressoDoJogo(j):0.##}%");
            }
        }

        public void ListarJogosCadastrados()
        {
            Console.WriteLine("--- Jogos cadastrados ---");
            if (Database.Dados.jogos.Count == 0)
            {
                Console.WriteLine("Nenhum jogo cadastrado.");
                return;
            }
            foreach (var j in Database.Dados.jogos)
            {
                Console.WriteLine($"{j.id} - {j.nome} | {j.genero} | {j.plataforma} | Conquistas: {j.conquistas.Count}");
            }
        }
    }

    class Usuario : Pessoa
    {
        public int Nivel { get; private set; }
        public int Experiencia { get; private set; }
        public BibliotecaUsuario biblioteca;

        public Usuario() { }

        public Usuario(int id, string nome, string senha)
            : base(id, nome, senha, TipoPessoa.Pessoa)
        {
            biblioteca = new BibliotecaUsuario();
            Nivel = 1;
            Experiencia = 0;
        }

        public void AdicionarJogo()
        {
            Console.WriteLine("--- Adicionar jogo à minha biblioteca ---");
            if (Database.Dados.jogos.Count == 0)
            {
                Console.WriteLine("Nenhum jogo disponível no sistema.");
                return;
            }

            Console.WriteLine("Jogos disponíveis:");
            foreach (var j in Database.Dados.jogos)
                Console.WriteLine($"{j.id} - {j.nome}");

            Console.Write("ID do jogo que deseja adicionar: ");
            if (!int.TryParse(Console.ReadLine(), out int idJ))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var jogo = Database.Dados.jogos.FirstOrDefault(j => j.id == idJ);
            if (jogo == null)
            {
                Console.WriteLine("Jogo não encontrado.");
                return;
            }

            if (biblioteca.jogos.Any(x => x.id == jogo.id))
            {
                Console.WriteLine("Você já possui esse jogo na biblioteca.");
                return;
            }

            biblioteca.AdicionarJogoComoCopia(jogo);
            Database.Salvar();
            Console.WriteLine($"Jogo '{jogo.nome}' adicionado à sua biblioteca.");
        }

        public void MarcarConquista()
        {
            Console.WriteLine("--- Marcar conquista ---");
            Console.Write("Nome do jogo: ");
            string Nomejogo = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(Nomejogo))
            {
                Console.WriteLine("Nome do jogo inválido.");
                return;
            }

            Jogo jogo = biblioteca.BuscarJogoPorNome(Nomejogo);
            if (jogo == null)
            {
                Console.WriteLine("Você não possui esse jogo!");
                return;
            }

            Console.Write("Nome da conquista: ");
            string Nomeconquista = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(Nomeconquista))
            {
                Console.WriteLine("Nome de conquista inválido.");
                return;
            }

            var conquista = biblioteca.BuscarConquistaPorNome(jogo, Nomeconquista);
            if (conquista == null)
            {
                Console.WriteLine("O jogo não possui essa conquista!");
                return;
            }

            if (conquista.status == "Desbloqueada")
            {
                Console.WriteLine("Conquista já desbloqueada.");
                return;
            }

            conquista.Desbloquear();

            Experiencia += conquista.XP;
            AtualizarNivel();

            Database.Salvar();
            Console.WriteLine($"Conquista '{conquista.nome}' desbloqueada! +{conquista.XP} XP. Nível atual: {Nivel}, XP: {Experiencia}");
        }

        private void AtualizarNivel()
        {
            Nivel = 1 + (Experiencia / 1000);
        }

        public void ListarBiblioteca()
        {
            Console.WriteLine($"--- Biblioteca de {nome} ---");
            if (biblioteca.jogos.Count == 0)
            {
                Console.WriteLine("Sua biblioteca está vazia.");
                return;
            }

            foreach (var j in biblioteca.jogos)
            {
                Console.WriteLine($"{j.id} - {j.nome} | Progresso: {biblioteca.ProgressoDoJogo(j):0.##}% | Conquistas: {j.conquistas.Count}");
            }
        }
    }

    class BancoDeDados
    {
        public List<Admin> admins { get; set; } = new();
        public List<Usuario> usuarios { get; set; } = new();
        public List<Jogo> jogos { get; set; } = new();
        public List<Conquista> conquistasGlobais { get; set; } = new(); // opcional, não muito usado
    }

    static class Database
    {
        public static BancoDeDados Dados { get; private set; } = new BancoDeDados();
        private static string caminho = "dados.json";

        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true
        };

        public static void Carregar()
        {
            try
            {
                if (File.Exists(caminho))
                {
                    string json = File.ReadAllText(caminho);
                    var dados = JsonSerializer.Deserialize<BancoDeDados>(json, options);
                    if (dados != null) Dados = dados;
                }
                else
                {
                    // primeiro uso: criar dados iniciais com um admin default
                    Dados = new BancoDeDados();
                    var admin = new Admin(1, "admin", "admin");
                    Dados.admins.Add(admin);
                    Salvar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha ao carregar dados: " + ex.Message);
                Dados = new BancoDeDados();
            }
        }

        public static void Salvar()
        {
            try
            {
                string json = JsonSerializer.Serialize(Dados, options);
                File.WriteAllText(caminho, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha ao salvar dados: " + ex.Message);
            }
        }

        public static int GetNextPessoaId()
        {
            int maxAdmin = Dados.admins.Count > 0 ? Dados.admins.Max(a => a.id) : 0;
            int maxUser = Dados.usuarios.Count > 0 ? Dados.usuarios.Max(u => u.id) : 0;
            return Math.Max(maxAdmin, maxUser) + 1;
        }

        public static int GetNextJogoId()
        {
            return (Dados.jogos.Count > 0 ? Dados.jogos.Max(j => j.id) : 0) + 1;
        }

        public static int GetNextConquistaId()
        {
            int maxConqGlobal = 0;
            if (Dados.jogos != null && Dados.jogos.Count > 0)
            {
                foreach (var j in Dados.jogos)
                    if (j.conquistas.Count > 0)
                        maxConqGlobal = Math.Max(maxConqGlobal, j.conquistas.Max(c => c.id));
            }
            return maxConqGlobal + 1;
        }
    }

    class Programa
    {
        public static void Main()
        {
            Database.Carregar();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("======================================");
                Console.WriteLine("\t1 - Logar");
                Console.WriteLine("\t2 - Cadastrar");
                Console.WriteLine("\t3 - Sair");
                Console.WriteLine("======================================");
                Console.Write("Opção: ");
                string entrada = Console.ReadLine();
                if (!int.TryParse(entrada, out int opt))
                {
                    Console.WriteLine("Opção inválida. Pressione qualquer tecla...");
                    Console.ReadKey();
                    continue;
                }

                switch (opt)
                {
                    case 1:
                        var pessoa = Pessoa.Logar();
                        if (pessoa is Admin admin)
                        {
                            TelaPrincipalAdmin(admin);
                        }
                        else if (pessoa is Usuario usuario)
                        {
                            TelaPrincipalUsuario(usuario);
                        }
                        else
                        {
                            Console.WriteLine("Falha no login. Pressione qualquer tecla...");
                            Console.ReadKey();
                        }
                        break;

                    case 2:
                        Pessoa.CadastrarPessoa();
                        Console.WriteLine("Pressione qualquer tecla...");
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.WriteLine("Saindo...");
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }

        public static void TelaPrincipalUsuario(Usuario u)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"--- Usuário: {u.nome} | Nível: {u.Nivel} | XP: {u.Experiencia} ---");
                Console.WriteLine("1 - Listar minha biblioteca");
                Console.WriteLine("2 - Adicionar jogo à biblioteca");
                Console.WriteLine("3 - Marcar conquista");
                Console.WriteLine("4 - Listar conquistas de um jogo");
                Console.WriteLine("5 - Sair (logout)");
                Console.Write("Opção: ");
                string s = Console.ReadLine();
                if (!int.TryParse(s, out int opt)) { Console.WriteLine("Inválido."); Console.ReadKey(); continue; }

                switch (opt)
                {
                    case 1:
                        u.ListarBiblioteca();
                        Console.WriteLine("Pressione qualquer tecla...");
                        Console.ReadKey();
                        break;
                    case 2:
                        u.AdicionarJogo();
                        Console.WriteLine("Pressione qualquer tecla...");
                        Console.ReadKey();
                        break;
                    case 3:
                        u.MarcarConquista();
                        Console.WriteLine("Pressione qualquer tecla...");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Write("Nome do jogo: ");
                        string nomej = Console.ReadLine()?.Trim();
                        var jogo = u.biblioteca.BuscarJogoPorNome(nomej);
                        if (jogo == null) Console.WriteLine("Jogo não encontrado na sua biblioteca.");
                        else u.biblioteca.ExibirConquistas(jogo);
                        Console.WriteLine("Pressione qualquer tecla...");
                        Console.ReadKey();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }

        public static void TelaPrincipalAdmin(Admin admin)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"--- Admin: {admin.nome} ---");
                Console.WriteLine("1 - Cadastrar jogo");
                Console.WriteLine("2 - Cadastrar conquista em jogo existente");
                Console.WriteLine("3 - Listar jogos cadastrados");
                Console.WriteLine("4 - Comparar usuários");
                Console.WriteLine("5 - Sair (logout)");
                Console.Write("Opção: ");
                string s = Console.ReadLine();
                if (!int.TryParse(s, out int opt)) { Console.WriteLine("Inválido."); Console.ReadKey(); continue; }

                switch (opt)
                {
                    case 1:
                        admin.CadastrarJogo();
                        Console.WriteLine("Pressione qualquer tecla...");
                        Console.ReadKey();
                        break;
                    case 2:
                        admin.CadastrarConquista();
                        Console.WriteLine("Pressione qualquer tecla...");
                        Console.ReadKey();
                        break;
                    case 3:
                        admin.ListarJogosCadastrados();
                        Console.WriteLine("Pressione qualquer tecla...");
                        Console.ReadKey();
                        break;
                    case 4:
                        admin.CompararUsuario();
                        Console.WriteLine("Pressione qualquer tecla...");
                        Console.ReadKey();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }
    }
}
*/