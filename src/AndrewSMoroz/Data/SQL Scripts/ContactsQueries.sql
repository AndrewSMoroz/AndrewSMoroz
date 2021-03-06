-- Positions with companies
/*
select	c.[Name]									as 'Company',
		p.Title										as 'Position',
		p.[Description]								as 'Position Description',
		CONVERT(nvarchar(10), p.DatePosted, 101)	as 'Date Posted'
from				dbo.Position	p
		inner join	dbo.Company		c	on c.ID = p.CompanyID
order by c.Name, p.DatePosted, p.Title
--*/

-- Number of positions by company
/*
select	c.Name		as 'Company',
		COUNT(p.ID)	as 'Number of Positions'
from					dbo.Company		c
		left outer join	dbo.Position	p on p.CompanyID = c.ID
group by c.Name
--*/

-- Companies and contacts
/*
select	c.Name			as 'Company',
		ct.FirstName	as 'First Name',
		ct.LastName		as 'Last Name',
		ctt.Description	as 'Type'
from				dbo.Company			c
		left join	dbo.Contact			ct	on ct.CompanyID = c.ID
		left join	lookup.ContactType	ctt	on ctt.ID = ct.ContactTypeID
order by c.Name, ct.LastName, ct.FirstName
--*/

-- Contacts and their phone numbers
--/*
select	c.Name									as 'Company',
		ct.FirstName							as 'First Name',
		ct.LastName								as 'Last Name',
		cpt.Description							as 'Phone Type',
		CASE
		  WHEN cp.IsPrimaryPhone = 1 THEN 'Yes'
		  ELSE ''
		END										as 'Primary',
		cp.PhoneNumber							as 'Phone Number'
from				dbo.Company				c
		inner join	dbo.Contact				ct	on ct.CompanyID = c.ID
		inner join	dbo.ContactPhone		cp	on cp.ContactID = ct.ID
		inner join	lookup.ContactPhoneType	cpt	on cpt.ID = cp.ContactPhoneTypeID
order by c.Name, ct.LastName, ct.FirstName, cpt.Description
--*/

-- Positions and their contacts
/*
select	c.[Name]									as 'Company',
		p.Title										as 'Position',
		p.[Description]								as 'Position Description',
		CONVERT(nvarchar(10), p.DatePosted, 101)	as 'Date Posted',
		ct.FirstName							as 'First Name',
		ct.LastName								as 'Last Name',
		cpt.Description							as 'Phone Type',
		CASE
		  WHEN cp.IsPrimaryPhone = 1 THEN 'Yes'
		  ELSE ''
		END										as 'Primary',
		cp.PhoneNumber							as 'Phone Number'
from				dbo.Position	p
		inner join	dbo.Company		c	on c.ID = p.CompanyID
		inner join	dbo.Contact				ct	on ct.CompanyID = c.ID
		inner join	dbo.ContactPhone		cp	on cp.ContactID = ct.ID and cp.IsPrimaryPhone = 1
		inner join	lookup.ContactPhoneType	cpt	on cpt.ID = cp.ContactPhoneTypeID
order by c.Name, p.DatePosted, p.Title, ct.LastName, ct.FirstName, cpt.Description
--*/
