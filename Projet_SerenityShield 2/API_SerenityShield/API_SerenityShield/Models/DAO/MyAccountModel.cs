using System.ComponentModel.DataAnnotations;

namespace API_SerenityShield.Models.DAO
{
    public class MyAccountModel
    {


        [Required]
        public string phone
        {
            get; set;
        }
        [Required]
        public string email
        {
            get; set;
        }
        [Required]
        public string firstName
        {
            get; set;
        }
        [Required]
        public string lastName
        {
            get; set;
        }

        public string? idCard
        {
            get; set;
        }
        public string?passport
        {
            get; set;
        }
        public string? addedSecurity
        {
            get; set;
        }

    }
}
