using WebAPI.Validation;

namespace WebAPI.Dto {
    public class StaffDto {
        [Update(ErrorMessage = "StaffId is required for update")]
        public int StaffId { get; set; }
        public string Name { get; set; }
        public string Password {  get; set; }
        public string Role {  get; set; }

    }
}
