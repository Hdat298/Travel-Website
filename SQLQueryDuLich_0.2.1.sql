use master
create database QLDL
use QLDL

set dateformat DMY

create table LoaiTour
(
	ID int Identity(1,1),
	MaLoaiTour as 'BP' + right('000' + cast(ID as varchar(3)), 3) persisted, 
	TenLoaiTour NVARCHAR(25) not null,
	Mota nvarchar(1000) NOT NULL
	CONSTRAINT PK_LoaiTour primary key (ID)
)

create table Tour
(
	ID int Identity(1,1),
	MaTour as 'BP' + right('000' + cast(ID as varchar(3)), 3) persisted, 
	TenTour NVARCHAR(25) not null,
	Gia int,
	NgayKhoiHanh date,
	NgayKetThuc date,
	SoCho int,
	NoiDung nvarchar(1000),
	ChiTietTour nvarchar(1000),
	MaLoaiTour int,
	HinhAnh image
	CONSTRAINT PK_Tour primary key (ID),
	foreign key (MaLoaiTour) references LoaiTour,
)

create table DatTour
(
	ID int Identity(1,1),
	MaDatTour as 'BP' + right('000' + cast(ID as varchar(3)), 3) persisted, 
	NgayDat date,
	SoCho int,
	ThanhTien int,
	MaTour int,
	CONSTRAINT PK_DatTour primary key (ID),
	foreign key (MaTour) references Tour,
)

create table KhachHang
(
	ID int Identity(1,1),
	MaKhachHang as 'BP' + right('000' + cast(ID as varchar(3)), 3) persisted, 
	Ten NVARCHAR(50) not null,
	SDT nvarchar(12),
	TenDangNhap Nvarchar(30),
	MatKhau	Nvarchar(100),
	CONSTRAINT PK_KhachHang primary key (ID),
)

create table TaiKhoanAdmin
(
	ID int Identity(1,1),
	MaTaiKhoanAdmin as 'BP' + right('000' + cast(ID as varchar(3)), 3) persisted, 
	TenDangNhap Nvarchar(15),
	MatKhau	Nvarchar(100),
	CONSTRAINT PK_TaiKhoanAdmin primary key (ID),
)

create table ChiTietDatTour
(
	ID int Identity(1,1),
	MaChiTietDatTour as 'BP' + right('000' + cast(ID as varchar(3)), 3) persisted, 
	MaKhachHang int,
	MaDatTour int,
	CONSTRAINT PK_ChiTietDatTour primary key (ID),
	foreign key (MaKhachHang) references KhachHang,
	foreign key (MaDatTour) references DatTour,
)

create table LienHe
(
	ID int Identity(1,1),
	MaLienHe as 'BP' + right('000' + cast(ID as varchar(3)), 3) persisted, 
	Ten Nvarchar(15),
	email Nvarchar(30),
	SDT char(12),
	NoiDung nvarchar(100),
	TinNhan nvarchar(1000)
)


--Nhap du lieu
--/Loai Tour
insert into LoaiTour(TenLoaiTour , Mota)
values(N'Du lịch nghỉ dưỡng', N'Du lịch nghỉ dưỡng iúp cho tâm trạng của khách du lịch thoải mái nhất có thể, giảm bớt căng thẳng bằng những bài tập yoga hoặc tắm nước nóng, massage,... cũng được tích hợp đầy đủ tại khu nghỉ dưỡng.')

insert into LoaiTour(TenLoaiTour , Mota)
values(N'Du lịch sinh thái',N'Du lịch sinh thái sẽ dựa vào điều kiện tự nhiên cũng như văn hóa của Việt Nam. Được diễn ra tại những vùng có hệ sinh thái tự nhiên và còn bảo tồn khá tốt để hưởng thụ và bảo vệ những giá trị mà thiên nhiên mang lại.')

insert into LoaiTour(TenLoaiTour , Mota)
values(N'Du lịch văn hóa, lịch sử',N'Du lịch văn hóa - lịch sử còn phản ánh được những cái nhìn tốt đẹp về lịch sử, về văn hóa dân tộc.')

--/Tour
insert into Tour(TenTour, Gia, NgayKhoiHanh, NgayKetThuc, SoCho, NoiDung, ChiTietTour, MaLoaiTour, HinhAnh)
select N'Tour du lịch Nha Trang', 2000000, '25-03-2022', '27-03-2022', 30, N'khám phá đảo Bình Ba, hòn Rùa, Ghé qua Bãi Chướng – ngắm rạn san hô' ,N'Công viên chủ đề VinWonders Nha Trang. Bãi Dài Cam Ranh. Nhà thờ Đá. Viện Hải dương học. Bãi biển đôi tại Đảo Yến' , 3, BulkColumn from openrowset (bulk 'C:\Users\Administrator\source\repos\Travel-Website\Travel-Website\Content\Images\p-7.jpg',Single_Clob) as Picture

