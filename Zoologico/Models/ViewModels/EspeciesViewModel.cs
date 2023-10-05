namespace Zoologico.Models.ViewModels
{
    public class EspeciesViewModel
    {
        public int Id { get; set; }
        public string NombreClase { get; set; } = null!;
        public ICollection<Especies> Especies { get; set; } = null!;
    }
}
