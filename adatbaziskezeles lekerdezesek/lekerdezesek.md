# <p align=center> Adatbázis lekérdezések </p>

```sql
SELECT szul_hely, COUNT(*) AS dolgozok 
FROM `dolgozo` 
GROUP BY szul_hely;			<- városonként hány dolgozó

SELECT szul_hely, SUM(fizetes) AS osszes, AVG(fizetes) AS atlag 
FROM dolgozo 
GROUP BY szul_hely;	<- átlag, összes fizetés városonként

SELECT szul_hely, AVG(fizetes) AS atlag 
FROM `dolgozo` 
GROUP BY szul_hely 
HAVING AVG(fizetes) <= 120000;  <-városonként átlág fizetés, ami nagyobb mint 120K

SELECT neve, fizetes 
FROM `dolgozo` 
ORDER BY(neve)

SELECT neve, fizetes 
FROM `dolgozo` 
ORDER BY fizetes DESC, neve;			<- fizetés szerint növekvő, ha megegyezik a fizetés akkor névsor szerint



SELECT [ALL/DISTINCT] * <oszlopnév>
FROM <táblanév> [másodnév] [,<táblanév> [<másodnév>]]..
     <táblanév> INNER JOIN <táblanév> ON <feltétel>	
     <táblanév> NAUTRAL JOIN <táblanév>
		FULL JOIN <táblanév> ON <feltétel>
		LEFT/RIGHT JOIN ... ON ...


	Több táblás lekérdezés:

SELECT dolgozo.neve, kifizetes.ber, kifizetes.ado, kifizetes.kif_dat 
FROM dolgozo, kifizetes
WHERE dolgozo.torzsszam = kifizetes.torzsszam
ORDER BY dolgozo.neve;

SELECT dolgozo.neve, kifizetes.ber, kifizetes.ado, kifizetes.kif_dat 
FROM dolgozo INNER JOIN kifizetes ON dolgozo.torzsszam = kifizetes.torzsszam
ORDER BY dolgozo.neve;

SELECT dolgozo.neve, kifizetes.ber, kifizetes.ado, kifizetes.kif_dat 
FROM dolgozo NATURAL LEFT JOIN kifizetes
ORDER BY dolgozo.neve;


	Egy táblát többször lekérdezve

SELECT dolgozo1.neve, dolgozo2.neve
FROM dolgozo dolgozo1, dolgozo dolgozo2
WHERE dolgozo1.szul_hely = dolgozo2.szul_hely AND dolgozo1.neve != dolgozo2.neve AND dolgozo1.neve < dolgozo2.neve


1. egy érték -DOGÁBAN CSAK EZ
2. egy oszlop (<oszlopkifejezés> NOT IN <belső lekérdezés>)
		(NOT <oszlopkifejezés> <reláció> ALL/ANY <belső lekérdezés>)
3. egy tábla (NOT EXISTS <belső lekérdezés>)
		(<oszlopkifejezés>, <oszlpokifejezés> NOT IN <belső lekérdezés>)




SELECT neve, fizetes 
FROM `dolgozo` 
WHERE dolgozo.fizetes < (SELECT AVG(fizetes) FROM dolgozo) <-- fizetés átlagánál kevesebbet keresnek


SELECT dolgozo.neve, dolgozo.fizetes
FROM dolgozo
WHERE dolgozo.fizetes  + 60000 >= (SELECT MAX(fizetes) FROM dolgozo) <-- a max fizetéstől legfeljebb 60.000 ft-tal tér el


SELECT dolgozo.neve, dolgozo.torzsszam
FROM dolgozo 
WHERE dolgozo.torzsszam NOT IN (SELECT kifizetes.torzsszam FROM kifizetes)
vagy
SELECT dolgozo.neve, dolgozo.torzsszam
FROM dolgozo 
WHERE NOT EXISTS (SELECT dolgozo.torzsszam FROM kifizetes WHERE kifizetes.torzsszam = dolgozo.torzsszam)
<-- akik nem kaptak fizetést (nincsnenek benne a kifizetés táblában)



SELECT * FROM dolgozo 
WHERE fizetes > ALL (SELECT fizetes FROM dolgozo WHERE szul_hely IN ("Eger", "Szeged"))  	<-- Szegeden és Egeren dolgozók fizezésébél több a fizetése
	
SELECT jaratSzam
FROM `jaratok` 
WHERE elsoAjtos = True



SELECT nev
FROM `megallok` 
WHERE nev LIKE '%sétány%'
ORDER BY nev



SELECT halozat.sorszam, megallok.nev AS megallo
FROM `halozat` 
	INNER JOIN megallok ON megallok.id = halozat.megallo
	INNER JOIN jaratok ON jaratok.id = halozat.jarat
WHERE halozat.irany = "A" AND jaratok.jaratSzam = "CITY"
ORDER BY halozat.sorszam



SELECT megallok.nev, COUNT(*) AS jaratokSzama
FROM halozat INNER JOIN megallok ON megallok.id = halozat.megallo
GROUP BY megallok.nev
HAVING jaratokSzama >= 3

SELECT Neve, Nem, Fajta, Kor FROM `kutya` 
WHERE Fajta = 'dalmata'


SELECT kutya.Neve, eltunt.Mikor
FROM `eltunt` NATURAL JOIN `kutya`
WHERE eltunt.Hely = "XIII. kerület"

SELECT kutya.Neve , eltunt.Mikor 
FROM `eltunt`NATURAL JOIN `kutya` 
WHERE eltunt.Mikor >"2004-12-31" 
ORDER BY(kutya.Neve);


SELECT kutya.Neve , eltunt.Mikor
FROM `eltunt` NATURAL JOIN `kutya`
WHERE kutya.Nem = "kan" 
ORDER BY eltunt.Mikor DESC 
LIMIT 1

(limit: hány sor jelenjen meg)


SELECT kutya.Fajta, COUNT(*) AS darabszám
FROM `kutya`
WHERE kutya.Fajta NOT LIKE "%keverék%" 
GROUP BY kutya.Fajta




SELECT eltunt.Hely, kutya.Fajta
FROM `eltunt`NATURAL JOIN `kutya`
GROUP BY eltunt.Hely, kutya.Fajta
HAVING COUNT(*) > 1




CREATE TABLE books (author_id INT AUTO_INCREMENT PRIMARY KEY, author_name VARCHAR(100) NOT NULL)


CREATE TABLE books (book_id INT PRIMARY KEY AUTO_INCREMENT, title VARCHAR(255) NOT NULL, author_id INT , published_year INT, FOREIGN KEY (author_id) REFERENCES authors(author_id) ON DELETE CASCADE)




INSERT INTO authors (author_name) 
VALUES ('J.K. Rowling'), ('George Orwell'), ('Jane austin')



INSERT INTO books (title, author_id, published_year) 
VALUES ("Harry Potter and the Philosopher's Stone", 1, 1997),
	("1984", 2, 1949),
	("Pride and Prejudice", 3, 1813)



SELECT * FROM `books` 
vagy
SELECT * FROM `books` INNER JOIN `authors` ON books.author_id = authors.author_id



SELECT authors.author_name 
FROM `books` INNER JOIN `authors` ON books.author_id = authors.author_id
WHERE 1

vagy

SELECT author_name 
FROM `books` NATURAL JOIN `authors`
GROUP BY author_name
HAVING COUNT(*) > 0



UPDATE books SET title = "Harry Potter and the Sorcerer's Stone" WHERE book_id = 1;


DELETE FROM authors WHERE author_id = 2;


INSERT INTO relációséma VALUES értéklista
DELETE FROM reléció WHERE feltétel
UPDATE reléció SET értékadások WHERE feltétel



INSERT INTO dolgozo VALUES ('T', 'dsjkd', 'sdsds', 'dsdge', 'sddsd', 160000)
INSERT INTO dolgozo (torzsszam, neve) VALUES ('T', 'dsjkd')

DELETE FROM DOLGOZO WHERE szul_hely = 'Budapest'

UPDATE dolgozo SET szul_hely = 'Kecskemét', szul_ido = '1978-05-12', fizetes = 185000 WHERE torzsszam = 'T985234';




```













