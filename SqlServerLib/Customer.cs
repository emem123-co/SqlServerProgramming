namespace SqlServerLib;

public class Customer
{
   public int Id { get; set; } = 0; //default value of 0
   public string Name {get; set; } = string.Empty; //default empty
   public string City { get; set; } = string.Empty; //default empty
   public string State { get; set; } = string.Empty; //default empty
   public decimal? Sales { get; set; } = 0; //question mark after the type allows this property to be null
   public bool Active { get; set; } = true; //default value is set to the option that is most likely to be the case

}
