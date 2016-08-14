SELECT o.name AS TableName,round(SUM(AU.used_pages)*8/1024,2) AS SizeInMB,p.rows AS TotalRows
FROM sys.objects o
JOIN sys.indexes i ON O.object_id = I.object_id
JOIN sys.partitions p ON O.object_id = P.object_id
JOIN sys.allocation_units AS AU on AU.container_id = P.partition_id
WHERE O.type = 'U' AND I.index_id IN (0,1) and p.rows>0
GROUP BY O.name, p.rows
ORDER BY SUM(AU.used_pages) DESC
