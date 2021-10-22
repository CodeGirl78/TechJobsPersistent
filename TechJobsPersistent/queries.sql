--Part 1
SELECT COLUMN_NAME, DATA_TYPE 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'jobs'; 

--Part 2
SELECT Name 
FROM employers 
WHERE Location = 'Saint Louis';

--Part 3
SELECT skills.Name,skills.Description 
FROM jobskills 
LEFT JOIN skills ON skills.id = jobskills.skillid 
WHERE techjobs.jobSkills.jobId IS NOT NULL
ORDER BY skills.Name ASC;