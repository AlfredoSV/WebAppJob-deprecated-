using Application.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAppJob.Controllers
{
    [Route("/")]
    public class CatalogController : Controller
    {
        private readonly IServiceCatalog<Area> _serviceCatalogArea;
        private readonly IServiceCatalog<Company> _serviceCatalogCompany;

        public CatalogController(IServiceCatalog<Area> serviceCatalogArea,
            IServiceCatalog<Company> serviceCatalogCompany)
        {
           _serviceCatalogArea = serviceCatalogArea;
            _serviceCatalogCompany = serviceCatalogCompany;
        }

        [HttpGet("[action]")]
        public IActionResult GetAreas()
        {
            try
            {
                return Ok(new { list = _serviceCatalogArea.GetAll() });
            }
            catch (Exception ex)
            {
                return StatusCode(500,new { err = ex.Message });
            }
            
        }

        [HttpGet("[action]")]
        public IActionResult GetCompanies()
        {
            try
            {
                return Ok(new { list = _serviceCatalogCompany.GetAll() });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { err = ex.Message });
            }
            
        }
    }
}
