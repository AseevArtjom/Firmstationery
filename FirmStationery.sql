
CREATE TABLE Managers(
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(100) NOT NULL
);

CREATE TABLE [Types](
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(100) NOT NULL
);

CREATE TABLE Products (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [Name] VARCHAR(50) NOT NULL,
    [TypeId] INT NOT NULL,
    [Count] INT NOT NULL,
    [CostPrice] DECIMAL(10, 2) NOT NULL
    FOREIGN KEY (TypeId) REFERENCES [Types](Id)
);

CREATE TABLE Sales(
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [ProductId] INT NOT NULL,
    [ManagerId] INT NOT NULL,
    [CustomerCompany] NVARCHAR(200) NOT NULL,
    [UnitPrice] DECIMAL(10,2) NOT NULL,
    [SaleDate] DATE NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES [Products](Id),
    FOREIGN KEY (ManagerId) REFERENCES [Managers](Id)
)

INSERT INTO [Types](Name)
VALUES
('Writing Instruments'),
('Clips and Staplers'),
('Office Equipment'),
('Organizers'),
('Office Supplies')

INSERT INTO Products (Name, TypeId, Count, CostPrice)
VALUES 
('Gel Pen', 1 , 200, 7.99),
('Stapler', 2 , 100, 12.49),
('Calculator', 3 , 50, 99.99),
('Document Files', 4 , 80, 4.79),
('Office Scissors', 5 , 120, 3.29);

INSERT INTO Managers (Name)
VALUES 
('John Doe'),
('Alice Johnson'),
('Robert Smith'),
('Emily Brown'),
('Michael Davis');

INSERT INTO Sales (ProductId, ManagerId, CustomerCompany, UnitPrice, SaleDate)
VALUES 
(1, 1, 'Company1', 7.99, '2023-11-01'),
(2, 2, 'Company2', 12.49, '2023-11-02'),
(3, 3, 'Company3', 99.99, '2023-11-03'),
(4, 4, 'Company4', 4.79, '2023-11-04'),
(5, 5, 'Company5', 3.29, '2023-11-05');