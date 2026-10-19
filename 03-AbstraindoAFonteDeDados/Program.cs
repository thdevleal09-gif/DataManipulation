/*
    Seja um arquivo com músicas em formato CSV (Comma Separated Values). 

    Implemente as funções abaixo:
    //     [ ] Leia-o como uma coleção de músicas
    //     [ ] Filtre a coleção por artista (por ex. Coldplay, Metallica, AC/DC)
    //     [ ] Filtre a coleção por gênero (por ex. rock)
    //     [ ] Filtre a coleção por duração (por ex. maiores que 5 minutos)
    //     [ ] Ordene a coleção por artista
    //     [ ] Ordene a coleção por artista e em seguida por músicas com duração crescente
    //     [ ] Crie uma coleção de artistas e suas músicas
    //     [ ] Informe a duração média das músicas da coleção
    //     [ ] Informe a duração total das músicas da coleção
    //     [ ] Informe qual artista tem mais músicas na coleção
 
*/

using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.IO;

var arquivo = new FileStream(@"arquivos/musicas.csv", FileMode.Open, FileAccess.Read);
using var stream = new StreamReader(arquivo);

var musicas = ObterMusicas(stream);
ExibirMusicas(musicas);





  void ExibirMusicas(IEnumerable<Musica> musicas)
 {
    var contador = 1;
    Console.WriteLine($"\nExibindo as musicas:");
    foreach (var musica in musicas)
    {

        Console.WriteLine($"\t - {musica.Titulo}");
        contador++;
        if (contador > 10) break;

    }
}


IEnumerable<Musica> ObterMusicas(StreamReader stream)
{
    var linha = stream.ReadLine();

    while (linha is not null)
    {
        var partes = linha.Split(';');

        Musica musica = new Musica
        {
            Titulo = partes[0],
            Artista = partes[1],
            Duracao = Convert.ToInt32(partes[2])

        };
        yield return musica;

        linha = stream.ReadLine();


    }
}
 internal class Musica
{

    public string Titulo { get; set; }
    public string Artista { get; set; }
    public int Duracao { get; set; }

}