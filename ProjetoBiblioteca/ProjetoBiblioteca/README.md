# Projeto Biblioteca - C# Console

Atividade da Semana 8 (21/09/2026): cadastro de livros, exemplares, emprestimos e devolucoes.

## Como executar

1. Instale o SDK .NET 8 ou superior.
2. Abra a pasta `ProjetoBiblioteca` no Visual Studio ou Visual Studio Code.
3. No terminal, execute `dotnet run`.

## Estrutura

- `Livro.cs`: dados do titulo e indicadores de disponibilidade.
- `Exemplar.cs`: exemplar fisico, historico e operacoes de emprestimo e devolucao.
- `Emprestimo.cs`: datas do emprestimo e da devolucao.
- `Livros.cs`: cadastro e pesquisa por ISBN.
- `Program.cs`: menu interativo e validacao das entradas.

## Regras

- ISBN e tombo sao inteiros positivos, conforme diagrama do exercicio.
- ISBN nao se repete no acervo; tombo nao se repete em nenhum exemplar.
- Nao e possivel emprestar um exemplar ja emprestado ou devolver um exemplar sem emprestimo ativo.
- A quantidade de emprestimos conta o historico completo, incluindo emprestimos ja devolvidos.
- Disponibilidade = exemplares disponiveis / total de exemplares * 100 (zero quando nao ha exemplares).
- Datas sao registradas automaticamente pelo relogio local.
- Os dados ficam apenas em memoria: fechando o programa, o acervo e perdido.
- Para seguir o diagrama, `DtDevolucao` e um `DateTime` que permanece em `DateTime.MinValue` ate a devolucao.

## Exemplo de teste

1. Adicionar livro com ISBN `1234`.
2. Adicionar dois exemplares, tombos `10` e `11`.
3. Emprestar tombo `10`: disponibilidade de 50%.
4. Pesquisar analiticamente: tombo `10` fica emprestado e traz data do emprestimo.
5. Devolver tombo `10`: disponibilidade de 100%, com data da devolucao no historico.
