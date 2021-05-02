-- drop table compatibilities;
-- drop table haircuts;
-- drop table predictions;
-- drop table time_intervals;
-- drop table zodiacs;

create table zodiacs
(
    id          integer primary key autoincrement,
    name        text,
    enum_number integer unique check (enum_number between 0 and 11)
);

create table time_intervals
(
    id          integer primary key autoincrement,
    name        text,
    enum_number integer unique check (enum_number between 0 and 3)
);

create table predictions
(
    zodiac_id        integer,
    time_interval_id integer,
    text_value       text,
    foreign key (zodiac_id) references zodiacs (id),
    foreign key (time_interval_id) references time_intervals (id)
);

create table haircuts
(
    zodiac_id  integer,
    time_value text,
    text_value text,
    foreign key (zodiac_id) references zodiacs (id)
);

create table compatibilities
(
    zodiac1_id          integer,
    zodiac2_id          integer,
    compatibility_value integer,
    text_value          text,
    foreign key (zodiac1_id) references zodiacs (id),
    foreign key (zodiac2_id) references zodiacs (id)
);