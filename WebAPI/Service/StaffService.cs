using BusinessObject;
using Microsoft.EntityFrameworkCore;
using WebAPI.Dto;
using WebAPI.Repositories;

namespace WebAPI.Service {
    public class StaffService : IStaffRepository {
        private readonly TDbContext _context;
        public StaffService(TDbContext context) {
            _context = context;
        }
        public bool DeleteStaffById(int id) {
            var s = _context.Staffs.SingleOrDefault(
                        s => s.StaffId == id);
            if (s != null) {
                _context.Staffs.Remove(s);
                _context.SaveChanges();
                return true;
            } else {
                return false;
            }
        }

        public Staff GetStaffById(int id) {
            Staff s = _context.Staffs
                .SingleOrDefault(x => x.StaffId == id);
            return s;
        }

        public List<Staff> GetStaffs() {
            return _context.Staffs.ToList();
        }

        public bool SaveStaff(Staff s) {
            try {
                _context.Staffs.Add(s);
                _context.SaveChanges();
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public void UpdateStaff(Staff s) {
            _context.Entry<Staff>(s).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public bool StaffIsExited(StaffDto sd) {
            bool s = _context.Staffs.Any(s => s.Name.Equals(sd.Name));
            return s;
        }
        public Staff CheckNameAndPass(StaffDto sd) {
            Staff s = _context.Staffs.SingleOrDefault(s => s.Name.Equals(sd.Name) && s.Password.Equals(sd.Password));
            return s;
        }
    }
}
