use railwaydatabase
-- Normalized Tables
CREATE TABLE Admins (
    AdminId INT IDENTITY(1,1) PRIMARY KEY,
    AdminUsername VARCHAR(50) NOT NULL,
    AdminPassword VARCHAR(50) NOT NULL
);

CREATE TABLE Customers (
    CustId INT IDENTITY(1,1) PRIMARY KEY,
    CustName VARCHAR(100) NOT NULL,
    CustPhone VARCHAR(15) NOT NULL,
    CustEmail VARCHAR(100) NOT NULL,
    CustPassword VARCHAR(50) NOT NULL
);

CREATE TABLE TrainMaster (
    TrainNumber INT PRIMARY KEY,
    TrainName VARCHAR(100) NOT NULL,
    Source VARCHAR(50) NOT NULL,
    Destination VARCHAR(50) NOT NULL
);

CREATE TABLE TrainClasses (
    TrainClassId INT IDENTITY(1,1) PRIMARY KEY,
    TrainNumber INT NOT NULL FOREIGN KEY REFERENCES TrainMaster(TrainNumber),
    ClassType VARCHAR(20) NOT NULL, -- 'Sleeper','AC3','AC2','AC1'
    AvailableSeats INT NOT NULL,
    MaxSeats INT NOT NULL,
    Price DECIMAL(10,2) NOT NULL
);
select * from TrainClasses
CREATE TABLE Reservations (
    BookingId INT IDENTITY(1,1) PRIMARY KEY,
    CustId INT NOT NULL FOREIGN KEY REFERENCES Customers(CustId),
    TrainClassId INT NOT NULL FOREIGN KEY REFERENCES TrainClasses(TrainClassId),
    TravelDate DATE NOT NULL,
    CurrentStatus VARCHAR(20) NOT NULL DEFAULT 'Confirmed',
    BookingDate DATETIME NOT NULL DEFAULT GETDATE(),
    Amount DECIMAL(10,2) NOT NULL DEFAULT 0
);

CREATE TABLE Cancellations (
    CancelId INT IDENTITY(1,1) PRIMARY KEY,
    BookingId INT NOT NULL FOREIGN KEY REFERENCES Reservations(BookingId),
    CancelDate DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Refunds (
    RefundId INT IDENTITY(1,1) PRIMARY KEY,
    BookingId INT NOT NULL,
    
    RefundAmount DECIMAL(10,2) NOT NULL,
    RefundDate DATETIME NOT NULL DEFAULT GETDATE(),
    Reason VARCHAR(50) NULL
);
drop table refunds

-- Trigger: decrement available seats after booking
CREATE TRIGGER trg_DecrementSeats_AfterInsertReservation
ON Reservations
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE tc
    SET tc.AvailableSeats = tc.AvailableSeats - i.PassengerCount
    FROM TrainClasses tc
    INNER JOIN (
        SELECT TrainClassId, COUNT(*) AS PassengerCount
        FROM inserted
        GROUP BY TrainClassId
    ) i ON tc.TrainClassId = i.TrainClassId
    WHERE tc.AvailableSeats >= i.PassengerCount;
END;

GO
drop trigger trg_DecrementSeats_AfterInsertReservation
-- Trigger: increment available seats after cancellation (not exceeding MaxSeats)
CREATE TRIGGER trg_IncrementSeats_AfterInsertCancellation
ON Cancellations
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @BookingId INT, @TrainClassId INT;
    SELECT @BookingId = BookingId FROM inserted;
    SELECT @TrainClassId = TrainClassId FROM Reservations WHERE BookingId = @BookingId;
    UPDATE TrainClasses SET AvailableSeats = CASE WHEN AvailableSeats < MaxSeats THEN AvailableSeats + 1 ELSE AvailableSeats END WHERE TrainClassId = @TrainClassId;
END;
GO

-- Sample data
INSERT INTO Admins (AdminUsername, AdminPassword) VALUES ('admin','admin123');
INSERT INTO Customers (CustName, CustPhone, CustEmail, CustPassword) VALUES ('Shami','9867890989','sh123@gmail..com','sh2345');
INSERT INTO TrainMaster (TrainNumber,TrainName,Source,Destination) VALUES (1,'Swarna Jayanti Express ','Bokaro','Delhi');
INSERT INTO TrainClasses (TrainNumber,ClassType,AvailableSeats,MaxSeats,Price) VALUES (1,'Sleeper',50,50,200),(1,'AC3',40,40,500),(1,'AC2',30,30,800),(1,'AC1',20,20,1200);
-- Insert Train Classes for Bokaro → NDLS (TrainNumber = 1)


-- Suppose TrainNumber = 2 for Patna → Ranchi
INSERT INTO TrainClasses (TrainNumber, ClassType, AvailableSeats, MaxSeats, Price)
VALUES
 (3, 'Sleeper', 60, 60, 180),
 (3, 'AC3',     50, 50, 480),
 (3, 'AC2',     35, 35, 750),
 (3, 'AC1',     25, 25, 1100);

-- Suppose TrainNumber = 3 for Ranchi → Mumbai
INSERT INTO TrainClasses (TrainNumber, ClassType, AvailableSeats, MaxSeats, Price)
VALUES
 (4, 'Sleeper', 70, 70, 220),
 (4, 'AC3',     55, 55, 520),
 (4, 'AC2',     40, 40, 820),
 (4, 'AC1',     30, 30, 1250);

 INSERT INTO TrainMaster (TrainNumber, TrainName, Source, Destination)
VALUES
 
 (3, 'Patna–Ranchi Local',    'Patna',  'Ranchi'),
 (4, 'Ranchi–Mumbai Mail',    'Ranchi',  'Mumbai');


select * from Customers
ALTER TABLE TrainMaster
ADD IsActive BIT NOT NULL DEFAULT 1;

ALTER TABLE Reservations
ADD PassengerCount INT NOT NULL DEFAULT 1;
select * from Reservations
select * from Customers
select * from TrainClasses
select * from TrainMaster