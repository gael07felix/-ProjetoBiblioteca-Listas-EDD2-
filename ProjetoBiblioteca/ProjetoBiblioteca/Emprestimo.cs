public class Emprestimo
{
    private DateTime dtEmprestimo;
    private DateTime dtDevolucao;

    public DateTime DtEmprestimo => dtEmprestimo;
    // DateTime.MinValue representa um emprestimo ainda nao devolvido.
    public DateTime DtDevolucao => dtDevolucao;
    public bool Ativo => dtDevolucao == DateTime.MinValue;

    public Emprestimo(DateTime data)
    {
        dtEmprestimo = data;
        dtDevolucao = DateTime.MinValue;
    }

    public void RegistrarDevolucao(DateTime data)
    {
        if (!Ativo) throw new InvalidOperationException("Emprestimo ja devolvido.");
        if (data < dtEmprestimo) throw new ArgumentException("Data de devolucao invalida.");
        dtDevolucao = data;
    }
}
