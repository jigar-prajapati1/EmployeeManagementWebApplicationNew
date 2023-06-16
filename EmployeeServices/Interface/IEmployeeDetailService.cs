using CommonModels.ViewModel;

namespace Services.Interfaces
{
    public interface IEmployeeDetailService
    {
        List<EmployeeDetailView> GetAllEmployee();
        List<EmployeeDesignationView> GetDesignations();
        EmployeeDetailView GetEmployeeById(int id);
        void AddEmployeeDetail(EmployeeDetailView employeeDetail);
        void UpdateEmployeeDetail(EmployeeDetailView employeeDetail,int id);
        void DeleteEmployee(int id);

    }
}