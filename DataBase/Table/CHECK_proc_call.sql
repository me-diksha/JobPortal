SELECT 
    o.name          AS calling_function,
    o.type_desc     AS type,
    m.definition    AS source_code
FROM sys.sql_modules m
JOIN sys.objects o ON o.object_id = m.object_id
WHERE m.definition LIKE '%P_GET_TIMEWINDOW%'
  AND o.name != 'P_GET_TIMEWINDOW'; 