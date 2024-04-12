using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
List<Produto> produtos = new();
Console.WriteLine("Cadastro de Produtos!");

while (true)
{
    Console.WriteLine("\nDigite uma opção:" +
        "\n(1) Cadastrar um novo Produto" +
        "\n(2) Listar todos os Produtos cadastrados" +
        "\n(0) Para sair e encerrar o programa");

    string opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            int codigo = 1;
            if (codigo == 1)
            {
                codigo = codigo + 1;
            }
                    
            Produto produto = new(codigo);
            Console.WriteLine("Insira a Descrição do Produto: ");
            produto.Descricao = Console.ReadLine();
            produto.ValorCusto = pegarValor("Insira o valor de Custo do produto: ");
            produto.MargemLucro = pegarValor("Insira a Margem de Lucro sobre o Produto: ");
            double calculoMargem = (produto.ValorCusto * produto.MargemLucro) / 100;
            produto.ValorVenda = produto.ValorCusto + calculoMargem;
            produtos.Add(produto);
            Console.WriteLine($"Código do produto: {produto.Codigo}, Descrição: {produto.Descricao}; Valor de Venda: R${produto.ValorVenda}" +
                $", Valor de Custo: R${produto.ValorCusto}, Margem de Lucro: {produto.MargemLucro}%");
            break;
        case "2":

            break;
    }


    double pegarValor(string texto)
    {
        double valor = transformarValor(texto);
        return valor;

        double transformarValor(string texto)
        {
            while (true)
            {
                Console.WriteLine(texto);
                string? valor = Console.ReadLine();
                if (double.TryParse(valor, out double valorFinal))
                {
                    return valorFinal;
                }
                Console.WriteLine("Digite um valor em R$");
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

    public int Codigo
    {
        get;
    }
    public string Descricao { get; set; }
    public double ValorVenda { get; set; }
    public double ValorCusto { get; set; }
    public double MargemLucro { get; set; }
    
    
}
