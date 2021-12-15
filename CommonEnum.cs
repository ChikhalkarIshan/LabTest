using System.ComponentModel;
using System.Reflection;

namespace LabTest
{
         enum TypeOfTest
        {
            [Description("glucose Test")]
            glucoseTests,
            [Description("Complete Blood Count")]
            completeBloodCount,
            [Description("Lipid Panel")]
            lipidPanel,
            [Description("Urinalysis")]
            urinalysis
        }

}
