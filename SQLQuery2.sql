  SELECT * FROM Patients WHERE followUp_doctorID NOT IN (SELECT Id FROM Staff)
