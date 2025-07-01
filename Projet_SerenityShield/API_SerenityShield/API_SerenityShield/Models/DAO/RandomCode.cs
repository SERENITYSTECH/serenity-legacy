namespace API_SerenityShield.Models.DAO
{
    public class RandomCode
    {
        public string CreateRandomCode(int nb)
        {
            string code = string.Empty;
            Random rnd = new Random();

            for (int j = 0; j < nb; j++)
            {
                code += (rnd.Next(10));//returns random integers < 10
            }

            return code;
        }

        public RandomCode() { }
    }




}
