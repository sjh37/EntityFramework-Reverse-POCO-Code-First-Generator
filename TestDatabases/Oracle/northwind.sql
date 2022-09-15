ALTER SESSION SET CONTAINER=pdb1;
CREATE USER efrpg IDENTIFIED BY abc123;
GRANT CONNECT, RESOURCE, DBA, SYSDBA TO efrpg;

/*
-- Drop all tables and their indexes, constraints, and triggers.
DROP TABLE efrpg.ORDER_DETAILS PURGE;
DROP TABLE efrpg.ORDERS PURGE;
DROP TABLE efrpg.PRODUCTS PURGE;
DROP TABLE efrpg.SUPPLIERS PURGE;
DROP TABLE efrpg.CATEGORIES PURGE;
DROP TABLE efrpg.CUSTOMERS PURGE;
DROP TABLE efrpg.EMPLOYEES PURGE;
DROP TABLE efrpg.SHIPPERS PURGE;

-- Drop all sequences.
DROP SEQUENCE SEQ_NW_CATEGORIES;
DROP SEQUENCE SEQ_NW_CUSTOMERS;
DROP SEQUENCE SEQ_NW_EMPLOYEES;
DROP SEQUENCE SEQ_NW_ORDERS;
DROP SEQUENCE SEQ_NW_PRODUCTS;
DROP SEQUENCE SEQ_NW_SHIPPERS;
DROP SEQUENCE SEQ_NW_SUPPLIERS;
*/

CREATE TABLE efrpg.CATEGORIES
(
    CATEGORY_ID   NUMBER(9)         NOT NULL,
    CATEGORY_NAME VARCHAR2(15 BYTE) NOT NULL,
    DESCRIPTION   VARCHAR2(2000 BYTE),
    PICTURE       VARCHAR2(255 BYTE),
    CONSTRAINT PK_CATEGORIES PRIMARY KEY (CATEGORY_ID)
);

CREATE UNIQUE INDEX UIDX_CATEGORY_NAME ON efrpg.CATEGORIES (CATEGORY_NAME);

CREATE SEQUENCE SEQ_NW_CATEGORIES
    MINVALUE 1
    MAXVALUE 999999999999999999999999999
    START WITH 9
    INCREMENT BY 1
    NOCYCLE
    NOCACHE
    NOORDER;

COMMENT ON COLUMN efrpg.CATEGORIES.CATEGORY_ID IS 'Number automatically assigned to a new category.';

COMMENT ON COLUMN efrpg.CATEGORIES.CATEGORY_NAME IS 'Name of food category.';

COMMENT ON COLUMN efrpg.CATEGORIES.PICTURE IS 'A picture representing the food category.';

SET DEFINE OFF;

INSERT INTO efrpg.CATEGORIES(CATEGORY_ID, CATEGORY_NAME, DESCRIPTION, PICTURE)
VALUES (1, 'Beverages', 'Soft drinks, coffees, teas, beers, and ales', 'beverages.gif');

INSERT INTO efrpg.CATEGORIES(CATEGORY_ID, CATEGORY_NAME, DESCRIPTION, PICTURE)
VALUES (2, 'Condiments', 'Sweet and savory sauces, relishes, spreads, and seasonings', 'condiments.gif');

INSERT INTO efrpg.CATEGORIES(CATEGORY_ID, CATEGORY_NAME, DESCRIPTION, PICTURE)
VALUES (3, 'Confections', 'Desserts, candies, and sweet breads', 'confections.gif');

INSERT INTO efrpg.CATEGORIES(CATEGORY_ID, CATEGORY_NAME, DESCRIPTION, PICTURE)
VALUES (4, 'Dairy Products', 'Cheeses', 'diary.gif');

INSERT INTO efrpg.CATEGORIES(CATEGORY_ID, CATEGORY_NAME, DESCRIPTION, PICTURE)
VALUES (5, 'Grains/Cereals', 'Breads, crackers, pasta, and cereal', 'cereals.gif');

INSERT INTO efrpg.CATEGORIES(CATEGORY_ID, CATEGORY_NAME, DESCRIPTION, PICTURE)
VALUES (6, 'Meat/Poultry', 'Prepared meats', 'meat.gif');

INSERT INTO efrpg.CATEGORIES(CATEGORY_ID, CATEGORY_NAME, DESCRIPTION, PICTURE)
VALUES (7, 'Produce', 'Dried fruit and bean curd', 'produce.gif');

INSERT INTO efrpg.CATEGORIES(CATEGORY_ID, CATEGORY_NAME, DESCRIPTION, PICTURE)
VALUES (8, 'Seafood', 'Seaweed and fish', 'seafood.gif');

COMMIT;

CREATE TABLE efrpg.CUSTOMERS
(
    CUSTOMER_ID   NUMBER(9)         NOT NULL,
    CUSTOMER_CODE VARCHAR2(5 BYTE)  NOT NULL,
    COMPANY_NAME  VARCHAR2(40 BYTE) NOT NULL,
    CONTACT_NAME  VARCHAR2(30 BYTE),
    CONTACT_TITLE VARCHAR2(30 BYTE),
    ADDRESS       VARCHAR2(60 BYTE),
    CITY          VARCHAR2(15 BYTE),
    REGION        VARCHAR2(15 BYTE),
    POSTAL_CODE   VARCHAR2(10 BYTE),
    COUNTRY       VARCHAR2(15 BYTE),
    PHONE         VARCHAR2(24 BYTE),
    FAX           VARCHAR2(24 BYTE),
    CONSTRAINT PK_CUSTOMERS PRIMARY KEY (CUSTOMER_ID)
);

CREATE UNIQUE INDEX UIDX_CUSTOMERS_CODE ON CUSTOMERS (CUSTOMER_CODE);

CREATE INDEX IDX_CUSTOMERS_CITY ON CUSTOMERS (CITY);

CREATE INDEX IDX_CUSTOMERS_COMPANY_NAME ON CUSTOMERS (COMPANY_NAME);

CREATE INDEX IDX_CUSTOMERS_POSTAL_CODE ON CUSTOMERS (POSTAL_CODE);

CREATE INDEX IDX_CUSTOMERS_REGION ON CUSTOMERS (REGION);

CREATE SEQUENCE SEQ_NW_CUSTOMERS
    MINVALUE 1
    MAXVALUE 999999999999999999999999999
    START WITH 92
    INCREMENT BY 1
    NOCYCLE
    NOCACHE
    NOORDER;

COMMENT ON COLUMN efrpg.CUSTOMERS.CUSTOMER_ID IS 'Unique five-character code based on customer name.';

COMMENT ON COLUMN efrpg.CUSTOMERS.ADDRESS IS 'Street or post-office box.';

COMMENT ON COLUMN efrpg.CUSTOMERS.REGION IS 'State or province.';

COMMENT ON COLUMN efrpg.CUSTOMERS.PHONE IS 'Phone number includes country code or area code.';

COMMENT ON COLUMN efrpg.CUSTOMERS.FAX IS 'Phone number includes country code or area code.';

SET DEFINE OFF;
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (1, 'ALFKI', 'Alfreds Futterkiste', 'Maria Anders', 'Sales Representative',
        'Obere Str. 57', 'Berlin', '12209', 'Germany',
        '030-0074321', '030-0076545');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (2, 'ANATR', 'Ana Trujillo Emparedados y helados', 'Ana Trujillo', 'Owner',
        'Avda. de la Constitución 2222', 'México D.F.', '05021', 'Mexico',
        '(5) 555-4729', '(5) 555-3745');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (3, 'ANTON', 'Antonio Moreno Taquería', 'Antonio Moreno', 'Owner',
        'Mataderos  2312', 'México D.F.', '05023', 'Mexico', '(5) 555-3932');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (4, 'AROUT', 'Around the Horn', 'Thomas Hardy', 'Sales Representative',
        '120 Hanover Sq.', 'London', 'WA1 1DP', 'UK',
        '(171) 555-7788', '(171) 555-6750');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (5, 'BERGS', 'Berglunds snabbköp', 'Christina Berglund', 'Order Administrator',
        'Berguvsvägen  8', 'Luleå', 'S-958 22', 'Sweden',
        '0921-12 34 65', '0921-12 34 67');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (6, 'BLAUS', 'Blauer See Delikatessen', 'Hanna Moos', 'Sales Representative',
        'Forsterstr. 57', 'Mannheim', '68306', 'Germany',
        '0621-08460', '0621-08924');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (7, 'BLONP', 'Blondel père et fils', 'Frédérique Citeaux', 'Marketing Manager',
        '24, place Kléber', 'Strasbourg', '67000', 'France',
        '88.60.15.31', '88.60.15.32');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (8, 'BOLID', 'Bólido Comidas preparadas', 'Martín Sommer', 'Owner',
        'C/ Araquil, 67', 'Madrid', '28023', 'Spain',
        '(91) 555 22 82', '(91) 555 91 99');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (9, 'BONAP', 'Bon app''', 'Laurence Lebihan', 'Owner',
        '12, rue des Bouchers', 'Marseille', '13008', 'France',
        '91.24.45.40', '91.24.45.41');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (10, 'BOTTM', 'Bottom-Dollar Markets', 'Elizabeth Lincoln', 'Accounting Manager',
        '23 Tsawassen Blvd.', 'Tsawassen', 'BC', 'T2F 8M4', 'Canada',
        '(604) 555-4729', '(604) 555-3745');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (11, 'BSBEV', 'B''s Beverages', 'Victoria Ashworth', 'Sales Representative',
        'Fauntleroy Circus', 'London', 'EC2 5NT', 'UK', '(171) 555-1212');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (12, 'CACTU', 'Cactus Comidas para llevar', 'Patricio Simpson', 'Sales Agent',
        'Cerrito 333', 'Buenos Aires', '1010', 'Argentina',
        '(1) 135-5555', '(1) 135-4892');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (13, 'CENTC', 'Centro comercial Moctezuma', 'Francisco Chang', 'Marketing Manager',
        'Sierras de Granada 9993', 'México D.F.', '05022', 'Mexico',
        '(5) 555-3392', '(5) 555-7293');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (14, 'CHOPS', 'Chop-suey Chinese', 'Yang Wang', 'Owner',
        'Hauptstr. 29', 'Bern', '3012', 'Switzerland', '0452-076545');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE)
VALUES (15, 'COMMI', 'Comércio Mineiro', 'Pedro Afonso', 'Sales Associate',
        'Av. dos Lusíadas, 23', 'São Paulo', 'SP', '05432-043', 'Brazil',
        '(11) 555-7647');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (16, 'CONSH', 'Consolidated Holdings', 'Elizabeth Brown', 'Sales Representative',
        'Berkeley Gardens
12  Brewery ', 'London', 'WX1 6LT', 'UK',
        '(171) 555-2282', '(171) 555-9199');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (17, 'DRACD', 'Drachenblut Delikatessen', 'Sven Ottlieb', 'Order Administrator',
        'Walserweg 21', 'Aachen', '52066', 'Germany',
        '0241-039123', '0241-059428');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (18, 'DUMON', 'Du monde entier', 'Janine Labrune', 'Owner',
        '67, rue des Cinquante Otages', 'Nantes', '44000', 'France',
        '40.67.88.88', '40.67.89.89');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (19, 'EASTC', 'Eastern Connection', 'Ann Devon', 'Sales Agent',
        '35 King George', 'London', 'WX3 6FW', 'UK',
        '(171) 555-0297', '(171) 555-3373');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (20, 'ERNSH', 'Ernst Handel', 'Roland Mendel', 'Sales Manager',
        'Kirchgasse 6', 'Graz', '8010', 'Austria',
        '7675-3425', '7675-3426');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE)
VALUES (21, 'FAMIA', 'Familia Arquibaldo', 'Aria Cruz', 'Marketing Assistant',
        'Rua Orós, 92', 'São Paulo', 'SP', '05442-030', 'Brazil',
        '(11) 555-9857');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (22, 'FISSA', 'FISSA Fabrica Inter. Salchichas S.A.', 'Diego Roel', 'Accounting Manager',
        'C/ Moralzarzal, 86', 'Madrid', '28034', 'Spain',
        '(91) 555 94 44', '(91) 555 55 93');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (23, 'FOLIG', 'Folies gourmandes', 'Martine Rancé', 'Assistant Sales Agent',
        '184, chaussée de Tournai', 'Lille', '59000', 'France',
        '20.16.10.16', '20.16.10.17');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (24, 'FOLKO', 'Folk och fä HB', 'Maria Larsson', 'Owner',
        'Åkergatan 24', 'Bräcke', 'S-844 67', 'Sweden', '0695-34 67 21');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (25, 'FRANK', 'Frankenversand', 'Peter Franken', 'Marketing Manager',
        'Berliner Platz 43', 'München', '80805', 'Germany',
        '089-0877310', '089-0877451');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (26, 'FRANR', 'France restauration', 'Carine Schmitt', 'Marketing Manager',
        '54, rue Royale', 'Nantes', '44000', 'France',
        '40.32.21.21', '40.32.21.20');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (27, 'FRANS', 'Franchi S.p.A.', 'Paolo Accorti', 'Sales Representative',
        'Via Monte Bianco 34', 'Torino', '10100', 'Italy',
        '011-4988260', '011-4988261');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (28, 'FURIB', 'Furia Bacalhau e Frutos do Mar', 'Lino Rodriguez ', 'Sales Manager',
        'Jardim das rosas n. 32', 'Lisboa', '1675', 'Portugal',
        '(1) 354-2534', '(1) 354-2535');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (29, 'GALED', 'Galería del gastrónomo', 'Eduardo Saavedra', 'Marketing Manager',
        'Rambla de Cataluña, 23', 'Barcelona', '08022', 'Spain',
        '(93) 203 4560', '(93) 203 4561');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (30, 'GODOS', 'Godos Cocina Típica', 'José Pedro Freyre', 'Sales Manager',
        'C/ Romero, 33', 'Sevilla', '41101', 'Spain', '(95) 555 82 82');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE)
VALUES (31, 'GOURL', 'Gourmet Lanchonetes', 'André Fonseca', 'Sales Associate',
        'Av. Brasil, 442', 'Campinas', 'SP', '04876-786', 'Brazil',
        '(11) 555-9482');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE)
VALUES (32, 'GREAL', 'Great Lakes Food Market', 'Howard Snyder', 'Marketing Manager',
        '2732 Baker Blvd.', 'Eugene', 'OR', '97403', 'USA',
        '(503) 555-7555');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (33, 'GROSR', 'GROSELLA-Restaurante', 'Manuel Pereira', 'Owner',
        '5ª Ave. Los Palos Grandes', 'Caracas', 'DF', '1081', 'Venezuela',
        '(2) 283-2951', '(2) 283-3397');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (34, 'HANAR', 'Hanari Carnes', 'Mario Pontes', 'Accounting Manager',
        'Rua do Paço, 67', 'Rio de Janeiro', 'RJ', '05454-876', 'Brazil',
        '(21) 555-0091', '(21) 555-8765');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (35, 'HILAA', 'HILARIÓN-Abastos', 'Carlos Hernández', 'Sales Representative',
        'Carrera 22 con Ave. Carlos Soublette #8-35', 'San Cristóbal', 'Táchira', '5022', 'Venezuela',
        '(5) 555-1340', '(5) 555-1948');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (36, 'HUNGC', 'Hungry Coyote Import Store', 'Yoshi Latimer', 'Sales Representative',
        'City Center Plaza
516 Main St.', 'Elgin', 'OR', '97827', 'USA',
        '(503) 555-6874', '(503) 555-2376');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, COUNTRY, PHONE, FAX)
VALUES (37, 'HUNGO', 'Hungry Owl All-Night Grocers', 'Patricia McKenna', 'Sales Associate',
        '8 Johnstown Road', 'Cork', 'Co. Cork', 'Ireland',
        '2967 542', '2967 3333');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE)
VALUES (38, 'ISLAT', 'Island Trading', 'Helen Bennett', 'Marketing Manager',
        'Garden House
Crowther Way', 'Cowes', 'Isle of Wight', 'PO31 7PJ', 'UK',
        '(198) 555-8888');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (39, 'KOENE', 'Königlich Essen', 'Philip Cramer', 'Sales Associate',
        'Maubelstr. 90', 'Brandenburg', '14776', 'Germany', '0555-09876');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (40, 'LACOR', 'La corne d''abondance', 'Daniel Tonini', 'Sales Representative',
        '67, avenue de l''Europe', 'Versailles', '78000', 'France',
        '30.59.84.10', '30.59.85.11');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (41, 'LAMAI', 'La maison d''Asie', 'Annette Roulet', 'Sales Manager',
        '1 rue Alsace-Lorraine', 'Toulouse', '31000', 'France',
        '61.77.61.10', '61.77.61.11');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (42, 'LAUGB', 'Laughing Bacchus Wine Cellars', 'Yoshi Tannamuri', 'Marketing Assistant',
        '1900 Oak St.', 'Vancouver', 'BC', 'V3F 2K1', 'Canada',
        '(604) 555-3392', '(604) 555-7293');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (43, 'LAZYK', 'Lazy K Kountry Store', 'John Steel', 'Marketing Manager',
        '12 Orchestra Terrace', 'Walla Walla', 'WA', '99362', 'USA',
        '(509) 555-7969', '(509) 555-6221');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (44, 'LEHMS', 'Lehmanns Marktstand', 'Renate Messner', 'Sales Representative',
        'Magazinweg 7', 'Frankfurt a.M. ', '60528', 'Germany',
        '069-0245984', '069-0245874');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE)
VALUES (45, 'LETSS', 'Let''s Stop N Shop', 'Jaime Yorres', 'Owner',
        '87 Polk St.
Suite 5', 'San Francisco', 'CA', '94117', 'USA',
        '(415) 555-5938');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (46, 'LILAS', 'LILA-Supermercado', 'Carlos González', 'Accounting Manager',
        'Carrera 52 con Ave. Bolívar #65-98 Llano Largo', 'Barquisimeto', 'Lara', '3508', 'Venezuela',
        '(9) 331-6954', '(9) 331-7256');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (47, 'LINOD', 'LINO-Delicateses', 'Felipe Izquierdo', 'Owner',
        'Ave. 5 de Mayo Porlamar', 'I. de Margarita', 'Nueva Esparta', '4980', 'Venezuela',
        '(8) 34-56-12', '(8) 34-93-93');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (48, 'LONEP', 'Lonesome Pine Restaurant', 'Fran Wilson', 'Sales Manager',
        '89 Chiaroscuro Rd.', 'Portland', 'OR', '97219', 'USA',
        '(503) 555-9573', '(503) 555-9646');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (49, 'MAGAA', 'Magazzini Alimentari Riuniti', 'Giovanni Rovelli', 'Marketing Manager',
        'Via Ludovico il Moro 22', 'Bergamo', '24100', 'Italy',
        '035-640230', '035-640231');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (50, 'MAISD', 'Maison Dewey', 'Catherine Dewey', 'Sales Agent',
        'Rue Joseph-Bens 532', 'Bruxelles', 'B-1180', 'Belgium',
        '(02) 201 24 67', '(02) 201 24 68');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (51, 'MEREP', 'Mère Paillarde', 'Jean Fresnière', 'Marketing Assistant',
        '43 rue St. Laurent', 'Montréal', 'Québec', 'H1J 1C3', 'Canada',
        '(514) 555-8054', '(514) 555-8055');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (52, 'MORGK', 'Morgenstern Gesundkost', 'Alexander Feuer', 'Marketing Assistant',
        'Heerstr. 22', 'Leipzig', '04179', 'Germany', '0342-023176');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (53, 'NORTS', 'North/South', 'Simon Crowther', 'Sales Associate',
        'South House
300 Queensbridge', 'London', 'SW7 1RZ', 'UK',
        '(171) 555-7733', '(171) 555-2530');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (54, 'OCEAN', 'Océano Atlántico Ltda.', 'Yvonne Moncada', 'Sales Agent',
        'Ing. Gustavo Moncada 8585
Piso 20-A', 'Buenos Aires', '1010', 'Argentina',
        '(1) 135-5333', '(1) 135-5535');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (55, 'OLDWO', 'Old World Delicatessen', 'Rene Phillips', 'Sales Representative',
        '2743 Bering St.', 'Anchorage', 'AK', '99508', 'USA',
        '(907) 555-7584', '(907) 555-2880');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (56, 'OTTIK', 'Ottilies Käseladen', 'Henriette Pfalzheim', 'Owner',
        'Mehrheimerstr. 369', 'Köln', '50739', 'Germany',
        '0221-0644327', '0221-0765721');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (57, 'PARIS', 'Paris spécialités', 'Marie Bertrand', 'Owner',
        '265, boulevard Charonne', 'Paris', '75012', 'France',
        '(1) 42.34.22.66', '(1) 42.34.22.77');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (58, 'PERIC', 'Pericles Comidas clásicas', 'Guillermo Fernández', 'Sales Representative',
        'Calle Dr. Jorge Cash 321', 'México D.F.', '05033', 'Mexico',
        '(5) 552-3745', '(5) 545-3745');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (59, 'PICCO', 'Piccolo und mehr', 'Georg Pipps', 'Sales Manager',
        'Geislweg 14', 'Salzburg', '5020', 'Austria',
        '6562-9722', '6562-9723');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (60, 'PRINI', 'Princesa Isabel Vinhos', 'Isabel de Castro', 'Sales Representative',
        'Estrada da saúde n. 58', 'Lisboa', '1756', 'Portugal', '(1) 356-5634');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (61, 'QUEDE', 'Que Delícia', 'Bernardo Batista', 'Accounting Manager',
        'Rua da Panificadora, 12', 'Rio de Janeiro', 'RJ', '02389-673', 'Brazil',
        '(21) 555-4252', '(21) 555-4545');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE)
VALUES (62, 'QUEEN', 'Queen Cozinha', 'Lúcia Carvalho', 'Marketing Assistant',
        'Alameda dos Canàrios, 891', 'São Paulo', 'SP', '05487-020', 'Brazil',
        '(11) 555-1189');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (63, 'QUICK', 'QUICK-Stop', 'Horst Kloss', 'Accounting Manager',
        'Taucherstraße 10', 'Cunewalde', '01307', 'Germany', '0372-035188');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (64, 'RANCH', 'Rancho grande', 'Sergio Gutiérrez', 'Sales Representative',
        'Av. del Libertador 900', 'Buenos Aires', '1010', 'Argentina',
        '(1) 123-5555', '(1) 123-5556');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (65, 'RATTC', 'Rattlesnake Canyon Grocery', 'Paula Wilson', 'Assistant Sales Representative',
        '2817 Milton Dr.', 'Albuquerque', 'NM', '87110', 'USA',
        '(505) 555-5939', '(505) 555-3620');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (66, 'REGGC', 'Reggiani Caseifici', 'Maurizio Moroni', 'Sales Associate',
        'Strada Provinciale 124', 'Reggio Emilia', '42100', 'Italy',
        '0522-556721', '0522-556722');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE)
VALUES (67, 'RICAR', 'Ricardo Adocicados', 'Janete Limeira', 'Assistant Sales Agent',
        'Av. Copacabana, 267', 'Rio de Janeiro', 'RJ', '02389-890', 'Brazil',
        '(21) 555-3412');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (68, 'RICSU', 'Richter Supermarkt', 'Michael Holz', 'Sales Manager',
        'Grenzacherweg 237', 'Genève', '1203', 'Switzerland', '0897-034214');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (69, 'ROMEY', 'Romero y tomillo', 'Alejandra Camino', 'Accounting Manager',
        'Gran Vía, 1', 'Madrid', '28001', 'Spain',
        '(91) 745 6200', '(91) 745 6210');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (70, 'SANTG', 'Santé Gourmet', 'Jonas Bergulfsen', 'Owner',
        'Erling Skakkes gate 78', 'Stavern', '4110', 'Norway',
        '07-98 92 35', '07-98 92 47');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE)
VALUES (71, 'SAVEA', 'Save-a-lot Markets', 'Jose Pavarotti', 'Sales Representative',
        '187 Suffolk Ln.', 'Boise', 'ID', '83720', 'USA',
        '(208) 555-8097');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (72, 'SEVES', 'Seven Seas Imports', 'Hari Kumar', 'Sales Manager',
        '90 Wadhurst Rd.', 'London', 'OX15 4NB', 'UK',
        '(171) 555-1717', '(171) 555-5646');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (73, 'SIMOB', 'Simons bistro', 'Jytte Petersen', 'Owner',
        'Vinbæltet 34', 'København', '1734', 'Denmark',
        '31 12 34 56', '31 13 35 57');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (74, 'SPECD', 'Spécialités du monde', 'Dominique Perrier', 'Marketing Manager',
        '25, rue Lauriston', 'Paris', '75016', 'France',
        '(1) 47.55.60.10', '(1) 47.55.60.20');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (75, 'SPLIR', 'Split Rail Beer & Ale', 'Art Braunschweiger', 'Sales Manager',
        'P.O. Box 555', 'Lander', 'WY', '82520', 'USA',
        '(307) 555-4680', '(307) 555-6525');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (76, 'SUPRD', 'Suprêmes délices', 'Pascale Cartrain', 'Accounting Manager',
        'Boulevard Tirou, 255', 'Charleroi', 'B-6000', 'Belgium',
        '(071) 23 67 22 20', '(071) 23 67 22 21');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE)
VALUES (77, 'THEBI', 'The Big Cheese', 'Liz Nixon', 'Marketing Manager',
        '89 Jefferson Way
Suite 2', 'Portland', 'OR', '97201', 'USA',
        '(503) 555-3612');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (78, 'THECR', 'The Cracker Box', 'Liu Wong', 'Marketing Assistant',
        '55 Grizzly Peak Rd.', 'Butte', 'MT', '59801', 'USA',
        '(406) 555-5834', '(406) 555-8083');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (79, 'TOMSP', 'Toms Spezialitäten', 'Karin Josephs', 'Marketing Manager',
        'Luisenstr. 48', 'Münster', '44087', 'Germany',
        '0251-031259', '0251-035695');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (80, 'TORTU', 'Tortuga Restaurante', 'Miguel Angel Paolino', 'Owner',
        'Avda. Azteca 123', 'México D.F.', '05033', 'Mexico', '(5) 555-2933');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (81, 'TRADH', 'Tradição Hipermercados', 'Anabela Domingues', 'Sales Representative',
        'Av. Inês de Castro, 414', 'São Paulo', 'SP', '05634-030', 'Brazil',
        '(11) 555-2167', '(11) 555-2168');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (82, 'TRAIH', 'Trail''s Head Gourmet Provisioners', 'Helvetius Nagy', 'Sales Associate',
        '722 DaVinci Blvd.', 'Kirkland', 'WA', '98034', 'USA',
        '(206) 555-8257', '(206) 555-2174');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (83, 'VAFFE', 'Vaffeljernet', 'Palle Ibsen', 'Sales Manager',
        'Smagsløget 45', 'Århus', '8200', 'Denmark',
        '86 21 32 43', '86 22 33 44');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (84, 'VICTE', 'Victuailles en stock', 'Mary Saveley', 'Sales Agent',
        '2, rue du Commerce', 'Lyon', '69004', 'France',
        '78.32.54.86', '78.32.54.87');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (85, 'VINET', 'Vins et alcools Chevalier', 'Paul Henriot', 'Accounting Manager',
        '59 rue de l''Abbaye', 'Reims', '51100', 'France',
        '26.47.15.10', '26.47.15.11');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (86, 'WANDK', 'Die Wandernde Kuh', 'Rita Müller', 'Sales Representative',
        'Adenauerallee 900', 'Stuttgart', '70563', 'Germany',
        '0711-020361', '0711-035428');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (87, 'WARTH', 'Wartian Herkku', 'Pirkko Koskitalo', 'Accounting Manager',
        'Torikatu 38', 'Oulu', '90110', 'Finland',
        '981-443655', '981-443655');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE)
VALUES (88, 'WELLI', 'Wellington Importadora', 'Paula Parente', 'Sales Manager',
        'Rua do Mercado, 12', 'Resende', 'SP', '08737-363', 'Brazil',
        '(14) 555-8122');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (89, 'WHITC', 'White Clover Markets', 'Karl Jablonski', 'Owner',
        '305 - 14th Ave. S.
Suite 3B', 'Seattle', 'WA', '98128', 'USA',
        '(206) 555-4112', '(206) 555-4115');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (90, 'WILMK', 'Wilman Kala', 'Matti Karttunen', 'Owner/Marketing Assistant',
        'Keskuskatu 45', 'Helsinki', '21240', 'Finland',
        '90-224 8858', '90-224 8858');