insert into Tour(TenTour, Gia, NgayKhoiHanh, NgayKetThuc, SoCho, NoiDung, ChiTietTour, MaLoaiTour, HinhAnh)
select N'Tour du lịch Đà Lạt', 1500000, '26-03-2022', '27-03-2022', 30, N'Cánh đồng hoa cẩm tú cầu, Tham quan Chùa Linh Phước' ,N'Thác Datanla Đà Lạt. Vườn Thú ZooDoo .Núi Langbiang Đà Lạt. Vườn Ánh Sáng Lumiere. Đồi Robin. Hồ Tuyền Lâm ', 3, BulkColumn from openrowset (bulk 'C:\Users\Administrator\source\repos\Travel-Website\Travel-Website\Content\Images\p-8.png',Single_Clob) as Picture

insert into Tour(TenTour, Gia, NgayKhoiHanh, NgayKetThuc, SoCho, NoiDung, ChiTietTour, MaLoaiTour, HinhAnh)
select N'Tour du lịch Đà Năng', 4399000, '24-03-2022', '27-03-2022', 30, N'Chiêm ngưỡng Tượng Phật Quan Thế Âm Bồ Tát cao 67m ở Chùa Linh Ứng' ,N'Bãi Bụt .Ghềnh Bàng. Đèo Hải Vân. Làng cổ Phong Nam. Vườn hoa Le Jardin D’Amour. Thánh địa Mỹ Sơn', 2, BulkColumn from openrowset (bulk 'C:\Users\Administrator\source\repos\Travel-Website\Travel-Website\Content\Images\p-11.jpg',Single_Clob) as Picture

insert into Tour(TenTour, Gia, NgayKhoiHanh, NgayKetThuc, SoCho, NoiDung, ChiTietTour, MaLoaiTour, HinhAnh)
select N'Tour du lịch Sapa', 2990000, '25-03-2022', '27-03-2022', 30, N'Khám phá chữ “Tình” của vùng núi Tây Bắc',N'Sunworld Fansipan Legend. Thung lũng hoa hồng Sapa. Núi Hàm Rồng. Dinh thự Hoàng A Tưởng' ,1, BulkColumn from openrowset (bulk 'C:\Users\Administrator\source\repos\Travel-Website\Travel-Website\Content\Images\p-10.jpg',Single_Clob) as Picture

insert into Tour(TenTour, Gia, NgayKhoiHanh, NgayKetThuc, SoCho, NoiDung, ChiTietTour, MaLoaiTour, HinhAnh)
select N'Tour du lịch Huế', 2000000, '25-03-2022', '27-03-2022', 30, N'Nhắc đến Huế người ta thường nghĩ ngay đến các cung điện, đền đài mang hơi thở cổ xưa một thời vua chúa huy hoàng. Ai cũng muốn một lần du lịch Huế để tận mắt nhìn thấy những bằng chứng sinh động của triều đại phong kiến cuối cùng.' ,N'Lăng tẩm Huế. Sông Hương - Địa điểm du lịch Huế khơi nguồn bao áng văn chương. Đồi Vọng Cảnh - điểm đến ngắm trọn vẻ đẹp xứ Huế. Cầu Tràng Tiền - biểu tượng của mảnh đất cố đô. Đồi Thiên An – Hồ Thuỷ Tiên. Bảo tàng Mỹ Thuật Cung Đình Huế',2, BulkColumn from openrowset (bulk 'C:\Users\Administrator\source\repos\Travel-Website\Travel-Website\Content\Images\p-9.jpg',Single_Clob) as Picture

insert into Tour(TenTour, Gia, NgayKhoiHanh, NgayKetThuc, SoCho, NoiDung, ChiTietTour, MaLoaiTour, HinhAnh)
select N'Tour du lịch Hội An', 2150000, '25-03-2022', '27-03-2022', 30, N'Nói đến Hội An như nói đến viên ngọc cổ xưa đầy sức hấp dẫn trên bản đồ du lịch miền Trung Việt Nam' ,N'Hội quán Triều Châu. Nhà cổ Quân Thắng. Chợ Hội An. Bảo tàng gốm sứ mậu dịch - địa điểm du lịch Hội An lâu đời độc đáo. Cù Lao Chàm. Xưởng thủ công mỹ nghệ Hội An',1, BulkColumn from openrowset (bulk 'C:\Users\Administrator\source\repos\Travel-Website\Travel-Website\Content\Images\p-12.jpg',Single_Clob) as Picture

insert into Tour(TenTour, Gia, NgayKhoiHanh, NgayKetThuc, SoCho, NoiDung, ChiTietTour, MaLoaiTour, HinhAnh)
select N'Tour du lịch Hội An', 2150000, '25-03-2022', '27-03-2022', 30, N'Nói đến Hội An như nói đến viên ngọc cổ xưa đầy sức hấp dẫn trên bản đồ du lịch miền Trung Việt Nam' ,N'Hội quán Triều Châu. Nhà cổ Quân Thắng. Chợ Hội An. Bảo tàng gốm sứ mậu dịch - địa điểm du lịch Hội An lâu đời độc đáo. Cù Lao Chàm. Xưởng thủ công mỹ nghệ Hội An',1, BulkColumn from openrowset (bulk 'C:\Users\Administrator\source\repos\Travel-Website\Travel-Website\Content\Images\p-12.jpg',Single_Clob) as Picture

