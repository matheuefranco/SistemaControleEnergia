using System;
class Program
{
    struct Eletro
    {
        public string nome;
        public double potencia;
        public double tempoMedioUso;
    }
    static void addEletro(List<Eletro> lista)
    {
        Eletro novoEletrodomestico = new Eletro();
        Console.Write("Nome do Eletrodoméstico: ");
        novoEletrodomestico.nome = Console.ReadLine();
        Console.Write("Potência (em kW): ");
        novoEletrodomestico.potencia = Convert.ToDouble(Console.ReadLine());
        Console.Write("Tempo Médio Ativo por Dia (em horas): ");
        novoEletrodomestico.tempoMedioUso = Convert.ToDouble(Console.ReadLine());

        lista.Add(novoEletrodomestico);
        Console.WriteLine("Eletrodoméstico adicionado com sucesso!");
    }

    static void listarEletros(List<Eletro> lista)
    {
        Console.WriteLine("Lista de Eletrodomésticos:");
        foreach (Eletro eletro in lista)
        {
            Console.WriteLine("Nome:" + eletro.nome);
            Console.WriteLine($"Potência: {eletro.potencia} kW");
            Console.WriteLine($"Tempo Médio Ativo por Dia: {eletro.tempoMedioUso} horas");
            Console.WriteLine();
        }
    }
    static void calcularCustoEletro(List<Eletro> vetorEletros, string nomeEletro)
    {
        double consumoDia, valorGastoDia, valorKw;
        Console.Write("Valor do Kw em R$:");
        valorKw = Convert.ToDouble(Console.ReadLine());
        foreach(Eletro eletro in vetorEletros)
        {
            if (eletro.nome.ToUpper().Equals(nomeEletro.ToUpper())){
                consumoDia = eletro.potencia * eletro.tempoMedioUso;
                valorGastoDia = consumoDia * valorKw;
                Console.WriteLine($"Consumo em KW por dia:" +
                    $"{consumoDia}, por mês: {consumoDia*30}");
            }

        }

    }

    static void salvarDados(List<Eletro> lista, string nomeArquivo)
    {
        using (StreamWriter writer = new StreamWriter(nomeArquivo))
        {
            foreach (var eletro in lista)
            {
                writer.WriteLine($"{eletro.nome};{eletro.potencia};{eletro.tempoMedioUso}");
            }
        }
        Console.WriteLine("Dados salvos com sucesso!");
    }

    static void carregarDados(List<Eletro> lista, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhas = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhas)
            {
                string[] campos = linha.Split(';');
                Eletro eletro = new Eletro
                {
                    nome = campos[0],
                    potencia = double.Parse(campos[1]),
                    tempoMedioUso = double.Parse(campos[2])
                };
                lista.Add(eletro);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }
    static int menu()
    {
        int op;
        Console.WriteLine("*** Sistema de Controle de Energia C# ***");
        Console.WriteLine("1-Cadastrar");
        Console.WriteLine("2-Listar");
        Console.WriteLine("3-Calcular custo");
        Console.WriteLine("0-Sair");
        Console.Write("Escolha uma opção:");
        op = Convert.ToInt32(Console.ReadLine());
        return op;
    }// fim funcao menu
    static void Main()
    {
        List<Eletro> vetorEletros = new List<Eletro>();
        carregarDados(vetorEletros, "dadosEletro.txt");
        int op=0;
        do{
            op = menu();
            switch (op){
                case 1: addEletro(vetorEletros);
                    break;
                case 2: listarEletros(vetorEletros);
                    break ;
                case 3: //calcularCustoEletro(vetorEletros);
                    break;
                case 0: Console.WriteLine("Saindo");
                    salvarDados(vetorEletros, "dadosEletro.txt");
                    break;
            }// fim switch
         Console.ReadKey(); // pausa
         Console.Clear();

        }while(op!=0);
        
    }


}