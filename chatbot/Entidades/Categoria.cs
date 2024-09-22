using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace chatbot.Entidades
{
    public class Categoria
    {
        
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
