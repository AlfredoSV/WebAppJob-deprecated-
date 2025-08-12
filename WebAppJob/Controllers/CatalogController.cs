using Application.IServices;
using Domain.Entities;
using Framework.Utilities.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAppJob.Controllers
{
    [Route("/")]
    [ApiController]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme,Roles ="Admi")]
    public class CatalogController : BaseController
    {

        private readonly IServiceCatalog<Area> _serviceCatalogArea;
        private readonly IServiceCatalog<Company> _serviceCatalogCompany;

        public CatalogController(IServiceLogBook serviceLogBook, IServiceCatalog<Area> serviceCatalogArea,
            IServiceCatalog<Company> serviceCatalogCompany):base(serviceLogBook)
        {
            _serviceCatalogArea = serviceCatalogArea;
            _serviceCatalogCompany = serviceCatalogCompany;

        }

        [HttpGet("[action]")]
        [ResponseCache(Duration = 15, Location = ResponseCacheLocation.Client)]    
        public async Task<ActionResult> GetAreas()
        {
            try
            {
                return Ok(new { list = await _serviceCatalogArea.GetAllAsync() });

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

        [HttpGet("[action]")]
        [ResponseCache(Duration = 15, Location = ResponseCacheLocation.Client)]
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
