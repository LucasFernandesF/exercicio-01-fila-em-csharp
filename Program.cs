using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

class Cliente
{
    public string Nome { get; set; }
    public int TempoDeEspera { get; set; }
}

class Funcionario
{
    public string Nome { get; set; }

    public void AtenderCliente(Cliente cliente)
    {
        
        Console.WriteLine($"[{DateTime.Now}] {Nome} está atendendo o cliente {cliente.Nome} por {cliente.TempoDeEspera} segundos.");
        Thread.Sleep(cliente.TempoDeEspera * 1000);
        Console.WriteLine($"[{DateTime.Now}] {Nome} atendeu o cliente {cliente.Nome} em {cliente.TempoDeEspera} segundos.");
    }
}

class Program
{
    static void Main()
    {
        Random random = new Random();

        List<Funcionario> funcionarios = new List<Funcionario>
        {
            new Funcionario { Nome = "Funcionário 1" },
            new Funcionario { Nome = "Funcionário 2" },
            new Funcionario { Nome = "Funcionário 3" }
        };

        Queue<Cliente> fila = new Queue<Cliente>(Enumerable.Range(1, 10)
            .Select(i => new Cliente
            {
                Nome = $"Cliente {i}",
                TempoDeEspera = random.Next(1, 10)
            }));

        foreach (var cliente in fila)
        {
            Console.WriteLine($"[{DateTime.Now}] entrou na fila de espera {cliente.Nome} (Tempo de espera: {cliente.TempoDeEspera} segundos)");
        }

        while (fila.Count > 0)
        {
            foreach (var funcionario in funcionarios)
            {
                if (fila.Count == 0)
                    break;

                Cliente cliente = fila.Dequeue();
                funcionario.AtenderCliente(cliente);
            }
        }

        Console.WriteLine("Todos os clientes foram atendidos.");
        Console.ReadLine();
    }
}
