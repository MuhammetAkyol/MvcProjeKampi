namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateDatabase : DbMigration
    {
        public override void Up()
        {
            // Mevcut foreign key'leri kaldır
            DropForeignKey("dbo.Headings", "WriterID", "dbo.Writers");
            DropForeignKey("dbo.Headings", "CategoryID", "dbo.Categories");

            // Mevcut index'leri kaldır
            DropIndex("dbo.Headings", new[] { "WriterID" });
            DropIndex("dbo.Headings", new[] { "CategoryID" });

            // Sütunları düzenle
            AlterColumn("dbo.Headings", "CategoryID", c => c.Int(nullable: false));
            AlterColumn("dbo.Headings", "WriterID", c => c.Int(nullable: false));

            // Yeni index'leri oluştur
            CreateIndex("dbo.Headings", "CategoryID");
            CreateIndex("dbo.Headings", "WriterID");

            // Foreign Key'leri yeniden ekle
            AddForeignKey("dbo.Headings", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
            AddForeignKey("dbo.Headings", "WriterID", "dbo.Writers", "WriterID", cascadeDelete: true);
        }



        public override void Down()
        {
            // Foreign Key'leri kaldır
            DropForeignKey("dbo.Headings", "WriterID", "dbo.Writers");
            DropForeignKey("dbo.Headings", "CategoryID", "dbo.Categories");

            // Index'leri kaldır
            DropIndex("dbo.Headings", new[] { "WriterID" });
            DropIndex("dbo.Headings", new[] { "CategoryID" });

            // Sütunları eski haline döndür
            AlterColumn("dbo.Headings", "WriterID", c => c.Int());
            AlterColumn("dbo.Headings", "CategoryID", c => c.Int());

            // Eğer eski adlar gerekiyorsa, burada yeniden adlandırabilirsin
            // RenameColumn(table: "dbo.Headings", name: "WriterID", newName: "Writer_CategoryID");
            // RenameColumn(table: "dbo.Headings", name: "CategoryID", newName: "Category_CategoryID");

            // Index'leri yeniden oluştur
            // CreateIndex("dbo.Headings", "Writer_CategoryID");
            // CreateIndex("dbo.Headings", "Category_CategoryID");

            // Foreign Key'leri yeniden ekle
            // AddForeignKey("dbo.Headings", "Writer_CategoryID", "dbo.Categories", "CategoryID");
            // AddForeignKey("dbo.Headings", "Category_CategoryID", "dbo.Categories", "CategoryID");
        }
    }
}
