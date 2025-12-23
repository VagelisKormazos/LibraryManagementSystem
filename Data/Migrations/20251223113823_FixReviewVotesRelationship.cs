using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixReviewVotesRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewId1",
                table: "ReviewVotes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewVotes_ReviewId1",
                table: "ReviewVotes",
                column: "ReviewId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewVotes_Reviews_ReviewId1",
                table: "ReviewVotes",
                column: "ReviewId1",
                principalTable: "Reviews",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewVotes_Reviews_ReviewId1",
                table: "ReviewVotes");

            migrationBuilder.DropIndex(
                name: "IX_ReviewVotes_ReviewId1",
                table: "ReviewVotes");

            migrationBuilder.DropColumn(
                name: "ReviewId1",
                table: "ReviewVotes");
        }
    }
}
