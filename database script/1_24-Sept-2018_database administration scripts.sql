select * from BudgetYear
select * from Department
select * from Adverts
select * from ProjectCategory
select * from AdvertCategory
select * from Procurements
select * from UserProfile
select * from AspNetUsers
select * from AspNetUserRoles
select * from AspNetRoles

select * from RequestForDemo
select * from Catalog
select * from OrganizationSettings

select * from SourceOfFunds
select * from SourceOfFundsOrganizationSettings

select * from ProjectCategoryOrganizationSettings
select * from ProcurementMethodOrganizationSettings

select * from Department

update department set departmentheaduserid = null where departmentheaduserid = '5C99B26F-CBA8-493E-ABD4-E049BB548DB5'
delete from userprofile where useremail='oaro@techspecialistlimited.com'
delete from aspnetusers where email='oaro@techspecialistlimited.com'
UPDATE OrganizationSettings set OrganizationNameAbbreviation = 'NCC' where OrganizationNameAbbreviation='NITDA'
UPDATE OrganizationSettings set OrganizationNameInFull = 'Old Nigeria Information Technology Development Agency' where OrganizationNameInFull='Nigeria Information Technology Development Agency'
UPDATE catalog set subdomain = 'old NITDA' where subdomain='NITDA'
UPDATE requestfordemo set OrganizationShortName = 'old NITDA' where OrganizationShortName='NITDA'
UPDATE requestfordemo set OrganizationFullName = 'old Nigeria Information Technology Development Agency' where OrganizationFullName='Nigeria Information Technology Development Agency'
update requestfordemo set AdministratorEmail='o@techspecialistlimited.com' where AdministratorEmail='oaro@techspecialistlimited.com'
update requestfordemo set AdministratorPhoneNumber='+2347065949502' where AdministratorEmail='+2347065949501'