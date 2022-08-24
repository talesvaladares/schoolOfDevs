using System.ComponentModel.DataAnnotations.Schema;
using schoolOfDevs.Enuns;

namespace schoolOfDevs.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public TypeUser TypeUser { get; set; }
        public string  UserName { get; set; }
        public string Password { get; set; }

        //não vira uma coluna na tabela
        //não é mapeada nas migrations
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [NotMapped]
        public string CurrentPassword { get; set; }
    }
}
