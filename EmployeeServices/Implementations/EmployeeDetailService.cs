using CommonModels.DbModels;
using CommonModels.ViewModel;
using Repositorys.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
    public class EmployeeDetailService : IEmployeeDetailService
    {
        private readonly IEmployeeDetailRepository _empDetailRepo;

        public EmployeeDetailService(IEmployeeDetailRepository empDetailRepo)
        {
            _empDetailRepo = empDetailRepo;
        }
        public List<EmployeeDetailView> GetAllEmployee()
        {
            try
            {
                List<EmployeeDetailView> Employeelist = new List<EmployeeDetailView>();
                var Dbmodellist = _empDetailRepo.GetAllEmployee();
                if (Dbmodellist != null && Dbmodellist.Count > 0)
                {
                    foreach (var items in Dbmodellist)
                    {
                        Employeelist.Add(new EmployeeDetailView
                        {
                            Id = items.Id,
                            Name = items.Name,
                            DesignationId = items.DesignationId,
                            ProfilePicture = items.ProfilePicture,
                            Salary = items.Salary,
                            DataOfBirth = items.DataOfBirth,
                            Email = items.Email,
                            Address = items.Address,
                        });
                    }
                }
                return Employeelist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EmployeeDesignationView> GetDesignations()
        {
            try
            {
                List<EmployeeDesignation> viewModelList = _empDetailRepo.GetDesignations();
                List<EmployeeDesignationView> employeeList = viewModelList.Select(v => new EmployeeDesignationView
                {
                    DesignationId = v.DesignationId,
                    Designation = v.Designation,

                }).ToList();
                return employeeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public EmployeeDetailView GetEmployeeById(int id)
        {
            try
            {
                EmployeeDetail employeeDetail = _empDetailRepo.GetEmployeeById(id);
                EmployeeDetailView employee = new EmployeeDetailView
                {
                    Id = employeeDetail.Id,
                    Name = employeeDetail.Name,
                    DesignationId = employeeDetail.DesignationId,
                    ProfilePicture = employeeDetail.ProfilePicture,
                    DataOfBirth = employeeDetail.DataOfBirth,
                    Salary = employeeDetail.Salary,
                    Email = employeeDetail.Email,
                    Address = employeeDetail.Address,
                };
                return employee;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void AddEmployeeDetail(EmployeeDetailView employeeDetail)
        {
            try
            {
                //mapping Dbmode to view model
                var empDbModel = new EmployeeDetail
                {

                    Name = employeeDetail.Name,
                    DesignationId = employeeDetail.DesignationId,
                    ProfilePicture = employeeDetail.ProfilePicture,
                    DataOfBirth = employeeDetail.DataOfBirth,
                    Salary = employeeDetail.Salary,
                    Email = employeeDetail.Email,
                    Address = employeeDetail.Address,
                };
                _empDetailRepo.AddEmployee(empDbModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void UpdateEmployeeDetail(EmployeeDetailView employeeDetail, int id)
        {
            try
            {
                EmployeeDetail employee = _empDetailRepo.GetEmployeeById(employeeDetail.Id);

                employee.Name = employeeDetail.Name;
                employee.DesignationId = employeeDetail.DesignationId;
                employee.ProfilePicture = employeeDetail.ProfilePicture;
                employee.Salary = employeeDetail.Salary;
                employee.DataOfBirth = employeeDetail.DataOfBirth;
                employee.Email = employeeDetail.Email;
                employee.Address = employeeDetail.Address;
                _empDetailRepo.UpdateEmployee(employee, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void DeleteEmployee(int id)
        {
            try
            {
                _empDetailRepo.DeleteEmployee(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
