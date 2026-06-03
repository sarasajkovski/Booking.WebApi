CREATE TABLE room (
    room_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    capacity INT NOT NULL,
    "roomType" VARCHAR(50) NOT NULL,
    "isAvailable" BOOLEAN NOT NULL
);

CREATE TABLE reservations (
    reservation_id SERIAL PRIMARY KEY,
    room_id INT NOT NULL,
    "fullName" VARCHAR(100) NOT NULL,
    "reservationDate" DATE NOT NULL,
    "isAvailable" BOOLEAN NOT NULL,

    CONSTRAINT fk_reservation_room
        FOREIGN KEY (room_id)
        REFERENCES room(id)
);

INSERT INTO room (name, capacity, roomType, isAvailable)
VALUES
('Conference Room', 10, 'Conference', true),
('Meeting Room', 5, 'Meeting', false),
('Event Hall', 50, 'Event', true);