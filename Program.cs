using System.Collections;

DiasDaSemana DiasDaSemana = new DiasDaSemana();


List<Produto> carros = new List<Produto>()
{
new Produto() { Nome = "Fusca", Preco = 10000 },
new Produto() { Nome = "Gol", Preco = 20000 },
new Produto() { Nome = "Civic", Preco = 30000 },
new Produto() { Nome = "Corolla", Preco = 40000 },
};

IEnumerable<int> numerosParesComYield = NumerosParesComYield(10000);
var contador = 0;
foreach( var par in numerosParesComYield)
{
    contador++;
    Console.WriteLine(par);
    if(contador > 10) break;
}

IEnumerable<int> NumerosParesSemYield(int limite)
 {
    var lista = new List<int>();
    for (int i = 0; i <= limite; i++)
    {
        if (i % 2 == 0)
        {
            Console.WriteLine($"Processando elemento: {i} ");
            lista.Add(i);
        }
    }
    return lista;

}

IEnumerable<int> NumerosParesComYield(int limite)
{
    for(var i = 0; i <= limite; i++)
    {
        Console.WriteLine($"Processando elemento: {i} " );
        yield return i * 2; //arquivo -> elemento
    }

}


void PercorrendoComEnumerator()
{
    foreach (var dia in DiasDaSemana)
    {

        Console.WriteLine(dia);
    }
}

void PercorrendoComEnumeratorManual()
{
    var enumerator = DiasDaSemana.GetEnumerator();
    while (enumerator.MoveNext())
    {
        var dia = enumerator.Current;
        Console.WriteLine(dia);
    }

}

void PercorrerListaFor()
{
    for (int i = 0; i < carros.Count; i++)
    {
        var produto = carros[i];
        Console.WriteLine($"Nome: {produto.Nome} - Preço: {produto.Preco}");
    }

}
    void PercorrerListaForeach()
    {
        foreach (var produto in carros)
        {
            Console.WriteLine($"Nome: {produto.Nome} - Preço: {produto.Preco}");
        }

    }


class Produto
{

    public string Nome { get; set; }
    public double Preco { get; set; }

}

class DiasDaSemanaEnumerator : IEnumerator<string>
{

    private int posicaoAtual = -1;
    private string[] dias = { "Domingo", "Segunda-feira", "Terça-feira", "Quarta-feira", "Quinta-feira", "Sexta-feira", "Sábado" };

    public string Current => dias[posicaoAtual];
    object IEnumerator.Current => Current;

    public void Dispose()
    {
       

    }

    public bool MoveNext()
    {
        posicaoAtual++;
        return posicaoAtual < dias.Length;

    }

    public void Reset()
    {
        posicaoAtual = -1;
    }

}

    class DiasDaSemana : IEnumerable<string>
    {
      

        public IEnumerator<string> GetEnumerator()
        {
        yield return "Domingo";
        yield return "Segunda-feira";
        yield return "Terça-feira";
        yield return "Quarta-feira";
        yield return "Quinta-feira";
        yield return "Sexta-feira";
        yield return "Sábado";
    }


    IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }





