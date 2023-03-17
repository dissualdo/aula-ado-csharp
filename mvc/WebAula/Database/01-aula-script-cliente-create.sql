CREATE TABLE Cliente (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Telefone VARCHAR(20) NULL
);

INSERT INTO Cliente (Nome, Email, Telefone) VALUES ('João da Silva', 'joao.silva@gmail.com', '(11) 9999-9999');
INSERT INTO Cliente (Nome, Email, Telefone) VALUES ('Maria Souza', 'maria.souza@hotmail.com', '(21) 8888-8888');
INSERT INTO Cliente (Nome, Email, Telefone) VALUES ('José Santos', 'jose.santos@yahoo.com.br', '(31) 7777-7777');
