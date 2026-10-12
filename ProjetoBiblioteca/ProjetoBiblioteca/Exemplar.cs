public class Exemplar
{
    private int tombo;
    private List<Emprestimo> emprestimos = new();

    public int Tombo => tombo;
    public IReadOnlyList<Emprestimo> Emprestimos => emprestimos.AsReadOnly();

    public Exemplar(int tombo) => this.tombo = tombo;

    public bool disponivel() => !emprestimos.Any(e => e.Ativo);

    public bool emprestar()
    {
        if (!disponivel()) return false;
        emprestimos.Add(new Emprestimo(DateTime.Now));
        return true;
    }

    public bool devolver()
    {
        var ativo = emprestimos.LastOrDefault(e => e.Ativo);
        if (ativo == null) return false;
        ativo.RegistrarDevolucao(DateTime.Now);
        return true;
    }

    public int qtdeEmprestimos() => emprestimos.Count;
}
