using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace coupon.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CouponIdCode",
                table: "Coupons",
                newName: "CouponCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CouponCode",
                table: "Coupons",
                newName: "CouponIdCode");
        }
    }
}
