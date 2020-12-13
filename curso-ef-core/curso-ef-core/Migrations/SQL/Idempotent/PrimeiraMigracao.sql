IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201105212840_PrimeiraMigration')
BEGIN
    CREATE TABLE [clientes] (
        [id] int NOT NULL IDENTITY,
        [nome] VARCHAR(100) NOT NULL,
        [telefone] CHAR(11) NULL,
        [cep] CHAR(8) NOT NULL,
        [estado] CHAR(2) NOT NULL,
        [cidade] nvarchar(60) NOT NULL,
        CONSTRAINT [PK_clientes] PRIMARY KEY ([id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201105212840_PrimeiraMigration')
BEGIN
    CREATE TABLE [produtos] (
        [id] int NOT NULL IDENTITY,
        [codigo_barras] VARCHAR(14) NOT NULL,
        [descricao] VARCHAR(60) NOT NULL,
        [valor] decimal(18,2) NOT NULL,
        [tipo_produto] nvarchar(max) NOT NULL,
        [ativo] bit NOT NULL DEFAULT CAST(1 AS bit),
        CONSTRAINT [id] PRIMARY KEY ([id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201105212840_PrimeiraMigration')
BEGIN
    CREATE TABLE [pedidos] (
        [id] int NOT NULL IDENTITY,
        [cliente_id] int NOT NULL,
        [iniciado_em] datetime2 NOT NULL DEFAULT (GETDATE()),
        [finalizado_em] datetime2 NOT NULL,
        [tipo_frete] int NOT NULL,
        [status] nvarchar(max) NOT NULL,
        [observacao] VARCHAR(512) NULL,
        CONSTRAINT [PK_pedidos] PRIMARY KEY ([id]),
        CONSTRAINT [FK_pedidos_clientes_cliente_id] FOREIGN KEY ([cliente_id]) REFERENCES [clientes] ([id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201105212840_PrimeiraMigration')
BEGIN
    CREATE TABLE [pedido_items] (
        [id] int NOT NULL IDENTITY,
        [pedido_id] int NOT NULL,
        [produto_id] int NOT NULL,
        [quantidade] int NOT NULL DEFAULT 1,
        [valor] decimal(18,2) NOT NULL,
        [desconto] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_pedido_items] PRIMARY KEY ([id]),
        CONSTRAINT [FK_pedido_items_pedidos_pedido_id] FOREIGN KEY ([pedido_id]) REFERENCES [pedidos] ([id]) ON DELETE CASCADE,
        CONSTRAINT [FK_pedido_items_produtos_produto_id] FOREIGN KEY ([produto_id]) REFERENCES [produtos] ([id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201105212840_PrimeiraMigration')
BEGIN
    CREATE INDEX [index_cliente_telefone] ON [clientes] ([telefone]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201105212840_PrimeiraMigration')
BEGIN
    CREATE INDEX [IX_pedido_items_pedido_id] ON [pedido_items] ([pedido_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201105212840_PrimeiraMigration')
BEGIN
    CREATE INDEX [IX_pedido_items_produto_id] ON [pedido_items] ([produto_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201105212840_PrimeiraMigration')
BEGIN
    CREATE INDEX [IX_pedidos_cliente_id] ON [pedidos] ([cliente_id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201105212840_PrimeiraMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201105212840_PrimeiraMigration', N'3.1.9');
END;

GO

