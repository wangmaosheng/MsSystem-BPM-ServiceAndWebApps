-- CREATE USER 'root'@'%' IDENTIFIED BY '123456';
-- GRANT All privileges ON *.* TO 'wms'@'%';

GRANT All privileges on *.* to 'root'@'%' identified by '123456' with grant option;
flush privileges;