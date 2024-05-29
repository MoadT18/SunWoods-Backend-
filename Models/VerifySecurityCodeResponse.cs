namespace AirBnB_for_Campers___TAKE_HOME_EXAM.Models
{
    public class VerifySecurityCodeResponse
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public bool IsVerified { get; set; }
    }
}
