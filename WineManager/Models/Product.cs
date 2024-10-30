namespace WineManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Mã sản phẩm không được bỏ trống!")]
        [DisplayName("Mã sản phẩm")]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được bỏ trống!")]
        [StringLength(50)]
        [DisplayName("Tên sản phẩm")]
        public string ProductName { get; set; }

        [Column(TypeName = "text")]
        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [Column(TypeName = "numeric")]
        [DisplayName("Giá nhập")]
        [Required(ErrorMessage = "Giá nhập không được bỏ trống!")]
        public decimal PurchasePrice { get; set; }

        [Column(TypeName = "numeric")]
        [DisplayName("Giá bán")]
        [Required(ErrorMessage = "Giá bán không được bỏ trống!")]
        public decimal Price { get; set; }

        [DisplayName("Số lượng")]
        public int Quantity { get; set; }

        [StringLength(20)]
        [DisplayName("Năm sản xuất")]
        public string Vintage { get; set; }

        [Required(ErrorMessage = "Danh mục không được để trống!")]
        [StringLength(10)]
        [DisplayName("Danh mục")]
        public string CatalogyID { get; set; }

        [Column(TypeName = "text")]
        [DisplayName("Hình ảnh")]
        public string Image { get; set; }

        [Required]
        [StringLength(100)]
        public string Region { get; set; }

        public virtual Catalogy Catalogy { get; set; }
    }
}
