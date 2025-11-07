using Microsoft.Build.Framework;

namespace flappyBirb_serveur.Models
{
    public class Score
    {
        public int Id { get; set; }
        // Valeur du score obtenu par le joueur
        [Required]
        public int ScoreValue { get; set; }

        // Temps du jeu (en secondes)
        [Required]
        public double TimeInSeconds { get; set; }

        // Date et heure de la partie (déterminée par le serveur)
        [Required]
        public DateTime Date { get; set; }

        // Visibilité publique ou privée
        [Required]
        public bool IsPublic { get; set; } = false;

        // Nom du joueur (optionnel si tu veux afficher le pseudo)
        public virtual User? Pseudo { get; set; }


    }
}
