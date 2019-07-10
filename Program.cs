using System;
using System.Collections.Generic;
using System.IO;

namespace Employees
{
    abstract class Employee : IComparable<Employee>
    {
        public int CompareTo(Employee e) => this.Name.CompareTo(e.Name);
        protected string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        protected int age;
        public int Age
        {
            get { return age; }
            set { if (value > 0) age = value; }
        }
        public string Company { get; set; }

        public Employee(string name, int age, string company)
        {
            Name = name;
            Age = age;
            Company = company;
        }

        public Employee(string[] fields)
        {
            Name = fields[0];
            Age = Convert.ToInt32(fields[1]);
            Company = fields[2];
        }

        public abstract override string ToString();
    }

    class Manager : Employee
    {
        int subords;
        public int Subords
        {
            get { return subords; }
            set { if (value > 0) subords = value; }
        }

        public Manager(string name, int age, string company, int subords) : base(name, age, company)
        {
            Subords = subords;
        }
        public Manager(string[] fields) : base(fields)
        {
            Subords = Convert.ToInt32(fields[3]);
        }

        public override string ToString() => String.Format("Name: {0, -10}\tAge: {1,-3}\tCompany: {2,-10}\tSubordinates: {3,-4}", Name, Age, Company, Subords);
    }

    sealed class Worker : Employee
    {
        public string Department { get; set; }

        public Worker(string name, int age, string company, string department) : base(name, age, company)
        {
            Department = department;
        }
        public Worker(string[] fields) : base(fields)
        {
            Department = fields[3];
        }

        public override string ToString() => String.Format("Name: {0, -10}\tAge: {1,-3}\tCompany: {2,-10}\tDepartment: {3,-10}", Name, Age, Company, Department);
    }

    class Program
    {
        static void Main(string[] args)
        {
            const string path = "Text.txt";
            List<Employee> data = new List<Employee>();

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] fields = line.Split(';');
                    if (line[line.Length - 1] == 'm')
                    {
                        Manager manager = new Manager(fields);
                        data.Add(manager);
                    }
                    else
                    {
                        Worker worker = new Worker(fields);
                        data.Add(worker);
                    }
                }
                sr.Close();
            }

            data.Sort();
            foreach (var item in data)
            {
                Console.WriteLine(item);
            }
        }
    }
}
