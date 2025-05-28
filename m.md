# 🏡 Property Tax Calculation Program in C#

This C# console application processes data from a file (`utca.txt`) containing information about plots of land, including their size, location, zone classification, and owner. It performs various operations such as:

- Counting properties
- Looking up owners by ID
- Calculating property taxes based on zones
- Detecting zoning inconsistencies in streets
- Writing calculated tax data to a file (`fizetendo.txt`)

---

## 📦 Class Definition: `PlotData`

```csharp
internal class PlotData
{
    public int OwnerId;
    public string StreetName;
    public string HouseNumber;
    public string Zone;
    public int Area;

    public PlotData(string line)
    {
        string[] parts = line.Split(' ');
        this.OwnerId = Convert.ToInt32(parts[0]);
        this.StreetName = parts[1];
        this.HouseNumber = parts[2];
        this.Zone = parts[3];
        this.Area = Convert.ToInt32(parts[4]);
    }
}
```
🔍 Why Use a Class?
- Using a class helps to group all related property data together. It enhances code organization, reusability, and maintainability

## 📂 Reading Data from File (StreamReader)
```csharp
List<PlotData> properties = new List<PlotData>();

using (StreamReader reader = new StreamReader("utca.txt"))
{
    string header = reader.ReadLine(); // Skip the header

    while (!reader.EndOfStream)
    {
        properties.Add(new PlotData(reader.ReadLine()));
    }
} 
```
💡 Why Use using?
- The using statement ensures that the file is properly closed and disposed of, even in case of an exception. It's equivalent to Python's with open(...).

### 💰 Switch Case Example
```csharp
int countA = 0, taxA = 0;
int countB = 0, taxB = 0;
int countC = 0, taxC = 0;

foreach (var property in properties)
{
    switch (property.Zone)
    {
        case "A": countA++; taxA += property.Area * 800; break;
        case "B": countB++; taxB += property.Area * 600; break;
        case "C": countC++; taxC += property.Area * 100; break;
    }
}
```
## 💾 Writing to Output File (StreamWriter)
```csharp
HashSet<int> processedOwners = new HashSet<int>();

using (StreamWriter writer = new StreamWriter("fizetendo.txt"))
{
    foreach (var property in properties)
    {
        if (!processedOwners.Contains(property.OwnerId))
        {
            processedOwners.Add(property.OwnerId);
            writer.WriteLine($"{property.OwnerId} {CalculateTax(property.Zone, property.Area)}");
        }
    }
}
```
💡 Why Use HashSet?
- To ensure that each owner is only processed once, avoiding duplicate entries in the output file

### 📄 Output File (fizetendo.txt)
```yaml
1001 160000
1002 108000
1003 15000
...
```

# 📊 Most Common SQL Queries with Example Outputs

This document covers the most widely used SQL queries with examples and expected outputs.

---

## 🟢 1. SELECT – Retrieve Data

### Query:
```sql
SELECT * FROM Employees;
```
🧾 Output:
| EmployeeID | FirstName | LastName | Department |
|------------|-----------|----------|------------|
| 1          | John      | Smith    | Sales      |
| 2          | Alice     | Johnson  | HR         |

🔍 2. SELECT with WHERE – Filter Data
```sql
SELECT * FROM Employees WHERE Department = 'Sales';
```
Purpose: Fetch only employees who work in the Sales department.

🧾 Output:
| EmployeeID | FirstName | LastName | Department |
|------------|-----------|----------|------------|
| 1          | John      | Smith    | Sales      |

🎯 3. SELECT Specific Columns
```sql
SELECT FirstName, Department FROM Employees;
```
Purpose: Retrieve specific columns only.

🧾 Output:
| FirstName | Department |
|-----------|------------|
| John      | Sales      |
| Alice     | HR         |

🆕 4. INSERT – Add Data
```sql
INSERT INTO Employees (FirstName, LastName, Department)
VALUES ('Bob', 'Taylor', 'IT');
```
Purpose: Insert a new employee into the table.

✏️ 5. UPDATE – Modify Existing Data
```sql
UPDATE Employees
SET Department = 'Marketing'
WHERE EmployeeID = 2;
```
Purpose: Change the department of a specific employee.

❌ 6. DELETE – Remove Data
```sql
DELETE FROM Employees
WHERE EmployeeID = 1;
```
Purpose: Delete a record from the table.

🧮 7. COUNT – Count Records
```sql
SELECT COUNT(*) FROM Employees;
```
Output Example:
| COUNT(*) |
|----------|
| 2        |

📚 8. ORDER BY – Sort Results
```sql
SELECT * FROM Employees
ORDER BY LastName ASC;
```
Purpose: Sort employees alphabetically by last name.

🧩 9. JOIN – Combine Data from Multiple Tables
```sql
SELECT e.FirstName, d.DepartmentName
FROM Employees e
JOIN Departments d ON e.DepartmentID = d.DepartmentID;
```
🧾 Output:
| FirstName | DepartmentName |
|-----------|-----------------|
| John      | Sales           |
| Alice     | HR              |

🎛 10. GROUP BY – Aggregate Data
```sql
SELECT Department, COUNT(*) as NumEmployees
FROM Employees
GROUP BY Department;
```
🧾 Output:
| Department | NumEmployees |
|------------|--------------|
| Sales      | 1            |
| HR         | 1            |

🔄 11. DISTINCT – Remove Duplicates
```sql
SELECT DISTINCT Department FROM Employees;
```
🧾 Output:
| Department |
|------------|
| Sales      |
| HR         |

🧠 12. BETWEEN – Range Filtering
```sql
SELECT * FROM Salaries
WHERE Amount BETWEEN 3000 AND 6000;

⌛ 13. LIMIT / TOP – Restrict Result Count
PostgreSQL / MySQL:
```sql
SELECT * FROM Employees LIMIT 5;
```
SQL Server:
```sql
SELECT TOP 5 * FROM Employees;
```
🔐 14. IN – Match Any from a List
```sql
SELECT * FROM Employees
WHERE Department IN ('Sales', 'IT');
```
🚫 15. IS NULL / IS NOT NULL – Check Null Values
```sql
SELECT * FROM Employees
WHERE MiddleName IS NULL;
```
🧪 16. LIKE – Pattern Matching
```sql
SELECT * FROM Employees
WHERE FirstName LIKE 'A%';
```
Purpose: Find names starting with 'A'.

🧠 17. CREATE TABLES
```sql
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Department VARCHAR(50)
);

CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    DepartmentID INT,
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);

CREATE TABLE IF NOT EXISTS Projects (
    ProjectID INT PRIMARY KEY,
    ProjectName VARCHAR(100)
);

CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY,
    DepartmentName VARCHAR(100) UNIQUE NOT NULL
);

```
🆕 18. ALTER TABLE – Add Columns, Drop Columns
```sql
ALTER TABLE Employees
ADD Email VARCHAR(100);
DROP COLUMN Email;
```

🧹 19. Clean-Up / Reset Script
```sql
DROP TABLE IF EXISTS Employees;
DROP TABLE IF EXISTS Departments;
DROP DATABASE IF EXISTS CompanyDB;
```

🧪 20. Database Creation
```sql 
CREATE DATABASE CompanyDB;
USE CompanyDB;
...
```



📌 Tips
- Use aliases (e.g. e.FirstName) for readability when joining.
- Always back up your database before running DELETE or UPDATE queries.
- Use EXPLAIN to check query performance.

📂 End of SQL Cheatsheet



