using System;
using System.Linq;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
var biblioteca = new Livros();

while (true)
{
    Console.WriteLine("\n========== BIBLIOTECA ==========");
    Console.WriteLine("0 - Sair");
    Console.WriteLine("1 - Adicionar livro");
    Console.WriteLine("2 - Pesquisar livro (sintetico)");
    Console.WriteLine("3 - Pesquisar livro (analitico)");
    Console.WriteLine("4 - Adicionar exemplar");
    Console.WriteLine("5 - Registrar emprestimo");
    Console.WriteLine("6 - Registrar devolucao");
    int opcao = LerInt("Escolha uma opcao: ", permitirZero: true);

    switch (opcao)
    {
        case 0:
            Console.WriteLine("Programa encerrado.");
            return;
        case 1:
            AdicionarLivro();
            break;
        case 2:
        case 3:
            var livro = SelecionarLivro();
            if (livro != null)
            {
                MostrarResumo(livro);
                if (opcao == 3) MostrarDetalhes(livro);
            }
            break;
        case 4:
        {
            var selecionado = SelecionarLivro();
            if (selecionado == null) break;
            int tombo = LerInt("Numero do tombo: ");
            if (biblioteca.tomboExiste(tombo))
                Console.WriteLine("Tombo ja cadastrado no acervo.");
            else
            {
                selecionado.adicionarExemplar(new Exemplar(tombo));
                Console.WriteLine("Exemplar adicionado com sucesso.");
            }
            break;
        }
        case 5:
        case 6:
        {
            var selecionado = SelecionarLivro();
            if (selecionado == null) break;
            int tombo = LerInt("Numero do tombo: ");
            var exemplar = selecionado.buscarExemplar(tombo);
            if (exemplar == null)
            {
                Console.WriteLine("Exemplar nao encontrado neste livro.");
                break;
            }
            bool sucesso = opcao == 5 ? exemplar.emprestar() : exemplar.devolver();
            Console.WriteLine(sucesso
                ? (opcao == 5 ? "Emprestimo registrado." : "Devolucao registrada.")
                : (opcao == 5 ? "Exemplar ja esta emprestado." : "Exemplar nao possui emprestimo ativo."));
            break;
        }
        default:
            Console.WriteLine("Opcao invalida.");
            break;
    }
}

void AdicionarLivro()
{
    int isbn = LerInt("ISBN (numero inteiro): ");
    if (biblioteca.pesquisarPorIsbn(isbn) != null)
    {
        Console.WriteLine("Ja existe livro com esse ISBN.");
        return;
    }
    string titulo = LerTexto("Titulo: ");
    string autor = LerTexto("Autor: ");
    string editora = LerTexto("Editora: ");
    biblioteca.adicionar(new Livro(isbn, titulo, autor, editora));
    Console.WriteLine("Livro adicionado com sucesso.");
}

Livro? SelecionarLivro()
{
    int isbn = LerInt("Informe o ISBN: ");
    // A pesquisa usa a classe Livros conforme o diagrama.
    var encontrado = biblioteca.pesquisar(new Livro(isbn, "", "", ""));
    if (encontrado == null) Console.WriteLine("Livro nao encontrado.");
    return encontrado;
}

void MostrarResumo(Livro livro)
{
    Console.WriteLine("\n--- Dados do livro ---");
    Console.WriteLine($"ISBN: {livro.Isbn}");
    Console.WriteLine($"Titulo: {livro.Titulo}");
    Console.WriteLine($"Autor: {livro.Autor}");
    Console.WriteLine($"Editora: {livro.Editora}");
    Console.WriteLine($"Total de exemplares: {livro.qtdeExemplares()}");
    Console.WriteLine($"Exemplares disponiveis: {livro.qtdeDisponiveis()}");
    Console.WriteLine($"Total historico de emprestimos: {livro.qtdeEmprestimos()}");
    Console.WriteLine($"Disponibilidade: {livro.percDisponibilidade():F2}%");
}

void MostrarDetalhes(Livro livro)
{
    Console.WriteLine("\n--- Exemplares e historico ---");
    if (livro.qtdeExemplares() == 0)
    {
        Console.WriteLine("Nenhum exemplar cadastrado.");
        return;
    }
    foreach (var exemplar in livro.Exemplares)
    {
        Console.WriteLine($"\nTombo: {exemplar.Tombo} | Status: {(exemplar.disponivel() ? "Disponivel" : "Emprestado")}" +
                          $" | Emprestimos: {exemplar.qtdeEmprestimos()}");
        if (exemplar.Emprestimos.Count == 0)
            Console.WriteLine("  Nenhum emprestimo.");
        foreach (var emprestimo in exemplar.Emprestimos)
        {
            string devolucao = emprestimo.Ativo ? "Pendente" : emprestimo.DtDevolucao.ToString("dd/MM/yyyy HH:mm");
            Console.WriteLine($"  Emprestimo: {emprestimo.DtEmprestimo:dd/MM/yyyy HH:mm} | Devolucao: {devolucao}");
        }
    }
}

int LerInt(string mensagem, bool permitirZero = false)
{
    while (true)
    {
        Console.Write(mensagem);
        if (int.TryParse(Console.ReadLine(), out int valor) && (permitirZero ? valor >= 0 : valor > 0))
            return valor;
        Console.WriteLine("Digite um numero inteiro valido" + (permitirZero ? " (zero ou maior)." : " (maior que zero)."));
    }
}

string LerTexto(string mensagem)
{
    while (true)
    {
        Console.Write(mensagem);
        string texto = (Console.ReadLine() ?? "").Trim();
        if (!string.IsNullOrWhiteSpace(texto)) return texto;
        Console.WriteLine("Este campo nao pode ficar vazio.");
    }
}
