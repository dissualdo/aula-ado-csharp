using ExemploCRUD;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using WebAula.Domain;
using WebAula.Models;

namespace WebAula.Controllers
{
    /// <summary>
    /// Controlador para a Home do sistema
    /// </summary>
    public class HomeController : Controller
    { 
        private string _connectionString;
        private ClienteRepository _clienteRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        private HomeViewModel _homeViewModel;

        /// <summary>
        /// Construtor 
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="configuration">configuration</param>
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _homeViewModel = new HomeViewModel();
            _connectionString = _configuration.GetConnectionString("MyConnectionString");
            _clienteRepository = new ClienteRepository(_connectionString);
           
        }

        /// <summary>
        /// Ponto de acesso que responde a chamada padrão da home
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            try
            { 
                _homeViewModel.Clientes  = _clienteRepository.ListarClientes();

            }
            catch (Exception err)
            {
                _logger.LogInformation(err.Message, DateTime.UtcNow.ToLongTimeString()); 
            }

            return View(_homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}