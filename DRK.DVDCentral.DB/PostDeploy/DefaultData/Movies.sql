BEGIN
	INSERT INTO tblMovie(Id,Title,Description,FormatId,DirectorId,RatingId,Cost,IntStkQty,ImagePath)
	VALUES
	(1,'Sandlot','INFO--',1,1,1,57.1,22,'Image-Path-INFO'),
	(2,'Starwars','INFO--',2,2,2,52.1,72,'Image-Path-INFO'),
	(3,'Pokemon','INFO--',3,3,3,17.1,12,'Image-Path-INFO')
END