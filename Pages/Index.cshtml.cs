using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using asmuKantine.Model;

namespace IBAS_kantine.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly List<asmuKantineDTO> _menuItems;

        public IndexModel(ILogger<IndexModel> logger, List<asmuKantineDTO> menuItems)
        {
            _logger = logger;
            _menuItems = menuItems;
        }

        public List<asmuKantineDTO> MenuItems { get; private set; }

        public void OnGet()
        {
            MenuItems = _menuItems;
        }
    }
}
