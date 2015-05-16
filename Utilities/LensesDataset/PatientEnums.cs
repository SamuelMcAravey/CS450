using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensesDataset
{
    public enum PatientAge
    {
        Young = 1,
        PrePresbyopic = 2,
        Presbyopic = 3
    }
    public enum PatientPrescription
    {
        Myope = 1,
        Hypermetrope = 2
    }
    public enum PatientAstigmatic
    {
        No = 1,
        Yes = 2
    }
    public enum PatientTearProductionRate
    {
        Reduced = 1,
        Normal = 2
    }
    public enum PatientLenseClass
    {
        Hard = 1,
        Soft = 2,
        NoLense = 3
    }
}
