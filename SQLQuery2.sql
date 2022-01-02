USE DBKUTUPHANE
ALTER TABLE tblUyeler  
DROP CONSTRAINT check_tc_uzunlugu; --constraint silme

USE DBKUTUPHANE ALTER TABLE tblKitap  with nocheck add CONSTRAINT k_kategori foreign key(KATEGORI) REFERENCES tblKategori([ID]) ON DELETE CASCADE ON UPDATE CASCADE
DELETE tblKitap --CASCADE OLAYI---

ALTER TABLE tblHareket ADD CONSTRAINT DC_hareketDurumDefault DEFAULT 0 FOR ISLEMDURUM --1-islem durumu girilmediği zaman otomatik olarak false atayacak.(default constraint)-

ALTER TABLE tblUyeler with nocheck ADD CONSTRAINT check_telefon_uzunlugu 
CHECK (LEN(TELEFON) = 11);--2-telefon uzunlugunun 11 olmasinin kontrolu -CHECK constraint kullanimi

 USE DBKUTUPHANE insert into tblUyeler values ('zelal','turkmen','mail','emret123','123','05059429545','abay orta okulu',97654244451,'2001'); --check constraint telefonNo testi


ALTER TABLE tblUyeler with nocheck ADD CONSTRAINT check_kullaniciAdi_uzunlugu 
CHECK (LEN(KULLANICIADI) = 30);--3-Kullanıcı adı  uzunlugunun 30 olmasinin kontrolu -CHECK constraint kullanimi

insert into tblUyeler values ('zelal','turkmen','mail','emret123','123','05059429545','abay orta okulu',97654244451,'2001'); --CHECK constraintinin kontrolu eklenme

ALTER TABLE tblUyeler
with nocheck ADD CONSTRAINT unique_tcKontrol UNIQUE(TC)--4.constraint unique  kullanimi-tc nin tek bir tane olmasının kontrolu

 USE DBKUTUPHANE insert into tblUyeler values ('zelal','turkmen','mail','emr123','ınkmnnnnnhhh','tel','okul',45322930918,'2374'); --CHECK constraintinin kontrolu eklenme


ALTER TABLE tblUyeler with nocheck ADD CONSTRAINT unique_uye_mail UNIQUE(MAIL)--5.constraint unique  kullanimi

 USE DBKUTUPHANE  insert into tblUyeler values ('zelal','turkmen','mail','emr123','ınkmnnnnnhhh','tel','okul',45322930918,'2374'); --UNIQUE constraintinin kontrolu eklenmesi



create rule hareketIslemDurum--1. rule kullanimi
as
@ISLEMTURU = 'alış' OR @ISLEMTURU = 'iade'

exec sp_bindrule 'hareketIslemDurum', 'tblHareket.ISLEMTURU'

insert into tblHareket values (1,1,1,GETDATE(),'iade',0)


create rule uyeDogumYili--2. rule kullanimi
as
LEN(@DOGUMYILI)=4

EXEC sp_bindrule 'uyeDogumYili','tblUyeler.DOGUMYILI'

insert into tblUyeler values ('1','1','1','2001','alış','10000000000','10000',10101010100,'2143')


 USE DBKUTUPHANE
GO
CREATE PROCEDURE basimYilAraligiFiltre --1. procedure kullanımı
 (
   @baslangic INT,
   @bitis INT
 )
AS 
BEGIN 
SELECT * FROM tblKitap
WHERE BASIMYIL BETWEEN @baslangic AND @bitis
END;


EXEC basimYilAraligiFiltre
   @baslangic ='2000',
   @bitis='2010'


  
USE DBKUTUPHANE
GO
   CREATE PROCEDURE sayfaAraligiFiltre --2. procedure kullanımı
   (
   @baslangic INT,
   @bitis INT
   )
   AS
   BEGIN
   SELECT * FROM tblKitap
   WHERE SAYFA BETWEEN @baslangic AND @bitis
   END;
   

   EXEC sayfaAraligiFiltre
   @baslangic ='100',
   @bitis='300'

   
   
   create view getAllUye--1.view kullanımı
   as 
   select U.*, (select count(*) from tblHareket where UYE = U.ID and ISLEMTURU = 'alış' and ISLEMDURUM = 0 ) as iadesizKitapSayisi from tblUyeler U

   select * from getAllUye


  create view getAllYazar--2.view kullanımı
   as 
   select Y.*, (select count(*) from tblKitap where YAZAR=Y.ID) as kitapSayisi  from tblYazar Y

  select * from getAllYazar

   create view getAllKategori--3.view kullanımı
   as 
   select K.*, (select count(*) from tblKitap where KATEGORI=K.ID) as kitapSayisi from tblKategori K

  select * from getAllKategori




 
create function fn_kitap_sayisi()--1.fonksiyonn
returns int
as
Begin
return (select count(*) from tblKitap)
End


select dbo.fn_kitap_sayisi() as kitapSayisi

create function fn_uye_sayisi()--2.fonksiyonn
returns int
as
Begin
return (select count(*) from tblUyeler)
End

select dbo.fn_uye_sayisi() as uyeSayisi






USE DBKUTUPHANE--trigger
GO
create trigger kitap
on tblKitap
after insert
as
begin 
select 'yeni bir kitap kaydı yapıldı'
END

insert into tblKitap values('ada',1,1,'2001','yakamoz','155',1)





select con.[name] as constraint_name,
schema_name(t.schema_id) + '.' + t.[name]  as [table],
col.[name] as column_name,
con.[definition],
case when con.is_disabled = 0 
then 'Active' 
else 'Disabled' 
end as [status]
from sys.check_constraints con
left outer join sys.objects t
on con.parent_object_id = t.object_id
left outer join sys.all_columns col
on con.parent_column_id = col.column_id
and con.parent_object_id = col.object_id
order by con.name