INSERT INTO efrpg.CUSTOMERS
(CUSTOMER_ID, CUSTOMER_CODE, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (91, 'WOLZA', 'Wolski  Zajazd', 'Zbyszek Piestrzeniewicz', 'Owner',
        'ul. Filtrowa 68', 'Warszawa', '01-012', 'Poland',
        '(26) 642-7012', '(26) 642-7012');
COMMIT;

CREATE TABLE efrpg.EMPLOYEES
(
    EMPLOYEE_ID       NUMBER(9)         NOT NULL,
    LASTNAME          VARCHAR2(20 BYTE) NOT NULL,
    FIRSTNAME         VARCHAR2(10 BYTE) NOT NULL,
    TITLE             VARCHAR2(30 BYTE),
    TITLE_OF_COURTESY VARCHAR2(25 BYTE),
    BIRTHDATE         DATE,
    HIREDATE          DATE,
    ADDRESS           VARCHAR2(60 BYTE),
    CITY              VARCHAR2(15 BYTE),
    REGION            VARCHAR2(15 BYTE),
    POSTAL_CODE       VARCHAR2(10 BYTE),
    COUNTRY           VARCHAR2(15 BYTE),
    HOME_PHONE        VARCHAR2(24 BYTE),
    EXTENSION         VARCHAR2(4 BYTE),
    PHOTO             VARCHAR2(255 BYTE),
    NOTES             VARCHAR2(2000 BYTE),
    REPORTS_TO        NUMBER(9),
    CONSTRAINT PK_EMPLOYEES PRIMARY KEY (EMPLOYEE_ID)
);

CREATE INDEX IDX_EMPLOYEES_LASTNAME ON EMPLOYEES (LASTNAME);

CREATE INDEX IDX_EMPLOYEES_POSTAL_CODE ON EMPLOYEES (POSTAL_CODE);

-- Create a trigger to validate employee birthdate.
CREATE OR REPLACE TRIGGER TRG_EMP_BIRTHDATE
    BEFORE INSERT OR UPDATE
        OF BIRTHDATE
    ON EMPLOYEES
    REFERENCING NEW AS NEW OLD AS OLD
    FOR EACH ROW
BEGIN
    IF :New.birthdate > TRUNC(SYSDATE) THEN
        RAISE_APPLICATION_ERROR(num => -20000, msg => 'Birthdate cannot be in the future');
    END IF;
END;

CREATE SEQUENCE SEQ_NW_EMPLOYEES
    MINVALUE 1
    MAXVALUE 999999999999999999999999999
    START WITH 10
    INCREMENT BY 1
    NOCYCLE
    NOCACHE
    NOORDER;

COMMENT ON COLUMN efrpg.EMPLOYEES.EMPLOYEE_ID IS 'Number automatically assigned to new employee.';

COMMENT ON COLUMN efrpg.EMPLOYEES.TITLE IS 'Employee''s title.';

COMMENT ON COLUMN efrpg.EMPLOYEES.TITLE_OF_COURTESY IS 'Title used in salutations.';

COMMENT ON COLUMN efrpg.EMPLOYEES.ADDRESS IS 'Street or post-office box.';

COMMENT ON COLUMN efrpg.EMPLOYEES.REGION IS 'State or province.';

COMMENT ON COLUMN efrpg.EMPLOYEES.HOME_PHONE IS 'Phone number includes country code or area code.';

COMMENT ON COLUMN efrpg.EMPLOYEES.EXTENSION IS 'Internal telephone extension number.';

COMMENT ON COLUMN efrpg.EMPLOYEES.PHOTO IS 'Picture of employee.';

COMMENT ON COLUMN efrpg.EMPLOYEES.NOTES IS 'General information about employee''s background.';

COMMENT ON COLUMN efrpg.EMPLOYEES.REPORTS_TO IS 'Employee''s supervisor.';

SET DEFINE OFF;

INSERT INTO efrpg.EMPLOYEES(EMPLOYEE_ID, LASTNAME, FIRSTNAME, TITLE, TITLE_OF_COURTESY, BIRTHDATE, HIREDATE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, HOME_PHONE,
                      EXTENSION, PHOTO, NOTES, REPORTS_TO)
VALUES (1, 'Davolio', 'Nancy', 'Sales Representative', 'Ms.', TO_DATE('12/08/1968 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/01/1992 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), '507 - 20th Ave. E.Apt. 2A', 'Seattle', 'WA', '98122', 'USA', '(206) 555-9857', '5467',
        'nancy.jpg',
        'Education includes a BA in psychology from Colorado State University.  She also completed "The Art of the Cold Call."  Nancy is a member of Toastmasters International.',
        2);

INSERT INTO efrpg.EMPLOYEES(EMPLOYEE_ID, LASTNAME, FIRSTNAME, TITLE, TITLE_OF_COURTESY, BIRTHDATE, HIREDATE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, HOME_PHONE,
                      EXTENSION, PHOTO, NOTES)
VALUES (2, 'Fuller', 'Andrew', 'Vice President, Sales', 'Dr.', TO_DATE('02/19/1952 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/14/1992 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), '908 W. Capital Way', 'Tacoma', 'WA', '98401', 'USA', '(206) 555-9482', '3457', 'andrew  .jpg',
        'Andrew received his BTS commercial and a Ph.D. in international marketing from the University of Dallas.  He is fluent in French and Italian and reads German.  He joined the company as a sales representative, was promoted to sales manager and was then named vice president of sales.  Andrew is a member of the Sales Management Roundtable, the Seattle Chamber of Commerce, and the Pacific Rim Importers Association.');

INSERT INTO efrpg.EMPLOYEES(EMPLOYEE_ID, LASTNAME, FIRSTNAME, TITLE, TITLE_OF_COURTESY, BIRTHDATE, HIREDATE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, HOME_PHONE,
                      EXTENSION, PHOTO, NOTES, REPORTS_TO)
VALUES (3, 'Leverling', 'Janet', 'Sales Representative', 'Ms.', TO_DATE('08/30/1963 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/01/1992 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), '722 Moss Bay Blvd.', 'Kirkland', 'WA', '98033', 'USA', '(206) 555-3412', '3355', 'janet.jpg',
        'Janet has a BS degree in chemistry from Boston College).  She has also completed a certificate program in food retailing management.  Janet was hired as a sales associate and was promoted to sales representative.',
        2);

INSERT INTO efrpg.EMPLOYEES(EMPLOYEE_ID, LASTNAME, FIRSTNAME, TITLE, TITLE_OF_COURTESY, BIRTHDATE, HIREDATE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, HOME_PHONE,
                      EXTENSION, PHOTO, NOTES, REPORTS_TO)
VALUES (4, 'Peacock', 'Margaret', 'Sales Representative', 'Mrs.', TO_DATE('09/19/1958 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/03/1993 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), '4110 Old Redmond Rd.', 'Redmond', 'WA', '98052', 'USA', '(206) 555-8122', '5176',
        'margaret.jpg',
        'Margaret holds a BA in English literature from Concordia College and an MA from the American Institute of Culinary Arts. She was temporarily assigned to the London office before returning to her permanent post in Seattle.',
        2);

INSERT INTO efrpg.EMPLOYEES(EMPLOYEE_ID, LASTNAME, FIRSTNAME, TITLE, TITLE_OF_COURTESY, BIRTHDATE, HIREDATE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, HOME_PHONE,
                      EXTENSION, PHOTO, NOTES, REPORTS_TO)
VALUES (5, 'Buchanan', 'Steven', 'Sales Manager', 'Mr.', TO_DATE('03/04/1955 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/17/1993 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), '14 Garrett Hill', 'London', 'SW1 8JR', 'UK', '(71) 555-4848', '3453', 'steven.jpg',
        'Steven Buchanan graduated from St. Andrews University, Scotland, with a BSC degree.  Upon joining the company as a sales representative, he spent 6 months in an orientation program at the Seattle office and then returned to his permanent post in London, where he was promoted to sales manager.  Mr. Buchanan has completed the courses "Successful Telemarketing" and "International Sales Management."  He is fluent in French.',
        2);

INSERT INTO efrpg.EMPLOYEES(EMPLOYEE_ID, LASTNAME, FIRSTNAME, TITLE, TITLE_OF_COURTESY, BIRTHDATE, HIREDATE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, HOME_PHONE,
                      EXTENSION, PHOTO, NOTES, REPORTS_TO)
VALUES (6, 'Suyama', 'Michael', 'Sales Representative', 'Mr.', TO_DATE('07/02/1963 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/17/1993 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 'Coventry House Miner Rd.', 'London', 'EC2 7JR', 'UK', '(71) 555-7773', '428', 'michael.jpg',
        'Michael is a graduate of Sussex University (MA, economics) and the University of California at Los Angeles (MBA, marketing).  He has also taken the courses "Multi-Cultural Selling" and "Time Management for the Sales Professional."  He is fluent in Japanese and can read and write French, Portuguese, and Spanish.',
        5);

INSERT INTO efrpg.EMPLOYEES(EMPLOYEE_ID, LASTNAME, FIRSTNAME, TITLE, TITLE_OF_COURTESY, BIRTHDATE, HIREDATE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, HOME_PHONE,
                      EXTENSION, PHOTO, NOTES, REPORTS_TO)
VALUES (7, 'King', 'Robert', 'Sales Representative', 'Mr.', TO_DATE('05/29/1960 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/02/1994 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 'Edgeham Hollow Winchester Way', 'London', 'RG1 9SP', 'UK', '(71) 555-5598', '465',
        'robert.jpg',
        'Robert King served in the Peace Corps and traveled extensively before completing his degree in English at the University of Michigan and then joining the company.  After completing a course entitled "Selling in Europe," he was transferred to the London office.',
        5);

INSERT INTO efrpg.EMPLOYEES(EMPLOYEE_ID, LASTNAME, FIRSTNAME, TITLE, TITLE_OF_COURTESY, BIRTHDATE, HIREDATE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, HOME_PHONE,
                      EXTENSION, PHOTO, NOTES, REPORTS_TO)
VALUES (8, 'Callahan', 'Laura', 'Inside Sales Coordinator', 'Ms.', TO_DATE('01/09/1958 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/05/1994 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), '4726 - 11th Ave. N.E.', 'Seattle', 'WA', '98105', 'USA', '(206) 555-1189', '2344',
        'laura.jpg',
        'Laura received a BA in psychology from the University of Washington.  She has also completed a course in business French.  She reads and writes French.',
        2);

INSERT INTO efrpg.EMPLOYEES(EMPLOYEE_ID, LASTNAME, FIRSTNAME, TITLE, TITLE_OF_COURTESY, BIRTHDATE, HIREDATE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, HOME_PHONE,
                      EXTENSION, PHOTO, NOTES, REPORTS_TO)
VALUES (9, 'Dodsworth', 'Anne', 'Sales Representative', 'Ms.', TO_DATE('07/02/1969 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/15/1994 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), '7 Houndstooth Rd.', 'London', 'WG2 7LT', 'UK', '(71) 555-4444', '452', 'anne.jpg',
        'Anne has a BA degree in English from St. Lawrence College.  She is fluent in French and German.', 5);

COMMIT;

ALTER TABLE EMPLOYEES
    ADD CONSTRAINT FK_REPORTS_TO FOREIGN KEY (REPORTS_TO) REFERENCES EMPLOYEES (EMPLOYEE_ID);

CREATE TABLE efrpg.SUPPLIERS
(
    SUPPLIER_ID   NUMBER(9)         NOT NULL,
    COMPANY_NAME  VARCHAR2(40 BYTE) NOT NULL,
    CONTACT_NAME  VARCHAR2(30 BYTE),
    CONTACT_TITLE VARCHAR2(30 BYTE),
    ADDRESS       VARCHAR2(60 BYTE),
    CITY          VARCHAR2(15 BYTE),
    REGION        VARCHAR2(15 BYTE),
    POSTAL_CODE   VARCHAR2(10 BYTE),
    COUNTRY       VARCHAR2(15 BYTE),
    PHONE         VARCHAR2(24 BYTE),
    FAX           VARCHAR2(24 BYTE),
    HOME_PAGE     VARCHAR2(500 BYTE),
    CONSTRAINT PK_SUPPLIERS PRIMARY KEY (SUPPLIER_ID)
);

CREATE INDEX IDX_SUPPLIERS_COMPANY_NAME ON SUPPLIERS (COMPANY_NAME);

CREATE INDEX IDX_SUPPLIERS_POSTAL_CODE ON SUPPLIERS (POSTAL_CODE);

CREATE SEQUENCE SEQ_NW_SUPPLIERS
    MINVALUE 1
    MAXVALUE 999999999999999999999999999
    START WITH 30
    INCREMENT BY 1
    NOCYCLE
    NOCACHE
    NOORDER;

COMMENT ON COLUMN efrpg.SUPPLIERS.SUPPLIER_ID IS 'Number automatically assigned to new supplier.';

COMMENT ON COLUMN efrpg.SUPPLIERS.ADDRESS IS 'Street or post-office box.';

COMMENT ON COLUMN efrpg.SUPPLIERS.REGION IS 'State or province.';

COMMENT ON COLUMN efrpg.SUPPLIERS.PHONE IS 'Phone number includes country code or area code.';

COMMENT ON COLUMN efrpg.SUPPLIERS.FAX IS 'Phone number includes country code or area code.';

COMMENT ON COLUMN efrpg.SUPPLIERS.HOME_PAGE IS 'Supplier''s home page on World Wide Web.';

SET DEFINE OFF;

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (1, 'Exotic Liquids', 'Charlotte Cooper', 'Purchasing Manager', '49 Gilbert St.', 'London', 'EC1 4SD', 'UK', '(171) 555-2222');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE)
VALUES (2, 'New Orleans Cajun Delights', 'Shelley Burke', 'Order Administrator', 'P.O. Box 78934', 'New Orleans', 'LA', '70117', 'USA', '(100) 555-4822');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (3, 'Grandma Kelly''s Homestead', 'Regina Murphy', 'Sales Representative', '707 Oxford Rd.', 'Ann Arbor', 'MI', '48104', 'USA', '(313) 555-5735',
        '(313) 555-3349');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (4, 'Tokyo Traders', 'Yoshi Nagase', 'Marketing Manager', '9-8 SekimaiMusashino-shi', 'Tokyo', '100', 'Japan', '(03) 3555-5011');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE)
VALUES (5, 'Cooperativa de Quesos ''Las Cabras''', 'Antonio del Valle Saavedra ', 'Export Administrator', 'Calle del Rosal 4', 'Oviedo', 'Asturias', '33007',
        'Spain', '(98) 598 76 54');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (6, 'Mayumi''s', 'Mayumi Ohno', 'Marketing Representative', '92 SetsukoChuo-ku', 'Osaka', '545', 'Japan', '(06) 431-7877');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (7, 'Pavlova, Ltd.', 'Ian Devling', 'Marketing Manager', '74 Rose St.Moonie Ponds', 'Melbourne', 'Victoria', '3058', 'Australia', '(03) 444-2343',
        '(03) 444-6588');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (8, 'Specialty Biscuits, Ltd.', 'Peter Wilson', 'Sales Representative', '29 King''s Way', 'Manchester', 'M14 GSD', 'UK', '(161) 555-4448');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (9, 'PB Knäckebröd AB', 'Lars Peterson', 'Sales Agent', 'Kaloadagatan 13', 'Göteborg', 'S-345 67', 'Sweden ', '031-987 65 43', '031-987 65 91');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (10, 'Refrescos Americanas LTDA', 'Carlos Diaz', 'Marketing Manager', 'Av. das Americanas 12.890', 'São Paulo', '5442', 'Brazil', '(11) 555 4640');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (11, 'Heli Süßwaren GmbH & Co. KG', 'Petra Winkler', 'Sales Manager', 'Tiergartenstraße 5', 'Berlin', '10785', 'Germany', '(010) 9984510');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (12, 'Plutzer Lebensmittelgroßmärkte AG', 'Martin Bein', 'International Marketing Mgr.', 'Bogenallee 51', 'Frankfurt', '60439', 'Germany',
        '(069) 992755');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (13, 'Nord-Ost-Fisch Handelsgesellschaft mbH', 'Sven Petersen', 'Coordinator Foreign Markets', 'Frahmredder 112a', 'Cuxhaven', '27478', 'Germany',
        '(04721) 8713', '(04721) 8714');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (14, 'Formaggi Fortini s.r.l.', 'Elio Rossi', 'Sales Representative', 'Viale Dante, 75', 'Ravenna', '48100', 'Italy', '(0544) 60323', '(0544) 60603');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (15, 'Norske Meierier', 'Beate Vileid', 'Marketing Manager', 'Hatlevegen 5', 'Sandvika', '1320', 'Norway', '(0)2-953010');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE)
VALUES (16, 'Bigfoot Breweries', 'Cheryl Saylor', 'Regional Account Rep.', '3400 - 8th AvenueSuite 210', 'Bend', 'OR', '97101', 'USA', '(503) 555-9931');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (17, 'Svensk Sjöföda AB', 'Michael Björn', 'Sales Representative', 'Brovallavägen 231', 'Stockholm', 'S-123 45', 'Sweden', '08-123 45 67');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (18, 'Aux joyeux ecclésiastiques', 'Guylène Nodier', 'Sales Manager', '203, Rue des Francs-Bourgeois', 'Paris', '75004', 'France', '(1) 03.83.00.68',
        '(1) 03.83.00.62');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (19, 'New England Seafood Cannery', 'Robb Merchant', 'Wholesale Account Agent', 'Order Processing Dept. 2100 Paul Revere Blvd.', 'Boston', 'MA', '02134',
        'USA', '(617) 555-3267', '(617) 555-3389');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (20, 'Leka Trading', 'Chandra Leka', 'Owner', '471 Serangoon Loop, Suite #402', 'Singapore', '0512', 'Singapore', '555-8787');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (21, 'Lyngbysild', 'Niels Petersen', 'Sales Manager', 'LyngbysildFiskebakken 10', 'Lyngby', '2800', 'Denmark', '43844108', '43844115');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (22, 'Zaanse Snoepfabriek', 'Dirk Luchte', 'Accounting Manager', 'VerkoopRijnweg 22', 'Zaandam', '9999 ZZ', 'Netherlands', '(12345) 1212',
        '(12345) 1210');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (23, 'Karkki Oy', 'Anne Heikkonen', 'Product Manager', 'Valtakatu 12', 'Lappeenranta', '53120', 'Finland', '(953) 10956');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (24, 'G''day, Mate', 'Wendy Mackenzie', 'Sales Representative', '170 Prince Edward ParadeHunter''s Hill', 'Sydney', 'NSW', '2042', 'Australia',
        '(02) 555-5914', '(02) 555-4873');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE)
VALUES (25, 'Ma Maison', 'Jean-Guy Lauzon', 'Marketing Manager', '2960 Rue St. Laurent', 'Montréal', 'Québec', 'H1J 1C3', 'Canada', '(514) 555-9022');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (26, 'Pasta Buttini s.r.l.', 'Giovanni Giudici', 'Order Administrator', 'Via dei Gelsomini, 153', 'Salerno', '84100', 'Italy', '(089) 6547665',
        '(089) 6547667');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE)
VALUES (27, 'Escargots Nouveaux', 'Marie Delamare', 'Sales Manager', '22, rue H. Voiron', 'Montceau', '71300', 'France', '85.57.00.07');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (28, 'Gai pâturage', 'Eliane Noz', 'Sales Representative', 'Bat. B3, rue des Alpes', 'Annecy', '74000', 'France', '38.76.98.06', '38.76.98.58');

INSERT INTO efrpg.SUPPLIERS(SUPPLIER_ID, COMPANY_NAME, CONTACT_NAME, CONTACT_TITLE, ADDRESS, CITY, REGION, POSTAL_CODE, COUNTRY, PHONE, FAX)
VALUES (29, 'Forêts d''érables', 'Chantal Goulet', 'Accounting Manager', '148 rue Chasseur', 'Ste-Hyacinthe', 'Québec', 'J2S 7S8', 'Canada', '(514) 555-2955',
        '(514) 555-2921');

COMMIT;


CREATE TABLE efrpg.SHIPPERS
(
    SHIPPER_ID   NUMBER(9)         NOT NULL,
    COMPANY_NAME VARCHAR2(40 BYTE) NOT NULL,
    PHONE        VARCHAR2(24 BYTE),
    CONSTRAINT PK_SHIPPERS PRIMARY KEY (SHIPPER_ID)
);

CREATE SEQUENCE SEQ_NW_SHIPPERS
    MINVALUE 1
    MAXVALUE 999999999999999999999999999
    START WITH 4
    INCREMENT BY 1
    NOCYCLE
    NOCACHE
    NOORDER;

COMMENT ON COLUMN efrpg.SHIPPERS.SHIPPER_ID IS 'Number automatically assigned to new shipper.';

COMMENT ON COLUMN efrpg.SHIPPERS.COMPANY_NAME IS 'Name of shipping company.';

COMMENT ON COLUMN efrpg.SHIPPERS.PHONE IS 'Phone number includes country code or area code.';

SET DEFINE OFF;

INSERT INTO efrpg.SHIPPERS(SHIPPER_ID, COMPANY_NAME, PHONE)
VALUES (1, 'Speedy Express', '(503) 555-9831');

INSERT INTO efrpg.SHIPPERS(SHIPPER_ID, COMPANY_NAME, PHONE)
VALUES (2, 'United Package', '(503) 555-3199');

INSERT INTO efrpg.SHIPPERS(SHIPPER_ID, COMPANY_NAME, PHONE)
VALUES (3, 'Federal Shipping', '(503) 555-9931');

COMMIT;


CREATE TABLE efrpg.PRODUCTS
(
    PRODUCT_ID        NUMBER(9)                 NOT NULL,
    PRODUCT_NAME      VARCHAR2(40 BYTE)         NOT NULL,
    SUPPLIER_ID       NUMBER(9)                 NOT NULL,
    CATEGORY_ID       NUMBER(9)                 NOT NULL,
    QUANTITY_PER_UNIT VARCHAR2(20 BYTE),
    UNIT_PRICE        NUMBER(10, 2) DEFAULT 0   NOT NULL
        CONSTRAINT CK_PRODUCTS_UNIT_PRICE CHECK (Unit_Price >= 0),
    UNITS_IN_STOCK    NUMBER(9)     DEFAULT 0   NOT NULL
        CONSTRAINT CK_PRODUCTS_UNITS_IN_STOCK CHECK (Units_In_Stock >= 0),
    UNITS_ON_ORDER    NUMBER(9)     DEFAULT 0   NOT NULL
        CONSTRAINT CK_PRODUCTS_UNITS_ON_ORDER CHECK (Units_On_Order >= 0),
    REORDER_LEVEL     NUMBER(9)     DEFAULT 0   NOT NULL
        CONSTRAINT CK_PRODUCTS_REORDER_LEVEL CHECK (Reorder_Level >= 0),
    DISCONTINUED      CHAR(1 BYTE)  DEFAULT 'N' NOT NULL
        CONSTRAINT CK_PRODUCTS_DISCONTINUED CHECK (Discontinued IN ('Y', 'N')),
    CONSTRAINT PK_PRODUCTS PRIMARY KEY (PRODUCT_ID),
    CONSTRAINT FK_CATEGORY_ID FOREIGN KEY (CATEGORY_ID) REFERENCES efrpg.CATEGORIES (CATEGORY_ID),
    CONSTRAINT FK_SUPPLIER_ID FOREIGN KEY (SUPPLIER_ID) REFERENCES SUPPLIERS (SUPPLIER_ID)
);

CREATE INDEX IDX_PRODUCTS_CATEGORY_ID ON PRODUCTS (CATEGORY_ID);

CREATE INDEX IDX_PRODUCTS_SUPPLIER_ID ON PRODUCTS (SUPPLIER_ID);

CREATE SEQUENCE SEQ_NW_PRODUCTS
    MINVALUE 1
    MAXVALUE 999999999999999999999999999
    START WITH 78
    INCREMENT BY 1
    NOCYCLE
    NOCACHE
    NOORDER;

COMMENT ON COLUMN efrpg.PRODUCTS.PRODUCT_ID IS 'Number automatically assigned to new product.';

COMMENT ON COLUMN efrpg.PRODUCTS.SUPPLIER_ID IS 'Same entry as in Suppliers table.';

COMMENT ON COLUMN efrpg.PRODUCTS.CATEGORY_ID IS 'Same entry as in efrpg.CATEGORIES table.';

COMMENT ON COLUMN efrpg.PRODUCTS.QUANTITY_PER_UNIT IS '(e.g., 24-count case, 1-liter bottle).';

COMMENT ON COLUMN efrpg.PRODUCTS.REORDER_LEVEL IS 'Minimum units to maintain in stock.';

COMMENT ON COLUMN efrpg.PRODUCTS.DISCONTINUED IS 'Yes means item is no longer available.';

SET DEFINE OFF;
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (1, 'Chai', 1, 1, '10 boxes x 20 bags',
        18, 39, 0, 10, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (2, 'Chang', 1, 1, '24 - 12 oz bottles',
        19, 17, 40, 25, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (3, 'Aniseed Syrup', 1, 2, '12 - 550 ml bottles',
        10, 13, 70, 25, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (4, 'Chef Anton''s Cajun Seasoning', 2, 2, '48 - 6 oz jars',
        22, 53, 0, 0, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (5, 'Chef Anton''s Gumbo Mix', 2, 2, '36 boxes',
        21.35, 0, 0, 0, 'Y');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (6, 'Grandma''s Boysenberry Spread', 3, 2, '12 - 8 oz jars',
        25, 120, 0, 25, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (7, 'Uncle Bob''s Organic Dried Pears', 3, 7, '12 - 1 lb pkgs.',
        30, 15, 0, 10, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (8, 'Northwoods Cranberry Sauce', 3, 2, '12 - 12 oz jars',
        40, 6, 0, 0, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (9, 'Mishi Kobe Niku', 4, 6, '18 - 500 g pkgs.',
        97, 29, 0, 0, 'Y');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (10, 'Ikura', 4, 8, '12 - 200 ml jars',
        31, 31, 0, 0, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (11, 'Queso Cabrales', 5, 4, '1 kg pkg.',
        21, 22, 30, 30, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (12, 'Queso Manchego La Pastora', 5, 4, '10 - 500 g pkgs.',
        38, 86, 0, 0, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (13, 'Konbu', 6, 8, '2 kg box',
        6, 24, 0, 5, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (14, 'Tofu', 6, 7, '40 - 100 g pkgs.',
        23.25, 35, 0, 0, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (15, 'Genen Shouyu', 6, 2, '24 - 250 ml bottles',
        15.5, 39, 0, 5, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (16, 'Pavlova', 7, 3, '32 - 500 g boxes',
        17.45, 29, 0, 10, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (17, 'Alice Mutton', 7, 6, '20 - 1 kg tins',
        39, 0, 0, 0, 'Y');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (18, 'Carnarvon Tigers', 7, 8, '16 kg pkg.',
        62.5, 42, 0, 0, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (19, 'Teatime Chocolate Biscuits', 8, 3, '10 boxes x 12 pieces',
        9.2, 25, 0, 5, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (20, 'Sir Rodney''s Marmalade', 8, 3, '30 gift boxes',
        81, 40, 0, 0, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (21, 'Sir Rodney''s Scones', 8, 3, '24 pkgs. x 4 pieces',
        10, 3, 40, 5, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (22, 'Gustaf''s Knäckebröd', 9, 5, '24 - 500 g pkgs.',
        21, 104, 0, 25, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (23, 'Tunnbröd', 9, 5, '12 - 250 g pkgs.',
        9, 61, 0, 25, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (24, 'Guaraná Fantástica', 10, 1, '12 - 355 ml cans',
        4.5, 20, 0, 0, 'Y');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (25, 'NuNuCa Nuß-Nougat-Creme', 11, 3, '20 - 450 g glasses',
        14, 76, 0, 30, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (26, 'Gumbär Gummibärchen', 11, 3, '100 - 250 g bags',
        31.23, 15, 0, 0, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (27, 'Schoggi Schokolade', 11, 3, '100 - 100 g pieces',
        43.9, 49, 0, 30, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (28, 'Rössle Sauerkraut', 12, 7, '25 - 825 g cans',
        45.6, 26, 0, 0, 'Y');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (29, 'Thüringer Rostbratwurst', 12, 6, '50 bags x 30 sausgs.',
        123.79, 0, 0, 0, 'Y');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (30, 'Nord-Ost Matjeshering', 13, 8, '10 - 200 g glasses',
        25.89, 10, 0, 15, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (31, 'Gorgonzola Telino', 14, 4, '12 - 100 g pkgs',
        12.5, 0, 70, 20, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (32, 'Mascarpone Fabioli', 14, 4, '24 - 200 g pkgs.',
        32, 9, 40, 25, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (33, 'Geitost', 15, 4, '500 g',
        2.5, 112, 0, 20, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (34, 'Sasquatch Ale', 16, 1, '24 - 12 oz bottles',
        14, 111, 0, 15, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (35, 'Steeleye Stout', 16, 1, '24 - 12 oz bottles',
        18, 20, 0, 15, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (36, 'Inlagd Sill', 17, 8, '24 - 250 g  jars',
        19, 112, 0, 20, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (37, 'Gravad lax', 17, 8, '12 - 500 g pkgs.',
        26, 11, 50, 25, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (38, 'Côte de Blaye', 18, 1, '12 - 75 cl bottles',
        263.5, 17, 0, 15, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (39, 'Chartreuse verte', 18, 1, '750 cc per bottle',
        18, 69, 0, 5, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (40, 'Boston Crab Meat', 19, 8, '24 - 4 oz tins',
        18.4, 123, 0, 30, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (41, 'Jack''s New England Clam Chowder', 19, 8, '12 - 12 oz cans',
        9.65, 85, 0, 10, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (42, 'Singaporean Hokkien Fried Mee', 20, 5, '32 - 1 kg pkgs.',
        14, 26, 0, 0, 'Y');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (43, 'Ipoh Coffee', 20, 1, '16 - 500 g tins',
        46, 17, 10, 25, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (44, 'Gula Malacca', 20, 2, '20 - 2 kg bags',
        19.45, 27, 0, 15, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (45, 'Røgede sild', 21, 8, '1k pkg.',
        9.5, 5, 70, 15, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (46, 'Spegesild', 21, 8, '4 - 450 g glasses',
        12, 95, 0, 0, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (47, 'Zaanse koeken', 22, 3, '10 - 4 oz boxes',
        9.5, 36, 0, 0, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (48, 'Chocolade', 22, 3, '10 pkgs.',
        12.75, 15, 70, 25, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (49, 'Maxilaku', 23, 3, '24 - 50 g pkgs.',
        20, 10, 60, 15, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (50, 'Valkoinen suklaa', 23, 3, '12 - 100 g bars',
        16.25, 65, 0, 30, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (51, 'Manjimup Dried Apples', 24, 7, '50 - 300 g pkgs.',
        53, 20, 0, 10, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (52, 'Filo Mix', 24, 5, '16 - 2 kg boxes',
        7, 38, 0, 25, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (53, 'Perth Pasties', 24, 6, '48 pieces',
        32.8, 0, 0, 0, 'Y');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (54, 'Tourtière', 25, 6, '16 pies',
        7.45, 21, 0, 10, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (55, 'Pâté chinois', 25, 6, '24 boxes x 2 pies',
        24, 115, 0, 20, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (56, 'Gnocchi di nonna Alice', 26, 5, '24 - 250 g pkgs.',
        38, 21, 10, 30, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (57, 'Ravioli Angelo', 26, 5, '24 - 250 g pkgs.',
        19.5, 36, 0, 20, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (58, 'Escargots de Bourgogne', 27, 8, '24 pieces',
        13.25, 62, 0, 20, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (59, 'Raclette Courdavault', 28, 4, '5 kg pkg.',
        55, 79, 0, 0, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (60, 'Camembert Pierrot', 28, 4, '15 - 300 g rounds',
        34, 19, 0, 0, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (61, 'Sirop d''érable', 29, 2, '24 - 500 ml bottles',
        28.5, 113, 0, 25, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (62, 'Tarte au sucre', 29, 3, '48 pies',
        49.3, 17, 0, 0, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (63, 'Vegie-spread', 7, 2, '15 - 625 g jars',
        43.9, 24, 0, 5, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (64, 'Wimmers gute Semmelknödel', 12, 5, '20 bags x 4 pieces',
        33.25, 22, 80, 30, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (65, 'Louisiana Fiery Hot Pepper Sauce', 2, 2, '32 - 8 oz bottles',
        21.05, 76, 0, 0, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (66, 'Louisiana Hot Spiced Okra', 2, 2, '24 - 8 oz jars',
        17, 4, 100, 20, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (67, 'Laughing Lumberjack Lager', 16, 1, '24 - 12 oz bottles',
        14, 52, 0, 10, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (68, 'Scottish Longbreads', 8, 3, '10 boxes x 8 pieces',
        12.5, 6, 10, 15, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (69, 'Gudbrandsdalsost', 15, 4, '10 kg pkg.',
        36, 26, 0, 15, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (70, 'Outback Lager', 7, 1, '24 - 355 ml bottles',
        15, 15, 10, 30, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (71, 'Fløtemysost', 15, 4, '10 - 500 g pkgs.',
        21.5, 26, 0, 0, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (72, 'Mozzarella di Giovanni', 14, 4, '24 - 200 g pkgs.',
        34.8, 14, 0, 0, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (73, 'Röd Kaviar', 17, 8, '24 - 150 g jars',
        15, 101, 0, 5, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (74, 'Longlife Tofu', 4, 7, '5 kg pkg.',
        10, 4, 20, 5, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (75, 'Rhönbräu Klosterbier', 12, 1, '24 - 0.5 l bottles',
        7.75, 125, 0, 25, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (76, 'Lakkalikööri', 23, 1, '500 ml',
        18, 57, 0, 20, 'N');
INSERT INTO efrpg.PRODUCTS
(PRODUCT_ID, PRODUCT_NAME, SUPPLIER_ID, CATEGORY_ID, QUANTITY_PER_UNIT, UNIT_PRICE, UNITS_IN_STOCK, UNITS_ON_ORDER, REORDER_LEVEL, DISCONTINUED)
VALUES (77, 'Original Frankfurter grüne Soße', 12, 2, '12 boxes',
        13, 32, 0, 15, 'N');

COMMIT;


CREATE TABLE efrpg.ORDERS
(
    ORDER_ID         NUMBER(9) NOT NULL,
    CUSTOMER_ID      NUMBER(9) NOT NULL,
    EMPLOYEE_ID      NUMBER(9) NOT NULL,
    ORDER_DATE       DATE      NOT NULL,
    REQUIRED_DATE    DATE,
    SHIPPED_DATE     DATE,
    SHIP_VIA         NUMBER(9),
    FREIGHT          NUMBER(10, 2) DEFAULT 0,
    SHIP_NAME        VARCHAR2(40 BYTE),
    SHIP_ADDRESS     VARCHAR2(60 BYTE),
    SHIP_CITY        VARCHAR2(15 BYTE),
    SHIP_REGION      VARCHAR2(15 BYTE),
    SHIP_POSTAL_CODE VARCHAR2(10 BYTE),
    SHIP_COUNTRY     VARCHAR2(15 BYTE),
    CONSTRAINT PK_ORDERS PRIMARY KEY (ORDER_ID),
    CONSTRAINT FK_CUSTOMER_ID FOREIGN KEY (CUSTOMER_ID) REFERENCES CUSTOMERS (CUSTOMER_ID),
    CONSTRAINT FK_EMPLOYEE_ID FOREIGN KEY (EMPLOYEE_ID) REFERENCES EMPLOYEES (EMPLOYEE_ID),
    CONSTRAINT FK_SHIPPER_ID FOREIGN KEY (SHIP_VIA) REFERENCES SHIPPERS (SHIPPER_ID)
);

CREATE INDEX IDX_ORDERS_CUSTOMER_ID ON ORDERS (CUSTOMER_ID);

CREATE INDEX IDX_ORDERS_EMPLOYEE_ID ON ORDERS (EMPLOYEE_ID);

CREATE INDEX IDX_ORDERS_SHIPPER_ID ON ORDERS (SHIP_VIA);

CREATE INDEX IDX_ORDERS_ORDER_DATE ON ORDERS (ORDER_DATE);

CREATE INDEX IDX_ORDERS_SHIPPED_DATE ON ORDERS (SHIPPED_DATE);

CREATE INDEX IDX_ORDERS_SHIP_POSTAL_CODE ON ORDERS (SHIP_POSTAL_CODE);

CREATE SEQUENCE SEQ_NW_ORDERS
    MINVALUE 1
    MAXVALUE 999999999999999999999999999
    START WITH 11018
    INCREMENT BY 1
    NOCYCLE
    NOCACHE
    NOORDER;

COMMENT ON COLUMN efrpg.ORDERS.ORDER_ID IS 'Unique order number.';

COMMENT ON COLUMN efrpg.ORDERS.CUSTOMER_ID IS 'Same entry as in Customers table.';

COMMENT ON COLUMN efrpg.ORDERS.EMPLOYEE_ID IS 'Same entry as in Employees table.';

COMMENT ON COLUMN efrpg.ORDERS.SHIP_VIA IS 'Same as Shipper ID in Shippers table.';

COMMENT ON COLUMN efrpg.ORDERS.SHIP_NAME IS 'Name of person or company to receive the shipment.';

COMMENT ON COLUMN efrpg.ORDERS.SHIP_ADDRESS IS 'Street address only -- no post-office box allowed.';

COMMENT ON COLUMN efrpg.ORDERS.SHIP_REGION IS 'State or province.';

SET DEFINE OFF;
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10440, 71, 4, TO_DATE('02/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 86.53, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10441, 55, 3, TO_DATE('02/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 73.02, 'Old World Delicatessen', '2743 Bering St.',
        'Anchorage', 'AK', '99508', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10442, 20, 3, TO_DATE('02/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 47.94, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10443, 66, 8, TO_DATE('02/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 13.95, 'Reggiani Caseifici', 'Strada Provinciale 124',
        'Reggio Emilia', '42100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10444, 5, 3, TO_DATE('02/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 3.5, 'Berglunds snabbköp', 'Berguvsvägen  8',
        'Luleå', 'S-958 22', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10445, 5, 3, TO_DATE('02/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 9.3, 'Berglunds snabbköp', 'Berguvsvägen  8',
        'Luleå', 'S-958 22', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10446, 79, 6, TO_DATE('02/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 14.68, 'Toms Spezialitäten', 'Luisenstr. 48',
        'Münster', '44087', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10447, 67, 4, TO_DATE('02/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 68.66, 'Ricardo Adocicados', 'Av. Copacabana, 267',
        'Rio de Janeiro', 'RJ', '02389-890', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10448, 64, 4, TO_DATE('02/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 38.82, 'Rancho grande', 'Av. del Libertador 900',
        'Buenos Aires', '1010', 'Argentina');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10449, 7, 3, TO_DATE('02/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 53.3, 'Blondel père et fils', '24, place Kléber',
        'Strasbourg', '67000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10450, 84, 8, TO_DATE('02/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 7.23, 'Victuailles en stock', '2, rue du Commerce',
        'Lyon', '69004', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10451, 63, 4, TO_DATE('02/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 189.09, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10452, 71, 8, TO_DATE('02/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 140.26, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10453, 4, 1, TO_DATE('02/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 25.36, 'Around the Horn', 'Brook Farm
Stratford St. Mary',
        'Colchester', 'Essex', 'CO7 6JX', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10454, 41, 4, TO_DATE('02/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 2.74, 'La maison d''Asie', '1 rue Alsace-Lorraine',
        'Toulouse', '31000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10455, 87, 8, TO_DATE('02/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 180.45, 'Wartian Herkku', 'Torikatu 38',
        'Oulu', '90110', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10456, 39, 8, TO_DATE('02/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 8.12, 'Königlich Essen', 'Maubelstr. 90',
        'Brandenburg', '14776', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10457, 39, 2, TO_DATE('02/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 11.57, 'Königlich Essen', 'Maubelstr. 90',
        'Brandenburg', '14776', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10458, 76, 7, TO_DATE('02/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 147.06, 'Suprêmes délices', 'Boulevard Tirou, 255',
        'Charleroi', 'B-6000', 'Belgium');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10459, 84, 4, TO_DATE('02/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 25.09, 'Victuailles en stock', '2, rue du Commerce',
        'Lyon', '69004', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10460, 24, 8, TO_DATE('02/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 16.27, 'Folk och fä HB', 'Åkergatan 24',
        'Bräcke', 'S-844 67', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10461, 46, 1, TO_DATE('02/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 148.61, 'LILA-Supermercado', 'Carrera 52 con Ave. Bolívar #65-98 Llano Largo',
        'Barquisimeto', 'Lara', '3508', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10462, 16, 2, TO_DATE('03/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 6.17, 'Consolidated Holdings', 'Berkeley Gardens
12  Brewery ',
        'London', 'WX1 6LT', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10463, 76, 5, TO_DATE('03/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 14.78, 'Suprêmes délices', 'Boulevard Tirou, 255',
        'Charleroi', 'B-6000', 'Belgium');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10464, 28, 4, TO_DATE('03/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 89, 'Furia Bacalhau e Frutos do Mar', 'Jardim das rosas n. 32',
        'Lisboa', '1675', 'Portugal');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10465, 83, 1, TO_DATE('03/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 145.04, 'Vaffeljernet', 'Smagsløget 45',
        'Århus', '8200', 'Denmark');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10466, 15, 4, TO_DATE('03/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 11.93, 'Comércio Mineiro', 'Av. dos Lusíadas, 23',
        'São Paulo', 'SP', '05432-043', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10467, 49, 8, TO_DATE('03/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 4.93, 'Magazzini Alimentari Riuniti', 'Via Ludovico il Moro 22',
        'Bergamo', '24100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10468, 39, 3, TO_DATE('03/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 44.12, 'Königlich Essen', 'Maubelstr. 90',
        'Brandenburg', '14776', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10469, 89, 1, TO_DATE('03/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 60.18, 'White Clover Markets', '1029 - 12th Ave. S.',
        'Seattle', 'WA', '98124', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10470, 9, 4, TO_DATE('03/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 64.56, 'Bon app''', '12, rue des Bouchers',
        'Marseille', '13008', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10471, 11, 2, TO_DATE('03/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 45.59, 'B''s Beverages', 'Fauntleroy Circus',
        'London', 'EC2 5NT', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10472, 72, 8, TO_DATE('03/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 4.2, 'Seven Seas Imports', '90 Wadhurst Rd.',
        'London', 'OX15 4NB', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10473, 38, 1, TO_DATE('03/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 16.37, 'Island Trading', 'Garden House
Crowther Way',
        'Cowes', 'Isle of Wight', 'PO31 7PJ', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10474, 58, 5, TO_DATE('03/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 83.49, 'Pericles Comidas clásicas', 'Calle Dr. Jorge Cash 321',
        'México D.F.', '05033', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10475, 76, 9, TO_DATE('03/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 68.52, 'Suprêmes délices', 'Boulevard Tirou, 255',
        'Charleroi', 'B-6000', 'Belgium');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10476, 35, 8, TO_DATE('03/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 4.41, 'HILARIÓN-Abastos', 'Carrera 22 con Ave. Carlos Soublette #8-35',
        'San Cristóbal', 'Táchira', '5022', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10477, 60, 5, TO_DATE('03/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 13.02, 'Princesa Isabel Vinhos', 'Estrada da saúde n. 58',
        'Lisboa', '1756', 'Portugal');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10478, 84, 2, TO_DATE('03/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 4.81, 'Victuailles en stock', '2, rue du Commerce',
        'Lyon', '69004', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10479, 65, 3, TO_DATE('03/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 708.95, 'Rattlesnake Canyon Grocery', '2817 Milton Dr.',
        'Albuquerque', 'NM', '87110', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10480, 23, 6, TO_DATE('03/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 1.35, 'Folies gourmandes', '184, chaussée de Tournai',
        'Lille', '59000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10481, 67, 8, TO_DATE('03/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 64.33, 'Ricardo Adocicados', 'Av. Copacabana, 267',
        'Rio de Janeiro', 'RJ', '02389-890', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10482, 43, 1, TO_DATE('03/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 7.48, 'Lazy K Kountry Store', '12 Orchestra Terrace',
        'Walla Walla', 'WA', '99362', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10483, 89, 7, TO_DATE('03/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 15.28, 'White Clover Markets', '1029 - 12th Ave. S.',
        'Seattle', 'WA', '98124', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10484, 11, 3, TO_DATE('03/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 6.88, 'B''s Beverages', 'Fauntleroy Circus',
        'London', 'EC2 5NT', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10485, 47, 4, TO_DATE('03/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 64.45, 'LINO-Delicateses', 'Ave. 5 de Mayo Porlamar',
        'I. de Margarita', 'Nueva Esparta', '4980', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10486, 35, 1, TO_DATE('03/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 30.53, 'HILARIÓN-Abastos', 'Carrera 22 con Ave. Carlos Soublette #8-35',
        'San Cristóbal', 'Táchira', '5022', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10487, 62, 2, TO_DATE('03/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 71.07, 'Queen Cozinha', 'Alameda dos Canàrios, 891',
        'São Paulo', 'SP', '05487-020', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10488, 25, 8, TO_DATE('03/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 4.93, 'Frankenversand', 'Berliner Platz 43',
        'München', '80805', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10489, 59, 6, TO_DATE('03/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 5.29, 'Piccolo und mehr', 'Geislweg 14',
        'Salzburg', '5020', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10490, 35, 7, TO_DATE('03/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 210.19, 'HILARIÓN-Abastos', 'Carrera 22 con Ave. Carlos Soublette #8-35',
        'San Cristóbal', 'Táchira', '5022', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10491, 28, 8, TO_DATE('03/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 16.96, 'Furia Bacalhau e Frutos do Mar', 'Jardim das rosas n. 32',
        'Lisboa', '1675', 'Portugal');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10492, 10, 3, TO_DATE('04/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 62.89, 'Bottom-Dollar Markets', '23 Tsawassen Blvd.',
        'Tsawassen', 'BC', 'T2F 8M4', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10493, 41, 4, TO_DATE('04/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 10.64, 'La maison d''Asie', '1 rue Alsace-Lorraine',
        'Toulouse', '31000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10494, 15, 4, TO_DATE('04/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 65.99, 'Comércio Mineiro', 'Av. dos Lusíadas, 23',
        'São Paulo', 'SP', '05432-043', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10495, 42, 3, TO_DATE('04/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 4.65, 'Laughing Bacchus Wine Cellars', '2319 Elm St.',
        'Vancouver', 'BC', 'V3F 2K1', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10496, 81, 7, TO_DATE('04/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 46.77, 'Tradição Hipermercados', 'Av. Inês de Castro, 414',
        'São Paulo', 'SP', '05634-030', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10497, 44, 7, TO_DATE('04/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 36.21, 'Lehmanns Marktstand', 'Magazinweg 7',
        'Frankfurt a.M. ', '60528', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10498, 35, 8, TO_DATE('04/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 29.75, 'HILARIÓN-Abastos', 'Carrera 22 con Ave. Carlos Soublette #8-35',
        'San Cristóbal', 'Táchira', '5022', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10499, 46, 4, TO_DATE('04/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 102.02, 'LILA-Supermercado', 'Carrera 52 con Ave. Bolívar #65-98 Llano Largo',
        'Barquisimeto', 'Lara', '3508', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10500, 41, 6, TO_DATE('04/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 42.68, 'La maison d''Asie', '1 rue Alsace-Lorraine',
        'Toulouse', '31000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10501, 6, 9, TO_DATE('04/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 8.85, 'Blauer See Delikatessen', 'Forsterstr. 57',
        'Mannheim', '68306', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10502, 58, 2, TO_DATE('04/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 69.32, 'Pericles Comidas clásicas', 'Calle Dr. Jorge Cash 321',
        'México D.F.', '05033', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_COUNTRY)
VALUES (10503, 37, 6, TO_DATE('04/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 16.74, 'Hungry Owl All-Night Grocers', '8 Johnstown Road',
        'Cork', 'Co. Cork', 'Ireland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10504, 89, 4, TO_DATE('04/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 59.13, 'White Clover Markets', '1029 - 12th Ave. S.',
        'Seattle', 'WA', '98124', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10505, 51, 3, TO_DATE('04/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 7.13, 'Mère Paillarde', '43 rue St. Laurent',
        'Montréal', 'Québec', 'H1J 1C3', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10506, 39, 9, TO_DATE('04/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 21.19, 'Königlich Essen', 'Maubelstr. 90',
        'Brandenburg', '14776', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10507, 3, 7, TO_DATE('04/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 47.45, 'Antonio Moreno Taquería', 'Mataderos  2312',
        'México D.F.', '05023', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10508, 56, 1, TO_DATE('04/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 4.99, 'Ottilies Käseladen', 'Mehrheimerstr. 369',
        'Köln', '50739', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10509, 6, 4, TO_DATE('04/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 0.15, 'Blauer See Delikatessen', 'Forsterstr. 57',
        'Mannheim', '68306', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10510, 71, 6, TO_DATE('04/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 367.63, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10511, 9, 4, TO_DATE('04/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 350.64, 'Bon app''', '12, rue des Bouchers',
        'Marseille', '13008', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10512, 21, 7, TO_DATE('04/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 3.53, 'Familia Arquibaldo', 'Rua Orós, 92',
        'São Paulo', 'SP', '05442-030', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10513, 86, 7, TO_DATE('04/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 105.65, 'Die Wandernde Kuh', 'Adenauerallee 900',
        'Stuttgart', '70563', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10514, 20, 3, TO_DATE('04/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 789.95, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10515, 63, 2, TO_DATE('04/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 204.47, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_COUNTRY)
VALUES (10516, 37, 2, TO_DATE('04/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 62.78, 'Hungry Owl All-Night Grocers', '8 Johnstown Road',
        'Cork', 'Co. Cork', 'Ireland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10517, 53, 3, TO_DATE('04/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 32.07, 'North/South', 'South House
300 Queensbridge',
        'London', 'SW7 1RZ', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10518, 80, 4, TO_DATE('04/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 218.15, 'Tortuga Restaurante', 'Avda. Azteca 123',
        'México D.F.', '05033', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10519, 14, 6, TO_DATE('04/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 91.76, 'Chop-suey Chinese', 'Hauptstr. 31',
        'Bern', '3012', 'Switzerland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10520, 70, 7, TO_DATE('04/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 13.37, 'Santé Gourmet', 'Erling Skakkes gate 78',
        'Stavern', '4110', 'Norway');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10521, 12, 8, TO_DATE('04/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 17.22, 'Cactus Comidas para llevar', 'Cerrito 333',
        'Buenos Aires', '1010', 'Argentina');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10522, 44, 4, TO_DATE('04/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 45.33, 'Lehmanns Marktstand', 'Magazinweg 7',
        'Frankfurt a.M. ', '60528', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10523, 72, 7, TO_DATE('05/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 77.63, 'Seven Seas Imports', '90 Wadhurst Rd.',
        'London', 'OX15 4NB', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10524, 5, 1, TO_DATE('05/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 244.79, 'Berglunds snabbköp', 'Berguvsvägen  8',
        'Luleå', 'S-958 22', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10525, 9, 1, TO_DATE('05/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 11.06, 'Bon app''', '12, rue des Bouchers',
        'Marseille', '13008', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10526, 87, 4, TO_DATE('05/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 58.59, 'Wartian Herkku', 'Torikatu 38',
        'Oulu', '90110', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10527, 63, 7, TO_DATE('05/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 41.9, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10528, 32, 6, TO_DATE('05/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 3.35, 'Great Lakes Food Market', '2732 Baker Blvd.',
        'Eugene', 'OR', '97403', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10529, 50, 5, TO_DATE('05/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 66.69, 'Maison Dewey', 'Rue Joseph-Bens 532',
        'Bruxelles', 'B-1180', 'Belgium');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10530, 59, 3, TO_DATE('05/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 339.22, 'Piccolo und mehr', 'Geislweg 14',
        'Salzburg', '5020', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10531, 54, 7, TO_DATE('05/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 8.12, 'Océano Atlántico Ltda.', 'Ing. Gustavo Moncada 8585
Piso 20-A',
        'Buenos Aires', '1010', 'Argentina');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10532, 19, 7, TO_DATE('05/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 74.46, 'Eastern Connection', '35 King George',
        'London', 'WX3 6FW', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10533, 24, 8, TO_DATE('05/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 188.04, 'Folk och fä HB', 'Åkergatan 24',
        'Bräcke', 'S-844 67', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10534, 44, 8, TO_DATE('05/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 27.94, 'Lehmanns Marktstand', 'Magazinweg 7',
        'Frankfurt a.M. ', '60528', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10535, 3, 4, TO_DATE('05/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 15.64, 'Antonio Moreno Taquería', 'Mataderos  2312',
        'México D.F.', '05023', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10536, 44, 3, TO_DATE('05/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 58.88, 'Lehmanns Marktstand', 'Magazinweg 7',
        'Frankfurt a.M. ', '60528', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10537, 68, 1, TO_DATE('05/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 78.85, 'Richter Supermarkt', 'Starenweg 5',
        'Genève', '1204', 'Switzerland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10538, 11, 9, TO_DATE('05/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 4.87, 'B''s Beverages', 'Fauntleroy Circus',
        'London', 'EC2 5NT', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10539, 11, 6, TO_DATE('05/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 12.36, 'B''s Beverages', 'Fauntleroy Circus',
        'London', 'EC2 5NT', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10540, 63, 3, TO_DATE('05/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 1007.64, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10541, 34, 2, TO_DATE('05/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 68.65, 'Hanari Carnes', 'Rua do Paço, 67',
        'Rio de Janeiro', 'RJ', '05454-876', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10542, 39, 1, TO_DATE('05/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 10.95, 'Königlich Essen', 'Maubelstr. 90',
        'Brandenburg', '14776', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10543, 46, 8, TO_DATE('05/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 48.17, 'LILA-Supermercado', 'Carrera 52 con Ave. Bolívar #65-98 Llano Largo',
        'Barquisimeto', 'Lara', '3508', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10544, 48, 4, TO_DATE('05/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 24.91, 'Lonesome Pine Restaurant', '89 Chiaroscuro Rd.',
        'Portland', 'OR', '97219', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10545, 43, 8, TO_DATE('05/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 11.92, 'Lazy K Kountry Store', '12 Orchestra Terrace',
        'Walla Walla', 'WA', '99362', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10546, 84, 1, TO_DATE('05/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 194.72, 'Victuailles en stock', '2, rue du Commerce',
        'Lyon', '69004', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10547, 72, 3, TO_DATE('05/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 178.43, 'Seven Seas Imports', '90 Wadhurst Rd.',
        'London', 'OX15 4NB', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10548, 79, 3, TO_DATE('05/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 1.43, 'Toms Spezialitäten', 'Luisenstr. 48',
        'Münster', '44087', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10549, 63, 5, TO_DATE('05/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 171.24, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10550, 30, 7, TO_DATE('05/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 4.32, 'Godos Cocina Típica', 'C/ Romero, 33',
        'Sevilla', '41101', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10551, 28, 4, TO_DATE('05/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 72.95, 'Furia Bacalhau e Frutos do Mar', 'Jardim das rosas n. 32',
        'Lisboa', '1675', 'Portugal');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10552, 35, 2, TO_DATE('05/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 83.22, 'HILARIÓN-Abastos', 'Carrera 22 con Ave. Carlos Soublette #8-35',
        'San Cristóbal', 'Táchira', '5022', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10553, 87, 2, TO_DATE('05/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 149.49, 'Wartian Herkku', 'Torikatu 38',
        'Oulu', '90110', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10554, 56, 4, TO_DATE('05/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 120.97, 'Ottilies Käseladen', 'Mehrheimerstr. 369',
        'Köln', '50739', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10555, 71, 6, TO_DATE('06/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 252.49, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10556, 73, 2, TO_DATE('06/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 9.8, 'Simons bistro', 'Vinbæltet 34',
        'København', '1734', 'Denmark');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10557, 44, 9, TO_DATE('06/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 96.72, 'Lehmanns Marktstand', 'Magazinweg 7',
        'Frankfurt a.M. ', '60528', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10558, 4, 1, TO_DATE('06/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 72.97, 'Around the Horn', 'Brook Farm
Stratford St. Mary',
        'Colchester', 'Essex', 'CO7 6JX', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10559, 7, 6, TO_DATE('06/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 8.05, 'Blondel père et fils', '24, place Kléber',
        'Strasbourg', '67000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10560, 25, 8, TO_DATE('06/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 36.65, 'Frankenversand', 'Berliner Platz 43',
        'München', '80805', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10248, 85, 5, TO_DATE('07/04/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/01/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 32.38, 'Vins et alcools Chevalier', '59 rue de l''Abbaye',
        'Reims', '51100', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10249, 79, 6, TO_DATE('07/05/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/10/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 11.61, 'Toms Spezialitäten', 'Luisenstr. 48',
        'Münster', '44087', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10250, 34, 4, TO_DATE('07/08/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/05/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/12/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 65.83, 'Hanari Carnes', 'Rua do Paço, 67',
        'Rio de Janeiro', 'RJ', '05454-876', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10251, 84, 3, TO_DATE('07/08/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/05/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/15/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 41.34, 'Victuailles en stock', '2, rue du Commerce',
        'Lyon', '69004', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10252, 76, 4, TO_DATE('07/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/06/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/11/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 51.3, 'Suprêmes délices', 'Boulevard Tirou, 255',
        'Charleroi', 'B-6000', 'Belgium');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10253, 34, 3, TO_DATE('07/10/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/24/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 58.17, 'Hanari Carnes', 'Rua do Paço, 67',
        'Rio de Janeiro', 'RJ', '05454-876', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10254, 14, 5, TO_DATE('07/11/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/08/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/23/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 22.98, 'Chop-suey Chinese', 'Hauptstr. 31',
        'Bern', '3012', 'Switzerland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10255, 68, 9, TO_DATE('07/12/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/15/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 148.33, 'Richter Supermarkt', 'Starenweg 5',
        'Genève', '1204', 'Switzerland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10256, 88, 3, TO_DATE('07/15/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/12/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/17/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 13.97, 'Wellington Importadora', 'Rua do Mercado, 12',
        'Resende', 'SP', '08737-363', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10257, 35, 4, TO_DATE('07/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/13/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/22/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 81.91, 'HILARIÓN-Abastos', 'Carrera 22 con Ave. Carlos Soublette #8-35',
        'San Cristóbal', 'Táchira', '5022', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10258, 20, 1, TO_DATE('07/17/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/14/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/23/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 140.51, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10259, 13, 4, TO_DATE('07/18/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/15/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/25/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 3.25, 'Centro comercial Moctezuma', 'Sierras de Granada 9993',
        'México D.F.', '05022', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10260, 56, 4, TO_DATE('07/19/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/29/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 55.09, 'Ottilies Käseladen', 'Mehrheimerstr. 369',
        'Köln', '50739', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10261, 61, 4, TO_DATE('07/19/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/30/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 3.05, 'Que Delícia', 'Rua da Panificadora, 12',
        'Rio de Janeiro', 'RJ', '02389-673', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10262, 65, 8, TO_DATE('07/22/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/19/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/25/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 48.29, 'Rattlesnake Canyon Grocery', '2817 Milton Dr.',
        'Albuquerque', 'NM', '87110', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10263, 20, 9, TO_DATE('07/23/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/20/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/31/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 146.06, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10264, 24, 6, TO_DATE('07/24/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/21/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/23/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 3.67, 'Folk och fä HB', 'Åkergatan 24',
        'Bräcke', 'S-844 67', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10265, 7, 2, TO_DATE('07/25/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/22/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/12/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 55.28, 'Blondel père et fils', '24, place Kléber',
        'Strasbourg', '67000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10266, 87, 3, TO_DATE('07/26/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/06/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/31/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 25.73, 'Wartian Herkku', 'Torikatu 38',
        'Oulu', '90110', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10267, 25, 4, TO_DATE('07/29/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/26/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/06/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 208.58, 'Frankenversand', 'Berliner Platz 43',
        'München', '80805', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10268, 33, 8, TO_DATE('07/30/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/27/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/02/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 66.29, 'GROSELLA-Restaurante', '5ª Ave. Los Palos Grandes',
        'Caracas', 'DF', '1081', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10269, 89, 5, TO_DATE('07/31/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/14/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 4.56, 'White Clover Markets', '1029 - 12th Ave. S.',
        'Seattle', 'WA', '98124', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10270, 87, 1, TO_DATE('08/01/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/29/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/02/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 136.54, 'Wartian Herkku', 'Torikatu 38',
        'Oulu', '90110', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10271, 75, 6, TO_DATE('08/01/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/29/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/30/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 4.54, 'Split Rail Beer & Ale', 'P.O. Box 555',
        'Lander', 'WY', '82520', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10272, 65, 6, TO_DATE('08/02/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/30/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/06/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 98.03, 'Rattlesnake Canyon Grocery', '2817 Milton Dr.',
        'Albuquerque', 'NM', '87110', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10273, 63, 3, TO_DATE('08/05/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/02/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/12/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 76.07, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10274, 85, 6, TO_DATE('08/06/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/03/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 6.01, 'Vins et alcools Chevalier', '59 rue de l''Abbaye',
        'Reims', '51100', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10275, 49, 1, TO_DATE('08/07/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/04/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 26.93, 'Magazzini Alimentari Riuniti', 'Via Ludovico il Moro 22',
        'Bergamo', '24100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10276, 80, 8, TO_DATE('08/08/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/22/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/14/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 13.84, 'Tortuga Restaurante', 'Avda. Azteca 123',
        'México D.F.', '05033', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10277, 52, 2, TO_DATE('08/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/06/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/13/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 125.77, 'Morgenstern Gesundkost', 'Heerstr. 22',
        'Leipzig', '04179', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10278, 5, 8, TO_DATE('08/12/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 92.69, 'Berglunds snabbköp', 'Berguvsvägen  8',
        'Luleå', 'S-958 22', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10279, 44, 8, TO_DATE('08/13/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/10/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 25.83, 'Lehmanns Marktstand', 'Magazinweg 7',
        'Frankfurt a.M. ', '60528', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10280, 5, 2, TO_DATE('08/14/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/11/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/12/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 8.98, 'Berglunds snabbköp', 'Berguvsvägen  8',
        'Luleå', 'S-958 22', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10281, 69, 4, TO_DATE('08/14/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/28/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/21/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 2.94, 'Romero y tomillo', 'Gran Vía, 1',
        'Madrid', '28001', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10282, 69, 4, TO_DATE('08/15/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/12/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/21/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 12.69, 'Romero y tomillo', 'Gran Vía, 1',
        'Madrid', '28001', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10283, 46, 3, TO_DATE('08/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/13/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/23/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 84.81, 'LILA-Supermercado', 'Carrera 52 con Ave. Bolívar #65-98 Llano Largo',
        'Barquisimeto', 'Lara', '3508', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10284, 44, 4, TO_DATE('08/19/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/27/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 76.56, 'Lehmanns Marktstand', 'Magazinweg 7',
        'Frankfurt a.M. ', '60528', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10285, 63, 1, TO_DATE('08/20/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/17/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/26/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 76.83, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10286, 63, 8, TO_DATE('08/21/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/18/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/30/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 229.24, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10287, 67, 8, TO_DATE('08/22/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/19/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/28/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 12.76, 'Ricardo Adocicados', 'Av. Copacabana, 267',
        'Rio de Janeiro', 'RJ', '02389-890', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10288, 66, 4, TO_DATE('08/23/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/20/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/03/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 7.45, 'Reggiani Caseifici', 'Strada Provinciale 124',
        'Reggio Emilia', '42100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10289, 11, 7, TO_DATE('08/26/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/23/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/28/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 22.77, 'B''s Beverages', 'Fauntleroy Circus',
        'London', 'EC2 5NT', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10290, 15, 8, TO_DATE('08/27/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/24/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/03/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 79.7, 'Comércio Mineiro', 'Av. dos Lusíadas, 23',
        'São Paulo', 'SP', '05432-043', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10291, 61, 6, TO_DATE('08/27/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/24/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/04/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 6.4, 'Que Delícia', 'Rua da Panificadora, 12',
        'Rio de Janeiro', 'RJ', '02389-673', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10292, 81, 1, TO_DATE('08/28/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/25/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/02/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 1.35, 'Tradição Hipermercados', 'Av. Inês de Castro, 414',
        'São Paulo', 'SP', '05634-030', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10293, 80, 1, TO_DATE('08/29/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/26/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/11/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 21.18, 'Tortuga Restaurante', 'Avda. Azteca 123',
        'México D.F.', '05033', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10294, 65, 4, TO_DATE('08/30/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/27/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/05/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 147.26, 'Rattlesnake Canyon Grocery', '2817 Milton Dr.',
        'Albuquerque', 'NM', '87110', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10295, 85, 2, TO_DATE('09/02/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/30/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/10/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 1.15, 'Vins et alcools Chevalier', '59 rue de l''Abbaye',
        'Reims', '51100', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10296, 46, 6, TO_DATE('09/03/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/01/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/11/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 0.12, 'LILA-Supermercado', 'Carrera 52 con Ave. Bolívar #65-98 Llano Largo',
        'Barquisimeto', 'Lara', '3508', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10297, 7, 5, TO_DATE('09/04/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/10/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 5.74, 'Blondel père et fils', '24, place Kléber',
        'Strasbourg', '67000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_COUNTRY)
VALUES (10298, 37, 6, TO_DATE('09/05/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/03/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/11/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 168.22, 'Hungry Owl All-Night Grocers', '8 Johnstown Road',
        'Cork', 'Co. Cork', 'Ireland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10299, 67, 4, TO_DATE('09/06/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/04/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/13/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 29.76, 'Ricardo Adocicados', 'Av. Copacabana, 267',
        'Rio de Janeiro', 'RJ', '02389-890', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10300, 49, 2, TO_DATE('09/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/07/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/18/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 17.68, 'Magazzini Alimentari Riuniti', 'Via Ludovico il Moro 22',
        'Bergamo', '24100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10301, 86, 8, TO_DATE('09/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/07/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/17/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 45.08, 'Die Wandernde Kuh', 'Adenauerallee 900',
        'Stuttgart', '70563', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10302, 76, 4, TO_DATE('09/10/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/08/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 6.27, 'Suprêmes délices', 'Boulevard Tirou, 255',
        'Charleroi', 'B-6000', 'Belgium');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10303, 30, 7, TO_DATE('09/11/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/18/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 107.83, 'Godos Cocina Típica', 'C/ Romero, 33',
        'Sevilla', '41101', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10304, 80, 1, TO_DATE('09/12/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/10/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/17/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 63.79, 'Tortuga Restaurante', 'Avda. Azteca 123',
        'México D.F.', '05033', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10305, 55, 8, TO_DATE('09/13/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/11/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 257.62, 'Old World Delicatessen', '2743 Bering St.',
        'Anchorage', 'AK', '99508', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10306, 69, 1, TO_DATE('09/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/14/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/23/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 7.56, 'Romero y tomillo', 'Gran Vía, 1',
        'Madrid', '28001', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10307, 48, 2, TO_DATE('09/17/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/15/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/25/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 0.56, 'Lonesome Pine Restaurant', '89 Chiaroscuro Rd.',
        'Portland', 'OR', '97219', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10308, 2, 7, TO_DATE('09/18/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/24/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 1.61, 'Ana Trujillo Emparedados y helados', 'Avda. de la Constitución 2222',
        'México D.F.', '05021', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_COUNTRY)
VALUES (10309, 37, 3, TO_DATE('09/19/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/17/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/23/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 47.3, 'Hungry Owl All-Night Grocers', '8 Johnstown Road',
        'Cork', 'Co. Cork', 'Ireland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10310, 77, 8, TO_DATE('09/20/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/18/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/27/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 17.52, 'The Big Cheese', '89 Jefferson Way
Suite 2',
        'Portland', 'OR', '97201', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10311, 18, 1, TO_DATE('09/20/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/04/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/26/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 24.69, 'Du monde entier', '67, rue des Cinquante Otages',
        'Nantes', '44000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10691, 63, 2, TO_DATE('10/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 810.05, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10692, 1, 4, TO_DATE('10/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 61.02, 'Alfreds Futterkiste', 'Obere Str. 57',
        'Berlin', '12209', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10693, 89, 3, TO_DATE('10/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 139.34, 'White Clover Markets', '1029 - 12th Ave. S.',
        'Seattle', 'WA', '98124', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10694, 63, 8, TO_DATE('10/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 398.36, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10695, 90, 7, TO_DATE('10/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 16.72, 'Wilman Kala', 'Keskuskatu 45',
        'Helsinki', '21240', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10696, 89, 8, TO_DATE('10/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 102.55, 'White Clover Markets', '1029 - 12th Ave. S.',
        'Seattle', 'WA', '98124', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10697, 47, 3, TO_DATE('10/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 45.52, 'LINO-Delicateses', 'Ave. 5 de Mayo Porlamar',
        'I. de Margarita', 'Nueva Esparta', '4980', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10698, 20, 4, TO_DATE('10/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 272.47, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10699, 52, 3, TO_DATE('10/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 0.58, 'Morgenstern Gesundkost', 'Heerstr. 22',
        'Leipzig', '04179', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10312, 86, 2, TO_DATE('09/23/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/21/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/03/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 40.26, 'Die Wandernde Kuh', 'Adenauerallee 900',
        'Stuttgart', '70563', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10313, 63, 2, TO_DATE('09/24/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/22/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/04/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 1.96, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10314, 65, 1, TO_DATE('09/25/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/23/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/04/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 74.16, 'Rattlesnake Canyon Grocery', '2817 Milton Dr.',
        'Albuquerque', 'NM', '87110', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10315, 38, 4, TO_DATE('09/26/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/24/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/03/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 41.76, 'Island Trading', 'Garden House
Crowther Way',
        'Cowes', 'Isle of Wight', 'PO31 7PJ', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10316, 65, 1, TO_DATE('09/27/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/25/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/08/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 150.15, 'Rattlesnake Canyon Grocery', '2817 Milton Dr.',
        'Albuquerque', 'NM', '87110', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10317, 48, 6, TO_DATE('09/30/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/28/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/10/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 12.69, 'Lonesome Pine Restaurant', '89 Chiaroscuro Rd.',
        'Portland', 'OR', '97219', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10318, 38, 8, TO_DATE('10/01/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/29/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/04/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 4.73, 'Island Trading', 'Garden House
Crowther Way',
        'Cowes', 'Isle of Wight', 'PO31 7PJ', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10319, 80, 7, TO_DATE('10/02/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/30/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/11/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 64.5, 'Tortuga Restaurante', 'Avda. Azteca 123',
        'México D.F.', '05033', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10320, 87, 5, TO_DATE('10/03/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/17/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/18/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 34.57, 'Wartian Herkku', 'Torikatu 38',
        'Oulu', '90110', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10321, 38, 3, TO_DATE('10/03/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/31/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/11/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 3.43, 'Island Trading', 'Garden House
Crowther Way',
        'Cowes', 'Isle of Wight', 'PO31 7PJ', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10322, 58, 7, TO_DATE('10/04/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/01/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/23/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 0.4, 'Pericles Comidas clásicas', 'Calle Dr. Jorge Cash 321',
        'México D.F.', '05033', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10323, 39, 4, TO_DATE('10/07/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/04/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/14/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 4.88, 'Königlich Essen', 'Maubelstr. 90',
        'Brandenburg', '14776', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10324, 71, 9, TO_DATE('10/08/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/05/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/10/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 214.27, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10325, 39, 1, TO_DATE('10/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/23/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/14/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 64.86, 'Königlich Essen', 'Maubelstr. 90',
        'Brandenburg', '14776', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10326, 8, 4, TO_DATE('10/10/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/07/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/14/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 77.92, 'Bólido Comidas preparadas', 'C/ Araquil, 67',
        'Madrid', '28023', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10327, 24, 2, TO_DATE('10/11/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/08/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/14/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 63.36, 'Folk och fä HB', 'Åkergatan 24',
        'Bräcke', 'S-844 67', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10328, 28, 4, TO_DATE('10/14/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/11/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/17/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 87.03, 'Furia Bacalhau e Frutos do Mar', 'Jardim das rosas n. 32',
        'Lisboa', '1675', 'Portugal');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10329, 75, 4, TO_DATE('10/15/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/26/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/23/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 191.67, 'Split Rail Beer & Ale', 'P.O. Box 555',
        'Lander', 'WY', '82520', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10330, 46, 3, TO_DATE('10/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/13/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/28/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 12.75, 'LILA-Supermercado', 'Carrera 52 con Ave. Bolívar #65-98 Llano Largo',
        'Barquisimeto', 'Lara', '3508', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10331, 9, 9, TO_DATE('10/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/27/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/21/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 10.19, 'Bon app''', '12, rue des Bouchers',
        'Marseille', '13008', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10332, 51, 3, TO_DATE('10/17/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/28/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/21/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 52.84, 'Mère Paillarde', '43 rue St. Laurent',
        'Montréal', 'Québec', 'H1J 1C3', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10333, 87, 5, TO_DATE('10/18/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/15/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/25/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 0.59, 'Wartian Herkku', 'Torikatu 38',
        'Oulu', '90110', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10334, 84, 8, TO_DATE('10/21/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/18/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/28/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 8.56, 'Victuailles en stock', '2, rue du Commerce',
        'Lyon', '69004', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_COUNTRY)
VALUES (10335, 37, 7, TO_DATE('10/22/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/19/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/24/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 42.11, 'Hungry Owl All-Night Grocers', '8 Johnstown Road',
        'Cork', 'Co. Cork', 'Ireland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10336, 60, 7, TO_DATE('10/23/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/20/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/25/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 15.51, 'Princesa Isabel Vinhos', 'Estrada da saúde n. 58',
        'Lisboa', '1756', 'Portugal');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10337, 25, 4, TO_DATE('10/24/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/21/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/29/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 108.26, 'Frankenversand', 'Berliner Platz 43',
        'München', '80805', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10338, 55, 4, TO_DATE('10/25/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/22/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/29/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 84.21, 'Old World Delicatessen', '2743 Bering St.',
        'Anchorage', 'AK', '99508', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10339, 51, 2, TO_DATE('10/28/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/25/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/04/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 15.66, 'Mère Paillarde', '43 rue St. Laurent',
        'Montréal', 'Québec', 'H1J 1C3', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10340, 9, 1, TO_DATE('10/29/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/26/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/08/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 166.31, 'Bon app''', '12, rue des Bouchers',
        'Marseille', '13008', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10341, 73, 7, TO_DATE('10/29/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/26/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/05/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 26.78, 'Simons bistro', 'Vinbæltet 34',
        'København', '1734', 'Denmark');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10342, 25, 4, TO_DATE('10/30/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/13/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/04/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 54.83, 'Frankenversand', 'Berliner Platz 43',
        'München', '80805', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10343, 44, 4, TO_DATE('10/31/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/28/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/06/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 110.37, 'Lehmanns Marktstand', 'Magazinweg 7',
        'Frankfurt a.M. ', '60528', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10344, 89, 4, TO_DATE('11/01/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/29/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/05/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 23.29, 'White Clover Markets', '1029 - 12th Ave. S.',
        'Seattle', 'WA', '98124', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10345, 63, 2, TO_DATE('11/04/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/02/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/11/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 249.06, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10346, 65, 3, TO_DATE('11/05/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/17/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/08/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 142.08, 'Rattlesnake Canyon Grocery', '2817 Milton Dr.',
        'Albuquerque', 'NM', '87110', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10347, 21, 4, TO_DATE('11/06/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/04/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/08/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 3.1, 'Familia Arquibaldo', 'Rua Orós, 92',
        'São Paulo', 'SP', '05442-030', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10348, 86, 4, TO_DATE('11/07/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/05/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/15/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 0.78, 'Die Wandernde Kuh', 'Adenauerallee 900',
        'Stuttgart', '70563', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10349, 75, 7, TO_DATE('11/08/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/06/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/15/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 8.63, 'Split Rail Beer & Ale', 'P.O. Box 555',
        'Lander', 'WY', '82520', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10350, 41, 6, TO_DATE('11/11/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/03/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 64.19, 'La maison d''Asie', '1 rue Alsace-Lorraine',
        'Toulouse', '31000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10351, 20, 1, TO_DATE('11/11/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/20/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 162.33, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10352, 28, 3, TO_DATE('11/12/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/26/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/18/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 1.3, 'Furia Bacalhau e Frutos do Mar', 'Jardim das rosas n. 32',
        'Lisboa', '1675', 'Portugal');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10353, 59, 7, TO_DATE('11/13/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/11/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/25/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 360.63, 'Piccolo und mehr', 'Geislweg 14',
        'Salzburg', '5020', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10354, 58, 8, TO_DATE('11/14/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/12/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/20/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 53.8, 'Pericles Comidas clásicas', 'Calle Dr. Jorge Cash 321',
        'México D.F.', '05033', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10355, 4, 6, TO_DATE('11/15/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/13/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/20/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 41.95, 'Around the Horn', 'Brook Farm
Stratford St. Mary',
        'Colchester', 'Essex', 'CO7 6JX', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10356, 86, 6, TO_DATE('11/18/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/27/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 36.71, 'Die Wandernde Kuh', 'Adenauerallee 900',
        'Stuttgart', '70563', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10357, 46, 1, TO_DATE('11/19/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/17/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/02/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 34.88, 'LILA-Supermercado', 'Carrera 52 con Ave. Bolívar #65-98 Llano Largo',
        'Barquisimeto', 'Lara', '3508', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10358, 41, 5, TO_DATE('11/20/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/18/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/27/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 19.64, 'La maison d''Asie', '1 rue Alsace-Lorraine',
        'Toulouse', '31000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10359, 72, 5, TO_DATE('11/21/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/19/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/26/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 288.43, 'Seven Seas Imports', '90 Wadhurst Rd.',
        'London', 'OX15 4NB', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10360, 7, 4, TO_DATE('11/22/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/20/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/02/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 131.7, 'Blondel père et fils', '24, place Kléber',
        'Strasbourg', '67000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10361, 63, 1, TO_DATE('11/22/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/20/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/03/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 183.17, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10362, 9, 3, TO_DATE('11/25/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/23/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/28/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 96.04, 'Bon app''', '12, rue des Bouchers',
        'Marseille', '13008', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10363, 17, 4, TO_DATE('11/26/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/24/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/04/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 30.54, 'Drachenblut Delikatessen', 'Walserweg 21',
        'Aachen', '52066', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10364, 19, 1, TO_DATE('11/26/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/04/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 71.97, 'Eastern Connection', '35 King George',
        'London', 'WX3 6FW', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10365, 3, 3, TO_DATE('11/27/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/25/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/02/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 22, 'Antonio Moreno Taquería', 'Mataderos  2312',
        'México D.F.', '05023', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10366, 29, 8, TO_DATE('11/28/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/30/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 10.14, 'Galería del gastronómo', 'Rambla de Cataluña, 23',
        'Barcelona', '8022', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10367, 83, 7, TO_DATE('11/28/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/26/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/02/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 13.55, 'Vaffeljernet', 'Smagsløget 45',
        'Århus', '8200', 'Denmark');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10368, 20, 2, TO_DATE('11/29/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/27/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/02/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 101.95, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10369, 75, 8, TO_DATE('12/02/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/30/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 195.68, 'Split Rail Beer & Ale', 'P.O. Box 555',
        'Lander', 'WY', '82520', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10370, 14, 6, TO_DATE('12/03/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/31/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/27/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 1.17, 'Chop-suey Chinese', 'Hauptstr. 31',
        'Bern', '3012', 'Switzerland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10371, 41, 1, TO_DATE('12/03/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/31/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/24/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 0.45, 'La maison d''Asie', '1 rue Alsace-Lorraine',
        'Toulouse', '31000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10372, 62, 5, TO_DATE('12/04/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 890.78, 'Queen Cozinha', 'Alameda dos Canàrios, 891',
        'São Paulo', 'SP', '05487-020', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10939, 49, 2, TO_DATE('03/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 76.33, 'Magazzini Alimentari Riuniti', 'Via Ludovico il Moro 22',
        'Bergamo', '24100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10940, 9, 8, TO_DATE('03/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 19.77, 'Bon app''', '12, rue des Bouchers',
        'Marseille', '13008', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10941, 71, 7, TO_DATE('03/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 400.81, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_COUNTRY)
VALUES (10373, 37, 4, TO_DATE('12/05/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/11/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 124.12, 'Hungry Owl All-Night Grocers', '8 Johnstown Road',
        'Cork', 'Co. Cork', 'Ireland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10374, 91, 1, TO_DATE('12/05/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 3.94, 'Wolski Zajazd', 'ul. Filtrowa 68',
        'Warszawa', '01-012', 'Poland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10375, 36, 3, TO_DATE('12/06/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 20.12, 'Hungry Coyote Import Store', 'City Center Plaza
516 Main St.',
        'Elgin', 'OR', '97827', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10376, 51, 1, TO_DATE('12/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/13/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 20.39, 'Mère Paillarde', '43 rue St. Laurent',
        'Montréal', 'Québec', 'H1J 1C3', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10377, 72, 1, TO_DATE('12/09/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/13/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 22.21, 'Seven Seas Imports', '90 Wadhurst Rd.',
        'London', 'OX15 4NB', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10378, 24, 5, TO_DATE('12/10/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/19/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 5.44, 'Folk och fä HB', 'Åkergatan 24',
        'Bräcke', 'S-844 67', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10379, 61, 2, TO_DATE('12/11/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/13/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 45.03, 'Que Delícia', 'Rua da Panificadora, 12',
        'Rio de Janeiro', 'RJ', '02389-673', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_COUNTRY)
VALUES (10380, 37, 8, TO_DATE('12/12/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 35.03, 'Hungry Owl All-Night Grocers', '8 Johnstown Road',
        'Cork', 'Co. Cork', 'Ireland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10381, 46, 3, TO_DATE('12/12/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/13/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 7.99, 'LILA-Supermercado', 'Carrera 52 con Ave. Bolívar #65-98 Llano Largo',
        'Barquisimeto', 'Lara', '3508', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10382, 20, 4, TO_DATE('12/13/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 94.77, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10383, 4, 8, TO_DATE('12/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/18/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 34.24, 'Around the Horn', 'Brook Farm
Stratford St. Mary',
        'Colchester', 'Essex', 'CO7 6JX', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10384, 5, 3, TO_DATE('12/16/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/20/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 168.64, 'Berglunds snabbköp', 'Berguvsvägen  8',
        'Luleå', 'S-958 22', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10385, 75, 1, TO_DATE('12/17/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/23/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 30.96, 'Split Rail Beer & Ale', 'P.O. Box 555',
        'Lander', 'WY', '82520', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10386, 21, 9, TO_DATE('12/18/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/25/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 13.99, 'Familia Arquibaldo', 'Rua Orós, 92',
        'São Paulo', 'SP', '05442-030', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10387, 70, 1, TO_DATE('12/18/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/20/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 93.63, 'Santé Gourmet', 'Erling Skakkes gate 78',
        'Stavern', '4110', 'Norway');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10388, 72, 2, TO_DATE('12/19/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/20/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 34.86, 'Seven Seas Imports', '90 Wadhurst Rd.',
        'London', 'OX15 4NB', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10389, 10, 4, TO_DATE('12/20/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/24/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 47.42, 'Bottom-Dollar Markets', '23 Tsawassen Blvd.',
        'Tsawassen', 'BC', 'T2F 8M4', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10390, 20, 6, TO_DATE('12/23/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/26/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 126.38, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10391, 17, 3, TO_DATE('12/23/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/31/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 5.45, 'Drachenblut Delikatessen', 'Walserweg 21',
        'Aachen', '52066', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10392, 59, 2, TO_DATE('12/24/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 122.46, 'Piccolo und mehr', 'Geislweg 14',
        'Salzburg', '5020', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10393, 71, 1, TO_DATE('12/25/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 126.56, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10394, 36, 1, TO_DATE('12/25/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 30.34, 'Hungry Coyote Import Store', 'City Center Plaza
516 Main St.',
        'Elgin', 'OR', '97827', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10395, 35, 6, TO_DATE('12/26/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 184.41, 'HILARIÓN-Abastos', 'Carrera 22 con Ave. Carlos Soublette #8-35',
        'San Cristóbal', 'Táchira', '5022', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10396, 25, 1, TO_DATE('12/27/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 135.35, 'Frankenversand', 'Berliner Platz 43',
        'München', '80805', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10397, 60, 5, TO_DATE('12/27/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 60.26, 'Princesa Isabel Vinhos', 'Estrada da saúde n. 58',
        'Lisboa', '1756', 'Portugal');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10398, 71, 2, TO_DATE('12/30/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 89.16, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10399, 83, 8, TO_DATE('12/31/1996 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 27.36, 'Vaffeljernet', 'Smagsløget 45',
        'Århus', '8200', 'Denmark');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10400, 19, 1, TO_DATE('01/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 83.93, 'Eastern Connection', '35 King George',
        'London', 'WX3 6FW', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10401, 65, 1, TO_DATE('01/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 12.51, 'Rattlesnake Canyon Grocery', '2817 Milton Dr.',
        'Albuquerque', 'NM', '87110', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10402, 20, 8, TO_DATE('01/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 67.88, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10403, 20, 4, TO_DATE('01/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 73.79, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10404, 49, 2, TO_DATE('01/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 155.97, 'Magazzini Alimentari Riuniti', 'Via Ludovico il Moro 22',
        'Bergamo', '24100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10405, 47, 1, TO_DATE('01/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 34.82, 'LINO-Delicateses', 'Ave. 5 de Mayo Porlamar',
        'I. de Margarita', 'Nueva Esparta', '4980', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10406, 62, 7, TO_DATE('01/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 108.04, 'Queen Cozinha', 'Alameda dos Canàrios, 891',
        'São Paulo', 'SP', '05487-020', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10407, 56, 2, TO_DATE('01/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 91.48, 'Ottilies Käseladen', 'Mehrheimerstr. 369',
        'Köln', '50739', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10408, 23, 8, TO_DATE('01/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 11.26, 'Folies gourmandes', '184, chaussée de Tournai',
        'Lille', '59000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10409, 54, 3, TO_DATE('01/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 29.83, 'Océano Atlántico Ltda.', 'Ing. Gustavo Moncada 8585
Piso 20-A',
        'Buenos Aires', '1010', 'Argentina');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10410, 10, 3, TO_DATE('01/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 2.4, 'Bottom-Dollar Markets', '23 Tsawassen Blvd.',
        'Tsawassen', 'BC', 'T2F 8M4', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10411, 10, 9, TO_DATE('01/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 23.65, 'Bottom-Dollar Markets', '23 Tsawassen Blvd.',
        'Tsawassen', 'BC', 'T2F 8M4', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10412, 87, 8, TO_DATE('01/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 3.77, 'Wartian Herkku', 'Torikatu 38',
        'Oulu', '90110', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10413, 41, 3, TO_DATE('01/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 95.66, 'La maison d''Asie', '1 rue Alsace-Lorraine',
        'Toulouse', '31000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10414, 21, 2, TO_DATE('01/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 21.48, 'Familia Arquibaldo', 'Rua Orós, 92',
        'São Paulo', 'SP', '05442-030', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10415, 36, 3, TO_DATE('01/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 0.2, 'Hungry Coyote Import Store', 'City Center Plaza
516 Main St.',
        'Elgin', 'OR', '97827', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10416, 87, 8, TO_DATE('01/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 22.72, 'Wartian Herkku', 'Torikatu 38',
        'Oulu', '90110', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10417, 73, 4, TO_DATE('01/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 70.29, 'Simons bistro', 'Vinbæltet 34',
        'København', '1734', 'Denmark');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10418, 63, 4, TO_DATE('01/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 17.55, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10419, 68, 4, TO_DATE('01/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 137.35, 'Richter Supermarkt', 'Starenweg 5',
        'Genève', '1204', 'Switzerland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10420, 88, 3, TO_DATE('01/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 44.12, 'Wellington Importadora', 'Rua do Mercado, 12',
        'Resende', 'SP', '08737-363', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10421, 61, 8, TO_DATE('01/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 99.23, 'Que Delícia', 'Rua da Panificadora, 12',
        'Rio de Janeiro', 'RJ', '02389-673', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10422, 27, 2, TO_DATE('01/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 3.02, 'Franchi S.p.A.', 'Via Monte Bianco 34',
        'Torino', '10100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10423, 31, 6, TO_DATE('01/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 24.5, 'Gourmet Lanchonetes', 'Av. Brasil, 442',
        'Campinas', 'SP', '04876-786', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10424, 51, 7, TO_DATE('01/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 370.61, 'Mère Paillarde', '43 rue St. Laurent',
        'Montréal', 'Québec', 'H1J 1C3', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10425, 41, 6, TO_DATE('01/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 7.93, 'La maison d''Asie', '1 rue Alsace-Lorraine',
        'Toulouse', '31000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10426, 29, 4, TO_DATE('01/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 18.69, 'Galería del gastronómo', 'Rambla de Cataluña, 23',
        'Barcelona', '8022', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10427, 59, 4, TO_DATE('01/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 31.29, 'Piccolo und mehr', 'Geislweg 14',
        'Salzburg', '5020', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10428, 66, 7, TO_DATE('01/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 11.09, 'Reggiani Caseifici', 'Strada Provinciale 124',
        'Reggio Emilia', '42100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_COUNTRY)
VALUES (10429, 37, 3, TO_DATE('01/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 56.63, 'Hungry Owl All-Night Grocers', '8 Johnstown Road',
        'Cork', 'Co. Cork', 'Ireland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10430, 20, 4, TO_DATE('01/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 458.78, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10431, 10, 4, TO_DATE('01/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 44.17, 'Bottom-Dollar Markets', '23 Tsawassen Blvd.',
        'Tsawassen', 'BC', 'T2F 8M4', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10432, 75, 3, TO_DATE('01/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 4.34, 'Split Rail Beer & Ale', 'P.O. Box 555',
        'Lander', 'WY', '82520', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10433, 60, 3, TO_DATE('02/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 73.83, 'Princesa Isabel Vinhos', 'Estrada da saúde n. 58',
        'Lisboa', '1756', 'Portugal');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10434, 24, 3, TO_DATE('02/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 17.92, 'Folk och fä HB', 'Åkergatan 24',
        'Bräcke', 'S-844 67', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10435, 16, 8, TO_DATE('02/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 9.21, 'Consolidated Holdings', 'Berkeley Gardens
12  Brewery ',
        'London', 'WX1 6LT', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10436, 7, 3, TO_DATE('02/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 156.66, 'Blondel père et fils', '24, place Kléber',
        'Strasbourg', '67000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10437, 87, 8, TO_DATE('02/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 19.97, 'Wartian Herkku', 'Torikatu 38',
        'Oulu', '90110', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10438, 79, 3, TO_DATE('02/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 8.24, 'Toms Spezialitäten', 'Luisenstr. 48',
        'Münster', '44087', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10439, 51, 6, TO_DATE('02/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 4.07, 'Mère Paillarde', '43 rue St. Laurent',
        'Montréal', 'Québec', 'H1J 1C3', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11018, 48, 4, TO_DATE('04/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 11.65, 'Lonesome Pine Restaurant', '89 Chiaroscuro Rd.',
        'Portland', 'OR', '97219', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11019, 64, 6, TO_DATE('04/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        3, 3.17, 'Rancho grande', 'Av. del Libertador 900',
        'Buenos Aires', '1010', 'Argentina');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11020, 56, 2, TO_DATE('04/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 43.3, 'Ottilies Käseladen', 'Mehrheimerstr. 369',
        'Köln', '50739', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11021, 63, 3, TO_DATE('04/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 297.18, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11022, 34, 9, TO_DATE('04/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 6.27, 'Hanari Carnes', 'Rua do Paço, 67',
        'Rio de Janeiro', 'RJ', '05454-876', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11023, 11, 1, TO_DATE('04/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/28/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 123.83, 'B''s Beverages', 'Fauntleroy Circus',
        'London', 'EC2 5NT', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11024, 19, 4, TO_DATE('04/15/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 74.36, 'Eastern Connection', '35 King George',
        'London', 'WX3 6FW', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11025, 87, 6, TO_DATE('04/15/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 29.17, 'Wartian Herkku', 'Torikatu 38',
        'Oulu', '90110', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11026, 27, 4, TO_DATE('04/15/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/28/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 47.09, 'Franchi S.p.A.', 'Via Monte Bianco 34',
        'Torino', '10100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11027, 10, 1, TO_DATE('04/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 52.52, 'Bottom-Dollar Markets', '23 Tsawassen Blvd.',
        'Tsawassen', 'BC', 'T2F 8M4', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11028, 39, 2, TO_DATE('04/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/22/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 29.59, 'Königlich Essen', 'Maubelstr. 90',
        'Brandenburg', '14776', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11029, 14, 4, TO_DATE('04/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 47.84, 'Chop-suey Chinese', 'Hauptstr. 31',
        'Bern', '3012', 'Switzerland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11030, 71, 7, TO_DATE('04/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/15/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 830.75, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11031, 71, 6, TO_DATE('04/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/15/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 227.22, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11032, 89, 2, TO_DATE('04/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/15/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 606.19, 'White Clover Markets', '1029 - 12th Ave. S.',
        'Seattle', 'WA', '98124', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11033, 68, 7, TO_DATE('04/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/15/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 84.74, 'Richter Supermarkt', 'Starenweg 5',
        'Genève', '1204', 'Switzerland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11034, 55, 8, TO_DATE('04/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 40.32, 'Old World Delicatessen', '2743 Bering St.',
        'Anchorage', 'AK', '99508', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11035, 76, 2, TO_DATE('04/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 0.17, 'Suprêmes délices', 'Boulevard Tirou, 255',
        'Charleroi', 'B-6000', 'Belgium');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11036, 17, 8, TO_DATE('04/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/22/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 149.47, 'Drachenblut Delikatessen', 'Walserweg 21',
        'Aachen', '52066', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11037, 30, 7, TO_DATE('04/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 3.2, 'Godos Cocina Típica', 'C/ Romero, 33',
        'Sevilla', '41101', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11038, 76, 1, TO_DATE('04/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 29.59, 'Suprêmes délices', 'Boulevard Tirou, 255',
        'Charleroi', 'B-6000', 'Belgium');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11039, 47, 1, TO_DATE('04/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        2, 65, 'LINO-Delicateses', 'Ave. 5 de Mayo Porlamar',
        'I. de Margarita', 'Nueva Esparta', '4980', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11040, 32, 4, TO_DATE('04/22/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        3, 18.84, 'Great Lakes Food Market', '2732 Baker Blvd.',
        'Eugene', 'OR', '97403', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11041, 14, 3, TO_DATE('04/22/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/28/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 48.22, 'Chop-suey Chinese', 'Hauptstr. 31',
        'Bern', '3012', 'Switzerland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11042, 15, 2, TO_DATE('04/22/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 29.99, 'Comércio Mineiro', 'Av. dos Lusíadas, 23',
        'São Paulo', 'SP', '05432-043', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11043, 74, 5, TO_DATE('04/22/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 8.8, 'Spécialités du monde', '25, rue Lauriston',
        'Paris', '75016', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11044, 91, 4, TO_DATE('04/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 8.72, 'Wolski Zajazd', 'ul. Filtrowa 68',
        'Warszawa', '01-012', 'Poland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11045, 10, 6, TO_DATE('04/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        2, 70.58, 'Bottom-Dollar Markets', '23 Tsawassen Blvd.',
        'Tsawassen', 'BC', 'T2F 8M4', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11046, 86, 8, TO_DATE('04/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 71.64, 'Die Wandernde Kuh', 'Adenauerallee 900',
        'Stuttgart', '70563', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11047, 19, 7, TO_DATE('04/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/22/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 46.62, 'Eastern Connection', '35 King George',
        'London', 'WX3 6FW', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11048, 10, 7, TO_DATE('04/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/22/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 24.12, 'Bottom-Dollar Markets', '23 Tsawassen Blvd.',
        'Tsawassen', 'BC', 'T2F 8M4', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11049, 31, 3, TO_DATE('04/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/22/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 8.34, 'Gourmet Lanchonetes', 'Av. Brasil, 442',
        'Campinas', 'SP', '04876-786', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11050, 24, 8, TO_DATE('04/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/25/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 59.41, 'Folk och fä HB', 'Åkergatan 24',
        'Bräcke', 'S-844 67', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11051, 41, 7, TO_DATE('04/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/25/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        3, 2.79, 'La maison d''Asie', '1 rue Alsace-Lorraine',
        'Toulouse', '31000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11052, 34, 3, TO_DATE('04/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/25/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 67.26, 'Hanari Carnes', 'Rua do Paço, 67',
        'Rio de Janeiro', 'RJ', '05454-876', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11053, 59, 2, TO_DATE('04/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/25/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 53.05, 'Piccolo und mehr', 'Geislweg 14',
        'Salzburg', '5020', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11054, 12, 8, TO_DATE('04/28/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        1, 0.33, 'Cactus Comidas para llevar', 'Cerrito 333',
        'Buenos Aires', '1010', 'Argentina');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11055, 35, 7, TO_DATE('04/28/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 120.92, 'HILARIÓN-Abastos', 'Carrera 22 con Ave. Carlos Soublette #8-35',
        'San Cristóbal', 'Táchira', '5022', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11056, 19, 8, TO_DATE('04/28/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 278.96, 'Eastern Connection', '35 King George',
        'London', 'WX3 6FW', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11057, 53, 3, TO_DATE('04/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 4.13, 'North/South', 'South House
300 Queensbridge',
        'London', 'SW7 1RZ', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11058, 6, 9, TO_DATE('04/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        3, 31.14, 'Blauer See Delikatessen', 'Forsterstr. 57',
        'Mannheim', '68306', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11059, 67, 2, TO_DATE('04/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        2, 85.8, 'Ricardo Adocicados', 'Av. Copacabana, 267',
        'Rio de Janeiro', 'RJ', '02389-890', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11060, 27, 2, TO_DATE('04/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/28/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 10.98, 'Franchi S.p.A.', 'Via Monte Bianco 34',
        'Torino', '10100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11061, 32, 4, TO_DATE('04/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        3, 14.01, 'Great Lakes Food Market', '2732 Baker Blvd.',
        'Eugene', 'OR', '97403', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11062, 66, 4, TO_DATE('04/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/28/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        2, 29.93, 'Reggiani Caseifici', 'Strada Provinciale 124',
        'Reggio Emilia', '42100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_COUNTRY)
VALUES (11063, 37, 3, TO_DATE('04/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/28/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 81.73, 'Hungry Owl All-Night Grocers', '8 Johnstown Road',
        'Cork', 'Co. Cork', 'Ireland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11064, 71, 1, TO_DATE('05/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 30.09, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11065, 46, 8, TO_DATE('05/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        1, 12.91, 'LILA-Supermercado', 'Carrera 52 con Ave. Bolívar #65-98 Llano Largo',
        'Barquisimeto', 'Lara', '3508', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11066, 89, 7, TO_DATE('05/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 44.72, 'White Clover Markets', '1029 - 12th Ave. S.',
        'Seattle', 'WA', '98124', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10561, 24, 2, TO_DATE('06/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 242.21, 'Folk och fä HB', 'Åkergatan 24',
        'Bräcke', 'S-844 67', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10562, 66, 1, TO_DATE('06/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 22.95, 'Reggiani Caseifici', 'Strada Provinciale 124',
        'Reggio Emilia', '42100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10563, 67, 2, TO_DATE('06/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 60.43, 'Ricardo Adocicados', 'Av. Copacabana, 267',
        'Rio de Janeiro', 'RJ', '02389-890', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10564, 65, 4, TO_DATE('06/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 13.75, 'Rattlesnake Canyon Grocery', '2817 Milton Dr.',
        'Albuquerque', 'NM', '87110', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10565, 51, 8, TO_DATE('06/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 7.15, 'Mère Paillarde', '43 rue St. Laurent',
        'Montréal', 'Québec', 'H1J 1C3', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10566, 7, 9, TO_DATE('06/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 88.4, 'Blondel père et fils', '24, place Kléber',
        'Strasbourg', '67000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_COUNTRY)
VALUES (10567, 37, 1, TO_DATE('06/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 33.97, 'Hungry Owl All-Night Grocers', '8 Johnstown Road',
        'Cork', 'Co. Cork', 'Ireland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10568, 29, 3, TO_DATE('06/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 6.54, 'Galería del gastronómo', 'Rambla de Cataluña, 23',
        'Barcelona', '8022', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10569, 65, 5, TO_DATE('06/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 58.98, 'Rattlesnake Canyon Grocery', '2817 Milton Dr.',
        'Albuquerque', 'NM', '87110', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10570, 51, 3, TO_DATE('06/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 188.99, 'Mère Paillarde', '43 rue St. Laurent',
        'Montréal', 'Québec', 'H1J 1C3', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10571, 20, 8, TO_DATE('06/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 26.06, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10572, 5, 3, TO_DATE('06/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 116.43, 'Berglunds snabbköp', 'Berguvsvägen  8',
        'Luleå', 'S-958 22', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10573, 3, 7, TO_DATE('06/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 84.84, 'Antonio Moreno Taquería', 'Mataderos  2312',
        'México D.F.', '05023', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10574, 82, 4, TO_DATE('06/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 37.6, 'Trail''s Head Gourmet Provisioners', '722 DaVinci Blvd.',
        'Kirkland', 'WA', '98034', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10575, 52, 5, TO_DATE('06/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 127.34, 'Morgenstern Gesundkost', 'Heerstr. 22',
        'Leipzig', '04179', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10576, 80, 3, TO_DATE('06/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 18.56, 'Tortuga Restaurante', 'Avda. Azteca 123',
        'México D.F.', '05033', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10577, 82, 9, TO_DATE('06/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('06/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 25.41, 'Trail''s Head Gourmet Provisioners', '722 DaVinci Blvd.',
        'Kirkland', 'WA', '98034', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10578, 11, 4, TO_DATE('06/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 29.6, 'B''s Beverages', 'Fauntleroy Circus',
        'London', 'EC2 5NT', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10649, 50, 5, TO_DATE('08/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 6.2, 'Maison Dewey', 'Rue Joseph-Bens 532',
        'Bruxelles', 'B-1180', 'Belgium');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10650, 21, 5, TO_DATE('08/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 176.81, 'Familia Arquibaldo', 'Rua Orós, 92',
        'São Paulo', 'SP', '05442-030', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10651, 86, 8, TO_DATE('09/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 20.6, 'Die Wandernde Kuh', 'Adenauerallee 900',
        'Stuttgart', '70563', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10652, 31, 4, TO_DATE('09/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 7.14, 'Gourmet Lanchonetes', 'Av. Brasil, 442',
        'Campinas', 'SP', '04876-786', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10653, 25, 1, TO_DATE('09/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 93.25, 'Frankenversand', 'Berliner Platz 43',
        'München', '80805', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10654, 5, 5, TO_DATE('09/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 55.26, 'Berglunds snabbköp', 'Berguvsvägen  8',
        'Luleå', 'S-958 22', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10655, 66, 1, TO_DATE('09/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 4.41, 'Reggiani Caseifici', 'Strada Provinciale 124',
        'Reggio Emilia', '42100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10656, 32, 6, TO_DATE('09/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 57.15, 'Great Lakes Food Market', '2732 Baker Blvd.',
        'Eugene', 'OR', '97403', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10657, 71, 2, TO_DATE('09/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 352.69, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10658, 63, 4, TO_DATE('09/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 364.15, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10659, 62, 7, TO_DATE('09/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 105.81, 'Queen Cozinha', 'Alameda dos Canàrios, 891',
        'São Paulo', 'SP', '05487-020', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10660, 36, 8, TO_DATE('09/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 111.29, 'Hungry Coyote Import Store', 'City Center Plaza
516 Main St.',
        'Elgin', 'OR', '97827', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_COUNTRY)
VALUES (10661, 37, 7, TO_DATE('09/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 17.55, 'Hungry Owl All-Night Grocers', '8 Johnstown Road',
        'Cork', 'Co. Cork', 'Ireland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10662, 48, 3, TO_DATE('09/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 1.28, 'Lonesome Pine Restaurant', '89 Chiaroscuro Rd.',
        'Portland', 'OR', '97219', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10663, 9, 2, TO_DATE('09/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 113.15, 'Bon app''', '12, rue des Bouchers',
        'Marseille', '13008', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10664, 28, 1, TO_DATE('09/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 1.27, 'Furia Bacalhau e Frutos do Mar', 'Jardim das rosas n. 32',
        'Lisboa', '1675', 'Portugal');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10665, 48, 1, TO_DATE('09/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 26.31, 'Lonesome Pine Restaurant', '89 Chiaroscuro Rd.',
        'Portland', 'OR', '97219', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10666, 68, 7, TO_DATE('09/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 232.42, 'Richter Supermarkt', 'Starenweg 5',
        'Genève', '1204', 'Switzerland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10667, 20, 7, TO_DATE('09/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 78.09, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10668, 86, 1, TO_DATE('09/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 47.22, 'Die Wandernde Kuh', 'Adenauerallee 900',
        'Stuttgart', '70563', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10669, 73, 2, TO_DATE('09/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 24.39, 'Simons bistro', 'Vinbæltet 34',
        'København', '1734', 'Denmark');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10670, 25, 4, TO_DATE('09/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 203.48, 'Frankenversand', 'Berliner Platz 43',
        'München', '80805', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10671, 26, 1, TO_DATE('09/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 30.34, 'France restauration', '54, rue Royale',
        'Nantes', '44000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10672, 5, 9, TO_DATE('09/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 95.75, 'Berglunds snabbköp', 'Berguvsvägen  8',
        'Luleå', 'S-958 22', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10673, 90, 2, TO_DATE('09/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 22.76, 'Wilman Kala', 'Keskuskatu 45',
        'Helsinki', '21240', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10674, 38, 4, TO_DATE('09/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 0.9, 'Island Trading', 'Garden House
Crowther Way',
        'Cowes', 'Isle of Wight', 'PO31 7PJ', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10675, 25, 5, TO_DATE('09/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 31.85, 'Frankenversand', 'Berliner Platz 43',
        'München', '80805', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10676, 80, 2, TO_DATE('09/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 2.01, 'Tortuga Restaurante', 'Avda. Azteca 123',
        'México D.F.', '05033', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10677, 3, 1, TO_DATE('09/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 4.03, 'Antonio Moreno Taquería', 'Mataderos  2312',
        'México D.F.', '05023', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10678, 71, 7, TO_DATE('09/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 388.98, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10679, 7, 8, TO_DATE('09/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 27.94, 'Blondel père et fils', '24, place Kléber',
        'Strasbourg', '67000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10680, 55, 1, TO_DATE('09/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 26.61, 'Old World Delicatessen', '2743 Bering St.',
        'Anchorage', 'AK', '99508', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10681, 32, 3, TO_DATE('09/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 76.13, 'Great Lakes Food Market', '2732 Baker Blvd.',
        'Eugene', 'OR', '97403', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10682, 3, 3, TO_DATE('09/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 36.13, 'Antonio Moreno Taquería', 'Mataderos  2312',
        'México D.F.', '05023', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10683, 18, 2, TO_DATE('09/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 4.4, 'Du monde entier', '67, rue des Cinquante Otages',
        'Nantes', '44000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10684, 56, 3, TO_DATE('09/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 145.63, 'Ottilies Käseladen', 'Mehrheimerstr. 369',
        'Köln', '50739', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10685, 31, 4, TO_DATE('09/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 33.75, 'Gourmet Lanchonetes', 'Av. Brasil, 442',
        'Campinas', 'SP', '04876-786', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10686, 59, 2, TO_DATE('09/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 96.5, 'Piccolo und mehr', 'Geislweg 14',
        'Salzburg', '5020', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_COUNTRY)
VALUES (10687, 37, 9, TO_DATE('09/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 296.43, 'Hungry Owl All-Night Grocers', '8 Johnstown Road',
        'Cork', 'Co. Cork', 'Ireland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10688, 83, 4, TO_DATE('10/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 299.09, 'Vaffeljernet', 'Smagsløget 45',
        'Århus', '8200', 'Denmark');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10689, 5, 1, TO_DATE('10/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 13.42, 'Berglunds snabbköp', 'Berguvsvägen  8',
        'Luleå', 'S-958 22', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10690, 34, 1, TO_DATE('10/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 15.8, 'Hanari Carnes', 'Rua do Paço, 67',
        'Rio de Janeiro', 'RJ', '05454-876', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11072, 20, 4, TO_DATE('05/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        2, 258.64, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11073, 58, 2, TO_DATE('05/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        2, 24.95, 'Pericles Comidas clásicas', 'Calle Dr. Jorge Cash 321',
        'México D.F.', '05033', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11074, 73, 7, TO_DATE('05/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        2, 18.44, 'Simons bistro', 'Vinbæltet 34',
        'København', '1734', 'Denmark');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11075, 68, 8, TO_DATE('05/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        2, 6.19, 'Richter Supermarkt', 'Starenweg 5',
        'Genève', '1204', 'Switzerland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11076, 9, 4, TO_DATE('05/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        2, 38.28, 'Bon app''', '12, rue des Bouchers',
        'Marseille', '13008', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11077, 65, 1, TO_DATE('05/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        2, 8.53, 'Rattlesnake Canyon Grocery', '2817 Milton Dr.',
        'Albuquerque', 'NM', '87110', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10700, 71, 3, TO_DATE('10/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 65.1, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_COUNTRY)
VALUES (10701, 37, 6, TO_DATE('10/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 220.31, 'Hungry Owl All-Night Grocers', '8 Johnstown Road',
        'Cork', 'Co. Cork', 'Ireland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10702, 1, 4, TO_DATE('10/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 23.94, 'Alfreds Futterkiste', 'Obere Str. 57',
        'Berlin', '12209', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10703, 24, 6, TO_DATE('10/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 152.3, 'Folk och fä HB', 'Åkergatan 24',
        'Bräcke', 'S-844 67', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10704, 62, 6, TO_DATE('10/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 4.78, 'Queen Cozinha', 'Alameda dos Canàrios, 891',
        'São Paulo', 'SP', '05487-020', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10705, 35, 9, TO_DATE('10/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 3.52, 'HILARIÓN-Abastos', 'Carrera 22 con Ave. Carlos Soublette #8-35',
        'San Cristóbal', 'Táchira', '5022', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10706, 55, 8, TO_DATE('10/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 135.63, 'Old World Delicatessen', '2743 Bering St.',
        'Anchorage', 'AK', '99508', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10707, 4, 4, TO_DATE('10/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 21.74, 'Around the Horn', 'Brook Farm
Stratford St. Mary',
        'Colchester', 'Essex', 'CO7 6JX', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10708, 77, 6, TO_DATE('10/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 2.96, 'The Big Cheese', '89 Jefferson Way
Suite 2',
        'Portland', 'OR', '97201', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10709, 31, 1, TO_DATE('10/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 210.8, 'Gourmet Lanchonetes', 'Av. Brasil, 442',
        'Campinas', 'SP', '04876-786', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10710, 27, 1, TO_DATE('10/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 4.98, 'Franchi S.p.A.', 'Via Monte Bianco 34',
        'Torino', '10100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10711, 71, 5, TO_DATE('10/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 52.41, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_COUNTRY)
VALUES (10712, 37, 3, TO_DATE('10/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 89.93, 'Hungry Owl All-Night Grocers', '8 Johnstown Road',
        'Cork', 'Co. Cork', 'Ireland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10713, 71, 1, TO_DATE('10/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 167.05, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10714, 71, 5, TO_DATE('10/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 24.49, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10715, 9, 3, TO_DATE('10/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 63.2, 'Bon app''', '12, rue des Bouchers',
        'Marseille', '13008', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10716, 64, 4, TO_DATE('10/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 22.57, 'Rancho grande', 'Av. del Libertador 900',
        'Buenos Aires', '1010', 'Argentina');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10717, 25, 1, TO_DATE('10/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 59.25, 'Frankenversand', 'Berliner Platz 43',
        'München', '80805', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10718, 39, 1, TO_DATE('10/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 170.88, 'Königlich Essen', 'Maubelstr. 90',
        'Brandenburg', '14776', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10719, 45, 8, TO_DATE('10/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 51.44, 'Let''s Stop N Shop', '87 Polk St.
Suite 5',
        'San Francisco', 'CA', '94117', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10720, 61, 8, TO_DATE('10/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 9.53, 'Que Delícia', 'Rua da Panificadora, 12',
        'Rio de Janeiro', 'RJ', '02389-673', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10721, 63, 5, TO_DATE('10/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('10/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 48.92, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10722, 71, 8, TO_DATE('10/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 74.58, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10723, 89, 3, TO_DATE('10/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 21.72, 'White Clover Markets', '1029 - 12th Ave. S.',
        'Seattle', 'WA', '98124', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10724, 51, 8, TO_DATE('10/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 57.75, 'Mère Paillarde', '43 rue St. Laurent',
        'Montréal', 'Québec', 'H1J 1C3', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10725, 21, 4, TO_DATE('10/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 10.83, 'Familia Arquibaldo', 'Rua Orós, 92',
        'São Paulo', 'SP', '05442-030', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10726, 19, 4, TO_DATE('11/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 16.56, 'Eastern Connection', '35 King George',
        'London', 'WX3 6FW', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10727, 66, 2, TO_DATE('11/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 89.9, 'Reggiani Caseifici', 'Strada Provinciale 124',
        'Reggio Emilia', '42100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10728, 62, 4, TO_DATE('11/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 58.33, 'Queen Cozinha', 'Alameda dos Canàrios, 891',
        'São Paulo', 'SP', '05487-020', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10729, 47, 8, TO_DATE('11/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 141.06, 'LINO-Delicateses', 'Ave. 5 de Mayo Porlamar',
        'I. de Margarita', 'Nueva Esparta', '4980', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10730, 9, 5, TO_DATE('11/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 20.12, 'Bon app''', '12, rue des Bouchers',
        'Marseille', '13008', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10731, 14, 7, TO_DATE('11/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 96.65, 'Chop-suey Chinese', 'Hauptstr. 31',
        'Bern', '3012', 'Switzerland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10732, 9, 3, TO_DATE('11/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 16.97, 'Bon app''', '12, rue des Bouchers',
        'Marseille', '13008', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10733, 5, 1, TO_DATE('11/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 110.11, 'Berglunds snabbköp', 'Berguvsvägen  8',
        'Luleå', 'S-958 22', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10734, 31, 2, TO_DATE('11/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 1.63, 'Gourmet Lanchonetes', 'Av. Brasil, 442',
        'Campinas', 'SP', '04876-786', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10735, 45, 6, TO_DATE('11/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 45.97, 'Let''s Stop N Shop', '87 Polk St.
Suite 5',
        'San Francisco', 'CA', '94117', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_COUNTRY)
VALUES (10736, 37, 9, TO_DATE('11/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 44.1, 'Hungry Owl All-Night Grocers', '8 Johnstown Road',
        'Cork', 'Co. Cork', 'Ireland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10737, 85, 2, TO_DATE('11/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 7.79, 'Vins et alcools Chevalier', '59 rue de l''Abbaye',
        'Reims', '51100', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10738, 74, 2, TO_DATE('11/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 2.91, 'Spécialités du monde', '25, rue Lauriston',
        'Paris', '75016', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10739, 85, 3, TO_DATE('11/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 11.08, 'Vins et alcools Chevalier', '59 rue de l''Abbaye',
        'Reims', '51100', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10740, 89, 4, TO_DATE('11/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 81.88, 'White Clover Markets', '1029 - 12th Ave. S.',
        'Seattle', 'WA', '98124', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10741, 4, 4, TO_DATE('11/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('11/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 10.96, 'Around the Horn', 'Brook Farm
Stratford St. Mary',
        'Colchester', 'Essex', 'CO7 6JX', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10742, 10, 3, TO_DATE('11/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 243.73, 'Bottom-Dollar Markets', '23 Tsawassen Blvd.',
        'Tsawassen', 'BC', 'T2F 8M4', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10743, 4, 1, TO_DATE('11/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 23.72, 'Around the Horn', 'Brook Farm
Stratford St. Mary',
        'Colchester', 'Essex', 'CO7 6JX', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10744, 83, 6, TO_DATE('11/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 69.19, 'Vaffeljernet', 'Smagsløget 45',
        'Århus', '8200', 'Denmark');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10745, 63, 9, TO_DATE('11/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 3.52, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10746, 14, 1, TO_DATE('11/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 31.43, 'Chop-suey Chinese', 'Hauptstr. 31',
        'Bern', '3012', 'Switzerland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10747, 59, 6, TO_DATE('11/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 117.33, 'Piccolo und mehr', 'Geislweg 14',
        'Salzburg', '5020', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10748, 71, 3, TO_DATE('11/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 232.55, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10749, 38, 4, TO_DATE('11/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 61.53, 'Island Trading', 'Garden House
Crowther Way',
        'Cowes', 'Isle of Wight', 'PO31 7PJ', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10750, 87, 9, TO_DATE('11/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 79.3, 'Wartian Herkku', 'Torikatu 38',
        'Oulu', '90110', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10751, 68, 3, TO_DATE('11/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 130.79, 'Richter Supermarkt', 'Starenweg 5',
        'Genève', '1204', 'Switzerland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10752, 53, 2, TO_DATE('11/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 1.39, 'North/South', 'South House
300 Queensbridge',
        'London', 'SW7 1RZ', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10753, 27, 3, TO_DATE('11/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 7.7, 'Franchi S.p.A.', 'Via Monte Bianco 34',
        'Torino', '10100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10754, 49, 6, TO_DATE('11/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 2.38, 'Magazzini Alimentari Riuniti', 'Via Ludovico il Moro 22',
        'Bergamo', '24100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10755, 9, 4, TO_DATE('11/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('11/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 16.71, 'Bon app''', '12, rue des Bouchers',
        'Marseille', '13008', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10756, 75, 8, TO_DATE('11/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 73.21, 'Split Rail Beer & Ale', 'P.O. Box 555',
        'Lander', 'WY', '82520', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10757, 71, 6, TO_DATE('11/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 8.19, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10758, 68, 3, TO_DATE('11/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 138.17, 'Richter Supermarkt', 'Starenweg 5',
        'Genève', '1204', 'Switzerland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10759, 2, 3, TO_DATE('11/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 11.99, 'Ana Trujillo Emparedados y helados', 'Avda. de la Constitución 2222',
        'México D.F.', '05021', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10760, 50, 4, TO_DATE('12/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 155.64, 'Maison Dewey', 'Rue Joseph-Bens 532',
        'Bruxelles', 'B-1180', 'Belgium');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10761, 65, 5, TO_DATE('12/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 18.66, 'Rattlesnake Canyon Grocery', '2817 Milton Dr.',
        'Albuquerque', 'NM', '87110', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10762, 24, 3, TO_DATE('12/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 328.74, 'Folk och fä HB', 'Åkergatan 24',
        'Bräcke', 'S-844 67', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10763, 23, 3, TO_DATE('12/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 37.35, 'Folies gourmandes', '184, chaussée de Tournai',
        'Lille', '59000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10764, 20, 6, TO_DATE('12/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 145.45, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10765, 63, 3, TO_DATE('12/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 42.74, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10766, 56, 4, TO_DATE('12/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 157.55, 'Ottilies Käseladen', 'Mehrheimerstr. 369',
        'Köln', '50739', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10767, 76, 4, TO_DATE('12/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 1.59, 'Suprêmes délices', 'Boulevard Tirou, 255',
        'Charleroi', 'B-6000', 'Belgium');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10768, 4, 3, TO_DATE('12/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 146.32, 'Around the Horn', 'Brook Farm
Stratford St. Mary',
        'Colchester', 'Essex', 'CO7 6JX', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10769, 83, 3, TO_DATE('12/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 65.06, 'Vaffeljernet', 'Smagsløget 45',
        'Århus', '8200', 'Denmark');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10770, 34, 8, TO_DATE('12/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 5.32, 'Hanari Carnes', 'Rua do Paço, 67',
        'Rio de Janeiro', 'RJ', '05454-876', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10771, 20, 9, TO_DATE('12/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 11.19, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10772, 44, 3, TO_DATE('12/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 91.28, 'Lehmanns Marktstand', 'Magazinweg 7',
        'Frankfurt a.M. ', '60528', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10773, 20, 1, TO_DATE('12/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 96.43, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10774, 24, 4, TO_DATE('12/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 48.2, 'Folk och fä HB', 'Åkergatan 24',
        'Bräcke', 'S-844 67', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10775, 78, 7, TO_DATE('12/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 20.25, 'The Cracker Box', '55 Grizzly Peak Rd.',
        'Butte', 'MT', '59801', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10776, 20, 1, TO_DATE('12/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 351.53, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10777, 31, 7, TO_DATE('12/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 3.01, 'Gourmet Lanchonetes', 'Av. Brasil, 442',
        'Campinas', 'SP', '04876-786', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10778, 5, 3, TO_DATE('12/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 6.79, 'Berglunds snabbköp', 'Berguvsvägen  8',
        'Luleå', 'S-958 22', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10779, 52, 3, TO_DATE('12/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 58.13, 'Morgenstern Gesundkost', 'Heerstr. 22',
        'Leipzig', '04179', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10780, 46, 2, TO_DATE('12/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('12/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 42.13, 'LILA-Supermercado', 'Carrera 52 con Ave. Bolívar #65-98 Llano Largo',
        'Barquisimeto', 'Lara', '3508', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10781, 87, 2, TO_DATE('12/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 73.16, 'Wartian Herkku', 'Torikatu 38',
        'Oulu', '90110', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10782, 12, 9, TO_DATE('12/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 1.1, 'Cactus Comidas para llevar', 'Cerrito 333',
        'Buenos Aires', '1010', 'Argentina');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10783, 34, 4, TO_DATE('12/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/15/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 124.98, 'Hanari Carnes', 'Rua do Paço, 67',
        'Rio de Janeiro', 'RJ', '05454-876', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10784, 49, 4, TO_DATE('12/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/15/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 70.09, 'Magazzini Alimentari Riuniti', 'Via Ludovico il Moro 22',
        'Bergamo', '24100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10785, 33, 1, TO_DATE('12/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/15/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 1.51, 'GROSELLA-Restaurante', '5ª Ave. Los Palos Grandes',
        'Caracas', 'DF', '1081', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10786, 62, 8, TO_DATE('12/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 110.87, 'Queen Cozinha', 'Alameda dos Canàrios, 891',
        'São Paulo', 'SP', '05487-020', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10787, 41, 2, TO_DATE('12/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 249.93, 'La maison d''Asie', '1 rue Alsace-Lorraine',
        'Toulouse', '31000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10788, 63, 1, TO_DATE('12/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 42.7, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10789, 23, 1, TO_DATE('12/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 100.6, 'Folies gourmandes', '184, chaussée de Tournai',
        'Lille', '59000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10790, 31, 6, TO_DATE('12/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 28.23, 'Gourmet Lanchonetes', 'Av. Brasil, 442',
        'Campinas', 'SP', '04876-786', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10791, 25, 6, TO_DATE('12/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 16.85, 'Frankenversand', 'Berliner Platz 43',
        'München', '80805', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10792, 91, 1, TO_DATE('12/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 23.79, 'Wolski Zajazd', 'ul. Filtrowa 68',
        'Warszawa', '01-012', 'Poland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10793, 4, 3, TO_DATE('12/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 4.52, 'Around the Horn', 'Brook Farm
Stratford St. Mary',
        'Colchester', 'Essex', 'CO7 6JX', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10794, 61, 6, TO_DATE('12/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 21.49, 'Que Delícia', 'Rua da Panificadora, 12',
        'Rio de Janeiro', 'RJ', '02389-673', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10795, 20, 8, TO_DATE('12/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 126.66, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10796, 35, 3, TO_DATE('12/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/22/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 26.52, 'HILARIÓN-Abastos', 'Carrera 22 con Ave. Carlos Soublette #8-35',
        'San Cristóbal', 'Táchira', '5022', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10797, 17, 7, TO_DATE('12/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/22/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 33.35, 'Drachenblut Delikatessen', 'Walserweg 21',
        'Aachen', '52066', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10798, 38, 2, TO_DATE('12/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 2.33, 'Island Trading', 'Garden House
Crowther Way',
        'Cowes', 'Isle of Wight', 'PO31 7PJ', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10799, 39, 9, TO_DATE('12/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 30.76, 'Königlich Essen', 'Maubelstr. 90',
        'Brandenburg', '14776', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10800, 72, 1, TO_DATE('12/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 137.44, 'Seven Seas Imports', '90 Wadhurst Rd.',
        'London', 'OX15 4NB', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10801, 8, 4, TO_DATE('12/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('12/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 97.09, 'Bólido Comidas preparadas', 'C/ Araquil, 67',
        'Madrid', '28023', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10802, 73, 4, TO_DATE('12/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 257.26, 'Simons bistro', 'Vinbæltet 34',
        'København', '1734', 'Denmark');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10803, 88, 4, TO_DATE('12/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 55.23, 'Wellington Importadora', 'Rua do Mercado, 12',
        'Resende', 'SP', '08737-363', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10804, 72, 6, TO_DATE('12/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 27.33, 'Seven Seas Imports', '90 Wadhurst Rd.',
        'London', 'OX15 4NB', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10805, 77, 2, TO_DATE('12/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 237.34, 'The Big Cheese', '89 Jefferson Way
Suite 2',
        'Portland', 'OR', '97201', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10806, 84, 3, TO_DATE('12/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/28/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 22.11, 'Victuailles en stock', '2, rue du Commerce',
        'Lyon', '69004', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10807, 27, 4, TO_DATE('12/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/28/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 1.36, 'Franchi S.p.A.', 'Via Monte Bianco 34',
        'Torino', '10100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10808, 55, 2, TO_DATE('01/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 45.53, 'Old World Delicatessen', '2743 Bering St.',
        'Anchorage', 'AK', '99508', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10809, 88, 7, TO_DATE('01/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 4.87, 'Wellington Importadora', 'Rua do Mercado, 12',
        'Resende', 'SP', '08737-363', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10810, 42, 2, TO_DATE('01/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 4.33, 'Laughing Bacchus Wine Cellars', '2319 Elm St.',
        'Vancouver', 'BC', 'V3F 2K1', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10811, 47, 8, TO_DATE('01/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 31.22, 'LINO-Delicateses', 'Ave. 5 de Mayo Porlamar',
        'I. de Margarita', 'Nueva Esparta', '4980', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10812, 66, 5, TO_DATE('01/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 59.78, 'Reggiani Caseifici', 'Strada Provinciale 124',
        'Reggio Emilia', '42100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10813, 67, 1, TO_DATE('01/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 47.38, 'Ricardo Adocicados', 'Av. Copacabana, 267',
        'Rio de Janeiro', 'RJ', '02389-890', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10814, 84, 3, TO_DATE('01/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 130.94, 'Victuailles en stock', '2, rue du Commerce',
        'Lyon', '69004', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10815, 71, 2, TO_DATE('01/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 14.62, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10816, 32, 4, TO_DATE('01/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 719.78, 'Great Lakes Food Market', '2732 Baker Blvd.',
        'Eugene', 'OR', '97403', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10817, 39, 3, TO_DATE('01/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 306.07, 'Königlich Essen', 'Maubelstr. 90',
        'Brandenburg', '14776', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10818, 49, 7, TO_DATE('01/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 65.48, 'Magazzini Alimentari Riuniti', 'Via Ludovico il Moro 22',
        'Bergamo', '24100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10819, 12, 2, TO_DATE('01/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 19.76, 'Cactus Comidas para llevar', 'Cerrito 333',
        'Buenos Aires', '1010', 'Argentina');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10820, 65, 3, TO_DATE('01/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 37.52, 'Rattlesnake Canyon Grocery', '2817 Milton Dr.',
        'Albuquerque', 'NM', '87110', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10821, 75, 1, TO_DATE('01/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/15/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 36.68, 'Split Rail Beer & Ale', 'P.O. Box 555',
        'Lander', 'WY', '82520', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10822, 82, 6, TO_DATE('01/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 7, 'Trail''s Head Gourmet Provisioners', '722 DaVinci Blvd.',
        'Kirkland', 'WA', '98034', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10823, 46, 5, TO_DATE('01/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 163.97, 'LILA-Supermercado', 'Carrera 52 con Ave. Bolívar #65-98 Llano Largo',
        'Barquisimeto', 'Lara', '3508', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10824, 24, 8, TO_DATE('01/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 1.23, 'Folk och fä HB', 'Åkergatan 24',
        'Bräcke', 'S-844 67', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10825, 17, 1, TO_DATE('01/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 79.25, 'Drachenblut Delikatessen', 'Walserweg 21',
        'Aachen', '52066', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10826, 7, 6, TO_DATE('01/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 7.09, 'Blondel père et fils', '24, place Kléber',
        'Strasbourg', '67000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10827, 9, 1, TO_DATE('01/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 63.54, 'Bon app''', '12, rue des Bouchers',
        'Marseille', '13008', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10828, 64, 9, TO_DATE('01/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('01/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 90.85, 'Rancho grande', 'Av. del Libertador 900',
        'Buenos Aires', '1010', 'Argentina');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10829, 38, 9, TO_DATE('01/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 154.72, 'Island Trading', 'Garden House
Crowther Way',
        'Cowes', 'Isle of Wight', 'PO31 7PJ', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10830, 81, 4, TO_DATE('01/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 81.83, 'Tradição Hipermercados', 'Av. Inês de Castro, 414',
        'São Paulo', 'SP', '05634-030', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10831, 70, 3, TO_DATE('01/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 72.19, 'Santé Gourmet', 'Erling Skakkes gate 78',
        'Stavern', '4110', 'Norway');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10832, 41, 2, TO_DATE('01/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 43.26, 'La maison d''Asie', '1 rue Alsace-Lorraine',
        'Toulouse', '31000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10833, 56, 6, TO_DATE('01/15/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 71.49, 'Ottilies Käseladen', 'Mehrheimerstr. 369',
        'Köln', '50739', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10834, 81, 1, TO_DATE('01/15/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 29.78, 'Tradição Hipermercados', 'Av. Inês de Castro, 414',
        'São Paulo', 'SP', '05634-030', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10835, 1, 1, TO_DATE('01/15/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 69.53, 'Alfreds Futterkiste', 'Obere Str. 57',
        'Berlin', '12209', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10836, 20, 7, TO_DATE('01/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 411.88, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10837, 5, 9, TO_DATE('01/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 13.32, 'Berglunds snabbköp', 'Berguvsvägen  8',
        'Luleå', 'S-958 22', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10838, 47, 3, TO_DATE('01/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 59.28, 'LINO-Delicateses', 'Ave. 5 de Mayo Porlamar',
        'I. de Margarita', 'Nueva Esparta', '4980', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10839, 81, 3, TO_DATE('01/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/22/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 35.43, 'Tradição Hipermercados', 'Av. Inês de Castro, 414',
        'São Paulo', 'SP', '05634-030', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10840, 47, 4, TO_DATE('01/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 2.71, 'LINO-Delicateses', 'Ave. 5 de Mayo Porlamar',
        'I. de Margarita', 'Nueva Esparta', '4980', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10841, 76, 5, TO_DATE('01/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 424.3, 'Suprêmes délices', 'Boulevard Tirou, 255',
        'Charleroi', 'B-6000', 'Belgium');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10842, 80, 1, TO_DATE('01/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 54.42, 'Tortuga Restaurante', 'Avda. Azteca 123',
        'México D.F.', '05033', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10843, 84, 4, TO_DATE('01/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 9.26, 'Victuailles en stock', '2, rue du Commerce',
        'Lyon', '69004', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10844, 59, 8, TO_DATE('01/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 25.22, 'Piccolo und mehr', 'Geislweg 14',
        'Salzburg', '5020', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10845, 63, 8, TO_DATE('01/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 212.98, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10846, 76, 2, TO_DATE('01/22/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 56.46, 'Suprêmes délices', 'Boulevard Tirou, 255',
        'Charleroi', 'B-6000', 'Belgium');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10847, 71, 4, TO_DATE('01/22/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 487.57, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10848, 16, 7, TO_DATE('01/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 38.24, 'Consolidated Holdings', 'Berkeley Gardens
12  Brewery ',
        'London', 'WX1 6LT', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10849, 39, 9, TO_DATE('01/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 0.56, 'Königlich Essen', 'Maubelstr. 90',
        'Brandenburg', '14776', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10850, 84, 1, TO_DATE('01/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 49.19, 'Victuailles en stock', '2, rue du Commerce',
        'Lyon', '69004', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10851, 67, 5, TO_DATE('01/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 160.55, 'Ricardo Adocicados', 'Av. Copacabana, 267',
        'Rio de Janeiro', 'RJ', '02389-890', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10852, 65, 8, TO_DATE('01/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('01/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 174.05, 'Rattlesnake Canyon Grocery', '2817 Milton Dr.',
        'Albuquerque', 'NM', '87110', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10853, 6, 9, TO_DATE('01/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 53.83, 'Blauer See Delikatessen', 'Forsterstr. 57',
        'Mannheim', '68306', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10854, 20, 3, TO_DATE('01/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 100.22, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10855, 55, 3, TO_DATE('01/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 170.97, 'Old World Delicatessen', '2743 Bering St.',
        'Anchorage', 'AK', '99508', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10856, 3, 3, TO_DATE('01/28/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/25/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 58.43, 'Antonio Moreno Taquería', 'Mataderos  2312',
        'México D.F.', '05023', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10857, 5, 8, TO_DATE('01/28/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/25/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 188.85, 'Berglunds snabbköp', 'Berguvsvägen  8',
        'Luleå', 'S-958 22', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10858, 40, 2, TO_DATE('01/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 52.51, 'La corne d''abondance', '67, avenue de l''Europe',
        'Versailles', '78000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10859, 25, 1, TO_DATE('01/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 76.1, 'Frankenversand', 'Berliner Platz 43',
        'München', '80805', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10860, 26, 3, TO_DATE('01/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 19.26, 'France restauration', '54, rue Royale',
        'Nantes', '44000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10861, 89, 4, TO_DATE('01/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 14.93, 'White Clover Markets', '1029 - 12th Ave. S.',
        'Seattle', 'WA', '98124', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10862, 44, 8, TO_DATE('01/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 53.23, 'Lehmanns Marktstand', 'Magazinweg 7',
        'Frankfurt a.M. ', '60528', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10863, 35, 4, TO_DATE('02/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 30.26, 'HILARIÓN-Abastos', 'Carrera 22 con Ave. Carlos Soublette #8-35',
        'San Cristóbal', 'Táchira', '5022', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10864, 4, 4, TO_DATE('02/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 3.04, 'Around the Horn', 'Brook Farm
Stratford St. Mary',
        'Colchester', 'Essex', 'CO7 6JX', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10865, 63, 2, TO_DATE('02/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('02/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 348.14, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10866, 5, 5, TO_DATE('02/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 109.11, 'Berglunds snabbköp', 'Berguvsvägen  8',
        'Luleå', 'S-958 22', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10867, 48, 6, TO_DATE('02/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 1.93, 'Lonesome Pine Restaurant', '89 Chiaroscuro Rd.',
        'Portland', 'OR', '97219', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10868, 62, 7, TO_DATE('02/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 191.27, 'Queen Cozinha', 'Alameda dos Canàrios, 891',
        'São Paulo', 'SP', '05487-020', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10869, 72, 5, TO_DATE('02/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 143.28, 'Seven Seas Imports', '90 Wadhurst Rd.',
        'London', 'OX15 4NB', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10870, 91, 5, TO_DATE('02/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 12.04, 'Wolski Zajazd', 'ul. Filtrowa 68',
        'Warszawa', '01-012', 'Poland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10871, 9, 9, TO_DATE('02/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 112.27, 'Bon app''', '12, rue des Bouchers',
        'Marseille', '13008', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10872, 30, 5, TO_DATE('02/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 175.32, 'Godos Cocina Típica', 'C/ Romero, 33',
        'Sevilla', '41101', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10873, 90, 4, TO_DATE('02/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 0.82, 'Wilman Kala', 'Keskuskatu 45',
        'Helsinki', '21240', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10874, 30, 5, TO_DATE('02/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 19.58, 'Godos Cocina Típica', 'C/ Romero, 33',
        'Sevilla', '41101', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10875, 5, 4, TO_DATE('02/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 32.37, 'Berglunds snabbköp', 'Berguvsvägen  8',
        'Luleå', 'S-958 22', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10876, 9, 7, TO_DATE('02/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 60.42, 'Bon app''', '12, rue des Bouchers',
        'Marseille', '13008', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10877, 67, 1, TO_DATE('02/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 38.06, 'Ricardo Adocicados', 'Av. Copacabana, 267',
        'Rio de Janeiro', 'RJ', '02389-890', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10878, 63, 4, TO_DATE('02/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 46.69, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10879, 90, 3, TO_DATE('02/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 8.5, 'Wilman Kala', 'Keskuskatu 45',
        'Helsinki', '21240', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10880, 24, 7, TO_DATE('02/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 88.01, 'Folk och fä HB', 'Åkergatan 24',
        'Bräcke', 'S-844 67', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10881, 12, 4, TO_DATE('02/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 2.84, 'Cactus Comidas para llevar', 'Cerrito 333',
        'Buenos Aires', '1010', 'Argentina');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10882, 71, 4, TO_DATE('02/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 23.1, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10883, 48, 8, TO_DATE('02/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 0.53, 'Lonesome Pine Restaurant', '89 Chiaroscuro Rd.',
        'Portland', 'OR', '97219', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10884, 45, 4, TO_DATE('02/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 90.97, 'Let''s Stop N Shop', '87 Polk St.
Suite 5',
        'San Francisco', 'CA', '94117', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10885, 76, 6, TO_DATE('02/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 5.64, 'Suprêmes délices', 'Boulevard Tirou, 255',
        'Charleroi', 'B-6000', 'Belgium');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10886, 34, 1, TO_DATE('02/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 4.99, 'Hanari Carnes', 'Rua do Paço, 67',
        'Rio de Janeiro', 'RJ', '05454-876', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10887, 29, 8, TO_DATE('02/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 1.25, 'Galería del gastronómo', 'Rambla de Cataluña, 23',
        'Barcelona', '8022', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10888, 30, 1, TO_DATE('02/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 51.87, 'Godos Cocina Típica', 'C/ Romero, 33',
        'Sevilla', '41101', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10889, 65, 9, TO_DATE('02/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 280.61, 'Rattlesnake Canyon Grocery', '2817 Milton Dr.',
        'Albuquerque', 'NM', '87110', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10890, 18, 7, TO_DATE('02/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 32.76, 'Du monde entier', '67, rue des Cinquante Otages',
        'Nantes', '44000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10891, 44, 7, TO_DATE('02/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 20.37, 'Lehmanns Marktstand', 'Magazinweg 7',
        'Frankfurt a.M. ', '60528', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10892, 50, 4, TO_DATE('02/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 120.27, 'Maison Dewey', 'Rue Joseph-Bens 532',
        'Bruxelles', 'B-1180', 'Belgium');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10893, 39, 9, TO_DATE('02/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 77.78, 'Königlich Essen', 'Maubelstr. 90',
        'Brandenburg', '14776', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10894, 71, 1, TO_DATE('02/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 116.13, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10895, 20, 3, TO_DATE('02/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 162.75, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10896, 50, 7, TO_DATE('02/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 32.45, 'Maison Dewey', 'Rue Joseph-Bens 532',
        'Bruxelles', 'B-1180', 'Belgium');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_COUNTRY)
VALUES (10897, 37, 3, TO_DATE('02/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/25/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 603.54, 'Hungry Owl All-Night Grocers', '8 Johnstown Road',
        'Cork', 'Co. Cork', 'Ireland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10898, 54, 4, TO_DATE('02/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 1.27, 'Océano Atlántico Ltda.', 'Ing. Gustavo Moncada 8585
Piso 20-A',
        'Buenos Aires', '1010', 'Argentina');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10899, 46, 5, TO_DATE('02/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 1.21, 'LILA-Supermercado', 'Carrera 52 con Ave. Bolívar #65-98 Llano Largo',
        'Barquisimeto', 'Lara', '3508', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10900, 88, 1, TO_DATE('02/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 1.66, 'Wellington Importadora', 'Rua do Mercado, 12',
        'Resende', 'SP', '08737-363', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10901, 35, 4, TO_DATE('02/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 62.09, 'HILARIÓN-Abastos', 'Carrera 22 con Ave. Carlos Soublette #8-35',
        'San Cristóbal', 'Táchira', '5022', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10902, 24, 1, TO_DATE('02/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 44.15, 'Folk och fä HB', 'Åkergatan 24',
        'Bräcke', 'S-844 67', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10903, 34, 3, TO_DATE('02/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 36.71, 'Hanari Carnes', 'Rua do Paço, 67',
        'Rio de Janeiro', 'RJ', '05454-876', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10904, 89, 3, TO_DATE('02/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 162.95, 'White Clover Markets', '1029 - 12th Ave. S.',
        'Seattle', 'WA', '98124', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10905, 88, 9, TO_DATE('02/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 13.72, 'Wellington Importadora', 'Rua do Mercado, 12',
        'Resende', 'SP', '08737-363', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10906, 91, 4, TO_DATE('02/25/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 26.29, 'Wolski Zajazd', 'ul. Filtrowa 68',
        'Warszawa', '01-012', 'Poland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10907, 74, 6, TO_DATE('02/25/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/25/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('02/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 9.19, 'Spécialités du monde', '25, rue Lauriston',
        'Paris', '75016', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10908, 66, 4, TO_DATE('02/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 32.96, 'Reggiani Caseifici', 'Strada Provinciale 124',
        'Reggio Emilia', '42100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10909, 70, 1, TO_DATE('02/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 53.05, 'Santé Gourmet', 'Erling Skakkes gate 78',
        'Stavern', '4110', 'Norway');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10910, 90, 1, TO_DATE('02/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 38.11, 'Wilman Kala', 'Keskuskatu 45',
        'Helsinki', '21240', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10911, 30, 3, TO_DATE('02/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 38.19, 'Godos Cocina Típica', 'C/ Romero, 33',
        'Sevilla', '41101', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_COUNTRY)
VALUES (10912, 37, 2, TO_DATE('02/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 580.91, 'Hungry Owl All-Night Grocers', '8 Johnstown Road',
        'Cork', 'Co. Cork', 'Ireland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10913, 62, 4, TO_DATE('02/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 33.05, 'Queen Cozinha', 'Alameda dos Canàrios, 891',
        'São Paulo', 'SP', '05487-020', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10914, 62, 6, TO_DATE('02/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 21.19, 'Queen Cozinha', 'Alameda dos Canàrios, 891',
        'São Paulo', 'SP', '05487-020', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10915, 80, 2, TO_DATE('02/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 3.51, 'Tortuga Restaurante', 'Avda. Azteca 123',
        'México D.F.', '05033', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10916, 64, 1, TO_DATE('02/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 63.77, 'Rancho grande', 'Av. del Libertador 900',
        'Buenos Aires', '1010', 'Argentina');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10917, 69, 4, TO_DATE('03/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 8.29, 'Romero y tomillo', 'Gran Vía, 1',
        'Madrid', '28001', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10918, 10, 3, TO_DATE('03/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 48.83, 'Bottom-Dollar Markets', '23 Tsawassen Blvd.',
        'Tsawassen', 'BC', 'T2F 8M4', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10919, 47, 2, TO_DATE('03/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 19.8, 'LINO-Delicateses', 'Ave. 5 de Mayo Porlamar',
        'I. de Margarita', 'Nueva Esparta', '4980', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10920, 4, 4, TO_DATE('03/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/31/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 29.61, 'Around the Horn', 'Brook Farm
Stratford St. Mary',
        'Colchester', 'Essex', 'CO7 6JX', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10921, 83, 1, TO_DATE('03/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 176.48, 'Vaffeljernet', 'Smagsløget 45',
        'Århus', '8200', 'Denmark');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10922, 34, 5, TO_DATE('03/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/31/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 62.74, 'Hanari Carnes', 'Rua do Paço, 67',
        'Rio de Janeiro', 'RJ', '05454-876', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10923, 41, 7, TO_DATE('03/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 68.26, 'La maison d''Asie', '1 rue Alsace-Lorraine',
        'Toulouse', '31000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10924, 5, 3, TO_DATE('03/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 151.52, 'Berglunds snabbköp', 'Berguvsvägen  8',
        'Luleå', 'S-958 22', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10925, 34, 3, TO_DATE('03/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 2.27, 'Hanari Carnes', 'Rua do Paço, 67',
        'Rio de Janeiro', 'RJ', '05454-876', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10926, 2, 4, TO_DATE('03/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 39.92, 'Ana Trujillo Emparedados y helados', 'Avda. de la Constitución 2222',
        'México D.F.', '05021', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10927, 40, 4, TO_DATE('03/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 19.79, 'La corne d''abondance', '67, avenue de l''Europe',
        'Versailles', '78000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10928, 29, 1, TO_DATE('03/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 1.36, 'Galería del gastronómo', 'Rambla de Cataluña, 23',
        'Barcelona', '8022', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10929, 25, 6, TO_DATE('03/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 33.93, 'Frankenversand', 'Berliner Platz 43',
        'München', '80805', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10930, 76, 4, TO_DATE('03/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 15.55, 'Suprêmes délices', 'Boulevard Tirou, 255',
        'Charleroi', 'B-6000', 'Belgium');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10931, 68, 4, TO_DATE('03/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 13.6, 'Richter Supermarkt', 'Starenweg 5',
        'Genève', '1204', 'Switzerland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10932, 9, 8, TO_DATE('03/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 134.64, 'Bon app''', '12, rue des Bouchers',
        'Marseille', '13008', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10933, 38, 6, TO_DATE('03/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 54.15, 'Island Trading', 'Garden House
Crowther Way',
        'Cowes', 'Isle of Wight', 'PO31 7PJ', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10934, 44, 3, TO_DATE('03/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 32.01, 'Lehmanns Marktstand', 'Magazinweg 7',
        'Frankfurt a.M. ', '60528', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10935, 88, 4, TO_DATE('03/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 47.59, 'Wellington Importadora', 'Rua do Mercado, 12',
        'Resende', 'SP', '08737-363', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10936, 32, 3, TO_DATE('03/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 33.68, 'Great Lakes Food Market', '2732 Baker Blvd.',
        'Eugene', 'OR', '97403', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10937, 12, 7, TO_DATE('03/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 31.51, 'Cactus Comidas para llevar', 'Cerrito 333',
        'Buenos Aires', '1010', 'Argentina');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10938, 63, 3, TO_DATE('03/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 31.89, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11067, 17, 1, TO_DATE('05/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 7.98, 'Drachenblut Delikatessen', 'Walserweg 21',
        'Aachen', '52066', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11068, 62, 8, TO_DATE('05/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        2, 81.75, 'Queen Cozinha', 'Alameda dos Canàrios, 891',
        'São Paulo', 'SP', '05487-020', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11069, 80, 1, TO_DATE('05/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('05/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 15.67, 'Tortuga Restaurante', 'Avda. Azteca 123',
        'México D.F.', '05033', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11070, 44, 2, TO_DATE('05/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        1, 136, 'Lehmanns Marktstand', 'Magazinweg 7',
        'Frankfurt a.M. ', '60528', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11071, 46, 1, TO_DATE('05/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('06/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        1, 0.93, 'LILA-Supermercado', 'Carrera 52 con Ave. Bolívar #65-98 Llano Largo',
        'Barquisimeto', 'Lara', '3508', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10942, 66, 9, TO_DATE('03/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 17.95, 'Reggiani Caseifici', 'Strada Provinciale 124',
        'Reggio Emilia', '42100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10943, 11, 4, TO_DATE('03/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 2.17, 'B''s Beverages', 'Fauntleroy Circus',
        'London', 'EC2 5NT', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10944, 10, 6, TO_DATE('03/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 52.92, 'Bottom-Dollar Markets', '23 Tsawassen Blvd.',
        'Tsawassen', 'BC', 'T2F 8M4', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10945, 52, 4, TO_DATE('03/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 10.22, 'Morgenstern Gesundkost', 'Heerstr. 22',
        'Leipzig', '04179', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10946, 83, 1, TO_DATE('03/12/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 27.2, 'Vaffeljernet', 'Smagsløget 45',
        'Århus', '8200', 'Denmark');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10947, 11, 3, TO_DATE('03/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 3.26, 'B''s Beverages', 'Fauntleroy Circus',
        'London', 'EC2 5NT', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10948, 30, 3, TO_DATE('03/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 23.39, 'Godos Cocina Típica', 'C/ Romero, 33',
        'Sevilla', '41101', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10949, 10, 2, TO_DATE('03/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 74.44, 'Bottom-Dollar Markets', '23 Tsawassen Blvd.',
        'Tsawassen', 'BC', 'T2F 8M4', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10950, 49, 1, TO_DATE('03/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 2.5, 'Magazzini Alimentari Riuniti', 'Via Ludovico il Moro 22',
        'Bergamo', '24100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10951, 68, 9, TO_DATE('03/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 30.85, 'Richter Supermarkt', 'Starenweg 5',
        'Genève', '1204', 'Switzerland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10952, 1, 1, TO_DATE('03/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 40.42, 'Alfreds Futterkiste', 'Obere Str. 57',
        'Berlin', '12209', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10579, 45, 1, TO_DATE('06/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 13.73, 'Let''s Stop N Shop', '87 Polk St.
Suite 5',
        'San Francisco', 'CA', '94117', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10580, 56, 4, TO_DATE('06/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 75.89, 'Ottilies Käseladen', 'Mehrheimerstr. 369',
        'Köln', '50739', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10581, 21, 3, TO_DATE('06/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 3.01, 'Familia Arquibaldo', 'Rua Orós, 92',
        'São Paulo', 'SP', '05442-030', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10582, 6, 3, TO_DATE('06/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 27.71, 'Blauer See Delikatessen', 'Forsterstr. 57',
        'Mannheim', '68306', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10583, 87, 2, TO_DATE('06/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 7.28, 'Wartian Herkku', 'Torikatu 38',
        'Oulu', '90110', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10584, 7, 4, TO_DATE('06/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 59.14, 'Blondel père et fils', '24, place Kléber',
        'Strasbourg', '67000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10585, 88, 7, TO_DATE('07/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 13.41, 'Wellington Importadora', 'Rua do Mercado, 12',
        'Resende', 'SP', '08737-363', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10586, 66, 9, TO_DATE('07/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 0.48, 'Reggiani Caseifici', 'Strada Provinciale 124',
        'Reggio Emilia', '42100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10587, 61, 1, TO_DATE('07/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 62.52, 'Que Delícia', 'Rua da Panificadora, 12',
        'Rio de Janeiro', 'RJ', '02389-673', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10588, 63, 2, TO_DATE('07/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 194.67, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10589, 32, 8, TO_DATE('07/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 4.42, 'Great Lakes Food Market', '2732 Baker Blvd.',
        'Eugene', 'OR', '97403', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10590, 51, 4, TO_DATE('07/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 44.77, 'Mère Paillarde', '43 rue St. Laurent',
        'Montréal', 'Québec', 'H1J 1C3', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10591, 83, 1, TO_DATE('07/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('07/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 55.92, 'Vaffeljernet', 'Smagsløget 45',
        'Århus', '8200', 'Denmark');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10592, 44, 3, TO_DATE('07/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 32.1, 'Lehmanns Marktstand', 'Magazinweg 7',
        'Frankfurt a.M. ', '60528', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10593, 44, 7, TO_DATE('07/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 174.2, 'Lehmanns Marktstand', 'Magazinweg 7',
        'Frankfurt a.M. ', '60528', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10594, 55, 3, TO_DATE('07/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 5.24, 'Old World Delicatessen', '2743 Bering St.',
        'Anchorage', 'AK', '99508', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10595, 20, 2, TO_DATE('07/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 96.78, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10596, 89, 8, TO_DATE('07/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 16.34, 'White Clover Markets', '1029 - 12th Ave. S.',
        'Seattle', 'WA', '98124', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10597, 59, 7, TO_DATE('07/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 35.12, 'Piccolo und mehr', 'Geislweg 14',
        'Salzburg', '5020', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10598, 65, 1, TO_DATE('07/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 44.42, 'Rattlesnake Canyon Grocery', '2817 Milton Dr.',
        'Albuquerque', 'NM', '87110', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10599, 11, 6, TO_DATE('07/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 29.98, 'B''s Beverages', 'Fauntleroy Circus',
        'London', 'EC2 5NT', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10600, 36, 4, TO_DATE('07/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 45.13, 'Hungry Coyote Import Store', 'City Center Plaza
516 Main St.',
        'Elgin', 'OR', '97827', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10601, 35, 7, TO_DATE('07/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 58.3, 'HILARIÓN-Abastos', 'Carrera 22 con Ave. Carlos Soublette #8-35',
        'San Cristóbal', 'Táchira', '5022', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10602, 83, 8, TO_DATE('07/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 2.92, 'Vaffeljernet', 'Smagsløget 45',
        'Århus', '8200', 'Denmark');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10603, 71, 8, TO_DATE('07/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 48.77, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10604, 28, 1, TO_DATE('07/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 7.46, 'Furia Bacalhau e Frutos do Mar', 'Jardim das rosas n. 32',
        'Lisboa', '1675', 'Portugal');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10605, 51, 1, TO_DATE('07/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 379.13, 'Mère Paillarde', '43 rue St. Laurent',
        'Montréal', 'Québec', 'H1J 1C3', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10606, 81, 4, TO_DATE('07/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 79.4, 'Tradição Hipermercados', 'Av. Inês de Castro, 414',
        'São Paulo', 'SP', '05634-030', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10607, 71, 5, TO_DATE('07/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 200.24, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10608, 79, 4, TO_DATE('07/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 27.79, 'Toms Spezialitäten', 'Luisenstr. 48',
        'Münster', '44087', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10609, 18, 7, TO_DATE('07/24/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('07/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 1.85, 'Du monde entier', '67, rue des Cinquante Otages',
        'Nantes', '44000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10610, 41, 8, TO_DATE('07/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 26.78, 'La maison d''Asie', '1 rue Alsace-Lorraine',
        'Toulouse', '31000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10611, 91, 6, TO_DATE('07/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 80.65, 'Wolski Zajazd', 'ul. Filtrowa 68',
        'Warszawa', '01-012', 'Poland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10612, 71, 1, TO_DATE('07/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 544.08, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10613, 35, 4, TO_DATE('07/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 8.11, 'HILARIÓN-Abastos', 'Carrera 22 con Ave. Carlos Soublette #8-35',
        'San Cristóbal', 'Táchira', '5022', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10614, 6, 8, TO_DATE('07/29/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 1.93, 'Blauer See Delikatessen', 'Forsterstr. 57',
        'Mannheim', '68306', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10615, 90, 2, TO_DATE('07/30/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 0.75, 'Wilman Kala', 'Keskuskatu 45',
        'Helsinki', '21240', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10616, 32, 1, TO_DATE('07/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 116.53, 'Great Lakes Food Market', '2732 Baker Blvd.',
        'Eugene', 'OR', '97403', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10617, 32, 4, TO_DATE('07/31/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('08/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 18.53, 'Great Lakes Food Market', '2732 Baker Blvd.',
        'Eugene', 'OR', '97403', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10618, 51, 1, TO_DATE('08/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 154.68, 'Mère Paillarde', '43 rue St. Laurent',
        'Montréal', 'Québec', 'H1J 1C3', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10619, 51, 3, TO_DATE('08/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 91.05, 'Mère Paillarde', '43 rue St. Laurent',
        'Montréal', 'Québec', 'H1J 1C3', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10620, 42, 2, TO_DATE('08/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 0.94, 'Laughing Bacchus Wine Cellars', '2319 Elm St.',
        'Vancouver', 'BC', 'V3F 2K1', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10621, 38, 4, TO_DATE('08/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 23.73, 'Island Trading', 'Garden House
Crowther Way',
        'Cowes', 'Isle of Wight', 'PO31 7PJ', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10622, 67, 4, TO_DATE('08/06/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 50.97, 'Ricardo Adocicados', 'Av. Copacabana, 267',
        'Rio de Janeiro', 'RJ', '02389-890', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10623, 25, 8, TO_DATE('08/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 97.18, 'Frankenversand', 'Berliner Platz 43',
        'München', '80805', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10624, 78, 4, TO_DATE('08/07/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/04/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 94.8, 'The Cracker Box', '55 Grizzly Peak Rd.',
        'Butte', 'MT', '59801', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10625, 2, 3, TO_DATE('08/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 43.9, 'Ana Trujillo Emparedados y helados', 'Avda. de la Constitución 2222',
        'México D.F.', '05021', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10626, 5, 1, TO_DATE('08/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 138.69, 'Berglunds snabbköp', 'Berguvsvägen  8',
        'Luleå', 'S-958 22', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10627, 71, 8, TO_DATE('08/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 107.46, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10628, 7, 4, TO_DATE('08/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 30.36, 'Blondel père et fils', '24, place Kléber',
        'Strasbourg', '67000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10629, 30, 4, TO_DATE('08/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 85.46, 'Godos Cocina Típica', 'C/ Romero, 33',
        'Sevilla', '41101', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10630, 39, 1, TO_DATE('08/13/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 32.35, 'Königlich Essen', 'Maubelstr. 90',
        'Brandenburg', '14776', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10631, 41, 8, TO_DATE('08/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 0.87, 'La maison d''Asie', '1 rue Alsace-Lorraine',
        'Toulouse', '31000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10632, 86, 8, TO_DATE('08/14/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/11/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 41.38, 'Die Wandernde Kuh', 'Adenauerallee 900',
        'Stuttgart', '70563', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10633, 20, 7, TO_DATE('08/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 477.9, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10634, 23, 4, TO_DATE('08/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/12/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 487.38, 'Folies gourmandes', '184, chaussée de Tournai',
        'Lille', '59000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10635, 49, 8, TO_DATE('08/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/15/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 47.46, 'Magazzini Alimentari Riuniti', 'Via Ludovico il Moro 22',
        'Bergamo', '24100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10636, 87, 4, TO_DATE('08/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 1.15, 'Wartian Herkku', 'Torikatu 38',
        'Oulu', '90110', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10637, 62, 6, TO_DATE('08/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/16/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 201.29, 'Queen Cozinha', 'Alameda dos Canàrios, 891',
        'São Paulo', 'SP', '05487-020', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10638, 47, 3, TO_DATE('08/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 158.44, 'LINO-Delicateses', 'Ave. 5 de Mayo Porlamar',
        'I. de Margarita', 'Nueva Esparta', '4980', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10639, 70, 7, TO_DATE('08/20/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/17/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 38.64, 'Santé Gourmet', 'Erling Skakkes gate 78',
        'Stavern', '4110', 'Norway');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10640, 86, 4, TO_DATE('08/21/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/18/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 23.55, 'Die Wandernde Kuh', 'Adenauerallee 900',
        'Stuttgart', '70563', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10641, 35, 4, TO_DATE('08/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('08/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 179.61, 'HILARIÓN-Abastos', 'Carrera 22 con Ave. Carlos Soublette #8-35',
        'San Cristóbal', 'Táchira', '5022', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10642, 73, 7, TO_DATE('08/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/19/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/05/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 41.89, 'Simons bistro', 'Vinbæltet 34',
        'København', '1734', 'Denmark');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10643, 1, 6, TO_DATE('08/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 29.46, 'Alfreds Futterkiste', 'Obere Str. 57A
FDS',
        'Berlin', '12209', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10644, 88, 3, TO_DATE('08/25/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/22/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/01/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 0.14, 'Wellington Importadora', 'Rua do Mercado, 12',
        'Resende', 'SP', '08737-363', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10645, 34, 4, TO_DATE('08/26/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/23/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/02/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 12.41, 'Hanari Carnes', 'Rua do Paço, 67',
        'Rio de Janeiro', 'RJ', '05454-876', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_COUNTRY)
VALUES (10646, 37, 9, TO_DATE('08/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/08/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 142.33, 'Hungry Owl All-Night Grocers', '8 Johnstown Road',
        'Cork', 'Co. Cork', 'Ireland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10647, 61, 4, TO_DATE('08/27/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('09/10/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/03/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 45.54, 'Que Delícia', 'Rua da Panificadora, 12',
        'Rio de Janeiro', 'RJ', '02389-673', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10648, 67, 5, TO_DATE('08/28/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('10/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('09/09/1997 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 14.25, 'Ricardo Adocicados', 'Av. Copacabana, 267',
        'Rio de Janeiro', 'RJ', '02389-890', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10953, 4, 9, TO_DATE('03/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('03/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/25/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 23.72, 'Around the Horn', 'Brook Farm
Stratford St. Mary',
        'Colchester', 'Essex', 'CO7 6JX', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10954, 47, 5, TO_DATE('03/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/28/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 27.91, 'LINO-Delicateses', 'Ave. 5 de Mayo Porlamar',
        'I. de Margarita', 'Nueva Esparta', '4980', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10955, 24, 8, TO_DATE('03/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 3.26, 'Folk och fä HB', 'Åkergatan 24',
        'Bräcke', 'S-844 67', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10956, 6, 6, TO_DATE('03/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/28/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 44.65, 'Blauer See Delikatessen', 'Forsterstr. 57',
        'Mannheim', '68306', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10957, 35, 8, TO_DATE('03/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/15/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 105.36, 'HILARIÓN-Abastos', 'Carrera 22 con Ave. Carlos Soublette #8-35',
        'San Cristóbal', 'Táchira', '5022', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10958, 54, 7, TO_DATE('03/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/15/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 49.56, 'Océano Atlántico Ltda.', 'Ing. Gustavo Moncada 8585
Piso 20-A',
        'Buenos Aires', '1010', 'Argentina');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10959, 31, 6, TO_DATE('03/18/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 4.98, 'Gourmet Lanchonetes', 'Av. Brasil, 442',
        'Campinas', 'SP', '04876-786', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10960, 35, 3, TO_DATE('03/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 2.08, 'HILARIÓN-Abastos', 'Carrera 22 con Ave. Carlos Soublette #8-35',
        'San Cristóbal', 'Táchira', '5022', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10961, 62, 8, TO_DATE('03/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 104.47, 'Queen Cozinha', 'Alameda dos Canàrios, 891',
        'São Paulo', 'SP', '05487-020', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10962, 63, 8, TO_DATE('03/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 275.79, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10963, 28, 9, TO_DATE('03/19/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 2.7, 'Furia Bacalhau e Frutos do Mar', 'Jardim das rosas n. 32',
        'Lisboa', '1675', 'Portugal');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10964, 74, 3, TO_DATE('03/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 87.38, 'Spécialités du monde', '25, rue Lauriston',
        'Paris', '75016', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10965, 55, 6, TO_DATE('03/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 144.38, 'Old World Delicatessen', '2743 Bering St.',
        'Anchorage', 'AK', '99508', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10966, 14, 4, TO_DATE('03/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 27.19, 'Chop-suey Chinese', 'Hauptstr. 31',
        'Bern', '3012', 'Switzerland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10967, 79, 2, TO_DATE('03/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 62.22, 'Toms Spezialitäten', 'Luisenstr. 48',
        'Münster', '44087', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10968, 20, 1, TO_DATE('03/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 74.6, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10969, 15, 1, TO_DATE('03/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 0.21, 'Comércio Mineiro', 'Av. dos Lusíadas, 23',
        'São Paulo', 'SP', '05432-043', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10970, 8, 9, TO_DATE('03/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 16.16, 'Bólido Comidas preparadas', 'C/ Araquil, 67',
        'Madrid', '28023', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10971, 26, 2, TO_DATE('03/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 121.82, 'France restauration', '54, rue Royale',
        'Nantes', '44000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10972, 40, 4, TO_DATE('03/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 0.02, 'La corne d''abondance', '67, avenue de l''Europe',
        'Versailles', '78000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10973, 40, 6, TO_DATE('03/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 15.17, 'La corne d''abondance', '67, avenue de l''Europe',
        'Versailles', '78000', 'France');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10974, 75, 3, TO_DATE('03/25/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 12.96, 'Split Rail Beer & Ale', 'P.O. Box 555',
        'Lander', 'WY', '82520', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10975, 10, 1, TO_DATE('03/25/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/22/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 32.27, 'Bottom-Dollar Markets', '23 Tsawassen Blvd.',
        'Tsawassen', 'BC', 'T2F 8M4', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10976, 35, 1, TO_DATE('03/25/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 37.97, 'HILARIÓN-Abastos', 'Carrera 22 con Ave. Carlos Soublette #8-35',
        'San Cristóbal', 'Táchira', '5022', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10977, 24, 8, TO_DATE('03/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 208.5, 'Folk och fä HB', 'Åkergatan 24',
        'Bräcke', 'S-844 67', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10978, 50, 9, TO_DATE('03/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 32.82, 'Maison Dewey', 'Rue Joseph-Bens 532',
        'Bruxelles', 'B-1180', 'Belgium');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10979, 20, 8, TO_DATE('03/26/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('03/31/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 353.07, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10980, 24, 4, TO_DATE('03/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 1.26, 'Folk och fä HB', 'Åkergatan 24',
        'Bräcke', 'S-844 67', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10981, 34, 1, TO_DATE('03/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 193.37, 'Hanari Carnes', 'Rua do Paço, 67',
        'Rio de Janeiro', 'RJ', '05454-876', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10982, 10, 2, TO_DATE('03/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 14.01, 'Bottom-Dollar Markets', '23 Tsawassen Blvd.',
        'Tsawassen', 'BC', 'T2F 8M4', 'Canada');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10983, 71, 2, TO_DATE('03/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 657.54, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10984, 71, 1, TO_DATE('03/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 211.22, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION, SHIP_COUNTRY)
VALUES (10985, 37, 2, TO_DATE('03/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 91.51, 'Hungry Owl All-Night Grocers', '8 Johnstown Road',
        'Cork', 'Co. Cork', 'Ireland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10986, 54, 8, TO_DATE('03/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/27/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 217.86, 'Océano Atlántico Ltda.', 'Ing. Gustavo Moncada 8585
Piso 20-A',
        'Buenos Aires', '1010', 'Argentina');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10987, 19, 8, TO_DATE('03/31/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/28/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 185.48, 'Eastern Connection', '35 King George',
        'London', 'WX3 6FW', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10988, 65, 3, TO_DATE('03/31/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/28/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 61.14, 'Rattlesnake Canyon Grocery', '2817 Milton Dr.',
        'Albuquerque', 'NM', '87110', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10989, 61, 2, TO_DATE('03/31/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/28/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 34.76, 'Que Delícia', 'Rua da Panificadora, 12',
        'Rio de Janeiro', 'RJ', '02389-673', 'Brazil');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10990, 20, 2, TO_DATE('04/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 117.61, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10991, 63, 1, TO_DATE('04/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 38.51, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10992, 77, 1, TO_DATE('04/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 4.27, 'The Big Cheese', '89 Jefferson Way
Suite 2',
        'Portland', 'OR', '97201', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10993, 24, 7, TO_DATE('04/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/29/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 8.81, 'Folk och fä HB', 'Åkergatan 24',
        'Bräcke', 'S-844 67', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10994, 83, 2, TO_DATE('04/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 65.53, 'Vaffeljernet', 'Smagsløget 45',
        'Århus', '8200', 'Denmark');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10995, 58, 1, TO_DATE('04/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 46, 'Pericles Comidas clásicas', 'Calle Dr. Jorge Cash 321',
        'México D.F.', '05033', 'Mexico');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10996, 63, 4, TO_DATE('04/02/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/30/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 1.12, 'QUICK-Stop', 'Taucherstraße 10',
        'Cunewalde', '01307', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (10997, 46, 8, TO_DATE('04/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/15/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 73.91, 'LILA-Supermercado', 'Carrera 52 con Ave. Bolívar #65-98 Llano Largo',
        'Barquisimeto', 'Lara', '3508', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10998, 91, 8, TO_DATE('04/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 20.31, 'Wolski Zajazd', 'ul. Filtrowa 68',
        'Warszawa', '01-012', 'Poland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (10999, 56, 6, TO_DATE('04/03/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/01/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 96.35, 'Ottilies Käseladen', 'Mehrheimerstr. 369',
        'Köln', '50739', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11000, 65, 2, TO_DATE('04/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 55.12, 'Rattlesnake Canyon Grocery', '2817 Milton Dr.',
        'Albuquerque', 'NM', '87110', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11001, 24, 2, TO_DATE('04/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/14/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 197.3, 'Folk och fä HB', 'Åkergatan 24',
        'Bräcke', 'S-844 67', 'Sweden');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11002, 71, 4, TO_DATE('04/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/16/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 141.16, 'Save-a-lot Markets', '187 Suffolk Ln.',
        'Boise', 'ID', '83720', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11003, 78, 3, TO_DATE('04/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/04/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 14.91, 'The Cracker Box', '55 Grizzly Peak Rd.',
        'Butte', 'MT', '59801', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11004, 50, 3, TO_DATE('04/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 44.84, 'Maison Dewey', 'Rue Joseph-Bens 532',
        'Bruxelles', 'B-1180', 'Belgium');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11005, 90, 2, TO_DATE('04/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 0.75, 'Wilman Kala', 'Keskuskatu 45',
        'Helsinki', '21240', 'Finland');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11006, 32, 3, TO_DATE('04/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/05/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/15/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 25.19, 'Great Lakes Food Market', '2732 Baker Blvd.',
        'Eugene', 'OR', '97403', 'USA');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11007, 60, 8, TO_DATE('04/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 202.24, 'Princesa Isabel Vinhos', 'Estrada da saúde n. 58',
        'Lisboa', '1756', 'Portugal');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11008, 20, 7, TO_DATE('04/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        3, 79.46, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11009, 30, 2, TO_DATE('04/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/06/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 59.11, 'Godos Cocina Típica', 'C/ Romero, 33',
        'Sevilla', '41101', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11010, 66, 2, TO_DATE('04/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/21/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 28.71, 'Reggiani Caseifici', 'Strada Provinciale 124',
        'Reggio Emilia', '42100', 'Italy');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11011, 1, 3, TO_DATE('04/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 1.21, 'Alfreds Futterkiste', 'Obere Str. 57',
        'Berlin', '12209', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11012, 25, 1, TO_DATE('04/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/23/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/17/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 242.95, 'Frankenversand', 'Berliner Platz 43',
        'München', '80805', 'Germany');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11013, 69, 2, TO_DATE('04/09/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/07/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 1, 32.99, 'Romero y tomillo', 'Gran Vía, 1',
        'Madrid', '28001', 'Spain');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11014, 47, 2, TO_DATE('04/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/15/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 3, 23.6, 'LINO-Delicateses', 'Ave. 5 de Mayo Porlamar',
        'I. de Margarita', 'Nueva Esparta', '4980', 'Venezuela');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11015, 70, 2, TO_DATE('04/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('04/24/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 4.62, 'Santé Gourmet', 'Erling Skakkes gate 78',
        'Stavern', '4110', 'Norway');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_REGION,
 SHIP_POSTAL_CODE, SHIP_COUNTRY)
VALUES (11016, 4, 9, TO_DATE('04/10/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/08/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 33.8, 'Around the Horn', 'Brook Farm
Stratford St. Mary',
        'Colchester', 'Essex', 'CO7 6JX', 'UK');
INSERT INTO efrpg.ORDERS
(ORDER_ID, CUSTOMER_ID, EMPLOYEE_ID, ORDER_DATE, REQUIRED_DATE, SHIPPED_DATE, SHIP_VIA, FREIGHT, SHIP_NAME, SHIP_ADDRESS, SHIP_CITY, SHIP_POSTAL_CODE,
 SHIP_COUNTRY)
VALUES (11017, 20, 9, TO_DATE('04/13/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), TO_DATE('05/11/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'),
        TO_DATE('04/20/1998 00:00:00', 'MM/DD/YYYY HH24:MI:SS'), 2, 754.26, 'Ernst Handel', 'Kirchgasse 6',
        'Graz', '8010', 'Austria');
COMMIT;

CREATE TABLE efrpg.ORDER_DETAILS
(
    ORDER_ID   NUMBER(9)               NOT NULL,
    PRODUCT_ID NUMBER(9)               NOT NULL,
    UNIT_PRICE NUMBER(10, 2) DEFAULT 0 NOT NULL
        CONSTRAINT CK_ORDER_DETAILS_UNIT_PRICE CHECK (Unit_Price >= 0),
    QUANTITY   NUMBER(9)     DEFAULT 1 NOT NULL
        CONSTRAINT CK_ORDER_DETAILS_QUANTITY CHECK (Quantity > 0),
    DISCOUNT   NUMBER(4, 2)  DEFAULT 0 NOT NULL
        CONSTRAINT CK_ORDER_DETAILS_DISCOUNT CHECK (Discount BETWEEN 0 AND 1),
    CONSTRAINT PK_ORDER_DETAILS PRIMARY KEY (ORDER_ID, PRODUCT_ID),
    CONSTRAINT FK_ORDER_ID FOREIGN KEY (ORDER_ID) REFERENCES ORDERS (ORDER_ID),
    CONSTRAINT FK_PRODUCT_ID FOREIGN KEY (PRODUCT_ID) REFERENCES PRODUCTS (PRODUCT_ID)
);

CREATE INDEX IDX_ORDER_DETAILS_ORDER_ID ON ORDER_DETAILS (ORDER_ID);

CREATE INDEX IDX_ORDER_DETAILS_PRODUCT_ID ON ORDER_DETAILS (PRODUCT_ID);

COMMENT ON COLUMN efrpg.ORDER_DETAILS.ORDER_ID IS 'Same as Order ID in Orders table.';

COMMENT ON COLUMN efrpg.ORDER_DETAILS.PRODUCT_ID IS 'Same as Product ID in Products table.';

SET DEFINE OFF;

INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10248, 11, 14, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10248, 42, 9.8, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10248, 72, 34.8, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10249, 14, 18.6, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10249, 51, 42.4, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10250, 41, 7.7, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10250, 51, 42.4, 35, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10250, 65, 16.8, 15, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10251, 22, 16.8, 6, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10251, 57, 15.6, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10251, 65, 16.8, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10252, 20, 64.8, 40, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10252, 33, 2, 25, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10252, 60, 27.2, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10253, 31, 10, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10253, 39, 14.4, 42, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10253, 49, 16, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10254, 24, 3.6, 15, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10254, 55, 19.2, 21, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10254, 74, 8, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10255, 2, 15.2, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10255, 16, 13.9, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10255, 36, 15.2, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10255, 59, 44, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10256, 53, 26.2, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10256, 77, 10.4, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10257, 27, 35.1, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10257, 39, 14.4, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10257, 77, 10.4, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10258, 2, 15.2, 50, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10258, 5, 17, 65, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10258, 32, 25.6, 6, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10259, 21, 8, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10259, 37, 20.8, 1, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10260, 41, 7.7, 16, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10260, 57, 15.6, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10260, 62, 39.4, 15, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10260, 70, 12, 21, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10261, 21, 8, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10261, 35, 14.4, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10262, 5, 17, 12, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10262, 7, 24, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10262, 56, 30.4, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10263, 16, 13.9, 60, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10263, 24, 3.6, 28, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10263, 30, 20.7, 60, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10263, 74, 8, 36, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10264, 2, 15.2, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10264, 41, 7.7, 25, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10265, 17, 31.2, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10265, 70, 12, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10266, 12, 30.4, 12, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10267, 40, 14.7, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10267, 59, 44, 70, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10267, 76, 14.4, 15, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10268, 29, 99, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10268, 72, 27.8, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10269, 33, 2, 60, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10269, 72, 27.8, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10270, 36, 15.2, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10270, 43, 36.8, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10271, 33, 2, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10272, 20, 64.8, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10272, 31, 10, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10272, 72, 27.8, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10273, 10, 24.8, 24, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10273, 31, 10, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10273, 33, 2, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10273, 40, 14.7, 60, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10273, 76, 14.4, 33, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10274, 71, 17.2, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10274, 72, 27.8, 7, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10275, 24, 3.6, 12, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10275, 59, 44, 6, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10276, 10, 24.8, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10276, 13, 4.8, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10277, 28, 36.4, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10277, 62, 39.4, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10278, 44, 15.5, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10278, 59, 44, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10278, 63, 35.1, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10278, 73, 12, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10279, 17, 31.2, 15, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10280, 24, 3.6, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10280, 55, 19.2, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10280, 75, 6.2, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10281, 19, 7.3, 1, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10281, 24, 3.6, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10281, 35, 14.4, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10282, 30, 20.7, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10282, 57, 15.6, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10283, 15, 12.4, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10283, 19, 7.3, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10283, 60, 27.2, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10283, 72, 27.8, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10284, 27, 35.1, 15, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10284, 44, 15.5, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10284, 60, 27.2, 20, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10284, 67, 11.2, 5, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10285, 1, 14.4, 45, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10285, 40, 14.7, 40, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10285, 53, 26.2, 36, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10286, 35, 14.4, 100, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10286, 62, 39.4, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10287, 16, 13.9, 40, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10287, 34, 11.2, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10287, 46, 9.6, 15, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10288, 54, 5.9, 10, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10288, 68, 10, 3, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10289, 3, 8, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10289, 64, 26.6, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10290, 5, 17, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10290, 29, 99, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10290, 49, 16, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10290, 77, 10.4, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10291, 13, 4.8, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10291, 44, 15.5, 24, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10291, 51, 42.4, 2, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10292, 20, 64.8, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10293, 18, 50, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10293, 24, 3.6, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10293, 63, 35.1, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10293, 75, 6.2, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10294, 1, 14.4, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10294, 17, 31.2, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10294, 43, 36.8, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10294, 60, 27.2, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10294, 75, 6.2, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10295, 56, 30.4, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10296, 11, 16.8, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10296, 16, 13.9, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10296, 69, 28.8, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10297, 39, 14.4, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10297, 72, 27.8, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10298, 2, 15.2, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10298, 36, 15.2, 40, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10298, 59, 44, 30, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10298, 62, 39.4, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10299, 19, 7.3, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10299, 70, 12, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10300, 66, 13.6, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10300, 68, 10, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10301, 40, 14.7, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10301, 56, 30.4, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10302, 17, 31.2, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10302, 28, 36.4, 28, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10302, 43, 36.8, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10303, 40, 14.7, 40, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10303, 65, 16.8, 30, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10303, 68, 10, 15, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10304, 49, 16, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10304, 59, 44, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10304, 71, 17.2, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10305, 18, 50, 25, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10305, 29, 99, 25, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10305, 39, 14.4, 30, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10306, 30, 20.7, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10306, 53, 26.2, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10306, 54, 5.9, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10307, 62, 39.4, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10307, 68, 10, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10308, 69, 28.8, 1, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10308, 70, 12, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10309, 4, 17.6, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10309, 6, 20, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10309, 42, 11.2, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10309, 43, 36.8, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10309, 71, 17.2, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10310, 16, 13.9, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10310, 62, 39.4, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10311, 42, 11.2, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10311, 69, 28.8, 7, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10312, 28, 36.4, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10312, 43, 36.8, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10312, 53, 26.2, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10312, 75, 6.2, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10313, 36, 15.2, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10314, 32, 25.6, 40, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10314, 58, 10.6, 30, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10314, 62, 39.4, 25, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10315, 34, 11.2, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10315, 70, 12, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10316, 41, 7.7, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10316, 62, 39.4, 70, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10317, 1, 14.4, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10318, 41, 7.7, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10318, 76, 14.4, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10319, 17, 31.2, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10319, 28, 36.4, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10319, 76, 14.4, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10320, 71, 17.2, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10321, 35, 14.4, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10322, 52, 5.6, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10323, 15, 12.4, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10323, 25, 11.2, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10323, 39, 14.4, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10324, 16, 13.9, 21, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10324, 35, 14.4, 70, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10324, 46, 9.6, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10324, 59, 44, 40, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10324, 63, 35.1, 80, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10325, 6, 20, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10325, 13, 4.8, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10325, 14, 18.6, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10325, 31, 10, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10325, 72, 27.8, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10326, 4, 17.6, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10326, 57, 15.6, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10326, 75, 6.2, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10327, 2, 15.2, 25, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10327, 11, 16.8, 50, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10327, 30, 20.7, 35, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10327, 58, 10.6, 30, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10328, 59, 44, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10328, 65, 16.8, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10328, 68, 10, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10329, 19, 7.3, 10, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10329, 30, 20.7, 8, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10329, 38, 210.8, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10329, 56, 30.4, 12, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10330, 26, 24.9, 50, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10330, 72, 27.8, 25, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10331, 54, 5.9, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10332, 18, 50, 40, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10332, 42, 11.2, 10, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10332, 47, 7.6, 16, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10333, 14, 18.6, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10333, 21, 8, 10, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10333, 71, 17.2, 40, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10334, 52, 5.6, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10334, 68, 10, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10335, 2, 15.2, 7, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10335, 31, 10, 25, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10335, 32, 25.6, 6, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10335, 51, 42.4, 48, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10336, 4, 17.6, 18, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10337, 23, 7.2, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10337, 26, 24.9, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10337, 36, 15.2, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10337, 37, 20.8, 28, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10337, 72, 27.8, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10338, 17, 31.2, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10338, 30, 20.7, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10339, 4, 17.6, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10339, 17, 31.2, 70, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10339, 62, 39.4, 28, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10340, 18, 50, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10340, 41, 7.7, 12, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10340, 43, 36.8, 40, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10341, 33, 2, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10341, 59, 44, 9, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10342, 2, 15.2, 24, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10342, 31, 10, 56, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10439, 12, 30.4, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10439, 16, 13.9, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10439, 64, 26.6, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10439, 74, 8, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10440, 2, 15.2, 45, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10440, 16, 13.9, 49, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10440, 29, 99, 24, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10440, 61, 22.8, 90, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10441, 27, 35.1, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10442, 11, 16.8, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10442, 54, 5.9, 80, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10442, 66, 13.6, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10443, 11, 16.8, 6, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10443, 28, 36.4, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10444, 17, 31.2, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10444, 26, 24.9, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10444, 35, 14.4, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10444, 41, 7.7, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10445, 39, 14.4, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10445, 54, 5.9, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10446, 19, 7.3, 12, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10446, 24, 3.6, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10446, 31, 10, 3, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10446, 52, 5.6, 15, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10447, 19, 7.3, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10447, 65, 16.8, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10447, 71, 17.2, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10448, 26, 24.9, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10448, 40, 14.7, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10449, 10, 24.8, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10449, 52, 5.6, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10449, 62, 39.4, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10450, 10, 24.8, 20, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10450, 54, 5.9, 6, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10451, 55, 19.2, 120, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10451, 64, 26.6, 35, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10451, 65, 16.8, 28, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10451, 77, 10.4, 55, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10452, 28, 36.4, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10452, 44, 15.5, 100, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10453, 48, 10.2, 15, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10453, 70, 12, 25, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10454, 16, 13.9, 20, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10454, 33, 2, 20, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10454, 46, 9.6, 10, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10455, 39, 14.4, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10455, 53, 26.2, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10455, 61, 22.8, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10455, 71, 17.2, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10456, 21, 8, 40, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10456, 49, 16, 21, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10457, 59, 44, 36, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10458, 26, 24.9, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10458, 28, 36.4, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10458, 43, 36.8, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10458, 56, 30.4, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10458, 71, 17.2, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10459, 7, 24, 16, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10459, 46, 9.6, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10459, 72, 27.8, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10460, 68, 10, 21, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10460, 75, 6.2, 4, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10461, 21, 8, 40, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10461, 30, 20.7, 28, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10461, 55, 19.2, 60, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10462, 13, 4.8, 1, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10462, 23, 7.2, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10463, 19, 7.3, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10463, 42, 11.2, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10464, 4, 17.6, 16, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10464, 43, 36.8, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10464, 56, 30.4, 30, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10464, 60, 27.2, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10465, 24, 3.6, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10465, 29, 99, 18, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10465, 40, 14.7, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10465, 45, 7.6, 30, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10465, 50, 13, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10466, 11, 16.8, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10466, 46, 9.6, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10467, 24, 3.6, 28, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10467, 25, 11.2, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10468, 30, 20.7, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10468, 43, 36.8, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10469, 2, 15.2, 40, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10469, 16, 13.9, 35, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10469, 44, 15.5, 2, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10470, 18, 50, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10470, 23, 7.2, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10470, 64, 26.6, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10471, 7, 24, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10471, 56, 30.4, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10472, 24, 3.6, 80, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10472, 51, 42.4, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10473, 33, 2, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10473, 71, 17.2, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10474, 14, 18.6, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10474, 28, 36.4, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10474, 40, 14.7, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10474, 75, 6.2, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10475, 31, 10, 35, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10475, 66, 13.6, 60, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10475, 76, 14.4, 42, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10476, 55, 19.2, 2, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10476, 70, 12, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10477, 1, 14.4, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10477, 21, 8, 21, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10477, 39, 14.4, 20, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10478, 10, 24.8, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10479, 38, 210.8, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10479, 53, 26.2, 28, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10479, 59, 44, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10479, 64, 26.6, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10480, 47, 7.6, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10480, 59, 44, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10481, 49, 16, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10481, 60, 27.2, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10482, 40, 14.7, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10483, 34, 11.2, 35, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10483, 77, 10.4, 30, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10484, 21, 8, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10484, 40, 14.7, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10484, 51, 42.4, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10485, 2, 15.2, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10485, 3, 8, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10485, 55, 19.2, 30, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10485, 70, 12, 60, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10486, 11, 16.8, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10486, 51, 42.4, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10486, 74, 8, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10487, 19, 7.3, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10487, 26, 24.9, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10487, 54, 5.9, 24, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10488, 59, 44, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10488, 73, 12, 20, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10489, 11, 16.8, 15, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10489, 16, 13.9, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10490, 59, 44, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10490, 68, 10, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10490, 75, 6.2, 36, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10491, 44, 15.5, 15, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10491, 77, 10.4, 7, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10492, 25, 11.2, 60, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10492, 42, 11.2, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10493, 65, 16.8, 15, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10493, 66, 13.6, 10, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10493, 69, 28.8, 10, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10494, 56, 30.4, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10495, 23, 7.2, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10495, 41, 7.7, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10495, 77, 10.4, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10496, 31, 10, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10497, 56, 30.4, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10497, 72, 27.8, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10497, 77, 10.4, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10498, 24, 4.5, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10498, 40, 18.4, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10498, 42, 14, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10499, 28, 45.6, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10499, 49, 20, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10500, 15, 15.5, 12, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10500, 28, 45.6, 8, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10501, 54, 7.45, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10502, 45, 9.5, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10502, 53, 32.8, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10502, 67, 14, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10503, 14, 23.25, 70, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10503, 65, 21.05, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10504, 2, 19, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10504, 21, 10, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10504, 53, 32.8, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10504, 61, 28.5, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10505, 62, 49.3, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10506, 25, 14, 18, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10506, 70, 15, 14, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10507, 43, 46, 15, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10507, 48, 12.75, 15, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10508, 13, 6, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10508, 39, 18, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10509, 28, 45.6, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10510, 29, 123.79, 36, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10510, 75, 7.75, 36, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10511, 4, 22, 50, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10511, 7, 30, 50, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10511, 8, 40, 10, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10512, 24, 4.5, 10, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10512, 46, 12, 9, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10512, 47, 9.5, 6, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10512, 60, 34, 12, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10513, 21, 10, 40, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10513, 32, 32, 50, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10513, 61, 28.5, 15, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10514, 20, 81, 39, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10514, 28, 45.6, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10514, 56, 38, 70, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10514, 65, 21.05, 39, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10514, 75, 7.75, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10515, 9, 97, 16, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10515, 16, 17.45, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10515, 27, 43.9, 120, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10515, 33, 2.5, 16, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10515, 60, 34, 84, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10516, 18, 62.5, 25, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10516, 41, 9.65, 80, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10516, 42, 14, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10517, 52, 7, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10517, 59, 55, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10517, 70, 15, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10518, 24, 4.5, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10518, 38, 263.5, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10518, 44, 19.45, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10519, 10, 31, 16, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10519, 56, 38, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10519, 60, 34, 10, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10520, 24, 4.5, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10520, 53, 32.8, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10521, 35, 18, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10521, 41, 9.65, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10521, 68, 12.5, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10522, 1, 18, 40, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10522, 8, 40, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10522, 30, 25.89, 20, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10522, 40, 18.4, 25, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10523, 17, 39, 25, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10523, 20, 81, 15, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10523, 37, 26, 18, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10523, 41, 9.65, 6, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10524, 10, 31, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10524, 30, 25.89, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10524, 43, 46, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10524, 54, 7.45, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10525, 36, 19, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10525, 40, 18.4, 15, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10526, 1, 18, 8, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10526, 13, 6, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10526, 56, 38, 30, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10527, 4, 22, 50, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10527, 36, 19, 30, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10528, 11, 21, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10528, 33, 2.5, 8, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10528, 72, 34.8, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10529, 55, 24, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10529, 68, 12.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10529, 69, 36, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10530, 17, 39, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10530, 43, 46, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10530, 61, 28.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10530, 76, 18, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10531, 59, 55, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10532, 30, 25.89, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10532, 66, 17, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10533, 4, 22, 50, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10533, 72, 34.8, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10533, 73, 15, 24, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10534, 30, 25.89, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10534, 40, 18.4, 10, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10534, 54, 7.45, 10, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10535, 11, 21, 50, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10535, 40, 18.4, 10, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10535, 57, 19.5, 5, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10535, 59, 55, 15, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10536, 12, 38, 15, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10536, 31, 12.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10536, 33, 2.5, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10536, 60, 34, 35, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10537, 31, 12.5, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10537, 51, 53, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10537, 58, 13.25, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10537, 72, 34.8, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10537, 73, 15, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10538, 70, 15, 7, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10538, 72, 34.8, 1, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10539, 13, 6, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10539, 21, 10, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10539, 33, 2.5, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10539, 49, 20, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10540, 3, 10, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10540, 26, 31.23, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10540, 38, 263.5, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10540, 68, 12.5, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10541, 24, 4.5, 35, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10541, 38, 263.5, 4, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10541, 65, 21.05, 36, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10541, 71, 21.5, 9, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10542, 11, 21, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10542, 54, 7.45, 24, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10543, 12, 38, 30, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10543, 23, 9, 70, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10544, 28, 45.6, 7, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10544, 67, 14, 7, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10545, 11, 21, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10546, 7, 30, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10546, 35, 18, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10546, 62, 49.3, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10547, 32, 32, 24, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10547, 36, 19, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10548, 34, 14, 10, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10548, 41, 9.65, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10549, 31, 12.5, 55, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10549, 45, 9.5, 100, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10549, 51, 53, 48, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10550, 17, 39, 8, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10550, 19, 9.2, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10550, 21, 10, 6, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10550, 61, 28.5, 10, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10551, 16, 17.45, 40, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10551, 35, 18, 20, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10551, 44, 19.45, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10552, 69, 36, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10552, 75, 7.75, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10553, 11, 21, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10553, 16, 17.45, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10553, 22, 21, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10553, 31, 12.5, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10553, 35, 18, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10554, 16, 17.45, 30, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10554, 23, 9, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10554, 62, 49.3, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10554, 77, 13, 10, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10555, 14, 23.25, 30, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10555, 19, 9.2, 35, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10555, 24, 4.5, 18, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10555, 51, 53, 20, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10555, 56, 38, 40, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10556, 72, 34.8, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10557, 64, 33.25, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10557, 75, 7.75, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10558, 47, 9.5, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10558, 51, 53, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10558, 52, 7, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10558, 53, 32.8, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10558, 73, 15, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10559, 41, 9.65, 12, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10559, 55, 24, 18, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10560, 30, 25.89, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10560, 62, 49.3, 15, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10561, 44, 19.45, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10561, 51, 53, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10562, 33, 2.5, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10562, 62, 49.3, 10, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10563, 36, 19, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10563, 52, 7, 70, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10564, 17, 39, 16, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10564, 31, 12.5, 6, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10564, 55, 24, 25, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10565, 24, 4.5, 25, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10565, 64, 33.25, 18, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10566, 11, 21, 35, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10566, 18, 62.5, 18, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10688, 28, 45.6, 60, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10688, 34, 14, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10689, 1, 18, 35, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10690, 56, 38, 20, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10690, 77, 13, 30, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10691, 1, 18, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10691, 29, 123.79, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10691, 43, 46, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10691, 44, 19.45, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10691, 62, 49.3, 48, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10692, 63, 43.9, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10693, 9, 97, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10693, 54, 7.45, 60, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10693, 69, 36, 30, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10693, 73, 15, 15, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10694, 7, 30, 90, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10694, 59, 55, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10694, 70, 15, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10695, 8, 40, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10695, 12, 38, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10695, 24, 4.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10696, 17, 39, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10696, 46, 12, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10697, 19, 9.2, 7, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10697, 35, 18, 9, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10697, 58, 13.25, 30, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10697, 70, 15, 30, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10698, 11, 21, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10698, 17, 39, 8, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10698, 29, 123.79, 12, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10698, 65, 21.05, 65, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10698, 70, 15, 8, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10699, 47, 9.5, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10700, 1, 18, 5, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10700, 34, 14, 12, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10700, 68, 12.5, 40, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10700, 71, 21.5, 60, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10701, 59, 55, 42, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10701, 71, 21.5, 20, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10701, 76, 18, 35, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10702, 3, 10, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10702, 76, 18, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10703, 2, 19, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10703, 59, 55, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10703, 73, 15, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10704, 4, 22, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10704, 24, 4.5, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10704, 48, 12.75, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10705, 31, 12.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10705, 32, 32, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10706, 16, 17.45, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10706, 43, 46, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10706, 59, 55, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10707, 55, 24, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10707, 57, 19.5, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10707, 70, 15, 28, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10708, 5, 21.35, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10708, 36, 19, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10709, 8, 40, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10709, 51, 53, 28, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10709, 60, 34, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10710, 19, 9.2, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10710, 47, 9.5, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10711, 19, 9.2, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10711, 41, 9.65, 42, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10711, 53, 32.8, 120, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10712, 53, 32.8, 3, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10712, 56, 38, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10713, 10, 31, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10713, 26, 31.23, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10713, 45, 9.5, 110, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10713, 46, 12, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10714, 2, 19, 30, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10714, 17, 39, 27, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10714, 47, 9.5, 50, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10714, 56, 38, 18, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10714, 58, 13.25, 12, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10715, 10, 31, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10715, 71, 21.5, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10716, 21, 10, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10716, 51, 53, 7, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10716, 61, 28.5, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10717, 21, 10, 32, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10717, 54, 7.45, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10717, 69, 36, 25, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10718, 12, 38, 36, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10718, 16, 17.45, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10718, 36, 19, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10718, 62, 49.3, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10719, 18, 62.5, 12, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10719, 30, 25.89, 3, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10719, 54, 7.45, 40, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10720, 35, 18, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10720, 71, 21.5, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10721, 44, 19.45, 50, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10722, 2, 19, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10722, 31, 12.5, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10722, 68, 12.5, 45, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10722, 75, 7.75, 42, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10723, 26, 31.23, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10724, 10, 31, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10724, 61, 28.5, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10725, 41, 9.65, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10725, 52, 7, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10725, 55, 24, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10726, 4, 22, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10342, 36, 15.2, 40, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10342, 55, 19.2, 40, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10343, 64, 26.6, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10343, 68, 10, 4, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10343, 76, 14.4, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10344, 4, 17.6, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10344, 8, 32, 70, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10345, 8, 32, 70, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10345, 19, 7.3, 80, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10345, 42, 11.2, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10346, 17, 31.2, 36, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10346, 56, 30.4, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10347, 25, 11.2, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10347, 39, 14.4, 50, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10347, 40, 14.7, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10347, 75, 6.2, 6, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10348, 1, 14.4, 15, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10348, 23, 7.2, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10349, 54, 5.9, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10350, 50, 13, 15, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10350, 69, 28.8, 18, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10351, 38, 210.8, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10351, 41, 7.7, 13, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10351, 44, 15.5, 77, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10351, 65, 16.8, 10, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10352, 24, 3.6, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10352, 54, 5.9, 20, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10353, 11, 16.8, 12, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10353, 38, 210.8, 50, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10354, 1, 14.4, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10354, 29, 99, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10355, 24, 3.6, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10355, 57, 15.6, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10356, 31, 10, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10356, 55, 19.2, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10356, 69, 28.8, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10357, 10, 24.8, 30, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10357, 26, 24.9, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10357, 60, 27.2, 8, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10358, 24, 3.6, 10, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10358, 34, 11.2, 10, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10358, 36, 15.2, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10359, 16, 13.9, 56, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10359, 31, 10, 70, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10359, 60, 27.2, 80, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10360, 28, 36.4, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10360, 29, 99, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10360, 38, 210.8, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10360, 49, 16, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10360, 54, 5.9, 28, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10361, 39, 14.4, 54, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10361, 60, 27.2, 55, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10362, 25, 11.2, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10362, 51, 42.4, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10362, 54, 5.9, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10363, 31, 10, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10363, 75, 6.2, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10363, 76, 14.4, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10364, 69, 28.8, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10364, 71, 17.2, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10365, 11, 16.8, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10366, 65, 16.8, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10366, 77, 10.4, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10367, 34, 11.2, 36, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10367, 54, 5.9, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10367, 65, 16.8, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10367, 77, 10.4, 7, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10368, 21, 8, 5, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10368, 28, 36.4, 13, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10368, 57, 15.6, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10368, 64, 26.6, 35, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10369, 29, 99, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10369, 56, 30.4, 18, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10370, 1, 14.4, 15, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10370, 64, 26.6, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10370, 74, 8, 20, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10371, 36, 15.2, 6, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10372, 20, 64.8, 12, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10372, 38, 210.8, 40, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10372, 60, 27.2, 70, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10372, 72, 27.8, 42, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10373, 58, 10.6, 80, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10373, 71, 17.2, 50, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10374, 31, 10, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10374, 58, 10.6, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10375, 14, 18.6, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10375, 54, 5.9, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10376, 31, 10, 42, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10377, 28, 36.4, 20, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10377, 39, 14.4, 20, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10378, 71, 17.2, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10379, 41, 7.7, 8, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10379, 63, 35.1, 16, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10379, 65, 16.8, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10380, 30, 20.7, 18, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10380, 53, 26.2, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10380, 60, 27.2, 6, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10380, 70, 12, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10381, 74, 8, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10382, 5, 17, 32, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10382, 18, 50, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10382, 29, 99, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10382, 33, 2, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10382, 74, 8, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10383, 13, 4.8, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10383, 50, 13, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10383, 56, 30.4, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10384, 20, 64.8, 28, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10384, 60, 27.2, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10385, 7, 24, 10, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10385, 60, 27.2, 20, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10385, 68, 10, 8, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10386, 24, 3.6, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10386, 34, 11.2, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10387, 24, 3.6, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10387, 28, 36.4, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10387, 59, 44, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10387, 71, 17.2, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10388, 45, 7.6, 15, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10388, 52, 5.6, 20, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10388, 53, 26.2, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10389, 10, 24.8, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10389, 55, 19.2, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10389, 62, 39.4, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10389, 70, 12, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10390, 31, 10, 60, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10390, 35, 14.4, 40, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10390, 46, 9.6, 45, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10390, 72, 27.8, 24, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10391, 13, 4.8, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10392, 69, 28.8, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10393, 2, 15.2, 25, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10393, 14, 18.6, 42, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10393, 25, 11.2, 7, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10393, 26, 24.9, 70, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10393, 31, 10, 32, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10394, 13, 4.8, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10394, 62, 39.4, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10395, 46, 9.6, 28, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10395, 53, 26.2, 70, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10395, 69, 28.8, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10396, 23, 7.2, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10396, 71, 17.2, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10396, 72, 27.8, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10397, 21, 8, 10, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10397, 51, 42.4, 18, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10398, 35, 14.4, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10398, 55, 19.2, 120, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10399, 68, 10, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10399, 71, 17.2, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10399, 76, 14.4, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10399, 77, 10.4, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10400, 29, 99, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10400, 35, 14.4, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10400, 49, 16, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10401, 30, 20.7, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10401, 56, 30.4, 70, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10401, 65, 16.8, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10401, 71, 17.2, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10402, 23, 7.2, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10402, 63, 35.1, 65, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10403, 16, 13.9, 21, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10403, 48, 10.2, 70, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10404, 26, 24.9, 30, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10404, 42, 11.2, 40, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10404, 49, 16, 30, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10405, 3, 8, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10406, 1, 14.4, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10406, 21, 8, 30, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10406, 28, 36.4, 42, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10406, 36, 15.2, 5, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10406, 40, 14.7, 2, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10407, 11, 16.8, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10407, 69, 28.8, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10407, 71, 17.2, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10408, 37, 20.8, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10408, 54, 5.9, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10408, 62, 39.4, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10409, 14, 18.6, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10409, 21, 8, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10410, 33, 2, 49, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10410, 59, 44, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10411, 41, 7.7, 25, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10411, 44, 15.5, 40, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10411, 59, 44, 9, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10412, 14, 18.6, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10413, 1, 14.4, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10413, 62, 39.4, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10413, 76, 14.4, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10414, 19, 7.3, 18, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10414, 33, 2, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10415, 17, 31.2, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10415, 33, 2, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10416, 19, 7.3, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10416, 53, 26.2, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10416, 57, 15.6, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10417, 38, 210.8, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10417, 46, 9.6, 2, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10417, 68, 10, 36, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10417, 77, 10.4, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10418, 2, 15.2, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10418, 47, 7.6, 55, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10418, 61, 22.8, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10418, 74, 8, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10419, 60, 27.2, 60, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10419, 69, 28.8, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10420, 9, 77.6, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10420, 13, 4.8, 2, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10420, 70, 12, 8, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10420, 73, 12, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10421, 19, 7.3, 4, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10421, 26, 24.9, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10421, 53, 26.2, 15, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10421, 77, 10.4, 10, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10422, 26, 24.9, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10423, 31, 10, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10423, 59, 44, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10424, 35, 14.4, 60, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10424, 38, 210.8, 49, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10424, 68, 10, 30, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10425, 55, 19.2, 10, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10425, 76, 14.4, 20, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10426, 56, 30.4, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10426, 64, 26.6, 7, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10427, 14, 18.6, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10428, 46, 9.6, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10429, 50, 13, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10429, 63, 35.1, 35, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10430, 17, 31.2, 45, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10430, 21, 8, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10430, 56, 30.4, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10430, 59, 44, 70, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10431, 17, 31.2, 50, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10431, 40, 14.7, 50, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10431, 47, 7.6, 30, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10432, 26, 24.9, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10432, 54, 5.9, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10433, 56, 30.4, 28, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10434, 11, 16.8, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10434, 76, 14.4, 18, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10435, 2, 15.2, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10435, 22, 16.8, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10435, 72, 27.8, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10436, 46, 9.6, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10436, 56, 30.4, 40, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10436, 64, 26.6, 30, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10436, 75, 6.2, 24, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10437, 53, 26.2, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10438, 19, 7.3, 15, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10438, 34, 11.2, 20, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10438, 57, 15.6, 15, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10829, 60, 34, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10830, 6, 25, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10830, 39, 18, 28, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10830, 60, 34, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10830, 68, 12.5, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10831, 19, 9.2, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10831, 35, 18, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10831, 38, 263.5, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10831, 43, 46, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10832, 13, 6, 3, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10832, 25, 14, 10, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10832, 44, 19.45, 16, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10832, 64, 33.25, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10833, 7, 30, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10833, 31, 12.5, 9, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10833, 53, 32.8, 9, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10834, 29, 123.79, 8, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10834, 30, 25.89, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10835, 59, 55, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10835, 77, 13, 2, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10836, 22, 21, 52, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10836, 35, 18, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10836, 57, 19.5, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10836, 60, 34, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10836, 64, 33.25, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10837, 13, 6, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10837, 40, 18.4, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10837, 47, 9.5, 40, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10837, 76, 18, 21, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10838, 1, 18, 4, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10838, 18, 62.5, 25, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10838, 36, 19, 50, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10839, 58, 13.25, 30, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10839, 72, 34.8, 15, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10840, 25, 14, 6, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10840, 39, 18, 10, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10841, 10, 31, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10841, 56, 38, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10841, 59, 55, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10841, 77, 13, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10842, 11, 21, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10842, 43, 46, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10842, 68, 12.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10842, 70, 15, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10843, 51, 53, 4, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10844, 22, 21, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10845, 23, 9, 70, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10845, 35, 18, 25, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10845, 42, 14, 42, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10845, 58, 13.25, 60, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10845, 64, 33.25, 48, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10846, 4, 22, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10846, 70, 15, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10846, 74, 10, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10847, 1, 18, 80, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10847, 19, 9.2, 12, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10847, 37, 26, 60, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10847, 45, 9.5, 36, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10847, 60, 34, 45, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10847, 71, 21.5, 55, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10848, 5, 21.35, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10848, 9, 97, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10849, 3, 10, 49, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10849, 26, 31.23, 18, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10850, 25, 14, 20, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10850, 33, 2.5, 4, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10850, 70, 15, 30, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10851, 2, 19, 5, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10851, 25, 14, 10, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10851, 57, 19.5, 10, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10851, 59, 55, 42, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10852, 2, 19, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10852, 17, 39, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10852, 62, 49.3, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10853, 18, 62.5, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10854, 10, 31, 100, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10854, 13, 6, 65, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10855, 16, 17.45, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10855, 31, 12.5, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10855, 56, 38, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10855, 65, 21.05, 15, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10856, 2, 19, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10856, 42, 14, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10857, 3, 10, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10857, 26, 31.23, 35, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10857, 29, 123.79, 10, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10858, 7, 30, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10858, 27, 43.9, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10858, 70, 15, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10859, 24, 4.5, 40, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10859, 54, 7.45, 35, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10859, 64, 33.25, 30, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10860, 51, 53, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10860, 76, 18, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10861, 17, 39, 42, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10861, 18, 62.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10861, 21, 10, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10861, 33, 2.5, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10861, 62, 49.3, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10862, 11, 21, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10862, 52, 7, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10863, 1, 18, 20, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10863, 58, 13.25, 12, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10864, 35, 18, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10864, 67, 14, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10865, 38, 263.5, 60, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10865, 39, 18, 80, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10866, 2, 19, 21, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10866, 24, 4.5, 6, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10866, 30, 25.89, 40, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10867, 53, 32.8, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10868, 26, 31.23, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10868, 35, 18, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10868, 49, 20, 42, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10869, 1, 18, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10869, 11, 21, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10869, 23, 9, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10869, 68, 12.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10870, 35, 18, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10870, 51, 53, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10871, 6, 25, 50, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10871, 16, 17.45, 12, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10871, 17, 39, 16, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10872, 55, 24, 10, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10872, 62, 49.3, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10872, 64, 33.25, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10872, 65, 21.05, 21, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10873, 21, 10, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10873, 28, 45.6, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10874, 10, 31, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10875, 19, 9.2, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10875, 47, 9.5, 21, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10875, 49, 20, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10876, 46, 12, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10876, 64, 33.25, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10877, 16, 17.45, 30, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10877, 18, 62.5, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10878, 20, 81, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10879, 40, 18.4, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10879, 65, 21.05, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10879, 76, 18, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10880, 23, 9, 30, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10880, 61, 28.5, 30, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10880, 70, 15, 50, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10881, 73, 15, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10882, 42, 14, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10882, 49, 20, 20, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10882, 54, 7.45, 32, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10883, 24, 4.5, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10884, 21, 10, 40, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10884, 56, 38, 21, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10884, 65, 21.05, 12, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10885, 2, 19, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10885, 24, 4.5, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10885, 70, 15, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10885, 77, 13, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10886, 10, 31, 70, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10886, 31, 12.5, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10886, 77, 13, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10887, 25, 14, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10888, 2, 19, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10888, 68, 12.5, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10889, 11, 21, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10889, 38, 263.5, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10890, 17, 39, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10890, 34, 14, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10890, 41, 9.65, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10891, 30, 25.89, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10892, 59, 55, 40, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10893, 8, 40, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10893, 24, 4.5, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10893, 29, 123.79, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10893, 30, 25.89, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10893, 36, 19, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10894, 13, 6, 28, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10894, 69, 36, 50, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10894, 75, 7.75, 120, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10895, 24, 4.5, 110, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10895, 39, 18, 45, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10895, 40, 18.4, 91, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10895, 60, 34, 100, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10896, 45, 9.5, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10896, 56, 38, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10897, 29, 123.79, 80, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10897, 30, 25.89, 36, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10898, 13, 6, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10899, 39, 18, 8, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10900, 70, 15, 3, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10901, 41, 9.65, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10901, 71, 21.5, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10902, 55, 24, 30, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10902, 62, 49.3, 6, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10903, 13, 6, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10903, 65, 21.05, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10903, 68, 12.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10904, 58, 13.25, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10904, 62, 49.3, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10905, 1, 18, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10906, 61, 28.5, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10907, 75, 7.75, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10908, 7, 30, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10908, 52, 7, 14, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10909, 7, 30, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10909, 16, 17.45, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10909, 41, 9.65, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10910, 19, 9.2, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10910, 49, 20, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10910, 61, 28.5, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10911, 1, 18, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10911, 17, 39, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10911, 67, 14, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10912, 11, 21, 40, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10912, 29, 123.79, 60, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10913, 4, 22, 30, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10913, 33, 2.5, 40, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10913, 58, 13.25, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10914, 71, 21.5, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10915, 17, 39, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10915, 33, 2.5, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10915, 54, 7.45, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10916, 16, 17.45, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10916, 32, 32, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10916, 57, 19.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10917, 30, 25.89, 1, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10917, 60, 34, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10918, 1, 18, 60, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10918, 60, 34, 25, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10919, 16, 17.45, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10919, 25, 14, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10919, 40, 18.4, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10920, 50, 16.25, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10921, 35, 18, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10921, 63, 43.9, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10922, 17, 39, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10922, 24, 4.5, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10923, 42, 14, 10, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10923, 43, 46, 10, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10923, 67, 14, 24, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10924, 10, 31, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10924, 28, 45.6, 30, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10924, 75, 7.75, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10925, 36, 19, 25, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10925, 52, 7, 12, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10926, 11, 21, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10926, 13, 6, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10926, 19, 9.2, 7, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10926, 72, 34.8, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10927, 20, 81, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10566, 76, 18, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10567, 31, 12.5, 60, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10567, 51, 53, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10567, 59, 55, 40, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10568, 10, 31, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10569, 31, 12.5, 35, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10569, 76, 18, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10570, 11, 21, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10570, 56, 38, 60, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10571, 14, 23.25, 11, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10571, 42, 14, 28, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10572, 16, 17.45, 12, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10572, 32, 32, 10, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10572, 40, 18.4, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10572, 75, 7.75, 15, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10573, 17, 39, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10573, 34, 14, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10573, 53, 32.8, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10574, 33, 2.5, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10574, 40, 18.4, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10574, 62, 49.3, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10574, 64, 33.25, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10575, 59, 55, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10575, 63, 43.9, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10575, 72, 34.8, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10575, 76, 18, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10576, 1, 18, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10576, 31, 12.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10576, 44, 19.45, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10577, 39, 18, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10577, 75, 7.75, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10577, 77, 13, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10578, 35, 18, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10578, 57, 19.5, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10579, 15, 15.5, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10579, 75, 7.75, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10580, 14, 23.25, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10580, 41, 9.65, 9, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10580, 65, 21.05, 30, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10581, 75, 7.75, 50, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10582, 57, 19.5, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10582, 76, 18, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10583, 29, 123.79, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10583, 60, 34, 24, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10583, 69, 36, 10, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10584, 31, 12.5, 50, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10585, 47, 9.5, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10586, 52, 7, 4, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10587, 26, 31.23, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10587, 35, 18, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10587, 77, 13, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10588, 18, 62.5, 40, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10588, 42, 14, 100, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10589, 35, 18, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10590, 1, 18, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10590, 77, 13, 60, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10591, 3, 10, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10591, 7, 30, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10591, 54, 7.45, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10592, 15, 15.5, 25, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10592, 26, 31.23, 5, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10593, 20, 81, 21, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10593, 69, 36, 20, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10593, 76, 18, 4, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10594, 52, 7, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10594, 58, 13.25, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10595, 35, 18, 30, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10595, 61, 28.5, 120, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10595, 69, 36, 65, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10596, 56, 38, 5, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10596, 63, 43.9, 24, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10596, 75, 7.75, 30, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10597, 24, 4.5, 35, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10597, 57, 19.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10597, 65, 21.05, 12, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10598, 27, 43.9, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10598, 71, 21.5, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10599, 62, 49.3, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10600, 54, 7.45, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10600, 73, 15, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10601, 13, 6, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10601, 59, 55, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10602, 77, 13, 5, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10603, 22, 21, 48, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10603, 49, 20, 25, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10604, 48, 12.75, 6, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10604, 76, 18, 10, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10605, 16, 17.45, 30, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10605, 59, 55, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10605, 60, 34, 70, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10605, 71, 21.5, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10606, 4, 22, 20, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10606, 55, 24, 20, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10606, 62, 49.3, 10, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10607, 7, 30, 45, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10607, 17, 39, 100, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10607, 33, 2.5, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10607, 40, 18.4, 42, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10607, 72, 34.8, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10608, 56, 38, 28, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10609, 1, 18, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10609, 10, 31, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10609, 21, 10, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10610, 36, 19, 21, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10611, 1, 18, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10611, 2, 19, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10611, 60, 34, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10612, 10, 31, 70, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10612, 36, 19, 55, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10612, 49, 20, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10612, 60, 34, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10612, 76, 18, 80, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10613, 13, 6, 8, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10613, 75, 7.75, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10614, 11, 21, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10614, 21, 10, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10614, 39, 18, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10615, 55, 24, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10616, 38, 263.5, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10616, 56, 38, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10616, 70, 15, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10616, 71, 21.5, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10617, 59, 55, 30, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10618, 6, 25, 70, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10618, 56, 38, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10618, 68, 12.5, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10619, 21, 10, 42, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10619, 22, 21, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10620, 24, 4.5, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10620, 52, 7, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10621, 19, 9.2, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10621, 23, 9, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10621, 70, 15, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10621, 71, 21.5, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10622, 2, 19, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10622, 68, 12.5, 18, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10623, 14, 23.25, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10623, 19, 9.2, 15, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10623, 21, 10, 25, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10623, 24, 4.5, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10623, 35, 18, 30, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10624, 28, 45.6, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10624, 29, 123.79, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10624, 44, 19.45, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10625, 14, 23.25, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10625, 42, 14, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10625, 60, 34, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10626, 53, 32.8, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10626, 60, 34, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10626, 71, 21.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10627, 62, 49.3, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10627, 73, 15, 35, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10628, 1, 18, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10629, 29, 123.79, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10629, 64, 33.25, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10630, 55, 24, 12, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10630, 76, 18, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10631, 75, 7.75, 8, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10632, 2, 19, 30, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10632, 33, 2.5, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10633, 12, 38, 36, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10633, 13, 6, 13, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10633, 26, 31.23, 35, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10633, 62, 49.3, 80, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10634, 7, 30, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10634, 18, 62.5, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10634, 51, 53, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10634, 75, 7.75, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10635, 4, 22, 10, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10635, 5, 21.35, 15, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10635, 22, 21, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10636, 4, 22, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10636, 58, 13.25, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10637, 11, 21, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10637, 50, 16.25, 25, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10637, 56, 38, 60, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10638, 45, 9.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10638, 65, 21.05, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10638, 72, 34.8, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10639, 18, 62.5, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10640, 69, 36, 20, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10640, 70, 15, 15, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10641, 2, 19, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10641, 40, 18.4, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10642, 21, 10, 30, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10642, 61, 28.5, 20, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10643, 28, 45.6, 15, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10643, 39, 18, 21, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10643, 46, 12, 2, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10644, 18, 62.5, 4, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10644, 43, 46, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10644, 46, 12, 21, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10645, 18, 62.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10645, 36, 19, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10646, 1, 18, 15, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10646, 10, 31, 18, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10646, 71, 21.5, 30, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10646, 77, 13, 35, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10647, 19, 9.2, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10647, 39, 18, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10648, 22, 21, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10648, 24, 4.5, 15, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10649, 28, 45.6, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10649, 72, 34.8, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10650, 30, 25.89, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10650, 53, 32.8, 25, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10650, 54, 7.45, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10651, 19, 9.2, 12, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10651, 22, 21, 20, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10652, 30, 25.89, 2, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10652, 42, 14, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10653, 16, 17.45, 30, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10653, 60, 34, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10654, 4, 22, 12, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10654, 39, 18, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10654, 54, 7.45, 6, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10655, 41, 9.65, 20, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10656, 14, 23.25, 3, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10656, 44, 19.45, 28, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10656, 47, 9.5, 6, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10657, 15, 15.5, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10657, 41, 9.65, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10657, 46, 12, 45, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10657, 47, 9.5, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10657, 56, 38, 45, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10657, 60, 34, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10658, 21, 10, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10658, 40, 18.4, 70, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10658, 60, 34, 55, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10658, 77, 13, 70, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10659, 31, 12.5, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10659, 40, 18.4, 24, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10659, 70, 15, 40, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10660, 20, 81, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10661, 39, 18, 3, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10661, 58, 13.25, 49, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10662, 68, 12.5, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10663, 40, 18.4, 30, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10663, 42, 14, 30, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10663, 51, 53, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10664, 10, 31, 24, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10664, 56, 38, 12, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10664, 65, 21.05, 15, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10665, 51, 53, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10665, 59, 55, 1, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10665, 76, 18, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10666, 29, 123.79, 36, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10666, 65, 21.05, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10667, 69, 36, 45, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10667, 71, 21.5, 14, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10668, 31, 12.5, 8, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10668, 55, 24, 4, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10668, 64, 33.25, 15, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10669, 36, 19, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10670, 23, 9, 32, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10670, 46, 12, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10670, 67, 14, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10670, 73, 15, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10670, 75, 7.75, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10671, 16, 17.45, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10671, 62, 49.3, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10671, 65, 21.05, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10672, 38, 263.5, 15, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10672, 71, 21.5, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10673, 16, 17.45, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10673, 42, 14, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10673, 43, 46, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10674, 23, 9, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10675, 14, 23.25, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10675, 53, 32.8, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10675, 58, 13.25, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10676, 10, 31, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10676, 19, 9.2, 7, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10676, 44, 19.45, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10677, 26, 31.23, 30, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10677, 33, 2.5, 8, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10678, 12, 38, 100, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10678, 33, 2.5, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10678, 41, 9.65, 120, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10678, 54, 7.45, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10679, 59, 55, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10680, 16, 17.45, 50, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10680, 31, 12.5, 20, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10680, 42, 14, 40, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10681, 19, 9.2, 30, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10681, 21, 10, 12, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10681, 64, 33.25, 28, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10682, 33, 2.5, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10682, 66, 17, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10682, 75, 7.75, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10683, 52, 7, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10684, 40, 18.4, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10684, 47, 9.5, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10684, 60, 34, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10685, 10, 31, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10685, 41, 9.65, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10685, 47, 9.5, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10686, 17, 39, 30, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10686, 26, 31.23, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10687, 9, 97, 50, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10687, 29, 123.79, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10687, 36, 19, 6, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10688, 10, 31, 18, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11052, 61, 28.5, 10, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11053, 18, 62.5, 35, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11053, 32, 32, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11053, 64, 33.25, 25, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11054, 33, 2.5, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11054, 67, 14, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11055, 24, 4.5, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11055, 25, 14, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11055, 51, 53, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11055, 57, 19.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11056, 7, 30, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11056, 55, 24, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11056, 60, 34, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11057, 70, 15, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11058, 21, 10, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11058, 60, 34, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11058, 61, 28.5, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11059, 13, 6, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11059, 17, 39, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11059, 60, 34, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11060, 60, 34, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11060, 77, 13, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11061, 60, 34, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11062, 53, 32.8, 10, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11062, 70, 15, 12, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11063, 34, 14, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11063, 40, 18.4, 40, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11063, 41, 9.65, 30, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11064, 17, 39, 77, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11064, 41, 9.65, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11064, 53, 32.8, 25, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11064, 55, 24, 4, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11064, 68, 12.5, 55, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11065, 30, 25.89, 4, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11065, 54, 7.45, 20, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11066, 16, 17.45, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11066, 19, 9.2, 42, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11066, 34, 14, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11067, 41, 9.65, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11068, 28, 45.6, 8, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11068, 43, 46, 36, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11068, 77, 13, 28, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11069, 39, 18, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11070, 1, 18, 40, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11070, 2, 19, 20, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11070, 16, 17.45, 30, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11070, 31, 12.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11071, 7, 30, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11071, 13, 6, 10, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11072, 2, 19, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11072, 41, 9.65, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11072, 50, 16.25, 22, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11072, 64, 33.25, 130, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11073, 11, 21, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11073, 24, 4.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11074, 16, 17.45, 14, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11075, 2, 19, 10, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11075, 46, 12, 30, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11075, 76, 18, 2, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11076, 6, 25, 20, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11076, 14, 23.25, 20, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11076, 19, 9.2, 10, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 2, 19, 24, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 3, 10, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 4, 22, 1, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 6, 25, 1, 0.02);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 7, 30, 1, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 8, 40, 2, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 10, 31, 1, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 12, 38, 2, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 13, 6, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 14, 23.25, 1, 0.03);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 16, 17.45, 2, 0.03);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 20, 81, 1, 0.04);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 23, 9, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 32, 32, 1, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 39, 18, 2, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 41, 9.65, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 46, 12, 3, 0.02);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 52, 7, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 55, 24, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 60, 34, 2, 0.06);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 64, 33.25, 2, 0.03);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 66, 17, 1, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 73, 15, 2, 0.01);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 75, 7.75, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11077, 77, 13, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10726, 11, 21, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10727, 17, 39, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10727, 56, 38, 10, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10727, 59, 55, 10, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10728, 30, 25.89, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10728, 40, 18.4, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10728, 55, 24, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10728, 60, 34, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10729, 1, 18, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10729, 21, 10, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10729, 50, 16.25, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10730, 16, 17.45, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10730, 31, 12.5, 3, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10730, 65, 21.05, 10, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10731, 21, 10, 40, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10731, 51, 53, 30, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10732, 76, 18, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10733, 14, 23.25, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10733, 28, 45.6, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10733, 52, 7, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10734, 6, 25, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10734, 30, 25.89, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10734, 76, 18, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10735, 61, 28.5, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10735, 77, 13, 2, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10736, 65, 21.05, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10736, 75, 7.75, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10737, 13, 6, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10737, 41, 9.65, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10738, 16, 17.45, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10739, 36, 19, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10739, 52, 7, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10740, 28, 45.6, 5, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10740, 35, 18, 35, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10740, 45, 9.5, 40, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10740, 56, 38, 14, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10741, 2, 19, 15, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10742, 3, 10, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10742, 60, 34, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10742, 72, 34.8, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10743, 46, 12, 28, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10744, 40, 18.4, 50, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10745, 18, 62.5, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10745, 44, 19.45, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10745, 59, 55, 45, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10745, 72, 34.8, 7, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10746, 13, 6, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10746, 42, 14, 28, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10746, 62, 49.3, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10746, 69, 36, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10747, 31, 12.5, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10747, 41, 9.65, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10747, 63, 43.9, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10747, 69, 36, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10748, 23, 9, 44, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10748, 40, 18.4, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10748, 56, 38, 28, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10749, 56, 38, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10749, 59, 55, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10749, 76, 18, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10750, 14, 23.25, 5, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10750, 45, 9.5, 40, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10750, 59, 55, 25, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10751, 26, 31.23, 12, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10751, 30, 25.89, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10751, 50, 16.25, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10751, 73, 15, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10752, 1, 18, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10752, 69, 36, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10753, 45, 9.5, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10753, 74, 10, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10754, 40, 18.4, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10755, 47, 9.5, 30, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10755, 56, 38, 30, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10755, 57, 19.5, 14, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10755, 69, 36, 25, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10756, 18, 62.5, 21, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10756, 36, 19, 20, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10756, 68, 12.5, 6, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10756, 69, 36, 20, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10757, 34, 14, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10757, 59, 55, 7, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10757, 62, 49.3, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10757, 64, 33.25, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10758, 26, 31.23, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10758, 52, 7, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10758, 70, 15, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10759, 32, 32, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10760, 25, 14, 12, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10760, 27, 43.9, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10760, 43, 46, 30, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10761, 25, 14, 35, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10761, 75, 7.75, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10762, 39, 18, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10762, 47, 9.5, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10762, 51, 53, 28, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10762, 56, 38, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10763, 21, 10, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10763, 22, 21, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10763, 24, 4.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10764, 3, 10, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10764, 39, 18, 130, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10765, 65, 21.05, 80, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10766, 2, 19, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10766, 7, 30, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10766, 68, 12.5, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10767, 42, 14, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10768, 22, 21, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10768, 31, 12.5, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10768, 60, 34, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10768, 71, 21.5, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10769, 41, 9.65, 30, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10769, 52, 7, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10769, 61, 28.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10769, 62, 49.3, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10770, 11, 21, 15, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10771, 71, 21.5, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10772, 29, 123.79, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10772, 59, 55, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10773, 17, 39, 33, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10773, 31, 12.5, 70, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10773, 75, 7.75, 7, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10774, 31, 12.5, 2, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10774, 66, 17, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10775, 10, 31, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10775, 67, 14, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10776, 31, 12.5, 16, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10776, 42, 14, 12, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10776, 45, 9.5, 27, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10776, 51, 53, 120, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10777, 42, 14, 20, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10778, 41, 9.65, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10779, 16, 17.45, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10779, 62, 49.3, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10780, 70, 15, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10780, 77, 13, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10781, 54, 7.45, 3, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10781, 56, 38, 20, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10781, 74, 10, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10782, 31, 12.5, 1, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10783, 31, 12.5, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10783, 38, 263.5, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10784, 36, 19, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10784, 39, 18, 2, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10784, 72, 34.8, 30, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10785, 10, 31, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10785, 75, 7.75, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10786, 8, 40, 30, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10786, 30, 25.89, 15, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10786, 75, 7.75, 42, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10787, 2, 19, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10787, 29, 123.79, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10788, 19, 9.2, 50, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10788, 75, 7.75, 40, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10789, 18, 62.5, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10789, 35, 18, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10789, 63, 43.9, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10789, 68, 12.5, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10790, 7, 30, 3, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10790, 56, 38, 20, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10791, 29, 123.79, 14, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10791, 41, 9.65, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10792, 2, 19, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10792, 54, 7.45, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10792, 68, 12.5, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10793, 41, 9.65, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10793, 52, 7, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10794, 14, 23.25, 15, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10794, 54, 7.45, 6, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10795, 16, 17.45, 65, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10795, 17, 39, 35, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10796, 26, 31.23, 21, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10796, 44, 19.45, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10796, 64, 33.25, 35, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10796, 69, 36, 24, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10797, 11, 21, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10798, 62, 49.3, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10798, 72, 34.8, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10799, 13, 6, 20, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10799, 24, 4.5, 20, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10799, 59, 55, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10800, 11, 21, 50, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10800, 51, 53, 10, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10800, 54, 7.45, 7, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10801, 17, 39, 40, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10801, 29, 123.79, 20, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10802, 30, 25.89, 25, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10802, 51, 53, 30, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10802, 55, 24, 60, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10802, 62, 49.3, 5, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10803, 19, 9.2, 24, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10803, 25, 14, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10803, 59, 55, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10804, 10, 31, 36, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10804, 28, 45.6, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10804, 49, 20, 4, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10805, 34, 14, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10805, 38, 263.5, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10806, 2, 19, 20, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10806, 65, 21.05, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10806, 74, 10, 15, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10807, 40, 18.4, 1, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10808, 56, 38, 20, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10808, 76, 18, 50, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10809, 52, 7, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10810, 13, 6, 7, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10810, 25, 14, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10810, 70, 15, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10811, 19, 9.2, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10811, 23, 9, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10811, 40, 18.4, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10812, 31, 12.5, 16, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10812, 72, 34.8, 40, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10812, 77, 13, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10813, 2, 19, 12, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10813, 46, 12, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10814, 41, 9.65, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10814, 43, 46, 20, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10814, 48, 12.75, 8, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10814, 61, 28.5, 30, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10815, 33, 2.5, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10816, 38, 263.5, 30, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10816, 62, 49.3, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10817, 26, 31.23, 40, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10817, 38, 263.5, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10817, 40, 18.4, 60, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10817, 62, 49.3, 25, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10818, 32, 32, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10818, 41, 9.65, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10819, 43, 46, 7, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10819, 75, 7.75, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10820, 56, 38, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10821, 35, 18, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10821, 51, 53, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10822, 62, 49.3, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10822, 70, 15, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10823, 11, 21, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10823, 57, 19.5, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10823, 59, 55, 40, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10823, 77, 13, 15, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10824, 41, 9.65, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10824, 70, 15, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10825, 26, 31.23, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10825, 53, 32.8, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10826, 31, 12.5, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10826, 57, 19.5, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10827, 10, 31, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10827, 39, 18, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10828, 20, 81, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10828, 38, 263.5, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10829, 2, 19, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10829, 8, 40, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10829, 13, 6, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10927, 52, 7, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10927, 76, 18, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10928, 47, 9.5, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10928, 76, 18, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10929, 21, 10, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10929, 75, 7.75, 49, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10929, 77, 13, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10930, 21, 10, 36, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10930, 27, 43.9, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10930, 55, 24, 25, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10930, 58, 13.25, 30, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10931, 13, 6, 42, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10931, 57, 19.5, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10932, 16, 17.45, 30, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10932, 62, 49.3, 14, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10932, 72, 34.8, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10932, 75, 7.75, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10933, 53, 32.8, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10933, 61, 28.5, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10934, 6, 25, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10935, 1, 18, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10935, 18, 62.5, 4, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10935, 23, 9, 8, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10936, 36, 19, 30, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10937, 28, 45.6, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10937, 34, 14, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10938, 13, 6, 20, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10938, 43, 46, 24, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10938, 60, 34, 49, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10938, 71, 21.5, 35, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10939, 2, 19, 10, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10939, 67, 14, 40, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10940, 7, 30, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10940, 13, 6, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10941, 31, 12.5, 44, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10941, 62, 49.3, 30, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10941, 68, 12.5, 80, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10941, 72, 34.8, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10942, 49, 20, 28, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10943, 13, 6, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10943, 22, 21, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10943, 46, 12, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10944, 11, 21, 5, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10944, 44, 19.45, 18, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10944, 56, 38, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10945, 13, 6, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10945, 31, 12.5, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10946, 10, 31, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10946, 24, 4.5, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10946, 77, 13, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10947, 59, 55, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10948, 50, 16.25, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10948, 51, 53, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10948, 55, 24, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10949, 6, 25, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10949, 10, 31, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10949, 17, 39, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10949, 62, 49.3, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10950, 4, 22, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10951, 33, 2.5, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10951, 41, 9.65, 6, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10951, 75, 7.75, 50, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10952, 6, 25, 16, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10952, 28, 45.6, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10953, 20, 81, 50, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10953, 31, 12.5, 50, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10954, 16, 17.45, 28, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10954, 31, 12.5, 25, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10954, 45, 9.5, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10954, 60, 34, 24, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10955, 75, 7.75, 12, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10956, 21, 10, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10956, 47, 9.5, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10956, 51, 53, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10957, 30, 25.89, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10957, 35, 18, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10957, 64, 33.25, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10958, 5, 21.35, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10958, 7, 30, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10958, 72, 34.8, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10959, 75, 7.75, 20, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10960, 24, 4.5, 10, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10960, 41, 9.65, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10961, 52, 7, 6, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10961, 76, 18, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10962, 7, 30, 45, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10962, 13, 6, 77, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10962, 53, 32.8, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10962, 69, 36, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10962, 76, 18, 44, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10963, 60, 34, 2, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10964, 18, 62.5, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10964, 38, 263.5, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10964, 69, 36, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10965, 51, 53, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10966, 37, 26, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10966, 56, 38, 12, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10966, 62, 49.3, 12, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10967, 19, 9.2, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10967, 49, 20, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10968, 12, 38, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10968, 24, 4.5, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10968, 64, 33.25, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10969, 46, 12, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10970, 52, 7, 40, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10971, 29, 123.79, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10972, 17, 39, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10972, 33, 2.5, 7, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10973, 26, 31.23, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10973, 41, 9.65, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10973, 75, 7.75, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10974, 63, 43.9, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10975, 8, 40, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10975, 75, 7.75, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10976, 28, 45.6, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10977, 39, 18, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10977, 47, 9.5, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10977, 51, 53, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10977, 63, 43.9, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10978, 8, 40, 20, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10978, 21, 10, 40, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10978, 40, 18.4, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10978, 44, 19.45, 6, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10979, 7, 30, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10979, 12, 38, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10979, 24, 4.5, 80, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10979, 27, 43.9, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10979, 31, 12.5, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10979, 63, 43.9, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10980, 75, 7.75, 40, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10981, 38, 263.5, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10982, 7, 30, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10982, 43, 46, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10983, 13, 6, 84, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10983, 57, 19.5, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10984, 16, 17.45, 55, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10984, 24, 4.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10984, 36, 19, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10985, 16, 17.45, 36, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10985, 18, 62.5, 8, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10985, 32, 32, 35, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10986, 11, 21, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10986, 20, 81, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10986, 76, 18, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10986, 77, 13, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10987, 7, 30, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10987, 43, 46, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10987, 72, 34.8, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10988, 7, 30, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10988, 62, 49.3, 40, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10989, 6, 25, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10989, 11, 21, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10989, 41, 9.65, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10990, 21, 10, 65, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10990, 34, 14, 60, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10990, 55, 24, 65, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10990, 61, 28.5, 66, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10991, 2, 19, 50, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10991, 70, 15, 20, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10991, 76, 18, 90, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10992, 72, 34.8, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10993, 29, 123.79, 50, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10993, 41, 9.65, 35, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10994, 59, 55, 18, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10995, 51, 53, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10995, 60, 34, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10996, 42, 14, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10997, 32, 32, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10997, 46, 12, 20, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10997, 52, 7, 20, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10998, 24, 4.5, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10998, 61, 28.5, 7, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10998, 74, 10, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10998, 75, 7.75, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10999, 41, 9.65, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10999, 51, 53, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (10999, 77, 13, 21, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11000, 4, 22, 25, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11000, 24, 4.5, 30, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11000, 77, 13, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11001, 7, 30, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11001, 22, 21, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11001, 46, 12, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11001, 55, 24, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11002, 13, 6, 56, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11002, 35, 18, 15, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11002, 42, 14, 24, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11002, 55, 24, 40, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11003, 1, 18, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11003, 40, 18.4, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11003, 52, 7, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11004, 26, 31.23, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11004, 76, 18, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11005, 1, 18, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11005, 59, 55, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11006, 1, 18, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11006, 29, 123.79, 2, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11007, 8, 40, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11007, 29, 123.79, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11007, 42, 14, 14, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11008, 28, 45.6, 70, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11008, 34, 14, 90, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11008, 71, 21.5, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11009, 24, 4.5, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11009, 36, 19, 18, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11009, 60, 34, 9, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11010, 7, 30, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11010, 24, 4.5, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11011, 58, 13.25, 40, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11011, 71, 21.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11012, 19, 9.2, 50, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11012, 60, 34, 36, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11012, 71, 21.5, 60, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11013, 23, 9, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11013, 42, 14, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11013, 45, 9.5, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11013, 68, 12.5, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11014, 41, 9.65, 28, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11015, 30, 25.89, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11015, 77, 13, 18, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11016, 31, 12.5, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11016, 36, 19, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11017, 3, 10, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11017, 59, 55, 110, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11017, 70, 15, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11018, 12, 38, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11018, 18, 62.5, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11018, 56, 38, 5, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11019, 46, 12, 3, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11019, 49, 20, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11020, 10, 31, 24, 0.15);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11021, 2, 19, 11, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11021, 20, 81, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11021, 26, 31.23, 63, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11021, 51, 53, 44, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11021, 72, 34.8, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11022, 19, 9.2, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11022, 69, 36, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11023, 7, 30, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11023, 43, 46, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11024, 26, 31.23, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11024, 33, 2.5, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11024, 65, 21.05, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11024, 71, 21.5, 50, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11025, 1, 18, 10, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11025, 13, 6, 20, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11026, 18, 62.5, 8, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11026, 51, 53, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11027, 24, 4.5, 30, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11027, 62, 49.3, 21, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11028, 55, 24, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11028, 59, 55, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11029, 56, 38, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11029, 63, 43.9, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11030, 2, 19, 100, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11030, 5, 21.35, 70, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11030, 29, 123.79, 60, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11030, 59, 55, 100, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11031, 1, 18, 45, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11031, 13, 6, 80, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11031, 24, 4.5, 21, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11031, 64, 33.25, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11031, 71, 21.5, 16, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11032, 36, 19, 35, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11032, 38, 263.5, 25, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11032, 59, 55, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11033, 53, 32.8, 70, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11033, 69, 36, 36, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11034, 21, 10, 15, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11034, 44, 19.45, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11034, 61, 28.5, 6, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11035, 1, 18, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11035, 35, 18, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11035, 42, 14, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11035, 54, 7.45, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11036, 13, 6, 7, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11036, 59, 55, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11037, 70, 15, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11038, 40, 18.4, 5, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11038, 52, 7, 2, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11038, 71, 21.5, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11039, 28, 45.6, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11039, 35, 18, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11039, 49, 20, 60, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11039, 57, 19.5, 28, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11040, 21, 10, 20, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11041, 2, 19, 30, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11041, 63, 43.9, 30, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11042, 44, 19.45, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11042, 61, 28.5, 4, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11043, 11, 21, 10, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11044, 62, 49.3, 12, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11045, 33, 2.5, 15, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11045, 51, 53, 24, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11046, 12, 38, 20, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11046, 32, 32, 15, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11046, 35, 18, 18, 0.05);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11047, 1, 18, 25, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11047, 5, 21.35, 30, 0.25);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11048, 68, 12.5, 42, 0);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11049, 2, 19, 10, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11049, 12, 38, 4, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11050, 76, 18, 50, 0.1);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11051, 24, 4.5, 10, 0.2);
INSERT INTO efrpg.ORDER_DETAILS (ORDER_ID, PRODUCT_ID, UNIT_PRICE, QUANTITY, DISCOUNT) VALUES (11052, 43, 46, 30, 0.2);

COMMIT;
