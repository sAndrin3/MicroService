using System.ComponentModel.DataAnnotations;
namespace TheCommerceFrontend.Models.Auth{
    public class UserDto{
      
        public string Id {get; set; } = string.Empty;
 
        public string Name {get; set; } = string.Empty;


        public string Email {get; set; } = string.Empty;
      
        public string PhoneNumber {get; set; } = string.Empty;
    }
}