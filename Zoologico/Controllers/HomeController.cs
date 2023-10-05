using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoologico.Models;
using Zoologico.Models.ViewModels;

namespace Zoologico.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            AnimalesContext context = new ();
            
            var datos = context.Clase.OrderBy(x => x.Nombre).Select(x => new IndexViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre ?? "Sin Nombre",
                Descripcion = x.Descripcion ?? "Sin Descripcion"
            });

            return View(datos);
        }

        public IActionResult Especies (string Id)
        {
            Id = Id.Replace("-", " ");

            AnimalesContext context = new ();
            
            var datos = context
                .Clase.Include(x => x.Especies).Select(x => new EspeciesViewModel
                {
                    Id= x.Id,
                    NombreClase = x.Nombre ?? "Sin Nombre",
                    Especies = x.Especies.Select(x => new Especies()
                    {
                        Id = x.Id,
                        Especie = x.Especie ?? "Sin Especie",
                    }).ToList()
                }).FirstOrDefault(x => x.NombreClase == Id);
            
            return View(datos);
        }

        public IActionResult Especie(string Id)
        {
            Id = Id.Replace("-", " ");
            AnimalesContext context = new ();
            
            var datos = context.Especies.Include(x => x.IdClaseNavigation).Select(x => new EspecieViewModel
            {
                Id = x.Id,
                Nombre = x.Especie ?? "Sin Especie",
                NombreClase = x.IdClaseNavigation.Nombre ?? "Sin Nombre",
                Descripcion = x.Observaciones ?? "Sin Descripcion",
                Peso = x.Peso.Value,
                Tamaño = x.Tamaño.Value,
                Habitat = x.Habitat ?? "Sin Habitat"
            }).FirstOrDefault(x => x.Nombre == Id);

            return View(datos);
        }
    }
}
