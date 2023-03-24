CREATE TABLE userlist(
id INT NOT NULL PRIMARY KEY IDENTITY,
user_name varchar (100) NOT NULL UNIQUE,
name VARCHAR (100) NOT NULL,
email varchar (100) NOT NULL UNIQUE,
phone varchar (20) NOT NULL,
address varchar (100) NOT NULL,
manager_name varchar (100) NOT NULL,
created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
status varchar (10) NOT NULL
);


INSERT INTO userlist (user_name, name, email, phone, address, manager_name, status)
VALUES
('test123', 'Test1', 'test1.check@gmail.com', '+911234567890', 'Varanasi, UP, India', 'Mr. Dubey', '1');