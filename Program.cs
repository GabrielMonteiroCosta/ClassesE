class Program
{
    static void Main(string[] args)
    {
        List<Produto> produtos = new List<Produto>();
        List<String> produtosCad = new() { "Meia Soquete" , "Garrafa de Água" , "Estojo Preto" , " Mouse Gamer" };

        Console.WriteLine("Cadastro de Produtos!");

        while (true)
        {
            Console.WriteLine("\nDigite uma opção:" +
                "\n(1) Cadastrar um novo Produto" +
                "\n(2) Listar todos os Produtos cadastrados" +
                "\n(3) Deletar um produto cadastrado" +
                "\n(0) Para sair e encerrar o programa\n");

            string opcao = Console.ReadLine();

            if (opcao == "0")
            {
                Console.WriteLine("O programa foi encerrado com sucesso!");
                break;
            }

            switch (opcao)
            {
                case "1":
                    CadastrarProduto(produtos);
                    break;
                case "2":
                    ListarProdutos(produtos);
                    break;
                case "3":
                    ListarProdutos(produtos);
                    DeletarProduto(produtos);
                    break;
                default:
                    Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
                    break;
            }
        }
    }

    static double pegarValor(string texto)
    {
        double valor;
        while (true)
        {
            Console.WriteLine(texto);
            if (double.TryParse(Console.ReadLine(), out valor))
            {
                return valor;
            }
            Console.WriteLine("Digite um valor em R$ válido.");
        }
    }
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

        Produto novoProduto = new Produto(maxCodigo);
        Console.WriteLine("Insira a Descrição do Produto: ");
        novoProduto.Descricao = Console.ReadLine();
        novoProduto.ValorCusto = pegarValor("Insira o valor de Custo do produto: ");
        novoProduto.MargemLucro = pegarValor("Insira a Margem de Lucro sobre o Produto: ");
        double calculoMargem = (novoProduto.ValorCusto * novoProduto.MargemLucro) / 100;
        novoProduto.ValorVenda = novoProduto.ValorCusto + calculoMargem;
        produtos.Add(novoProduto);
        Console.WriteLine($"Produto cadastrado com sucesso: {novoProduto}");
    }
    static void ListarProdutos(List<Produto> produtos)
    {
        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado.");
            return;
        }

        Console.WriteLine("\nLista de Produtos Cadastrados:");
        foreach (var produto in produtos)
        {
            Console.WriteLine(produto);
        }
    }

    static void DeletarProduto(List<Produto> produtos)
    {
        
        Console.WriteLine("\nDigite o código do produto que você deseja deletar: ");
        if (int.TryParse(Console.ReadLine(), out int codigoDeletado))
        {
            for (int i = 0; i < produtos.Count; i++)
            {
                if (produtos[i].Codigo == codigoDeletado)
                {
                    Console.WriteLine($"Produto com código {codigoDeletado} deletado com sucesso.");
                    produtos.RemoveAt(i);
                    break;
                }
            }
        }
    }
}

public class Produto
{
    public Produto(int _codigo)
    {
        this.Codigo = _codigo;
    }

    public int Codigo { get; }
    public string Descricao { get; set; }
    public double ValorVenda { get; set; }
    public double ValorCusto { get; set; }
    public double MargemLucro { get; set; }

    public override string ToString()
    {
        return $"\n--------------------\nCódigo: {Codigo} \nDescrição: {Descricao}\nValor de Custo: R${ValorCusto} \nMargem de Lucro: {MargemLucro}%\nValor de Venda: R${ValorVenda}\n--------------------";
    }
}
