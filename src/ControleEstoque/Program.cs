﻿using System;
using System.Collections.Generic;
using System.IO;

class Produto
{
    public int Codigo { get; set; }
    public string Nome { get; set; }
    public int Quantidade { get; set; }
    public double PrecoUnitario { get; set; }
    public double Lucro { get; set; } 

    public Produto(int codigo, string nome, int quantidade, double precoUnitario, double lucro)
    {
        Codigo = codigo;
        Nome = nome;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
        Lucro = lucro;
    }

    public override string ToString()
    {
        return $"Código: {Codigo}, Nome: {Nome}, Quantidade: {Quantidade}, Preço Unitário: {PrecoUnitario:C}, Lucro: {Lucro:C}";
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

    public void OrdenarProdutosPorCodigo()
    {
        produtos.Sort((p1, p2) => p1.Codigo.CompareTo(p2.Codigo));
    }

    public void OrdenarProdutosPorQuantidade()
    {
        produtos.Sort((p1, p2) => p1.Quantidade.CompareTo(p2.Quantidade));
    }

public void AdicionarProduto(Produto produto, double lucro)
{
    produto.Lucro = lucro;
    produtos.Add(produto);
    tabelaHash.Add(produto.Codigo, produto);
}

public void SalvarDadosEmArquivo(string nomeArquivo)
{
    using (StreamWriter sw = new StreamWriter(nomeArquivo))
    {
        foreach (var produto in produtos)
        {
            sw.WriteLine($"{produto.Codigo},{produto.Nome},{produto.Quantidade},{produto.PrecoUnitario},{produto.Lucro}");
        }
    }
}

public void AumentarQuantidadeProduto(int codigo, int quantidadeAumento)
    {
        if (tabelaHash.ContainsKey(codigo))
        {
            Produto produto = tabelaHash[codigo];
            produto.Quantidade += quantidadeAumento;
            Console.WriteLine($"Quantidade do produto {produto.Nome} aumentada para {produto.Quantidade}.");
        }
        else
        {
            Console.WriteLine($"Produto com código {codigo} não encontrado no estoque.");
        }
    }


public void VenderProdutos(string arquivoVendas)
    {
        Console.WriteLine("Venda de Produtos:");
        Console.Write("Quantos produtos diferentes deseja vender? ");
        int numeroProdutos = int.Parse(Console.ReadLine());

        Dictionary<Produto, int> produtosVenda = new Dictionary<Produto, int>();

        for (int i = 0; i < numeroProdutos; i++)
        {
            Console.Write($"Informe o código do Produto {i + 1}: ");
            int codigoProduto = int.Parse(Console.ReadLine());

            if (tabelaHash.ContainsKey(codigoProduto))
            {
                Produto produto = tabelaHash[codigoProduto];
                Console.Write($"Quantidade do Produto {produto.Nome} a vender: ");
                int quantidadeVenda = int.Parse(Console.ReadLine());

                if (quantidadeVenda <= produto.Quantidade)
                {
                    produtosVenda.Add(produto, quantidadeVenda);
                }
                else
                {
                    Console.WriteLine($"Erro: Não há quantidade suficiente de {produto.Nome} em estoque.");
                    i--;  
                }
            }
            else
            {
                Console.WriteLine($"Erro: Produto com código {codigoProduto} não encontrado no estoque.");
                i--;  
            }
        }


        double lucroTotal = 0.0;

        foreach (var venda in produtosVenda)
        {
            Produto produto = venda.Key;
            int quantidadeVendida = venda.Value;

            produto.Quantidade -= quantidadeVendida;
            double lucroVenda = quantidadeVendida * produto.Lucro;
            lucroTotal += lucroVenda;
        }


        using (StreamWriter sw = new StreamWriter(arquivoVendas, true))
        {
            sw.WriteLine($"Data: {DateTime.Now}, Lucro Total da Venda: {lucroTotal:C}");
        }

        Console.WriteLine($"Venda realizada com sucesso. Lucro total da venda: {lucroTotal:C}");
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
                if (dados.Length == 5)
                {
                    int codigo = int.Parse(dados[0]);
                    string nome = dados[1];
                    int quantidade = int.Parse(dados[2]);
                    double precoUnitario = double.Parse(dados[3]);
                    double lucro = double.Parse(dados[4]);
                    Produto produto = new Produto(codigo, nome, quantidade, precoUnitario, lucro);
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
                Console.WriteLine("1. Cadastrar Produto");
                Console.WriteLine("2. Listar Produtos");
                Console.WriteLine("3. Ordenar Produtos por Nome");
                Console.WriteLine("4. Ordenar Produtos por Codigo");
                Console.WriteLine("5. Ordenar Produtos por Quantidade");
                Console.WriteLine("6. Aumentar Quantidade de Produto");
                Console.WriteLine("7. Vender Produtos");
                Console.WriteLine("8. Listar Vendas");
                Console.WriteLine("9. Salvar Dados em Arquivo");
                Console.WriteLine("10. Fazer Logout");
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
                        Console.Write("Lucro: ");
                        double lucro = double.Parse(Console.ReadLine()); // Novo atributo "Lucro"
                        
                        Produto novoProduto = new Produto(codigo, nome, quantidade, preco, lucro);
                        estoque.AdicionarProduto(novoProduto, lucro);
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
                        estoque.OrdenarProdutosPorCodigo();
                        Console.WriteLine("Produtos ordenados por Codigo:");
                        estoque.ListarProdutos();
                        break;

                    case 5:
                        estoque.OrdenarProdutosPorQuantidade();
                        Console.WriteLine("Produtos ordenados por Quantidade:");
                        estoque.ListarProdutos();
                        break;

                    case 6:
                        Console.Write("Código do Produto: ");
                        int codigoAumento = int.Parse(Console.ReadLine());
                        Console.Write("Quantidade a Aumentar: ");
                        int quantidadeAumento = int.Parse(Console.ReadLine());
                        estoque.AumentarQuantidadeProduto(codigoAumento, quantidadeAumento);
                        break;

                    case 7:
                        estoque.VenderProdutos("vendas.txt");
                        break;

                    case 8:
                        if (File.Exists("vendas.txt")){
                            string[] linhas = File.ReadAllLines("vendas.txt");
                            Console.WriteLine();
                            foreach (string linha in linhas){
                                Console.WriteLine(linha);
                            }
                        }
                        else{
                            Console.WriteLine("O arquivo não existe.");
                        }
                        break;
                    
                    case 9:
                        estoque.SalvarDadosEmArquivo(arquivo);
                        Console.WriteLine("Dados salvos em arquivo.");
                        break;

                    case 10:
                        usuarioLogado = null;
                        Console.WriteLine("Logout realizado com sucesso.");
                        break;

                    case 11:
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