IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250512165738_AddBookToDb'
)
BEGIN
    CREATE TABLE [Books] (
        [BookId] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [ISBN] nvarchar(max) NOT NULL,
        [Price] float NOT NULL,
        CONSTRAINT [PK_Books] PRIMARY KEY ([BookId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250512165738_AddBookToDb'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250512165738_AddBookToDb', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513065045_ChangePriceColumnToDecimalInBooksTable'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'Price');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT [' + @var + '];');
    ALTER TABLE [Books] ALTER COLUMN [Price] decimal(10,5) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513065045_ChangePriceColumnToDecimalInBooksTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250513065045_ChangePriceColumnToDecimalInBooksTable', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513070932_AddGenreToDb'
)
BEGIN
    CREATE TABLE [Genres] (
        [GenreId] int NOT NULL IDENTITY,
        [GenreName] nvarchar(max) NOT NULL,
        [Display] int NOT NULL,
        CONSTRAINT [PK_Genres] PRIMARY KEY ([GenreId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513070932_AddGenreToDb'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250513070932_AddGenreToDb', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513105838_RenameColumnDisplayToDisplayOrder'
)
BEGIN
    EXEC sp_rename N'[Genres].[Display]', N'DisplayOrder', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513105838_RenameColumnDisplayToDisplayOrder'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250513105838_RenameColumnDisplayToDisplayOrder', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513105954_NoChangeMigrationCreatesActuallyAnEmptyMigration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250513105954_NoChangeMigrationCreatesActuallyAnEmptyMigration', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513111008_RemoveGenresTable'
)
BEGIN
    DROP TABLE [Genres];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513111008_RemoveGenresTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250513111008_RemoveGenresTable', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513113627_ReaddGenresTableToDb'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250513113627_ReaddGenresTableToDb', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513113810_ReaddGenresTableToDb2'
)
BEGIN
    CREATE TABLE [Genres] (
        [GenreId] int NOT NULL IDENTITY,
        [GenreName] nvarchar(max) NOT NULL,
        [DisplayOrder] int NOT NULL,
        CONSTRAINT [PK_Genres] PRIMARY KEY ([GenreId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513113810_ReaddGenresTableToDb2'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250513113810_ReaddGenresTableToDb2', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513115952_SeedBookTable'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BookId', N'ISBN', N'Price', N'Title') AND [object_id] = OBJECT_ID(N'[Books]'))
        SET IDENTITY_INSERT [Books] ON;
    EXEC(N'INSERT INTO [Books] ([BookId], [ISBN], [Price], [Title])
    VALUES (1, N''123B12'', 10.99, N''Spider without duty''),
    (2, N''12123B12'', 11.99, N''Fortune of time''),
    (3, N''773B12'', 20.99, N''Fake Sunday''),
    (4, N''CC3B12'', 25.99, N''Cookie Jar''),
    (5, N''XV4B12'', 40.99, N''Cloudy forest'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BookId', N'ISBN', N'Price', N'Title') AND [object_id] = OBJECT_ID(N'[Books]'))
        SET IDENTITY_INSERT [Books] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513115952_SeedBookTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250513115952_SeedBookTable', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513145217_UpdateGenreTableNameAndColumnNAme'
)
BEGIN
    ALTER TABLE [Genres] DROP CONSTRAINT [PK_Genres];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513145217_UpdateGenreTableNameAndColumnNAme'
)
BEGIN
    EXEC sp_rename N'[Genres]', N'th_genres', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513145217_UpdateGenreTableNameAndColumnNAme'
)
BEGIN
    EXEC sp_rename N'[th_genres].[GenreName]', N'Name', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513145217_UpdateGenreTableNameAndColumnNAme'
)
BEGIN
    ALTER TABLE [th_genres] ADD CONSTRAINT [PK_th_genres] PRIMARY KEY ([GenreId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513145217_UpdateGenreTableNameAndColumnNAme'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250513145217_UpdateGenreTableNameAndColumnNAme', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513150623_ChangePKandRequiredInGenreTable'
)
BEGIN
    EXEC sp_rename N'[th_genres].[GenreId]', N'Genre_Id', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513150623_ChangePKandRequiredInGenreTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250513150623_ChangePKandRequiredInGenreTable', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513150944_RenameGenreToCategory'
)
BEGIN
    DROP TABLE [th_genres];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513150944_RenameGenreToCategory'
)
BEGIN
    CREATE TABLE [Categories] (
        [CategoryId] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513150944_RenameGenreToCategory'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250513150944_RenameGenreToCategory', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513151636_MaxLengthAndNotMappedInBooksTable'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'ISBN');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Books] ALTER COLUMN [ISBN] nvarchar(20) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513151636_MaxLengthAndNotMappedInBooksTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250513151636_MaxLengthAndNotMappedInBooksTable', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513154748_AddAuthorPublisherAndSubCategoryTables'
)
BEGIN
    CREATE TABLE [Authors] (
        [Author_Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(50) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [BirthDate] datetime2 NOT NULL,
        [Location] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Authors] PRIMARY KEY ([Author_Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513154748_AddAuthorPublisherAndSubCategoryTables'
)
BEGIN
    CREATE TABLE [Publishers] (
        [Publisher_Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Location] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Publishers] PRIMARY KEY ([Publisher_Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513154748_AddAuthorPublisherAndSubCategoryTables'
)
BEGIN
    CREATE TABLE [SubCategories] (
        [SubCategory_Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_SubCategories] PRIMARY KEY ([SubCategory_Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250513154748_AddAuthorPublisherAndSubCategoryTables'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250513154748_AddAuthorPublisherAndSubCategoryTables', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250514063138_Updates'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Publishers]') AND [c].[name] = N'Location');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Publishers] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Publishers] ALTER COLUMN [Location] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250514063138_Updates'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'Title');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Books] ALTER COLUMN [Title] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250514063138_Updates'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Authors]') AND [c].[name] = N'Location');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Authors] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Authors] ALTER COLUMN [Location] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250514063138_Updates'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250514063138_Updates', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250514063436_AddBookDetailsToDb'
)
BEGIN
    CREATE TABLE [BookDetails] (
        [BookDetail_Id] int NOT NULL IDENTITY,
        [NumberOfChapters] int NOT NULL,
        [NumberOfPages] int NOT NULL,
        [Weight] nvarchar(max) NULL,
        CONSTRAINT [PK_BookDetails] PRIMARY KEY ([BookDetail_Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250514063436_AddBookDetailsToDb'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250514063436_AddBookDetailsToDb', N'9.0.4');
END;

COMMIT;
GO