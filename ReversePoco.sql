-- EFCore 10 specific types: vector and json
CREATE TABLE Testing_EfCore10
(
    Id INT NOT NULL IDENTITY(1, 1),
    Embedding vector(1234) NULL,
    ShippingAddress json NOT NULL,
    CONSTRAINT PK_Testing_EfCore10 PRIMARY KEY CLUSTERED (Id)
);