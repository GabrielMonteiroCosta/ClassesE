class Program
{
    static void Main(string[] args)
    {
        // Lista para armazenar os produtos
        List<Produto> produtos = new List<Produto>();
        Console.WriteLine("Cadastro de Produtos!");


        // Loop principal do programa - Opções iniciais para o usuário
        while (true)
        {
            Console.WriteLine("\nDigite uma opção:" +
                "\n(1) Cadastrar um novo Produto" +
                "\n(2) Listar todos os Produtos cadastrados" +
                "\n(3) Deletar um produto cadastrado" +
                "\n(4) Atualizar atributos de produtos cadastrados" +
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
                    CadastrarProduto(produtos); // Chama o método para cadastrar um novo produto
                    break;
                case "2":
                    ListarProdutos(produtos); // Chama o método para listar todos os produtos cadastrados
                    break;
                case "3":
                    ListarProdutos(produtos);
                    DeletarProduto(produtos); // Chama o método para deletar um produto cadastrado
                    break;
                    case "4":
                        AtualizarProduto(produtos); // Chama o método para atualizar atributos de produtos cadastrados
                    break;
                default:
                    Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
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
            Console.WriteLine(texto);
            if (double.TryParse(Console.ReadLine(), out valor))
            {
                return valor;
            }
            Console.WriteLine("Digite um valor em R$ válido.");
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
        Console.WriteLine("Insira a Descrição do Produto: ");
        novoProduto.Descricao = Console.ReadLine();
        novoProduto.ValorCusto = pegarValor("Insira o valor de Custo do produto: ");
        novoProduto.MargemLucro = pegarValor("Insira a Margem de Lucro sobre o Produto: ");
        double calculoMargem = (novoProduto.ValorCusto * novoProduto.MargemLucro) / 100;
        novoProduto.ValorVenda = novoProduto.ValorCusto + calculoMargem;
        produtos.Add(novoProduto);
        Console.WriteLine($"Produto cadastrado com sucesso: {novoProduto}");
    }

    // Método para listar todos os produtos cadastrados
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

    // Método para deletar um produto cadastrado
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

    // Método para atualizar atributos de produtos cadastrados
    static void AtualizarProduto(List<Produto> produtos)
    {

        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado.");
            return;
        }

        ListarProdutos(produtos);
        Console.WriteLine("\nDigite o --código-- do produto que você deseja atualizar: ");
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
                Console.WriteLine("\nQual atributo você deseja atualizar?" +
                    "\n(1) Descrição" +
                    "\n(2) Valor de Custo" +
                    "\n(3) Margem de Lucro");

                string? opcaoAtualizar = Console.ReadLine();

                switch (opcaoAtualizar)
                {
                    case "1":
                        // Armazena a descrição antiga
                        string descricaoAntiga = produtoParaAtualizar.Descricao;
                        Console.WriteLine("\nInsira a nova descrição do produto: ");
                        produtoParaAtualizar.Descricao = Console.ReadLine();
                        Console.WriteLine("Descrição atualizada com sucesso!");
                        Console.WriteLine($"Descrição antiga: {descricaoAntiga} ; Nova descrição: {produtoParaAtualizar.Descricao}");
                        break;
                    case "2":
                        produtoParaAtualizar.ValorCusto = pegarValor("\nInsira o novo valor de Custo do produto: ");
                        Console.WriteLine("Valor de Custo atualizado com sucesso!");
                        double novoCalculoMargem = (produtoParaAtualizar.ValorCusto * produtoParaAtualizar.MargemLucro) / 100;
                        produtoParaAtualizar.ValorVenda = produtoParaAtualizar.ValorCusto + novoCalculoMargem;
                        break;
                    case "3":
                        produtoParaAtualizar.MargemLucro = pegarValor("\nInsira a nova Margem de Lucro sobre o Produto: ");
                        Console.WriteLine("Margem de Lucro atualizada com sucesso!");
                        double calculoMargem = (produtoParaAtualizar.ValorCusto * produtoParaAtualizar.MargemLucro) / 100;
                        produtoParaAtualizar.ValorVenda = produtoParaAtualizar.ValorCusto + calculoMargem;
                        break;
                    /*case "4":
                        AtualizarCodigoProduto(produtos, produtoParaAtualizar);
                        break;*/
                    default:
                        Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Produto não encontrado.");
        }
            
        }
    /*static void AtualizarCodigoProduto(List<Produto> produtos, Produto produtoParaAtualizar)
    {
        Console.WriteLine("\nInsira o novo código para o produto: ");
        if (int.TryParse(Console.ReadLine(), out int novoCodigo))
        {
            bool codigoExiste = produtos.Exists(p => p.Codigo == novoCodigo);
            if (!codigoExiste)
            {
                produtoParaAtualizar.Codigo = novoCodigo;
                Console.WriteLine("Código do Produto atualizado com sucesso!");
            }
            else
            {
                Console.WriteLine("O novo código de produto já está em uso. Por favor, escolha outro.");
            }
        }
        else
        {
            Console.WriteLine("Código inválido. Por favor, insira um número inteiro válido.");
        }

    }*/
}
    

// Classe que representa um Produto, todos os seus getters e setters
public class Produto
{
    //private int codigo;

    public Produto(int _codigo)
    {
        this.Codigo = _codigo;
    }

    /*public int Codigo
    {
        get { return codigo; }
        set { codigo = value; }
    }*/
    public int Codigo { get; }
    public string Descricao { get; set; }
    public double ValorVenda { get; set; }
    public double ValorCusto { get; set; }
    public double MargemLucro { get; set; }

    // Sobrescrita do método ToString para formatar a exibição do produto
    public override string ToString()
    {
        return $"\n--------------------\nCódigo: {Codigo} \nDescrição: {Descricao}\nValor de Custo: R${ValorCusto} \nMargem de Lucro: {MargemLucro}%\nValor de Venda: R${ValorVenda}\n--------------------";
    }
}
