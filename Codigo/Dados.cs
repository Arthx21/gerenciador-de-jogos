using Classes;
using System.Text.Json;

namespace Dados
{
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