using System.ComponentModel.DataAnnotations;

namespace CleanArchitectureExample.Domain.Entities.Identity

{
    public class RoleModification
    {
        [Required]
        public string RoleName { get; set; }


        public string[]? AddIds { get; set; }

        public string[]? DeleteIds { get; set; }
    }
}
