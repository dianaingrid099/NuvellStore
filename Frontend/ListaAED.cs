using System;
using System.Collections;
using System.Collections.Generic;

class Medico
{
    public int CRM { get; set; }
    public string Nome { get; set; }
    public string Especialidade { get; set; }
}

class Paciente
{
    public int Codigo { get; set; }
    public string Nome { get; set; }
}

class ConsultaMedica
{
    public int NumeroConsulta { get; set; }
    public string DataConsulta { get; set; }
    public string HoraConsulta { get; set; }
    public int PacienteId { get; set; }
    public int MedicoCRM { get; set; }
}

class Program
{
    static Hashtable medicos = new Hashtable();
    static Hashtable pacientes = new Hashtable();
    static List<ConsultaMedica> consultas = new List<ConsultaMedica>();
    static int contadorConsultas = 1;

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n--- Menu ---");
            Console.WriteLine("1. Cadastrar Médico");
            Console.WriteLine("2. Cadastrar Paciente");
            Console.WriteLine("3. Marcar Consulta Médica");
            Console.WriteLine("4. Listar Consultas Médicas");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1": CadastrarMedico(); break;
                case "2": CadastrarPaciente(); break;
                case "3": MarcarConsulta(); break;
                case "4": ListarConsultas(); break;
                case "0": return;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }
    }

    static void CadastrarMedico()
    {
        Console.Write("CRM: ");
        int crm = int.Parse(Console.ReadLine());
        Console.Write("nome: ");
        string nome = Console.ReadLine();
        Console.Write("Especialidade: ");
        string especialidade = Console.ReadLine();

        medicos[crm] = new Medico { CRM = crm, Nome = nome, Especialidade = especialidade };
        Console.WriteLine("Médico cadastrado com sucesso!");
    }

    static void CadastrarPaciente()
    {
        Console.Write("Código do paciente: ");
        int codigo = int.Parse(Console.ReadLine());
        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        pacientes[codigo] = new Paciente { Codigo = codigo, Nome = nome };
        Console.WriteLine("Paciente cadastrado com sucesso!");
    }

    static void MarcarConsulta()
    {
        Console.Write("Data da consulta (DD/MM/AAAA): ");
        string data = Console.ReadLine();
        Console.Write("Hora da consulta (HH:MM): ");
        string hora = Console.ReadLine();

        Console.Write("CRM do médico: ");
        int crm = int.Parse(Console.ReadLine());
        if (!medicos.ContainsKey(crm))
        {
            Console.WriteLine("Médico não encontrado para o CRM informado.");
            return;
        }

        Console.Write("Código do Paciente: ");
        int codigo = int.Parse(Console.ReadLine());
        if (!pacientes.ContainsKey(codigo))
        {
            Console.WriteLine("Paciente não encontrado para a identificação informada.");
            return;
        }

        var consulta = new ConsultaMedica
        {
            NumeroConsulta = contadorConsultas++,
            DataConsulta = data,
            HoraConsulta = hora,
            MedicoCRM = crm,
            PacienteId = codigo
        };

        consultas.Add(consulta);
        Console.WriteLine("Consulta marcada com sucesso!");
    }

    static void ListarConsultas()
    {
        Console.WriteLine("\n--- Consultas Médicas ---");
        foreach (var consulta in consultas)
        {
            var medico = (Medico)medicos[consulta.MedicoCRM];
            var paciente = (Paciente)pacientes[consulta.PacienteId];
            Console.WriteLine($"Consulta #{consulta.NumeroConsulta} - {consulta.DataConsulta} às {consulta.HoraConsulta}");
            Console.WriteLine($"  Médico: {medico.Nome} (CRM {medico.CRM})");
            Console.WriteLine($"  Paciente: {paciente.Nome} (Código {paciente.Codigo})");
            Console.WriteLine();
        }
    }
}
