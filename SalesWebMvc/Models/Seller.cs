﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")] // para campo obrigatório. posso definir uma msg de errou não.
        [StringLength(60, MinimumLength = 3, ErrorMessage ="{0} size shoud be between {1} and {2}")] // define o tamanho máximo e minímo para o nome e adicionar uma mensagem de erro.
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)] //Annotations: ele tranforma o endereço de e-mail em um link que chama o app de mail padrão instalado na minha maquina.
        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }

        [Display(Name = "Birth Date")] //Annotations: para que na tela passe esse nome com o espaço e não o do atributo. E como usamo o tag helper srá plaicado tanto na tele de seller como na de create newSeller.
        [DataType(DataType.Date)] // Annotations: retira a parte de hora/minuto do campo onde tem o form de criar novo Seller.
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] //formata a data para o padrão BR.
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DisplayFormat(DataFormatString = "{0:F2}")] //formatação para que o salario na tela apareça apareça com duas casas decimais, como criamos o location para EUA, vai aparecer com "." separador dos centavos.
        [Display(Name = "Base Salary")] //Annotations: para que na tela passe esse nome com o espaço e não o do atributo.       
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")] //validação para que o salario seja no min 100 e no max 50000.
        public double BaseSalary { get; set; }

        public Department Department { get; set; } //um vendedor possui um departamento.

        public int DepartmentId { get; set; } //essa prop vai garantir que id do departamneto não seja nulo quando cadastrar um Seller.

        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>(); //um vendedor possui muitas vendas.
        //ICollection é um tipo de coleção mais generica


        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
            //não incluído atributo para a coleção
        }

        public void AddSales(SalesRecord sr) //esse metodo recebe como argumento um SalesRecord sr
        {
            Sales.Add(sr); // e adiciona na minha lista o sr
        }

        public void RemoveSales(SalesRecord sr) //esse metodo recebe como argumento um SalesRecord sr
        {
            Sales.Remove(sr); // e revome da minha lista o sr
        }


        public double TotalSales(DateTime initial, DateTime final)
        {
            //Para essa operação usar LINQ
            //fazer um filtro (where) e obter  uma lista no intervalo de data recebido no parametro
            //em outra expressão lambda chamar o metodo soma passando o atributo Amount da classe SalesRecord
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);

        }

    }
}
