# GITPL Task Project

## Table Creation

```sql
CREATE TABLE Clients (
    Client_ID INT PRIMARY KEY IDENTITY,
    Client_Name NVARCHAR(100),
    Project_Name NVARCHAR(100),
    Project_Cost DECIMAL(18, 2)
);
```

## Stored Procedure


### Fetch all records
<HR>

#### Create
```sql
CREATE PROCEDURE GetClientByID
    @Client_ID INT
AS
BEGIN
    SELECT Client_ID, Client_Name, Project_Name, Project_Cost
    FROM Clients
    WHERE Client_ID = @Client_ID;
END
```

#### Use

```SQL
EXEC GetClientByID @Client_ID = 123;
```

### Fetch by Id
<HR>

#### Create
```sql
CREATE PROCEDURE GetClientByID
    @Client_ID INT
AS
BEGIN
    SELECT Client_ID, Client_Name, Project_Name, Project_Cost
    FROM Clients
    WHERE Client_ID = @Client_ID;
END
```

#### Use

```SQL
EXEC GetClientByID @Client_ID = 123;
```




<br>

<HR>

#### Create (SEEDING SAMPLE DATA)

<HR>

```sql
CREATE PROCEDURE InsertClients
    @Client_Name NVARCHAR(100),
    @Project_Name NVARCHAR(100),
    @Project_Cost DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO GITPL_Clients (Client_Name, Project_Name, Project_Cost)
    VALUES (@Client_Name, @Project_Name, @Project_Cost)
END
```

#### Seeding

```sql
EXEC InsertClients 'LocalClient', 'LocalProject', 10000.00;
EXEC InsertClients 'ServicesClient', 'ServicesProject', 20000.00;
EXEC InsertClients 'ThirdpartyClient', 'ThirdPartyClient', 30000.00;
```