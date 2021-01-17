using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.API.Models
{
  [Serializable]
  public class Book
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    public virtual string Name { get; set; }
    public virtual int CategoryId { get; set; }
    public virtual Category Category { get; set; }
  }
}