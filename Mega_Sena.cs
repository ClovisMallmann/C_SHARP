
using System;
using System.Collections.Generic;
using System.Linq;

        Console.WriteLine("========== Mega Sena ==========");
        Console.WriteLine("Vamos cadastrar 5 jogadores e seus numeros aleatorios:\n");

        List<int> gabarito = new();
       
        Random rnd = new Random();
        
        //Gerar o gabarito de Mega Sena
        for (int i = 0; i < 5; i++)
        {   
            gabarito.Add(rnd.Next(1, 61));
        }
        
        List<string> jogadoresNomes = new() { "Paulo", "Ana", "Roberta", "Rodriguez", "Sandro" };
        List<Jogador> jogadores = new(); // Lista para armazenar os jogadores

        for (int i = 0; i < jogadoresNomes.Count; i++)
        {
            List<int> numeros = new();

            for (int r = 0; r < 5; r++)
            {
                numeros.Add(rnd.Next(1, 61)); // Inclui o 60, pois Next é exclusivo no máximo
            }

            // Cria uma NOVA instância de Jogador a cada iteração
            Jogador jogador = new Jogador(jogadoresNomes[i], numeros);

            jogadores.Add(jogador); // Adiciona o jogador à lista de jogadores
        }

        // Imprime os dados dos jogadores
        foreach (var jogador in jogadores)
        {
            
            int contagem = 0;
            string premio = "";
            
            //Verifica os acertos.
            foreach (var g in gabarito)
            {
                if (jogador.Numeros.Contains(g)) //Compara o CONTEÚDO DAS LISTAS!
                {
                    contagem++;
                }
                else
                {
                    break;
                }
            }
            
            //Apresenta os resultados por jogador.
            
            switch (contagem)
            {
                case 5: premio = "Ganhou R$ 1.000,00 reais."; break;
                case 4: premio = "Ganhou R$ 600,00 reais."; break;
                default:premio = "Nada."; break;
            };
            Console.Write($"{jogador.Nome} : {string.Join(", ", jogador.Numeros)}");
            Console.WriteLine($" / Acertos : {contagem} ");
            Console.WriteLine($"Prêmio : {premio} \n");
        }
        
        // Imprime o gabarito da Mega Sena
        Console.Write("\n Os números sorteados foram :");
        for (int i = 0; i < gabarito.Count(); i++)
        {
            Console.Write($" {string.Join(", ", gabarito[i])} ");
        }
        Console.WriteLine();
        
        Console.WriteLine();

        

        




        public class Jogador
{
    public string Nome { get; } // Propriedade somente leitura

    public List<int> Numeros { get; } // Propriedade somente leitura

    // Construtor para inicializar o jogador
    public Jogador(string nome, List<int> numeros)
    {
        Nome = nome;
        Numeros = numeros;
    }
}

