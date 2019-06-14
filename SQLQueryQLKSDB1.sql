CREATE DATABASE QLKS1
GO

USE QLKS1
GO


CREATE TABLE TAI_KHOAN (
  TenDangNhap NVARCHAR(50) ,
  MatKhau NVARCHAR(50),
  --[LoaiNguoiDung] varchar(3) NOT NULL,
  HoTen NVARCHAR(50) ,
  SoDienThoai NVARCHAR (11) ,
  Email VARCHAR (50) ,
  --FOREIGN KEY (LoaiNguoiDung) REFERENCES dbo.LOAI_NGUOI_DUNG
);


CREATE TABLE [KHACH_HANG] (
  [MaKhachHang] varchar (3) NOT NULL,
  [TenKhachHang] nvarchar (50),
  [CMND] nvarchar (15) NOT NULL,
  [DiaChi] nvarchar (50),
  [DienThoai] int,
  [GioiTinh] nvarchar (50),
  [QuocTich] nvarchar(50),
  PRIMARY KEY (MaKhachHang)
);
CREATE TABLE [DON_VI] (
  [MaDonVi] varchar (3) NOT NULL,
  [TenDonVI] nvarchar (50),
  PRIMARY KEY (MaDonVi)
);

CREATE TABLE [DICH_VU] (
  [MaDichVu] varchar (5) NOT NULL,
  [MaLoaiDichVu] varchar (5) NOT NULL,
  [MaDonVi] varchar (3) NOT NULL,
  [DonGia] float
  PRIMARY KEY (MaDichVu),
  --FOREIGN KEY (MaLoaiDichVu) REFERENCES dbo.LOAI_DICH_VU,
  --FOREIGN KEY (MaDonVi) REFERENCES dbo.DON_VI
);


CREATE TABLE [HOA_DON] (
  [MaHoaDon] varchar (3) NOT NULL,
  [NhanVienLap] nvarchar (50) NOT NULL,
  [MaKhachHang] varchar (3) NOT NULL,
  [MaNhanPhong] varchar (5) NOT NULL,
  [TongTien] float NOT NULL,	
  [NgayLap] DATE NOT NULL
  PRIMARY KEY (MaHoaDon),
  --FOREIGN KEY (MaKhachHang) REFERENCES dbo.KHACH_HANG,
);

CREATE TABLE [LOAI_DICH_VU] (
  [MaLoaiDichVu] varchar (5) NOT NULL,
  [TenLoaiDichVu]nvarchar (50),
  PRIMARY KEY (MaLoaiDichVu),
);

CREATE TABLE [LOAI_PHONG] (
  [MaLoaiPhong] varchar (3) NOT NULL,
  [TenLoaiPhong] nvarchar (50),
  [DonGia] float,
  [SoNguoiChuan] int,
  [SoNguoiToiDa] int,
  [TyLeTang] float,
  PRIMARY KEY (MaLoaiPhong)
);

CREATE TABLE [LOAI_TINH_TRANG] (
  [MaLoaiTinhTrangPhong] int NOT NULL,
  [TenLoaiTinhTrangPhong] nvarchar (50)
  PRIMARY KEY (MaLoaiTinhTrangPhong)
);

CREATE TABLE [PHONG] (
  [MaPhong] varchar (3) NOT NULL,
  [MaLoaiPhong] varchar (3) NOT NULL,
  [MaLoaiTinhTrangPhong] int NOT NULL,
  [GhiChu] nvarchar (50)
  PRIMARY KEY(MaPhong),
  --FOREIGN KEY (MaLoaiPHong) REFERENCES dbo.LOAI_PHONG,
  --FOREIGN KEY (MaLoaiTinhTrangPhong) REFERENCES dbo.LOAI_TINH_TRANG
);

CREATE TABLE [CHI_TIET_HOA_DON] (
  [MaHoaDon] varchar (3) NOT NULL,
  [MaPhong] varchar (3) NOT NULL,
  [MaSuDungDichVu] varchar (4) NOT NULL,
  [PhuThu] varchar (5),
  [TienPhong] float,
  [TienDichVu] float,
  [GiamGiaKhachHang] float,
  [HinhThucThanhToan] nvarchar(50),
  [SoNgay] int,
  [ThanhTien] float

  PRIMARY KEY (MaHoaDon,MaPhong,MaSuDungDichVu),
 --FOREIGN KEY (MaHoaDon) REFERENCES dbo.HOA_DON,
  --FOREIGN KEY (MaPhong) REFERENCES dbo.PHONG,
  --FOREIGN KEY (MaSuDungDichVu) REFERENCES dbo.DANH_SACH_SU_DUNG_DICH_VU,
);

