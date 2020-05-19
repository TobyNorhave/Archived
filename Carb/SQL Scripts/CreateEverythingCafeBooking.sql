create table ZipCity (
Zip int primary key,
City nvarchar(255) not null
);

create table CafeType ( 
ID int identity(1,1) primary key,
CType nvarchar(255)
);

create Table Cafe (
ID int identity(1,1) primary key,
CName nvarchar(255) not null,
ZipID int foreign key references ZipCity(Zip),
CAddress nvarchar(255) not null,
PriceRange decimal(5,2) not null,
Rating decimal(5,2) not null,
TypeID int not null
);

create table CafeTable ( 
ID int identity(1,1) primary key,
NoOfSeats int not null,
Available bit not null,
TableNumber int not null,
CafeID int foreign key references Cafe(ID)
);

create table Person (
ID int identity(1,1) primary key,
PersonName nvarchar(255) not null,
PhoneNo nvarchar(255) not null,
Email nvarchar(255) not null
);

create table Customer (
PersonID int foreign key references Person(ID),
VIP bit not null
);

create table Administrator (
PersonID int foreign key references Person(ID),
CafeID int foreign key references Cafe(ID)
);

create table Booking (
ID int identity(1,1) primary key,
BookingDate datetime not null,
PersonID int foreign key references Person(ID),
TableID int foreign key references CafeTable(ID),
);