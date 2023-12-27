using Application.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace WebAppJob.Controllers
{
    [Route("/")]
    public class CatalogController : BaseController
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
            
            }catch (CommonException ex)
            {
                return ReturnResponseErrorCommon(ex);
            }
            catch (Exception ex)
            {
                return ReturnResponseIncorrect(ex);
            }

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {

                return Ok(new { list = await _serviceCatalogCompany.GetAllAsync() });
            }
            catch (CommonException ex)
            {
                return ReturnResponseErrorCommon(ex);
            }
            catch (Exception ex)
            {
                return ReturnResponseIncorrect(ex);

            }

        }
    }
}
