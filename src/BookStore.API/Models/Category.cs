using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BookStore.API.Models
{
  [Serializable]
  public class Category
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    public virtual string Name { get; set; }
    public virtual ICollection<Book> Books { get; set; }
  }
}