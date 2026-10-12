public class Livro
{
    private int isbn;
    private string titulo;
    private string autor;
    private string editora;
    private List<Exemplar> exemplares = new();

    public int Isbn => isbn;
    public string Titulo => titulo;
    public string Autor => autor;
    public string Editora => editora;
    public IReadOnlyList<Exemplar> Exemplares => exemplares.AsReadOnly();

    public Livro(int isbn, string titulo, string autor, string editora)
    {
        this.isbn = isbn;
        this.titulo = titulo;
        this.autor = autor;
        this.editora = editora;
    }

    public void adicionarExemplar(Exemplar exemplar)
    {
        if (exemplares.Any(e => e.Tombo == exemplar.Tombo))
            throw new InvalidOperationException("Este tombo ja existe neste livro.");
        exemplares.Add(exemplar);
    }

    public int qtdeExemplares() => exemplares.Count;
    public int qtdeDisponiveis() => exemplares.Count(e => e.disponivel());
    public int qtdeEmprestimos() => exemplares.Sum(e => e.qtdeEmprestimos());
    public double percDisponibilidade() => qtdeExemplares() == 0
        ? 0 : 100.0 * qtdeDisponiveis() / qtdeExemplares();

    public Exemplar? buscarExemplar(int tombo) => exemplares.FirstOrDefault(e => e.Tombo == tombo);
}
