using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FreeRock.Migrations
{
    public partial class MyMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    ArtistID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PhotoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.ArtistID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreID);
                });

            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    AlbumID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    ReleaseDate = table.Column<int>(nullable: true),
                    IsVerified = table.Column<bool>(nullable: false, defaultValue: false),
                    ArtistID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => x.AlbumID);
                    table.ForeignKey(
                        name: "FK_Album_Artist_ArtistID",
                        column: x => x.ArtistID,
                        principalTable: "Artist",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment<Artist>",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    CommentableObjArtistID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment<Artist>", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comment<Artist>_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment<Artist>_Artist_CommentableObjArtistID",
                        column: x => x.CommentableObjArtistID,
                        principalTable: "Artist",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Like<Artist>",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    LikeableObjArtistID = table.Column<int>(nullable: true),
                    Mark = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like<Artist>", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Like<Artist>_Artist_LikeableObjArtistID",
                        column: x => x.LikeableObjArtistID,
                        principalTable: "Artist",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Like<Artist>_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment<Album>",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    CommentableObjAlbumID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment<Album>", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comment<Album>_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment<Album>_Album_CommentableObjAlbumID",
                        column: x => x.CommentableObjAlbumID,
                        principalTable: "Album",
                        principalColumn: "AlbumID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreAlbum",
                columns: table => new
                {
                    GenreID = table.Column<int>(nullable: false),
                    AlbumID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreAlbum", x => new { x.GenreID, x.AlbumID });
                    table.ForeignKey(
                        name: "FK_GenreAlbum_Album_AlbumID",
                        column: x => x.AlbumID,
                        principalTable: "Album",
                        principalColumn: "AlbumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreAlbum_Genre_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genre",
                        principalColumn: "GenreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Like<Album>",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    LikeableObjAlbumID = table.Column<int>(nullable: true),
                    Mark = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like<Album>", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Like<Album>_Album_LikeableObjAlbumID",
                        column: x => x.LikeableObjAlbumID,
                        principalTable: "Album",
                        principalColumn: "AlbumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Like<Album>_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Song",
                columns: table => new
                {
                    SongID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    AlbumID = table.Column<int>(nullable: false),
                    Number = table.Column<byte>(nullable: false),
                    YouTubeUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Song", x => x.SongID);
                    table.ForeignKey(
                        name: "FK_Song_Album_AlbumID",
                        column: x => x.AlbumID,
                        principalTable: "Album",
                        principalColumn: "AlbumID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Like<Comment<Artist>>",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    LikeableObjID = table.Column<int>(nullable: true),
                    Mark = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like<Comment<Artist>>", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Like<Comment<Artist>>_Comment<Artist>_LikeableObjID",
                        column: x => x.LikeableObjID,
                        principalTable: "Comment<Artist>",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Like<Comment<Artist>>_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Like<Comment<Album>>",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    LikeableObjID = table.Column<int>(nullable: true),
                    Mark = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like<Comment<Album>>", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Like<Comment<Album>>_Comment<Album>_LikeableObjID",
                        column: x => x.LikeableObjID,
                        principalTable: "Comment<Album>",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Like<Comment<Album>>_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment<Song>",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    CommentableObjSongID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment<Song>", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comment<Song>_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment<Song>_Song_CommentableObjSongID",
                        column: x => x.CommentableObjSongID,
                        principalTable: "Song",
                        principalColumn: "SongID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Like<Song>",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    LikeableObjSongID = table.Column<int>(nullable: true),
                    Mark = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like<Song>", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Like<Song>_Song_LikeableObjSongID",
                        column: x => x.LikeableObjSongID,
                        principalTable: "Song",
                        principalColumn: "SongID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Like<Song>_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Like<Comment<Song>>",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    LikeableObjID = table.Column<int>(nullable: true),
                    Mark = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like<Comment<Song>>", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Like<Comment<Song>>_Comment<Song>_LikeableObjID",
                        column: x => x.LikeableObjID,
                        principalTable: "Comment<Song>",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Like<Comment<Song>>_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Album_ArtistID",
                table: "Album",
                column: "ArtistID");

            migrationBuilder.CreateIndex(
                name: "IX_Album_Title",
                table: "Album",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Artist_Name",
                table: "Artist",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comment<Album>_AuthorId",
                table: "Comment<Album>",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment<Album>_CommentableObjAlbumID",
                table: "Comment<Album>",
                column: "CommentableObjAlbumID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment<Artist>_AuthorId",
                table: "Comment<Artist>",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment<Artist>_CommentableObjArtistID",
                table: "Comment<Artist>",
                column: "CommentableObjArtistID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment<Song>_AuthorId",
                table: "Comment<Song>",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment<Song>_CommentableObjSongID",
                table: "Comment<Song>",
                column: "CommentableObjSongID");

            migrationBuilder.CreateIndex(
                name: "IX_GenreAlbum_AlbumID",
                table: "GenreAlbum",
                column: "AlbumID");

            migrationBuilder.CreateIndex(
                name: "IX_Like<Album>_LikeableObjAlbumID",
                table: "Like<Album>",
                column: "LikeableObjAlbumID");

            migrationBuilder.CreateIndex(
                name: "IX_Like<Album>_UserId",
                table: "Like<Album>",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Like<Artist>_LikeableObjArtistID",
                table: "Like<Artist>",
                column: "LikeableObjArtistID");

            migrationBuilder.CreateIndex(
                name: "IX_Like<Artist>_UserId",
                table: "Like<Artist>",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Like<Comment<Album>>_LikeableObjID",
                table: "Like<Comment<Album>>",
                column: "LikeableObjID");

            migrationBuilder.CreateIndex(
                name: "IX_Like<Comment<Album>>_UserId",
                table: "Like<Comment<Album>>",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Like<Comment<Artist>>_LikeableObjID",
                table: "Like<Comment<Artist>>",
                column: "LikeableObjID");

            migrationBuilder.CreateIndex(
                name: "IX_Like<Comment<Artist>>_UserId",
                table: "Like<Comment<Artist>>",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Like<Comment<Song>>_LikeableObjID",
                table: "Like<Comment<Song>>",
                column: "LikeableObjID");

            migrationBuilder.CreateIndex(
                name: "IX_Like<Comment<Song>>_UserId",
                table: "Like<Comment<Song>>",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Like<Song>_LikeableObjSongID",
                table: "Like<Song>",
                column: "LikeableObjSongID");

            migrationBuilder.CreateIndex(
                name: "IX_Like<Song>_UserId",
                table: "Like<Song>",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Song_AlbumID",
                table: "Song",
                column: "AlbumID");

            migrationBuilder.CreateIndex(
                name: "IX_Song_Name",
                table: "Song",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "GenreAlbum");

            migrationBuilder.DropTable(
                name: "Like<Album>");

            migrationBuilder.DropTable(
                name: "Like<Artist>");

            migrationBuilder.DropTable(
                name: "Like<Comment<Album>>");

            migrationBuilder.DropTable(
                name: "Like<Comment<Artist>>");

            migrationBuilder.DropTable(
                name: "Like<Comment<Song>>");

            migrationBuilder.DropTable(
                name: "Like<Song>");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Comment<Album>");

            migrationBuilder.DropTable(
                name: "Comment<Artist>");

            migrationBuilder.DropTable(
                name: "Comment<Song>");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Song");

            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropTable(
                name: "Artist");
        }
    }
}
