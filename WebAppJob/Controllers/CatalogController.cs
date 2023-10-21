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
        public async Task<IActionResult> GetAreas()
        {
            try
            {
                return Ok(new { list = await _serviceCatalogArea.GetAllAsync() });
            }
            catch (Exception ex)
            {
                return StatusCode(500,new { err = ex.Message });
            }
            
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                return Ok(new { list = await _serviceCatalogCompany.GetAllAsync() });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { err = ex.Message });
            }
            
        }
    }
}
