namespace flappyBirb_serveur.Models.DTOs
{
    public class ScoreDisplayDTO
    {
        public int Id { get; set; }
       
        public int ScoreValue { get; set; }

       
        public double TimeInSeconds { get; set; }

     
        public DateTime Date { get; set; }

       
        public bool IsPublic { get; set; } = false;

       
        public virtual string? Pseudo { get; set; }

        public ScoreDisplayDTO(Score score) 
        { 
            Id = score.Id;
            ScoreValue = score.ScoreValue;
            TimeInSeconds = score.TimeInSeconds;
            Date = score.Date;
            IsPublic = score.IsPublic;
            Pseudo = score.Pseudo!.UserName;

        }
    }
}
