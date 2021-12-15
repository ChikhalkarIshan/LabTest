using LabTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabReportController : baseController
    {
        private IMemoryCache cache;
        private readonly ILogger _logger;
        public LabReportController(IMemoryCache cache, ILogger<LabReportController> logger) : base(cache)
        {
            this.cache = cache;
            _logger = logger;
        }

        // GET: api/<LabReportController>
        [HttpGet]
        //Api used to get all lab report list
        public List<LabTestViewModel> Get() => GetLabReportDataList();



        // GET api/<LabReportController>/5
        //Api used to get all labreport list with id
        [HttpGet("{id}")]
        public LabTestViewModel Get(int id)
        {
            return GetLabReportDataList().FirstOrDefault(x => x.LabTestId == id);
        }

        // POST api/<LabReportController>
        //Api used to save lab report into the system
        [HttpPost]
        public string Post([FromBody] LabTestViewModel labTestViewModel)
        {
            List<LabTestViewModel> labTestViewModels = GetLabReportDataList();
            try
            {
                if (!TypeOfTest.IsDefined(typeof(TypeOfTest), labTestViewModel.TypeOfTestId))
                {
                    return Constants.PleaseCheckTheTestAndTryAgain;
                }
                else if (!ValidatePatientExist(labTestViewModel.PatientId))
                {
                    return Constants.PaitentDoesNotExist;
                }
                else
                {
                    labTestViewModel.LabTestId = GetUniqueId("lab");
                    labTestViewModels.Add(labTestViewModel);
                    cache.Set<List<LabTestViewModel>>(Constants.LabReportInMemoryCache, labTestViewModels);
                    return Constants.recordSavedSuccessfully;
                    
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Constants.somethingWentWrong;
            }
        }

        // PUT api/<LabReportController>/5
        //Api used to update lab report into the system
        [HttpPut("{id}")]
        public string Put([FromBody] LabTestViewModel labTestViewModel)
        {

            List<LabTestViewModel> labTestViewModels = GetLabReportDataList();

            try
            {
                LabTestViewModel modelToDelete = GetLabReportDataList().FirstOrDefault(x => x.LabTestId == labTestViewModel.LabTestId);
                if (modelToDelete == null)
                    return Constants.recordNotValid;

                labTestViewModels.Remove(modelToDelete);
                labTestViewModels.Add(labTestViewModel);
                cache.Set<List<LabTestViewModel>>(Constants.LabReportInMemoryCache, labTestViewModels);
                return Constants.recordUpdateSuccessfully;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Constants.somethingWentWrong;
            }
        }

        // DELETE api/<LabReportController>/5
        //Api used to delete lab report from the system
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            try
            {
                List<LabTestViewModel> labTestViewModels = GetLabReportDataList();
                LabTestViewModel modelToDelete = labTestViewModels.Find(x => x.LabTestId == id);
                if (modelToDelete == null)
                    return Constants.recordNotExist;

                labTestViewModels.Remove(modelToDelete);
                return Constants.recordDeleteSuccessfully;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Constants.somethingWentWrong;
            }
        }
    }
}
