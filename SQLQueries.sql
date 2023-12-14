
SELECT * FROM student;

SELECT sID, sName, city
FROM student
WHERE city = 'kandy';

UPDATE student
SET city = 'Galle'
WHERE sID = 4;

SELECT s.sID, s.sName, s.city, s.cID, c.cName AS CourseName
FROM student s
JOIN course c ON s.cID = c.cID;