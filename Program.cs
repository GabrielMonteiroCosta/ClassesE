using System.ComponentModel.Design;
using System.Data;
using static Program;

class Program
{
    static void Main(string[] args)
    {
        // Lista para armazenar os produtos
        List<Produto> produtos = new List<Produto>();
        Console.WriteLine("Cadastro de Produtos!");


        // Loop principal do programa - Opções iniciais para o usuário -
        while (true)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(
                "\n(1) Cadastrar um novo Produto" +
                "\n(2) Listar todos os Produtos Cadastrados" +
                "\n(3) Deletar um Produto Cadastrado" +
                "\n(4) Atualizar um Produto já Cadastrado" +
                "\n(0) Para sair e encerrar o programa\n");
            Console.WriteLine("----------------------------------------");
            Console.Write("Selecione a Opção desejada: ");

            string opcao = Console.ReadLine();

            if (opcao == "0")
            {
                Console.WriteLine("O programa foi encerrado com sucesso!");
                break;
            }

            switch (opcao)
            {
                case "1":
                    CadastrarProduto(produtos); // Chama o método para cadastrar um novo produto
                    break;
                case "2":
                    ListarProdutos(produtos); // Chama o método para listar todos os produtos cadastrados
                    break;
                case "3":
                    DeletarProduto(produtos); // Chama o método para deletar um produto cadastrado
                    break;
                case "4":
                    AtualizarProduto(produtos); // Chama o método para atualizar atributos de produtos cadastrados
                    break;
                default:
                    mensagemPadrão(); // Chama o metódo para mostrar na tela a mensagem padrão de validação
                    break;
            }
        }
    }

    // Método para solicitar e ler um valor numérico do usuário
    static double pegarValor(string texto)
    {
        double valor;
        while (true)
        {
            Console.Write(texto);
            if (double.TryParse(Console.ReadLine(), out valor))
            {
                return valor;
            }
            Console.WriteLine("\nDigite um valor em R$ válido.");
        }
    }

    // Método para cadastrar um novo produto
    static void CadastrarProduto(List<Produto> produtos)
    {
        int maxCodigo = 1;
        foreach (var produto in produtos)
        {
            if (produto.Codigo >= maxCodigo)
            {
                maxCodigo = produto.Codigo + 1;
            }
        }

        // Criar um novo produto com o código
        Produto novoProduto = new Produto(maxCodigo);
        Console.Write("\nInsira a Descrição do Produto: ");
        novoProduto.Descricao = Console.ReadLine();
        novoProduto.ValorCusto = pegarValor("\nInsira o valor de Custo do produto: R$");
        novoProduto.MargemLucro = pegarValor("\nInsira a Margem de Lucro sobre o Produto(Em %): ");
        double calculoMargem = (novoProduto.ValorCusto * novoProduto.MargemLucro) / 100;
        novoProduto.ValorVenda = novoProduto.ValorCusto + calculoMargem;
        produtos.Add(novoProduto);
        Console.WriteLine($"\nProduto cadastrado com sucesso: \n{novoProduto}\n");
    }

    // Método para listar todos os produtos cadastrados
    static void ListarProdutos(List<Produto> produtos)
    {
        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado.");
            return;
        }

        Console.WriteLine("\nLista de Produtos Cadastrados:\n");
        foreach (var produto in produtos)
        {
            Console.WriteLine(produto);
        }
    }

    // Método para deletar um produto cadastrado
    static void DeletarProduto(List<Produto> produtos)
    {
        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado." +
                "\nPor favor cadastre qualquer produto antes de tentar efetuar uma exclusão.");
            return;
        }
        Console.WriteLine("\nDigite o código do produto que você deseja deletar: ");
        if (int.TryParse(Console.ReadLine(), out int codigoDeletado))
        {
            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado.");
                return;
            }
            for (int i = 0; i < produtos.Count; i++)
            {
                if (produtos[i].Codigo == codigoDeletado)
                {
                    Console.WriteLine($"Produto com código {codigoDeletado} deletado com sucesso.");
                    produtos.RemoveAt(i);
                    break;
                }
                else
                {
                    Console.WriteLine("Você ainda não tem nenhum produto cadastrado, por favor cadastre antes de efetuar a exclusão!");
                }
            }

        }
    }

    // Método para atualizar atributos de produtos cadastrados
    static void AtualizarProduto(List<Produto> produtos)
    {

        if (produtos.Count == 0)
        {
            Console.WriteLine("Você ainda não tem nenhum produto Cadastrado" +
                "Por favor cadastre o produto antes de tentar atualizar os atributos dele!");
            return;
        }
        ListarProdutos(produtos);
        Console.Write("\nDigite o CÓDIGO do produto que você deseja atualizar: ");
        if (int.TryParse(Console.ReadLine(), out int codigoAtualizar))
        {

            Produto? produtoParaAtualizar = null;

            // Encontrar o produto com o código fornecido
            foreach (var produto in produtos)
            {
                if (produto.Codigo == codigoAtualizar)
                {
                    produtoParaAtualizar = produto;
                    break;
                }
            }

            // Se o produto foi encontrado
            if (produtoParaAtualizar != null)
            {
                Console.Write("" +
                    "\n(1) Atualizar a Descrição do Produto" +
                    "\n(2) Atualizar o Valor de Custo do Produto" +
                    "\n(3) Atualizar a Margem de Lucro Sobre o Produto (Em %)" +
                    "\n(4) Atualizar o Valor de Venda do Produto" +
                    "\n(5) Atualizar o Código do Produto" +
                    "\n\nSelecione a opção para atualizar as informações de um produto: ");

                string? opcaoAtualizar = Console.ReadLine();

                switch (opcaoAtualizar)
                {
                    case "1":
                        // Armazena a descrição antiga
                        string descricaoAntiga = produtoParaAtualizar.Descricao;
                        Console.WriteLine($"\nA descrição atual é [{descricaoAntiga}]");
                        Console.WriteLine("Insira a nova descrição do produto: ");
                        produtoParaAtualizar.Descricao = Console.ReadLine();
                        Console.WriteLine("Descrição atualizada com sucesso!");
                        Console.WriteLine($"Descrição antiga: {descricaoAntiga} ; Nova descrição: {produtoParaAtualizar.Descricao}");
                        break;
                    case "2":
                        produtoParaAtualizar.ValorCusto = pegarValor("\nInsira o novo valor de Custo do produto: R$");
                        Console.WriteLine("Valor de Custo atualizado com sucesso!\nA partir de agora produto tem um novo Valor de Venda" +
                            " baseado no Valor de Custo atualizado!");
                        double novoCalculoMargem = (produtoParaAtualizar.ValorCusto * produtoParaAtualizar.MargemLucro) / 100;
                        produtoParaAtualizar.ValorVenda = produtoParaAtualizar.ValorCusto + novoCalculoMargem;
                        break;
                    case "3":
                        produtoParaAtualizar.MargemLucro = pegarValor("\nInsira a nova Margem de Lucro sobre o Produto: (%) ");
                        Console.WriteLine("Margem de Lucro atualizada com sucesso!");
                        double calculoMargem = (produtoParaAtualizar.ValorCusto * produtoParaAtualizar.MargemLucro) / 100;
                        produtoParaAtualizar.ValorVenda = produtoParaAtualizar.ValorCusto + calculoMargem;
                        break;
                    case "4":
                        double novoValorVenda = pegarValor("\nInsira o novo Valor de Venda do produto: R$");
                        produtoParaAtualizar.ValorVenda = novoValorVenda;
                        double novoCalculoVenda = ((produtoParaAtualizar.ValorVenda - produtoParaAtualizar.ValorCusto) / produtoParaAtualizar.ValorCusto) * 100;
                        produtoParaAtualizar.MargemLucro = novoCalculoVenda;

                        Console.WriteLine("O valor de venda foi alterado com sucesso!");
                        break;
                    case "5":
                        //AtualizarCodigoProduto(produtos, produtoParaAtualizar);
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Produto não encontrado.");
            }
        }

    }

    static void NovoCodigoProduto()
    {
        Console.WriteLine("Insira o novo código do produto: ");
        if (int.TryParse(Console.ReadLine(), out int novoCodigo))
        {
            Console.Write("");
        }
    }
    static void mensagemPadrão()
    {
        Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
    }

}

// Classe que representa um Produto, todos os seus getters e setters
public class Produto
{
    private int codigo;

    public Produto(int _codigo)
    {
        this.Codigo = _codigo;
    }

    public int Codigo
    {
        get { return codigo; }
        set { codigo = value; }
    }
    public string Descricao { get; set; }
    public double ValorVenda { get; set; }
    public double ValorCusto { get; set; }
    public double MargemLucro { get; set; }

    // Sobrescrita do método ToString para formatar a exibição do produto
    public override string ToString()
    {
        return $"\n*************************\nCódigo: {Codigo} \nDescrição: {Descricao}\nValor de Custo: R${ValorCusto} \nMargem de Lucro: {MargemLucro}%\nValor de Venda: R${ValorVenda}\n*************************";
    }
}

