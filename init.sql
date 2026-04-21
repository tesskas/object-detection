CREATE DATABASE VisualRecognition;
GO

USE VisualRecognition;
GO


CREATE TABLE Objects (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Variant NVARCHAR(100) NULL
);


CREATE TABLE Occurrences (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ObjectId INT NOT NULL,
    Timestamp DATETIME NOT NULL,
    Count INT NOT NULL,

    CONSTRAINT FK_Occurrences_Objects
        FOREIGN KEY (ObjectId) REFERENCES Objects(Id)
);


INSERT INTO Objects (Name, Variant)
VALUES 
('hrnek', 'červená'),
('hrnek', 'modrá'),
('hrnek', 'zelená'),
('lampa', 'zelená'),
('auto', 'modrá'),
('auto', 'červená');

INSERT INTO Occurrences (ObjectId, Timestamp, Count)
VALUES
-- hrnky
(1, '2026-04-20 08:00:00', 2),
(1, '2026-04-20 12:00:00', 1),
(2, '2026-04-19 09:00:00', 3),
(3, '2026-04-18 10:00:00', 1),

-- lampy
(4, '2026-04-20 14:00:00', 1),

-- auta
(5, '2026-04-18 15:00:00', 1),
(6, '2026-04-19 16:00:00', 2),
(5, '2026-04-20 09:00:00', 3);


CREATE INDEX IX_Occurrences_Timestamp
ON Occurrences(Timestamp);

CREATE INDEX IX_Occurrences_ObjectId
ON Occurrences(ObjectId);