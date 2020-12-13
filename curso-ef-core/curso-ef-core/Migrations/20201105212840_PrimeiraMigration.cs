using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace curso_ef_core.Migrations
{
    public partial class PrimeiraMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    telefone = table.Column<string>(type: "CHAR(11)", nullable: true),
                    cep = table.Column<string>(type: "CHAR(8)", nullable: false),
                    estado = table.Column<string>(type: "CHAR(2)", nullable: false),
                    cidade = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "produtos",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigo_barras = table.Column<string>(type: "VARCHAR(14)", nullable: false),
                    descricao = table.Column<string>(type: "VARCHAR(60)", nullable: false),
                    valor = table.Column<decimal>(nullable: false),
                    tipo_produto = table.Column<string>(nullable: false),
                    ativo = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pedidos",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cliente_id = table.Column<int>(nullable: false),
                    iniciado_em = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    finalizado_em = table.Column<DateTime>(nullable: false),
                    tipo_frete = table.Column<int>(nullable: false),
                    status = table.Column<string>(nullable: false),
                    observacao = table.Column<string>(type: "VARCHAR(512)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedidos", x => x.id);
                    table.ForeignKey(
                        name: "FK_pedidos_clientes_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "clientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pedido_items",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pedido_id = table.Column<int>(nullable: false),
                    produto_id = table.Column<int>(nullable: false),
                    quantidade = table.Column<int>(nullable: false, defaultValue: 1),
                    valor = table.Column<decimal>(nullable: false),
                    desconto = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedido_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_pedido_items_pedidos_pedido_id",
                        column: x => x.pedido_id,
                        principalTable: "pedidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pedido_items_produtos_produto_id",
                        column: x => x.produto_id,
                        principalTable: "produtos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "index_cliente_telefone",
                table: "clientes",
                column: "telefone");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_items_pedido_id",
                table: "pedido_items",
                column: "pedido_id");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_items_produto_id",
                table: "pedido_items",
                column: "produto_id");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_cliente_id",
                table: "pedidos",
                column: "cliente_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pedido_items");

            migrationBuilder.DropTable(
                name: "pedidos");

            migrationBuilder.DropTable(
                name: "produtos");

            migrationBuilder.DropTable(
                name: "clientes");
        }
    }
}
