using System.ComponentModel.DataAnnotations.Schema;

namespace Resturant.ViewModel
{
    public class ChefsVM
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string Officium { get; set; }
        public string Description { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }
    }
}
