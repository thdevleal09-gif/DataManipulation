/*
Seja um aplicativo de gerenciamento de músicas onde os usuários podem organizar suas faixas
favoritas em playlists personalizadas. Para cada playlist, é essencial que o usuário tenha
controle total sobre a sequência de reprodução das músicas, permitindo reordená-las
livremente a qualquer momento. Além disso, o aplicativo precisa oferecer a funcionalidade de
reprodução aleatória para uma playlist específica, proporcionando uma experiência de audição
dinâmica e variada, sem, contudo, alterar a ordem original que o usuário definiu. O desafio
é criar uma estrutura robusta que suporte a adição e remoção eficiente de músicas, a
reordenação flexível dentro das playlists e a seleção de faixas tanto em modo sequencial
quanto aleatório.
*/

// Funcoes que vamos implementar:
// [x] Criar as classes para musicas e playlist
// [x] Listar musicas da playlist
// [x] Adicionar musica à playlist
// [x] Obter uma musica especifica da playlist
// [x] Remover musica da playlist
// [x] Tocar uma musica aleatoria da playlist
// [x] Reordenar musicas segundo alguma logica especifica (ex. duracao)
// [x] Uma playlist nao pode ter musicas repetidas
// [x] Exibir as 3 musicas mais tocadas em todas as playlists (ranking)
// [ ] Player de musica com:
// [x] - Fila de reproducao (para musicas avulsas e/ou playlists)
// [x] - Historico de reproducao

using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using static System.Net.WebRequestMethods;


