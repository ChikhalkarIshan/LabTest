using LabTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : baseController
    {
        private IMemoryCache cache;
        private readonly ILogger _logger;

        public PatientController(IMemoryCache cache, ILogger<LabReportController> logger) : base(cache)
        {
            this.cache = cache;
            _logger = logger;
        }

        // GET: api/<PatientController>
        //Api used to get all paitient list
        [HttpGet]
        public List<PatientViewModel> Get() => GetPatientDataList();

        // GET api/<PatientController>/5
        //Api for getting paitent data by id.
        [HttpGet("{id}")]
        public PatientViewModel Get(int id)
        {
            return GetPatientDataList().FirstOrDefault(x => x.PatientId == id);
        }

        // POST api/<PatientController>
        //Api for saveing the paitent data into the system.
        [HttpPost(Name = "SavePatient")]
        public string Post([FromBody] PatientViewModel patientViewModel)
        {
            List<PatientViewModel> patientViewModels = GetPatientDataList();
            try
            {
                patientViewModel.PatientId = GetUniqueId("patient");
                patientViewModels.Add(patientViewModel);
                cache.Set<List<PatientViewModel>>(Constants.PatientInMemoryCache, patientViewModels);
                return Constants.recordSavedSuccessfully;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Constants.somethingWentWrong;
            }
        }

        // PUT api/<PatientController>/5
        //Api for updatinging the paitent data into the system.
        [HttpPut(Name = "UpdatePatient")]
        public string Put([FromBody] PatientViewModel patientViewModel)
        {

            List<PatientViewModel> patientViewModels = GetPatientDataList();

            try
            {
                PatientViewModel modelToDelete = GetPatientDataList().FirstOrDefault(x => x.PatientId == patientViewModel.PatientId);
                if (modelToDelete == null)
                    return Constants.recordNotValid;

                patientViewModels.Remove(modelToDelete);
                patientViewModels.Add(patientViewModel);
                cache.Set<List<PatientViewModel>>(Constants.PatientInMemoryCache, patientViewModels);
                return Constants.recordUpdateSuccessfully;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Constants.somethingWentWrong;
            }
        }

        // DELETE api/<PatientController>/5
        //Api for deleting the paitent data into the system.
        [HttpDelete(Name = "DeletePatientByName")]
        public string Delete(int id)
        {
            try
            {
                List<PatientViewModel> patientViewModels = GetPatientDataList();
                PatientViewModel modelToDelete = patientViewModels.Find(x => x.PatientId == id);
                if (modelToDelete == null)
                    return Constants.recordNotExist;

                patientViewModels.Remove(modelToDelete);
                return Constants.recordDeleteSuccessfully;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Constants.somethingWentWrong;
            }

        }
        //Api for getting the paitent data with typeoftest and date range the system.
        [HttpGet("{typeOfTestId}/{dateFrom}/{dateTo}")]
        public List<PatientLabTestViewModel> GetPatientViewModelsForSearchingCriteria(int typeOfTestId, DateTime dateFrom, DateTime dateTo)
        {

            List<PatientViewModel> patientViewModels = GetPatientDataList();
            List<LabTestViewModel> labTestViewModels = GetLabReportDataList();
            _logger.LogInformation("Log message in the GetPatientViewModelsForSearchingCriteria() method");

            try
            {
                List<PatientLabTestViewModel> patientLabTestViewModel = patientViewModels.Where(w=> labTestViewModels.Select(x=>x.PatientId).Contains(w.PatientId)).Select(y => new PatientLabTestViewModel()
                {
                    PatientId = y.PatientId,
                    Name = y.Name,
                    Gender = y.Gender,
                    DateOfBirth = y.DateOfBirth,
                    PaitentlabTest = labTestViewModels.Where(x => x.PatientId == y.PatientId && x.TypeOfTestId == typeOfTestId &&
                              (x.DateTimeOfTest.Date >= dateFrom.Date && x.DateTimeOfTest.Date <= dateTo.Date)).ToList()
                }).ToList();

                return patientLabTestViewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
    }
}
