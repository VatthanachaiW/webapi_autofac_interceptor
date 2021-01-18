using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BookStore.API.Models
{
  [Serializable]
  public class BookCategory
  {
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }
    public virtual ICollection<Book> Books { get; set; }
  }
}