#nullable disable
using System;

class Program{
    static void Main(string[] args){
        string connectionString = "server=localhost;database=banco_c_sharp_fun;user=root;password=root;";
        ProdutoRepository repository = new ProdutoRepository(connectionString);

        bool sair = false;

        while (!sair){
            Console.WriteLine("===== MENU =====");
            Console.WriteLine("1. Criar um item");
            Console.WriteLine("2. Excluir um item");
            Console.WriteLine("3. Verificar lista de itens cadastrados");
            Console.WriteLine("4. Alterar nome ou preço de um item");
            Console.WriteLine("5. Sair");
            Console.WriteLine("================");

            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            switch (opcao){
                case "1":
                    CriarItem(repository);
                    break;
                case "2":
                    ExcluirItem(repository);
                    break;
                case "3":
                    ListarItens(repository);
                    break;
                case "4":
                    AlterarItem(repository);
                    break;
                case "5":
                    sair = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void CriarItem(ProdutoRepository repository){
        Console.WriteLine("=== Criar Item ===");
        Console.Write("Nome: ");
        string nome = Console.ReadLine();
        Console.Write("Preço: ");
        decimal preco = Convert.ToDecimal(Console.ReadLine());

        Produto produto = new Produto
        {
            Nome = nome,
            Preco = preco
        };

        repository.AddProduto(produto);

        Console.WriteLine("Item criado com sucesso.");
    }

    static void ExcluirItem(ProdutoRepository repository){
        Console.WriteLine("=== Excluir Item ===");
        Console.Write("ID do item: ");
        int id = Convert.ToInt32(Console.ReadLine());

        repository.DeleteProduto(id);

        Console.WriteLine("Item excluído com sucesso.");
    }

    static void ListarItens(ProdutoRepository repository){
        Console.WriteLine("=== Lista de Itens ===");
        var produtos = repository.GetAllProdutos();
        foreach (var produto in produtos) {
            Console.WriteLine($"ID: {produto.Id}, Nome: {produto.Nome}, Preço: {produto.Preco}");
        }
    }

    static void AlterarItem(ProdutoRepository repository){
    Console.WriteLine("=== Alterar Nome ou Preço de um Item ===");
    Console.Write("ID do item: ");
    int id = Convert.ToInt32(Console.ReadLine());

    var produto = repository.GetAllProdutos().FirstOrDefault(p => p.Id == id);
    if (produto == null){
        Console.WriteLine("Item não encontrado.");
        return;
    }

    Console.Write("Novo nome (pressione Enter para manter o atual): ");
    string novoNome = Console.ReadLine();

    Console.Write("Novo preço (pressione Enter para manter o atual): ");
    string novoPrecoStr = Console.ReadLine();
    decimal novoPreco;
    if (!string.IsNullOrEmpty(novoPrecoStr) && decimal.TryParse(novoPrecoStr, out novoPreco)){
        produto.Preco = novoPreco;
    }

    if (!string.IsNullOrEmpty(novoNome)){
        produto.Nome = novoNome;
    }

    repository.UpdateProduto(produto);

    Console.WriteLine("Item alterado com sucesso.");
}

}
