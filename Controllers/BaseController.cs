using LabTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel;
using System.Reflection;

namespace LabTest.Controllers
{
    public abstract class baseController : ControllerBase
    {
        private IMemoryCache cache;
        public baseController(IMemoryCache cache)
        {
            this.cache = cache;
        }
        
        protected List<PatientViewModel> GetPatientDataList()
        {
            List<PatientViewModel> patientViewModels = cache.Get<List<PatientViewModel>>(Constants.PatientInMemoryCache);
            patientViewModels = patientViewModels?.Count >= 1 ? patientViewModels : new List<PatientViewModel>();
            return patientViewModels;
        }

        protected List<LabTestViewModel> GetLabReportDataList()
        {
            List<LabTestViewModel> labTestViewModels = cache.Get<List<LabTestViewModel>>(Constants.LabReportInMemoryCache);
            labTestViewModels = labTestViewModels?.Count >= 1 ? labTestViewModels : new List<LabTestViewModel>();
            return labTestViewModels;
        }

        protected int GetUniqueId(string type) {
            int id = 0;

            if (type == "patient")
            {
                PatientViewModel patientViewModel = GetPatientDataList().OrderByDescending(x => x.PatientId).FirstOrDefault();
                id = patientViewModel != null ? patientViewModel.PatientId : 0;
            }
            else {
                LabTestViewModel labTestViewModel = GetLabReportDataList().OrderByDescending(x => x.LabTestId).FirstOrDefault();
                id = labTestViewModel != null ? labTestViewModel.LabTestId : 0;
            }

            return ++id;
        }

        protected bool ValidatePatientExist(int paitentId) {
            PatientViewModel patientViewModel = GetPatientDataList().FirstOrDefault(x => x.PatientId == paitentId);
            return patientViewModel != null;
        }

        protected string GetDescription(Enum value)
        {

            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}