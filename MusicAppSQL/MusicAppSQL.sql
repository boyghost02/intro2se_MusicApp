create database MusicAppSQL
create table Song
(
    SongID int identity(1,1) primary key,
    SongName varchar(50) not null,
    SongArtist varchar(50) not null,
    SongAlbum varchar(50) not null,
    SongGenre varchar(50) not null,
    SongLength int not null,
    SongRating int not null,
    SongPath varchar(100) not null
)

create table Account
(
    AccountID int identity(1,1) primary key,
    AccountName varchar(50) not null,
    AccountPassword varchar(50) not null,
    AccountEmail varchar(50) not null,
    AccountType varchar(50) not null
)