public class Livros
{
    private List<Livro> acervo = new();
    public IReadOnlyList<Livro> Acervo => acervo.AsReadOnly();

    public void adicionar(Livro livro)
    {
        if (pesquisar(livro) != null)
            throw new InvalidOperationException("Ja existe livro com esse ISBN.");
        acervo.Add(livro);
    }

    public Livro? pesquisar(Livro livro) => acervo.FirstOrDefault(l => l.Isbn == livro.Isbn);
    public Livro? pesquisarPorIsbn(int isbn) => acervo.FirstOrDefault(l => l.Isbn == isbn);
    public bool tomboExiste(int tombo) => acervo.Any(l => l.Exemplares.Any(e => e.Tombo == tombo));
}