internal class Program
{
    private static void Main(string[] args)
    {
        var musica1 = new Musica { Titulo = "ASTROTHUNDER", Artista = "TRAVIS SCOTT", Duracao = 142 };
        var musica2 = new Musica { Titulo = "SICKO MODE", Artista = "TRAVIS SCOTT", Duracao = 312 };
        var musica3 = new Musica { Titulo = "STARGAZING", Artista = "TRAVIS SCOTT", Duracao = 240 };
        var musica4 = new Musica { Titulo = "HIGHEST IN THE ROOM", Artista = "TRAVIS SCOTT", Duracao = 200 };
        var musica5 = new Musica { Titulo = "STOP TRYING TO BE GOD", Artista = "TRAVIS SCOTT", Duracao = 300 };
        var musica6 = new Musica { Titulo = "BUTTERFLY EFFECT", Artista = "TRAVIS SCOTT", Duracao = 180 };
        var musica7 = new Musica { Titulo = "Pega Ladrao!", Artista = "GABRIEL O PENSADOR", Duracao = 240 };
        var musica8 = new Musica { Titulo = "Cachimbo da Paz", Artista = "GABRIEL O PENSADOR", Duracao = 300 };
        var musica9 = new Musica { Titulo = "Lôraburra", Artista = "GABRIEL O PENSADOR", Duracao = 180 };
        var musica10 = new Musica { Titulo = "Festa da Música", Artista = "GABRIEL O PENSADOR", Duracao = 240 };
        var musica11 = new Musica { Titulo = "Cachimbo da Paz", Artista = "GABRIEL O PENSADOR", Duracao = 300 };
        var musica12 = new Musica { Titulo = "Lôraburra", Artista = "GABRIEL O PENSADOR", Duracao = 180 };



        var playlist1 = new Playlist { Nome = "Minhas Favoritas" };
        playlist1.Add(musica1);
        playlist1.Add(musica2);
        playlist1.Add(musica3);
        playlist1.Add(musica4);
        playlist1.Add(musica5);
        playlist1.Add(musica6);
        playlist1.Add(musica7);
        playlist1.Add(musica8);
        playlist1.Add(musica9);
        playlist1.Add(musica10);
        playlist1.Add(musica11);
        playlist1.Add(musica12);
        playlist1.Add(new Musica { Titulo = "Cachimbo da Paz", Artista = "GABRIEL O PENSADOR", Duracao = 300 });


        Playlist MelhoresDoTravis = new Playlist { Nome = "As melhores do Travis" };
        MelhoresDoTravis.Add(musica1);
        MelhoresDoTravis.Add(musica2);
        MelhoresDoTravis.Add(musica3);
        MelhoresDoTravis.Add(musica4);
        MelhoresDoTravis.Add(musica5);
        MelhoresDoTravis.Add(musica6);

        // ExibirPlaylist(playlist1);

        var player = new PlayerDeMusica();
        player.AdicionarNaFila(musica1);
        player.AdicionarNaFila(MelhoresDoTravis);

        ExibirFila(player);
        ExibirHistorico(player);


        var proxima = player.ProximaMusicaDaFila();
        if(proxima is not null)

            Console.WriteLine($"Tocando a musica {proxima.Titulo}...");

        else Console.WriteLine("Fila de reproducao vazia!");

        ExibirFila(player);
        ExibirHistorico(player);

        var anterior = player.MusicaAnterior();
        if(anterior is not null)
            Console.WriteLine($"\nTocando a musica {anterior.Titulo}...");
        else Console.WriteLine("\nFila de reproducao vazia!");

        void ExibirHistorico(PlayerDeMusica player)
        {

            Console.WriteLine($"\nExibindo o historico:");
            foreach (var musica in player.Historico())
            {
                Console.WriteLine($"\t- {musica.Titulo}");
                    
            }
        }

        void ExibirFila(PlayerDeMusica player)
        {
            Console.WriteLine("\nExibindo a fila de reproducao: ");
            foreach (var musica in player.Fila())
            {
                Console.WriteLine($"\t - {musica.Titulo}");
            }
        }


        void ExibirPlaylist(Playlist playlist)
        {
            Console.WriteLine($"\n Tocando as musicas de {playlist.Nome}");
            foreach (var musica in playlist)
            {
                Console.WriteLine($"\t - {musica.Titulo} - {musica.Artista} - ({musica.Duracao}s)");

            }
        }

        void ExibirMaisTocadas(Playlist playlist1, Playlist playlist2)
        {
            // Musica(key), Contagem(valor)
            Dictionary<Musica, int> ranking = [];

            foreach (var musica in playlist1)
            {
                ranking.Add(musica, 1);
            }
            foreach (var musica in playlist2)
            {

                if (ranking.TryGetValue(musica, out int contagem))
                {
                    contagem++;
                    ranking[musica] = contagem;

                }
                else
                {
                    ranking[musica] = 1;
                }

                List<KeyValuePair<Musica, int>> top = new(ranking); //[..ranking];
                top.Sort(new PorContagem());

                Console.WriteLine("\nTop 3 - musicas mais incluidas nas playlists");
                int contador = 1;
                foreach (var par in top)
                {

                    Console.WriteLine($" - {par.Key.Titulo}");
                    contador++;
                    if (contador > 3) break;
                    
                }
            }



            void RemoverMusicaPeloTitulo(Playlist playlist, string titulo)
            {
                Musica? musicaEncontrada = playlist.ObterPeloTitulo(titulo);
                if (musicaEncontrada is not null)
                {
                    Console.WriteLine("\nRemovendo a musica: " + musicaEncontrada.Titulo);
                    playlist.Remove(musicaEncontrada);
                }
                else
                {
                    Console.WriteLine("\nMusica nao encontrada na playlist.");
                }
                ExibirPlaylist(playlist1);
            }

            void ExibirMusicaAleatoria(Playlist playlist)
            {
                Musica? musicaAleatoria = playlist1.ObterMusicaAleatoria();
                if (musicaAleatoria is not null)
                {
                    Console.WriteLine($"\nTocando musica aleatoria: {musicaAleatoria.Titulo} - {musicaAleatoria.Artista}");
                }
                else
                {
                    Console.WriteLine("\nA playlist está vazia.");
                }
            }
        }
    }

    class PorContagem : IComparer<KeyValuePair<Musica, int>>
    {
        public int Compare(KeyValuePair<Musica, int> x, KeyValuePair<Musica, int> y)
        {

            return y.Value.CompareTo(x.Value);
        }
    }

