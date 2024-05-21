using BusinessObject;

namespace WebAPI.Repositories {
    public interface IStaffRepository {
        bool SaveStaff(Staff s);
        Staff GetStaffById(int id);
        bool DeleteStaffById(int id);
        void UpdateStaff(Staff s);
        List<Staff> GetStaffs();
    }
}
