using System;
using SQLite;

namespace MauiAssessment.Models
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Category { get; set; }
        public string Detail { get; set; }
        public string LaunchDate { get; set; }
        public string ProductPhoto { get; set; }
        public bool HasPhoto { get; set; }

        public Product Clone() => MemberwiseClone() as Product;

        //public (bool IsValid, string? ErrorMessage) Validate()
        //{
        //    if (string.IsNullOrWhiteSpace(Name))
        //    {
        //        return (false, $"{nameof(Name)} is required.");
        //    }
        //    else if (Price <= 0)
        //    {
        //        return (false, $"{nameof(Price)} should be greater than 0.");
        //    }
        //    return (true, null);
        //}
    }
}

