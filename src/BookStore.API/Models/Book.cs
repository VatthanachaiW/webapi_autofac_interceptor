using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.API.Models
{
  [Serializable]
  public class Book
  {
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }
    public virtual int CategoryId { get; set; }
    public virtual BookCategory BookCategory { get; set; }
  }
}