    class PorArtista : IComparer<Musica>
    {
        public int Compare(Musica? x, Musica? y)
        {
            if (x is null || y is null) return 0;
            if (x is null) return 1;
            if (y is null) return -1;
            return x.Artista.CompareTo(y.Artista);
        }
    }
    class PorTitulo : IComparer<Musica>
    {
        public int Compare(Musica? x, Musica? y)
        {
            if (x is null || y is null) return 0;
            if (x is null) return 1;
            if (y is null) return -1;
            return x.Titulo.CompareTo(y.Titulo);
        }
    }
    class Musica : IComparable
    {
        public string Titulo { get; set; }
        public string Artista { get; set; }
        public int Duracao { get; set; }

        public int CompareTo(object? other) // iguais: 0; menor: -1; maior: 1
        {
            if (other is null) return -1;
            if (other is Musica otherMusica) return this.Duracao.CompareTo(otherMusica.Duracao);
            return -1;
        }
        public override bool Equals(object? obj)
        {
            if (obj is Musica otherMusica)
            {
                return this.Titulo == otherMusica.Titulo && this.Artista == otherMusica.Artista;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.Titulo.GetHashCode() ^ this.Artista.GetHashCode();
        }
    }
    class Playlist : ICollection<Musica>
    {
        public string Nome { get; set; }

        public int Count => musicas.Count;

        public bool IsReadOnly => false;

        private HashSet<Musica> musicasSet = new HashSet<Musica>();
        private List<Musica> musicas = [];

        public IEnumerator<Musica> GetEnumerator()
        {
            return musicas.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Musica musica)
        {
            Musica? musicaExistente = ObterPeloTitulo(musica.Titulo);
            if (musicaExistente is null)
            {
                musicas.Add(musica);
                musicasSet.Add(musica);
            }
            else
            {
                Console.WriteLine($"Musica já existe na playlist: {musica.Titulo}");
            }
        }

        public void Clear()
        {
            musicas.Clear();
            musicasSet.Clear();
        }

        public bool Contains(Musica item)
        {
            return musicas.Contains(item);
        }

        public Musica? ObterPeloTitulo(string titulo)
        {

            foreach (var musica in musicas)
            {
                if (musica.Titulo == titulo) return musica;
            }

            return null;
        }
        public Musica? ObterMusicaAleatoria()
        {
            if (musicas.Count == 0) return null;
            var random = new Random();
            var indiceAleatorio = random.Next(0, musicas.Count - 1);
            return musicas[indiceAleatorio];
        }
        public void OrdernarPorDuracao()
        {
            musicas.Sort();
        }
        public void OrdernarPorArtista()
        {
            musicas.Sort(new PorArtista());
        }
        public void OrdernarPorTitulo()
        {
            musicas.Sort(new PorTitulo());
        }

        public void CopyTo(Musica[] array, int arrayIndex)
        {
            musicas.CopyTo(array, arrayIndex);
        }

        public bool Remove(Musica item)
        {
            return musicas.Remove(item);
        }
    }

    class PlayerDeMusica
    {
        private Queue<Musica> fila = []; // primeiro a entrar, primeiro a sair (FIFO)
        private Stack<Musica> pilha = []; // o ultimo a entrar, e o primeiro a sair (LIFO)

        public void AdicionarNaFila(Musica musica)
        {
            fila.Enqueue(musica);
        }   
        public void AdicionarNaFila(Playlist playlist)
        {
            foreach (var musica in playlist)

                AdicionarNaFila(musica);
        }
        public Musica? ProximaMusicaDaFila()
        {
            if (fila.Count == 0) return null;
            var musica = fila.Dequeue();
            pilha.Push(musica);
            return musica;
        }

        public Musica? MusicaAnterior()
        {
            if (pilha.Count == 0) return null;
            return pilha.Pop();
        }

        public IEnumerable<Musica> Fila()
        {
            foreach (var musica in fila) yield return musica;

        }
      
        public IEnumerable<Musica> Historico()
        {

            foreach (var musica in pilha)
                yield return musica;
        }



    }
    

}
