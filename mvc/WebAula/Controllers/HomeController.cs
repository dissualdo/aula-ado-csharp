using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using WebAula.DAL;
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
            _connectionString = _configuration.GetConnectionString("Aula");
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
                _homeViewModel.Clientes = _clienteRepository.ListarClientes();

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

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            try
            {
                if (cliente != null)
                {
                    _clienteRepository.InserirCliente(cliente);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }


            return View();
        }

        public IActionResult Edit(long id)
        {
            var cliente = default(Cliente);
            try
            {
                if (id > 0)
                {
                    cliente = _clienteRepository
                              .ListarClientes()
                              .FirstOrDefault(a => a.Id == id);
                }
            }
            catch (Exception)
            {

                throw;
            }


            return View("Create", cliente);
        }

        [HttpPost]
        public IActionResult Edit(Cliente cliente)
        {
            try
            {
                if (cliente != null)
                {
                    _clienteRepository.AtualizarCliente(cliente.Id, cliente);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }


            return View();
        }

        public IActionResult Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    _clienteRepository.RemoverCliente(id);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }
    }
}