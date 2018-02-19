using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace events_planner.Models
{
    [Table("role")]
    public class Role
    {
        [Column("role_id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        [StringLength(30, MinimumLength = 3)]
        [MaxLength(30, ErrorMessage = "Name's Role must be under 20 characters")]
        [MinLength(3, ErrorMessage = "Name's Role must be at least 3 characters")]
        [Required]
        public string Name { get; set; }

        /***********************
            RELATIONS
        ************************/
        /// <summary> relation with User (One to Many) </summary>
        [JsonIgnore]
        public ICollection<User> Users { get; set; }

        /// <summary> relation with Price (One to Many) </summary>
        [JsonIgnore]
        public ICollection<Price> Prices { get; set; }
    }
}