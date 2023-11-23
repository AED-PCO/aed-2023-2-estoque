using System;
using System.Collections.Generic;
using System.IO;

class Produto
{
    public int Codigo { get; set; }
    public string Nome { get; set; }
    public int Quantidade { get; set; }
    public double PrecoUnitario { get; set; }
    

    public Produto(int codigo, string nome, int quantidade, double precoUnitario)
    {
        Codigo = codigo;
        Nome = nome;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
    }

    public override string ToString()
    {
        return $"Código: {Codigo}, Nome: {Nome}, Quantidade: {Quantidade}, Preço Unitário: {PrecoUnitario:C}";
    }
}

class Estoque
{
    private List<Produto> produtos;
    private Dictionary<int, Produto> tabelaHash;

    public Estoque()
    {
        produtos = new List<Produto>();
        tabelaHash = new Dictionary<int, Produto>();
    }

    public void AdicionarProduto(Produto produto)
    {
        produtos.Add(produto);
        tabelaHash.Add(produto.Codigo, produto);
    }

    public void ListarProdutos()
    {
        foreach (var produto in produtos)
        {
            Console.WriteLine(produto);
        }
    }

    public void OrdenarProdutosPorNome()
    {
        produtos.Sort((p1, p2) => p1.Nome.CompareTo(p2.Nome));
    }

    public void SalvarDadosEmArquivo(string nomeArquivo)
    {
        using (StreamWriter sw = new StreamWriter(nomeArquivo))
        {
            foreach (var produto in produtos)
            {
                sw.WriteLine($"{produto.Codigo},{produto.Nome},{produto.Quantidade},{produto.PrecoUnitario}");
            }
        }
    }

    public void CarregarDadosDeArquivo(string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            produtos.Clear();
            tabelaHash.Clear();

            using (StreamReader sr = new StreamReader(nomeArquivo))
            {
                string linha;
                while ((linha = sr.ReadLine()) != null)
                {
                    string[] dados = linha.Split(',');
                    if (dados.Length == 4)
                    {
                        int codigo = int.Parse(dados[0]);
                        string nome = dados[1];
                        int quantidade = int.Parse(dados[2]);
                        double precoUnitario = double.Parse(dados[3]);
                        Produto produto = new Produto(codigo, nome, quantidade, precoUnitario);
                        AdicionarProduto(produto);
                    }
                }
            }
        }
    }
}

class Usuario
{
    public string Username { get; set; }
    public string Senha { get; set; }

    public Usuario(string username, string senha)
    {
        Username = username;
        Senha = senha;
    }
}

class Autenticacao
{
    private List<Usuario> usuariosCadastrados;

    public Autenticacao()
    {
        usuariosCadastrados = new List<Usuario>();
        CarregarUsuariosDeArquivo("usuarios.txt");
    }

    public void CadastrarUsuario(string username, string senha)
    {
        Usuario novoUsuario = new Usuario(username, senha);
        usuariosCadastrados.Add(novoUsuario);
        Console.WriteLine($"Usuário {username} cadastrado com sucesso!");
        SalvarUsuariosEmArquivo("usuarios.txt");
    }

    public Usuario FazerLogin(string username, string senha)
    {
        foreach (Usuario usuario in usuariosCadastrados)
        {
            if (usuario.Username == username && usuario.Senha == senha)
            {
                Console.WriteLine($"Login bem-sucedido! Bem-vindo, {username}.");
                return usuario;
            }
        }
        Console.WriteLine("Usuário ou senha incorretos.");
        return null;
    }

    private void SalvarUsuariosEmArquivo(string nomeArquivo)
    {
        using (StreamWriter sw = new StreamWriter(nomeArquivo))
        {
            foreach (var usuario in usuariosCadastrados)
            {
                sw.WriteLine($"{usuario.Username},{usuario.Senha}");
            }
        }
    }

    private void CarregarUsuariosDeArquivo(string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            usuariosCadastrados.Clear();

            using (StreamReader sr = new StreamReader(nomeArquivo))
            {
                string linha;
                while ((linha = sr.ReadLine()) != null)
                {
                    string[] dados = linha.Split(',');
                    if (dados.Length == 2)
                    {
                        string username = dados[0];
                        string senha = dados[1];
                        Usuario usuario = new Usuario(username, senha);
                        usuariosCadastrados.Add(usuario);
                    }
                }
            }
        }
    }
}



class Program
{
    static void Main(string[] args)
    {
        Autenticacao autenticacao = new Autenticacao();
        Estoque estoque = new Estoque();
        string arquivo = "estoque.txt";
        Usuario usuarioLogado = null;

        // Carregar dados do arquivo se ele existir
        estoque.CarregarDadosDeArquivo(arquivo);

        bool executando = true;
        while (executando)
        {
            if (usuarioLogado == null)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Cadastrar Usuário");
                Console.WriteLine("2. Fazer Login");
                Console.WriteLine("3. Sair");
                Console.Write("Escolha uma opção: ");

                int opcaoAutenticacao = int.Parse(Console.ReadLine());

                switch (opcaoAutenticacao)
                {
                    case 1:
                        Console.Write("Novo Username: ");
                        string novoUsername = Console.ReadLine();
                        Console.Write("Nova Senha: ");
                        string novaSenha = Console.ReadLine();
                        autenticacao.CadastrarUsuario(novoUsername, novaSenha);
                        break;

                    case 2:
                        Console.Write("Username: ");
                        string username = Console.ReadLine();
                        Console.Write("Senha: ");
                        string senha = Console.ReadLine();
                        usuarioLogado = autenticacao.FazerLogin(username, senha);
                        break;

                    case 3:
                        executando = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Adicionar Produto");
                Console.WriteLine("2. Listar Produtos");
                Console.WriteLine("3. Ordenar Produtos por Nome");
                Console.WriteLine("4. Salvar Dados em Arquivo");
                Console.WriteLine("5. Fazer Logout");
                Console.Write("Escolha uma opção: ");

                int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Console.Write("Código do Produto: ");
                    int codigo = int.Parse(Console.ReadLine());
                    Console.Write("Nome do Produto: ");
                    string nome = Console.ReadLine();
                    Console.Write("Quantidade: ");
                    int quantidade = int.Parse(Console.ReadLine());
                    Console.Write("Preço Unitário: ");
                    double preco = double.Parse(Console.ReadLine());

                    Produto novoProduto = new Produto(codigo, nome, quantidade, preco);
                    estoque.AdicionarProduto(novoProduto);
                    break;

                case 2:
                    Console.WriteLine("Produtos no Estoque:");
                    estoque.ListarProdutos();
                    break;

                case 3:
                    estoque.OrdenarProdutosPorNome();
                    Console.WriteLine("Produtos ordenados por Nome:");
                    estoque.ListarProdutos();
                    break;

                case 4:
                    estoque.SalvarDadosEmArquivo(arquivo);
                    Console.WriteLine("Dados salvos em arquivo.");
                    break;

                case 5:
                    usuarioLogado = null;
                    Console.WriteLine("Logout realizado com sucesso.");
                    break;

                case 6:
                    executando = false;
                    break;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }
  }
}
