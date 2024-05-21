using BusinessObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Authentication;
using WebAPI.Dto;
using WebAPI.Service;

namespace WebAPI.Controllers {
    [ApiController]
    [Route("/ass1/api/staff/")]
    public class StaffController : ControllerBase {
        private readonly StaffService _staffService;
        private readonly JWTService _jWTService;
        public StaffController(StaffService staffService, JWTService jWTService) {
            _staffService = staffService;
            _jWTService = jWTService;
        }
        [HttpGet("all")]
        [Authorize("Admin")]
        public ActionResult<IEnumerable<Staff>> GetProducts() {
            return _staffService.GetStaffs();
        }

        [HttpGet("detail/{id}")]
        [Authorize("Admin")]
        public ActionResult<StaffDto> Details(int id) {
            Staff s = _staffService.GetStaffById(id);
            if (s == null) {
                return NotFound();
            } else {
                return new StaffDto() {
                    StaffId = s.StaffId,
                    Name = s.Name,
                    Password = s.Password,
                    Role = s.Role
                };
            }
        }

        [HttpPost("create")]
        [Authorize("Admin")]
        public ActionResult Create(StaffDto s) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            } else {
                if(_staffService.StaffIsExited(s)) {
                    return BadRequest("Staff is exit");
                }
                Staff s1 = new Staff() {
                    Name = s.Name,
                    Password = s.Password,
                    Role = s.Role
                };
                bool status = _staffService.SaveStaff(s1);
                if(status) {
                    return Ok("Add Staff Success");
                } else {
                    return BadRequest("Add Staff Fail");
                }
                
            }
        }
        [HttpPut("update")]
        [Authorize("Admin")]
        public ActionResult Update(StaffDto s) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            } else {
                Staff s1 = _staffService.GetStaffById(s.StaffId);
                if (s1 == null) {
                    return NotFound();
                }
                s1.Name = s.Name;
                s1.Password = s.Password;
                s1.Role = s.Role;

                _staffService.UpdateStaff(s1);
                return Ok("Update success");
            }
        }

        [HttpDelete("delete/{id}")]
        [Authorize("Admin")]

        public ActionResult Delete(int id) {
            bool status = _staffService.DeleteStaffById(id);
            if (!status) {
                return NotFound();
            } else {
                return Ok("Delete success");
            }
        }
        [HttpPost("login")]
        public ActionResult Login(StaffDto sd) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            } else {
                if(_staffService.StaffIsExited(sd)) {
                    Staff s = _staffService.CheckNameAndPass(sd);
                    if (s != null) {
                        StaffDto s1 = new StaffDto {
                            Name = sd.Name,
                            Role = s.Role
                        };
                        return Ok(_jWTService.GenerateToken(s1));
                    }
                }
            }
            return BadRequest("Login fail");
        }
    }
}
