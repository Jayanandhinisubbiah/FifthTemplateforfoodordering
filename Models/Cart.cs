using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FifthTemplateforfoodordering.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        
        public int UserId { get; set; }

        
        public int FoodId { get; set; }
        public int Qnt { get; set; }
        public virtual User User { get; set; }
        public virtual Food Food { get; set; }

    }
}
