using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace events_planner.Models
{
    [Table("recovery")]
    public class Recovery
    {
        [Column("recovery_id")]
        [Key]
        public int Id { get; set; }

        [Column("token")]
        [StringLength(200, MinimumLength = 200)]
        public string Token { get; set; }

        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }

        /***********************
            RELATIONS
        ************************/

        /// <summary> relation with User (One to One) </summary>
        public virtual User User { get; set; }
    }
}