CREATE TABLE [DANH_SACH_SU_DUNG_DICH_VU] (
  [MaSuDungDichVu] varchar (4) NOT NULL,
  [MaDichVu] varchar (5) NOT NULL,
  [MaNhanPhong] varchar (5) NOT NULL,
  [SoLuong] Int
  PRIMARY KEY (MaSuDungDichVu),
  --FOREIGN KEY (MaDichVu) REFERENCES dbo.DICH_VU,
  --FOREIGN KEY (MaNhanPhong) REFERENCES dbo.PHIEU_NHAN_PHONG,
);


CREATE TABLE [PHIEU_NHAN_PHONG] (
  [MaNhanPhong] varchar (5) NOT NULL,
  [MaPhieuThue] varchar (10) NOT NULL,
  [MaKhachHang] varchar (3) NOT NULL,
  PRIMARY KEY (MaNhanPhong),
  --FOREIGN KEY (MaPhieuThue) REFERENCES dbo.PHIEU_THUE_PHONG,
 --FOREIGN KEY (MaKhachHang) REFERENCES dbo.KHACH_HANG,
);
	

CREATE TABLE [PHIEU_THUE_PHONG] (
  [MaPhieuThue] varchar (10) NOT NULL,
  [MaKhachHang] varchar (3) NOT NULL,
  PRIMARY KEY (MaPhieuThue),
  --FOREIGN KEY (MaKhachHang) REFERENCES dbo.KHACH_HANG,
);

CREATE TABLE [CHI_TIET_PHIEU_NHAN_PHONG] (
  [MaNhanPhong] varchar (5) NOT NULL,
  [MaPhong] varchar (3) NOT NULL,
  [HoTenKhachHang] nvarchar (50),
  [CMND] nvarchar (15),
  [NgayNhan] DATE,
  [NgayTraDuKien] DATE,
  [NgayTraThucTe] DATE
  PRIMARY KEY(MaNhanPhong),
  --FOREIGN KEY (MaPhong) REFERENCES dbo.PHONG,
);


CREATE TABLE [CHI_TIET_PHIEU_THUE_PHONG] (
  [MaPhieuThue] varchar (10) NOT NULL,
  [MaPhong] varchar (3) NOT NULL,
  [NgayDangKi] DATE,
  [NgayNhan] DATE
  PRIMARY KEY (MaPhieuThue),
  --FOREIGN KEY (MaPhong) REFERENCES dbo.PHONG,
);

CREATE TABLE [THIET_BI] (
  [MaThietBi] varchar (8) NOT NULL,
  [MaLoaiPhong] varchar (3) NOT NULL,
  [TenThietBi] nvarchar (50),
  [SoLuong] int,
  PRIMARY KEY (MaThietBi),
);

alter table dbo.THIET_BI add foreign key (MaLoaiPhong) references dbo.LOAI_PHONG(MaLoaiPhong)
alter table dbo.DICH_VU ADD FOREIGN KEY (MaLoaiDichVu) REFERENCES dbo.LOAI_DICH_VU(MaLoaiDichVu)
alter table dbo.DICH_VU add foreign key (MaDonVi) references dbo.DON_VI(MaDonVi)
alter table dbo.PHONG add foreign key (MaLoaiPhong) references dbo.LOAI_PHONG(MaLoaiPhong)
alter table dbo.PHONG add foreign key (MaLoaiTinhTrangPhong) references dbo.LOAI_TINH_TRANG(MaLoaiTinhTrangPhong)
alter table dbo.CHI_TIET_HOA_DON add foreign key (MaHoaDon) references dbo.HOA_DON(MaHoaDon)
alter table dbo.CHI_TIET_HOA_DON add foreign key (MaPhong) references dbo.PHONG(MaPhong)
alter table dbo.CHI_TIET_HOA_DON add foreign key (MaSuDungDichVu) references dbo.DANH_SACH_SU_DUNG_DICH_VU(MaSuDungDichVu)
alter table dbo.DANH_SACH_SU_DUNG_DICH_VU add foreign key (MaDichVu) references dbo.DICH_VU(MaDichVu)
alter table dbo.DANH_SACH_SU_DUNG_DICH_VU add foreign key (MaNhanPhong) references dbo.PHIEU_NHAN_PHONG(MaNhanPhong)
alter table dbo.HOA_DON add foreign key (MaKhachHang) references dbo.KHACH_HANG(MaKhachHang)
alter table dbo.PHIEU_NHAN_PHONG add foreign key (MaPhieuThue) references dbo.PHIEU_THUE_PHONG(MaPhieuThue)
alter table dbo.PHIEU_NHAN_PHONG add foreign key (MaKhachHang) references dbo.KHACH_HANG(MaKhachHang)
alter table dbo.PHIEU_THUE_PHONG add foreign key (MaKhachHang) references dbo.KHACH_HANG(MaKhachHang)
alter table dbo.CHI_TIET_PHIEU_THUE_PHONG add foreign key (MaPhong) references dbo.PHONG(MaPhong)
alter table dbo.CHI_TIET_PHIEU_NHAN_PHONG add foreign key (MaPhong) references dbo.PHONG(MaPhong)
