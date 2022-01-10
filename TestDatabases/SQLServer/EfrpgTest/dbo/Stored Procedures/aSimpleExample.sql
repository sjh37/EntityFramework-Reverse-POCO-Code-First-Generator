
CREATE PROCEDURE aSimpleExample
AS
BEGIN
    SET NOCOUNT ON;

    declare @test table (
        id int,
        [stuff] varchar(50)
    )
    insert into @test (id, [stuff]) values (1, 'some stuff'), (2, 'more stuff')

    select 1 as id, 'even more' as [stuff] into #test

    select * from @test
    select * from #test
	DROP TABLE #test
END
