BEGIN
	INSERT INTO tblMovie(Id,Title,Description,FormatId,DirectorId,RatingId,Cost,InStkQty,ImagePath)
	VALUES
	(1,'Sandlot','baseball movie ',1,1,1,57.1,22,'sandlot.png'),
	(2,'Starwars','Space Movie',2,2,2,52.1,72,'starwars.png'),
	(3,'Pokemon','Animated Movie',3,3,3,17.1,12,'pokemon.png')
END