
CREATE procedure dbo.XmlDataV2
as
	declare @temp table ([SomeXML] [xml])
	insert into @temp values('<root></root>')

	declare @someXml xml	
	select top 1 @someXml = [SomeXML] from @temp

	--explicit assignment. when this is line commented out, ReversePoco generates incorrect output type (int) for this stored proc
	--set @someXml = '<root></root>'; 

	set @someXml.modify('insert <new>node</new> into (/root)[1]');

	SELECT getdate(), @someXml	
