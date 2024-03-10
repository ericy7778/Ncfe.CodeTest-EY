namespace Ncfe.CodeTest.Models
{
    public class LoadLearnerResponse
    {
        public Learner Learner { get; set; }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }
    }
}
