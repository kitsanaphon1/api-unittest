namespace WebApiProject.Models
{
    public class Product
    {
        public int Id { get; set; }

        // ✅ วิธีที่ 1: ใช้ required (เฉพาะ C# 11 ขึ้นไป)
        public required string Name { get; set; }

        // ✅ หรือ วิธีที่ 2: ทำให้ nullable (ถ้าใช้ C# ต่ำกว่า 11)
        // public string? Name { get; set; }

        public decimal Price { get; set; }
    }
}