insert into Tour(TenTour, Gia, NgayKhoiHanh, NgayKetThuc, SoCho, NoiDung, ChiTietTour, MaLoaiTour, HinhAnh)
select N'Tour du lịch Hội An', 2150000, '25-03-2022', '27-03-2022', 30, N'Nói đến Hội An như nói đến viên ngọc cổ xưa đầy sức hấp dẫn trên bản đồ du lịch miền Trung Việt Nam' ,N'Hội quán Triều Châu. Nhà cổ Quân Thắng. Chợ Hội An. Bảo tàng gốm sứ mậu dịch - địa điểm du lịch Hội An lâu đời độc đáo. Cù Lao Chàm. Xưởng thủ công mỹ nghệ Hội An',1, BulkColumn from openrowset (bulk 'C:\Users\Administrator\source\repos\Travel-Website\Travel-Website\Content\Images\p-12.jpg',Single_Clob) as Picture

insert into Tour(TenTour, Gia, NgayKhoiHanh, NgayKetThuc, SoCho, NoiDung, ChiTietTour, MaLoaiTour, HinhAnh)
select N'Tour du lịch Hội An', 2150000, '25-03-2022', '27-03-2022', 30, N'Nói đến Hội An như nói đến viên ngọc cổ xưa đầy sức hấp dẫn trên bản đồ du lịch miền Trung Việt Nam' ,N'Hội quán Triều Châu. Nhà cổ Quân Thắng. Chợ Hội An. Bảo tàng gốm sứ mậu dịch - địa điểm du lịch Hội An lâu đời độc đáo. Cù Lao Chàm. Xưởng thủ công mỹ nghệ Hội An',1, BulkColumn from openrowset (bulk 'C:\Users\Administrator\source\repos\Travel-Website\Travel-Website\Content\Images\p-12.jpg',Single_Clob) as Picture

insert into Tour(TenTour, Gia, NgayKhoiHanh, NgayKetThuc, SoCho, NoiDung, ChiTietTour, MaLoaiTour, HinhAnh)
select N'Tour du lịch Hội An', 2150000, '25-03-2022', '27-03-2022', 30, N'Nói đến Hội An như nói đến viên ngọc cổ xưa đầy sức hấp dẫn trên bản đồ du lịch miền Trung Việt Nam' ,N'Hội quán Triều Châu. Nhà cổ Quân Thắng. Chợ Hội An. Bảo tàng gốm sứ mậu dịch - địa điểm du lịch Hội An lâu đời độc đáo. Cù Lao Chàm. Xưởng thủ công mỹ nghệ Hội An',1, BulkColumn from openrowset (bulk 'C:\Users\Administrator\source\repos\Travel-Website\Travel-Website\Content\Images\p-12.jpg',Single_Clob) as Picture

insert into Tour(TenTour, Gia, NgayKhoiHanh, NgayKetThuc, SoCho, NoiDung, ChiTietTour, MaLoaiTour, HinhAnh)
select N'Tour du lịch Hội An', 2150000, '25-03-2022', '27-03-2022', 30, N'Nói đến Hội An như nói đến viên ngọc cổ xưa đầy sức hấp dẫn trên bản đồ du lịch miền Trung Việt Nam' ,N'Hội quán Triều Châu. Nhà cổ Quân Thắng. Chợ Hội An. Bảo tàng gốm sứ mậu dịch - địa điểm du lịch Hội An lâu đời độc đáo. Cù Lao Chàm. Xưởng thủ công mỹ nghệ Hội An',1, BulkColumn from openrowset (bulk 'C:\Users\Administrator\source\repos\Travel-Website\Travel-Website\Content\Images\p-12.jpg',Single_Clob) as Picture

insert into Tour(TenTour, Gia, NgayKhoiHanh, NgayKetThuc, SoCho, NoiDung, ChiTietTour, MaLoaiTour, HinhAnh)
select N'Tour du lịch Hội An', 2150000, '25-03-2022', '27-03-2022', 30, N'Nói đến Hội An như nói đến viên ngọc cổ xưa đầy sức hấp dẫn trên bản đồ du lịch miền Trung Việt Nam' ,N'Hội quán Triều Châu. Nhà cổ Quân Thắng. Chợ Hội An. Bảo tàng gốm sứ mậu dịch - địa điểm du lịch Hội An lâu đời độc đáo. Cù Lao Chàm. Xưởng thủ công mỹ nghệ Hội An',1, BulkColumn from openrowset (bulk 'C:\Users\Administrator\source\repos\Travel-Website\Travel-Website\Content\Images\p-12.jpg',Single_Clob) as Picture


